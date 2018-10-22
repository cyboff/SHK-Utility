using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using NModbus;
using NModbus.Serial;
using NModbus.IO;
using MadMilkman.Ini;


namespace SHK_Utility
{
    enum SHKModBusRegisters
    {
        // just add or remove registers and your good to go...
        // The first register starts at address 0
        ENUM_SIZE,
        MB_MODEL_TYPE,
        MB_MODEL_SERIAL_NUMBER,
        MB_FW_VERSION,

        MODBUS_ID,
        MODBUS_SPEED, // = baud rate/100 to fit into word
        MODBUS_FORMAT,

        SET,
        GAIN_SET1,
        THRESHOLD_SET1,
        GAIN_SET2,
        THRESHOLD_SET2,

        WINDOW_BEGIN,
        WINDOW_END,
        POSITION_MODE,
        ANALOG_OUT_MODE,
        POSITION_OFFSET,

        FILTER_POSITION,
        FILTER_ON,
        FILTER_OFF,

        ACT_TEMPERATURE,
        MAX_TEMPERATURE,
        TOTAL_RUNTIME,
        IO_STATE,

        PEAK_VALUE,
        POSITION_VALUE,
        POSITION_VALUE_AVG,

        AN_VALUES,                       // 25 registers
        MOTOR_TIME_DIFF = AN_VALUES + 25,
        EXEC_TIME_ADC,                   // time of adc convertions for one mirror
        EXEC_TIME,                       // time of adc conv + results calculation
        EXEC_TIME_TRIGGER,
        OFFSET_DELAY,
        TOTAL_ERRORS,                    // modbus errors
        // leave this one
        TOTAL_REGS_SIZE
        // total number of registers for function 3 and 16 share the same register array
    }

    enum IO_Status
    {
        IO_LASER,
        IO_IR_LED,
        IO_TEST_IN,
        IO_SET_IN,
        IO_ALARM_OUT,
        IO_SIGNAL_OUT,
        IO_BTN_A,
        IO_BTN_B,
        IO_LED_ALARM,
        IO_LED_SIGNAL,
        IO_LED_POWER
    };


    public partial class FormMain : Form
    {

        //private SerialPortAdapter adapter;
        private TcpClient tcpclient;
        private UdpClient udpclient;
        private IPEndPoint endPoint;
        private IModbusMaster master;
        private byte slaveId = 1;
        private ushort[] registers;
        private ushort startAddress = 0;
        private ushort numRegisters = 1;
        private int timer_counter = 0;
        private int timerLogout_counter = 0;
        private bool logFormatHex = false;
        private bool settingsChanged = false;


        public FormMain()
        {
            InitializeComponent();

            string[] ports = SerialPort.GetPortNames();
            comboBoxComPorts.Items.Clear();
            comboBoxComPorts.Items.AddRange(ports);
            if (comboBoxComPorts.Items.Count > 0)
            {
                comboBoxComPorts.SelectedIndex = comboBoxComPorts.FindStringExact(Properties.Settings.Default.ComPort);

                if (comboBoxComPorts.SelectedIndex < 0) comboBoxComPorts.SelectedIndex = 0;

            }
            else comboBoxComPorts.BackColor = Color.Red;

            comboBoxBaudrates.SelectedIndex = Properties.Settings.Default.BaudrateIndex;

            comboBoxDataBits.Items.Clear();
            comboBoxDataBits.Items.Add("8");
            //comboBoxDataBits.Items.Add("7");
            comboBoxDataBits.SelectedIndex = Properties.Settings.Default.DataBitsIndex;

            comboBoxParity.Items.Clear();
            comboBoxParity.Items.Add(Parity.Even.ToString());
            comboBoxParity.Items.Add(Parity.None.ToString());
            comboBoxParity.Items.Add(Parity.Odd.ToString());
            comboBoxParity.SelectedIndex = Properties.Settings.Default.ParityIndex;

            comboBoxStopBits.Items.Clear();
            if (comboBoxParity.SelectedIndex == 1)  // "None"
            {
                comboBoxStopBits.Items.Add("1");
                comboBoxStopBits.Items.Add("2");
            }
            else
            {
                comboBoxStopBits.Items.Add("1");
            }
            comboBoxStopBits.SelectedIndex = Properties.Settings.Default.StopBitsIndex;


            chart1.ChartAreas[1].AxisX.LabelStyle.Format = "HH:mm:ss";

            textBoxIP.Text = Properties.Settings.Default.IP;
            textBoxPort.Text = Properties.Settings.Default.Port;

            numericUpDownID.Value = Properties.Settings.Default.SlaveID;

            groupBoxMode.Enabled = true;
            radioButtonSerial.Checked = true;
            groupBoxTCP.Enabled = false;
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            if (buttonConnect.Text.Equals("&Connect"))
            {

                CreateModbusMaster();

                try
                {
                    slaveId = (byte)numericUpDownID.Value;

                    startAddress = 0;
                    numRegisters = 7;

                    // read total number of registers first - SHKModBusRegisters.ENUM_SIZE
                    registers = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);

                    numRegisters = registers[0];

                    textBoxLog.AppendText($"Connected to Slave ID: {slaveId}\r\nNumber of registers: {numRegisters}\r\n");

                    // enable groups
                    buttonConnect.Text = "&Disconnect";
                    groupBoxMode.Enabled = false;
                    groupBoxSystemInfo.Enabled = true;
                    groupBoxIOStatus.Enabled = true;
                    groupBoxActValues.Enabled = true;

                    if (buttonLogin.Text.Equals("&Login"))
                    {
                        groupBoxSensor.Enabled = false;
                        groupBoxAnalog.Enabled = false;
                        groupBoxFilters.Enabled = false;
                        groupBoxLaser.Enabled = false;
                        groupBoxTest.Enabled = false;
                        buttonLogin.Enabled = true;
                        buttonSaveSerial.Enabled = false;
                        //buttonLogin.Text = "&Login";
                        buttonLogin.Focus();
                    }
                    else
                    {
                        buttonImport.Enabled = true;
                        buttonExport.Enabled = true;
                        groupBoxLaser.Enabled = true;
                        groupBoxTest.Enabled = true;
                        groupBoxSensor.Enabled = true;
                        groupBoxAnalog.Enabled = true;
                        groupBoxFilters.Enabled = true;
                        buttonLogin.Enabled = true;
                        if (radioButtonSerial.Checked) buttonSaveSerial.Enabled = true;
                    }


                    foreach (var series in chart1.Series)
                    {
                        series.Points.Clear();
                    }

                    if (registers[(int)SHKModBusRegisters.MODBUS_SPEED] * 100 < 19200)
                    {
                        timer1.Interval = 2000;
                    }
                    else if (registers[(int)SHKModBusRegisters.MODBUS_SPEED] * 100 < 57600)
                    {
                        timer1.Interval = 500;
                    }
                    else
                    {
                        timer1.Interval = 250;
                    }

                    timer1.Start();
                }
                catch (Exception mbe)
                {
                    textBoxLog.AppendText(mbe.Message);
                    textBoxLog.AppendText("\r\n");
                    textBoxLog.AppendText("Unable to connect!\r\n");

                    timer1.Stop();

                    DoDisconnect();
                    return;
                }

            }
            else
            {
                timer1.Stop();

                DoDisconnect();
            }

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

            DateTime now = DateTime.Now;
            DateTime minDate = DateTime.Now.AddMinutes(-5);
            //DateTime maxDate = DateTime.Now.AddSeconds(1);

            if (timerLogout_counter == 0)  // automatic logout after 30 minutes
            {
                buttonLogin.Text = "&Login";
                buttonImport.Enabled = false;
                buttonExport.Enabled = false;
                buttonSaveSerial.Enabled = false;
                groupBoxLaser.Enabled = false;
                groupBoxTest.Enabled = false;
                groupBoxSensor.Enabled = false;
                groupBoxAnalog.Enabled = false;
                groupBoxFilters.Enabled = false;
            }
            else
            {
                timerLogout_counter--;
            }

            if ((settingsChanged) && (buttonLogin.Text.Equals("&Logout"))) // update only once per tick, to avoid hang-ups
            {
                // check Sensor settings
                try
                {
                    if (registers[(int)SHKModBusRegisters.SET] != ushort.Parse(comboBoxSet.SelectedIndex.ToString()))
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.SET, ushort.Parse(comboBoxSet.SelectedIndex.ToString()));
                        textBoxLog.AppendText("Set saved!\r\n");
                    }

                    if (registers[(int)SHKModBusRegisters.GAIN_SET1] != ushort.Parse(comboBoxGain1.SelectedItem.ToString()))
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.GAIN_SET1, ushort.Parse(comboBoxGain1.SelectedItem.ToString()));
                        textBoxLog.AppendText("Gain1 saved!\r\n");
                    }

                    if (registers[(int)SHKModBusRegisters.THRESHOLD_SET1] != ushort.Parse(numericUpDownThre1.Value.ToString()))
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.THRESHOLD_SET1, ushort.Parse(numericUpDownThre1.Value.ToString()));
                        textBoxLog.AppendText("Threshold1 saved!\r\n");
                    }

                    if (registers[(int)SHKModBusRegisters.GAIN_SET2] != ushort.Parse(comboBoxGain2.SelectedItem.ToString()))
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.GAIN_SET2, ushort.Parse(comboBoxGain2.SelectedItem.ToString()));
                        textBoxLog.AppendText("Gain2 saved!\r\n");
                    }

                    if (registers[(int)SHKModBusRegisters.THRESHOLD_SET2] != ushort.Parse(numericUpDownThre2.Value.ToString()))
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.THRESHOLD_SET2, ushort.Parse(numericUpDownThre2.Value.ToString()));
                        textBoxLog.AppendText("Threshold2 saved!\r\n");
                    }

                    //check Analog settings
                    if (registers[(int)SHKModBusRegisters.ANALOG_OUT_MODE] != ushort.Parse(comboBoxAnalogOut.SelectedIndex.ToString()))
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.ANALOG_OUT_MODE, ushort.Parse(comboBoxAnalogOut.SelectedIndex.ToString()));
                        textBoxLog.AppendText("Analog Out Mode saved!\r\n");
                    }

                    if (registers[(int)SHKModBusRegisters.POSITION_MODE] != ushort.Parse(comboBoxPositionMode.SelectedIndex.ToString()))
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.POSITION_MODE, ushort.Parse(comboBoxPositionMode.SelectedIndex.ToString()));
                        textBoxLog.AppendText("Position Mode saved!\r\n");
                    }

                    if (registers[(int)SHKModBusRegisters.WINDOW_BEGIN] != ushort.Parse(numericUpDownWindowBeg.Value.ToString()))
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.WINDOW_BEGIN, ushort.Parse(numericUpDownWindowBeg.Value.ToString()));
                        textBoxLog.AppendText("Window Begin saved!\r\n");
                    }

                    if (registers[(int)SHKModBusRegisters.WINDOW_END] != ushort.Parse(numericUpDownWindowEnd.Value.ToString()))
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.WINDOW_END, ushort.Parse(numericUpDownWindowEnd.Value.ToString()));
                        textBoxLog.AppendText("Window End saved!\r\n");
                    }

                    if (registers[(int)SHKModBusRegisters.POSITION_OFFSET] != ushort.Parse(numericUpDownOffset.Value.ToString()))
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.POSITION_OFFSET, ushort.Parse(numericUpDownOffset.Value.ToString()));
                        textBoxLog.AppendText("Offset saved!\r\n");
                    }

                    //check Filter settings
                    if (registers[(int)SHKModBusRegisters.FILTER_POSITION] != ushort.Parse(numericUpDownFilterPosition.Value.ToString()))
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.FILTER_POSITION, ushort.Parse(numericUpDownFilterPosition.Value.ToString()));
                        textBoxLog.AppendText("Filter Position saved!\r\n");
                    }

                    if (registers[(int)SHKModBusRegisters.FILTER_ON] != ushort.Parse(numericUpDownFilterOn.Value.ToString()))
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.FILTER_ON, ushort.Parse(numericUpDownFilterOn.Value.ToString()));
                        textBoxLog.AppendText("Filter Signal On saved!\r\n");
                    }

                    if (registers[(int)SHKModBusRegisters.FILTER_OFF] != ushort.Parse(numericUpDownFilterOff.Value.ToString()))
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.FILTER_OFF, ushort.Parse(numericUpDownFilterOff.Value.ToString()));
                        textBoxLog.AppendText("Filter Signal Off saved!\r\n");
                    }
                }
                catch (Exception mbe)
                {
                    textBoxLog.AppendText(mbe.Message);
                    textBoxLog.AppendText("\r\n");
                }

                settingsChanged = false;
            }

            try
            {
                registers = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);
            }
            catch (Exception mbe)
            {
                textBoxLog.AppendText(mbe.Message);
                textBoxLog.AppendText("\r\n");
                timer1.Stop();
                DoDisconnect();
                return;
            }


            // update log
            string registerslog = "";

            for (int i = 0; i < numRegisters; i++)
            {
                //textBox1.AppendText($"R{startAddress + i}={registers[i]} ");  
                if (logFormatHex)
                {
                    //textBoxLog.AppendText($"{registers[i]:X4} "); // hex 
                    registerslog += $"{registers[i]:X4} ";
                }
                else
                {
                    //textBoxLog.AppendText($"{registers[i]} ");
                    registerslog += $"{registers[i]} "; // lower CPU load than textBox.AppendText() all the time
                }
            }
            textBoxLog.AppendText($"{registerslog}\r\n");

            // update setup values every 20 ticks
            if (timer_counter == 0)
            {
                // load setup from registers
                textBoxModel.Text = "SHK01-" + registers[(int)SHKModBusRegisters.MB_MODEL_TYPE].ToString();
                textBoxSerial.Text = registers[(int)SHKModBusRegisters.MB_MODEL_SERIAL_NUMBER].ToString();
                textBoxFW.Text = registers[(int)SHKModBusRegisters.MB_FW_VERSION].ToString();


                if (!comboBoxSet.DroppedDown) comboBoxSet.SelectedIndex = registers[(int)SHKModBusRegisters.SET];
                if (!comboBoxPositionMode.DroppedDown) comboBoxPositionMode.SelectedIndex = registers[(int)SHKModBusRegisters.POSITION_MODE];
                if (!comboBoxAnalogOut.DroppedDown) comboBoxAnalogOut.SelectedIndex = registers[(int)SHKModBusRegisters.ANALOG_OUT_MODE];

                if (!comboBoxGain1.DroppedDown) comboBoxGain1.SelectedIndex = comboBoxGain1.FindStringExact(registers[(int)SHKModBusRegisters.GAIN_SET1].ToString());
                numericUpDownThre1.Value = registers[(int)SHKModBusRegisters.THRESHOLD_SET1];

                if (!comboBoxGain2.DroppedDown) comboBoxGain2.SelectedIndex = comboBoxGain2.FindStringExact(registers[(int)SHKModBusRegisters.GAIN_SET2].ToString());
                numericUpDownThre2.Value = registers[(int)SHKModBusRegisters.THRESHOLD_SET2];

                numericUpDownWindowBeg.Value = registers[(int)SHKModBusRegisters.WINDOW_BEGIN];
                numericUpDownWindowEnd.Value = registers[(int)SHKModBusRegisters.WINDOW_END];
                numericUpDownOffset.Value = registers[(int)SHKModBusRegisters.POSITION_OFFSET];

                numericUpDownFilterPosition.Value = registers[(int)SHKModBusRegisters.FILTER_POSITION];
                numericUpDownFilterOn.Value = registers[(int)SHKModBusRegisters.FILTER_ON];
                numericUpDownFilterOff.Value = registers[(int)SHKModBusRegisters.FILTER_OFF];

            }
            else
            {
                timer_counter = (timer_counter + 1) % 20;
            }

            for (int i = 0; i < checkedListBoxIOStatus.Items.Count; i++)
            {
                bool bit = Convert.ToBoolean(registers[(int)SHKModBusRegisters.IO_STATE] & (1 << i));

                checkedListBoxIOStatus.SetItemChecked(i, bit);
            }

            if (checkedListBoxIOStatus.GetItemCheckState((int)IO_Status.IO_ALARM_OUT) == CheckState.Checked)
            {
                checkedListBoxIOStatus.BackColor = Color.Red;
                groupBoxActValues.BackColor = Color.Red;
                groupBoxIOStatus.BackColor = Color.Red;
            }
            else if (checkedListBoxIOStatus.GetItemCheckState((int)IO_Status.IO_SIGNAL_OUT) == CheckState.Checked)
            {
                checkedListBoxIOStatus.BackColor = Color.Yellow;
                groupBoxActValues.BackColor = Color.Yellow;
                groupBoxIOStatus.BackColor = Color.Yellow;
            }
            else
            {
                checkedListBoxIOStatus.BackColor = DefaultBackColor;
                groupBoxActValues.BackColor = DefaultBackColor;
                groupBoxIOStatus.BackColor = DefaultBackColor;
            }

            radioButtonLaserOn.Checked = Convert.ToBoolean(registers[(int)SHKModBusRegisters.IO_STATE] & (1 << (int)IO_Status.IO_LASER));
            radioButtonLaserOff.Checked = !Convert.ToBoolean(registers[(int)SHKModBusRegisters.IO_STATE] & (1 << (int)IO_Status.IO_LASER));

            radioButtonTestOn.Checked = Convert.ToBoolean(registers[(int)SHKModBusRegisters.IO_STATE] & (1 << (int)IO_Status.IO_IR_LED));
            radioButtonTestOff.Checked = !Convert.ToBoolean(registers[(int)SHKModBusRegisters.IO_STATE] & (1 << (int)IO_Status.IO_IR_LED));

            textBoxTotalTime.Text = registers[(int)SHKModBusRegisters.TOTAL_RUNTIME].ToString();
            textBoxActTemp.Text = registers[(int)SHKModBusRegisters.ACT_TEMPERATURE].ToString();
            textBoxMaxTemp.Text = registers[(int)SHKModBusRegisters.MAX_TEMPERATURE].ToString();

            // if internal temperature is too high!
            if ((registers[(int)SHKModBusRegisters.ACT_TEMPERATURE] > 55) || ((registers[(int)SHKModBusRegisters.ACT_TEMPERATURE] > 50) && (textBoxActTemp.BackColor == Color.Red)))
            { textBoxActTemp.BackColor = Color.Red; }
            else
            { textBoxActTemp.BackColor = System.Drawing.SystemColors.Window; }

            textBoxInt.Text = registers[(int)SHKModBusRegisters.PEAK_VALUE].ToString("0.0");
            textBoxPos.Text = ((float)registers[(int)SHKModBusRegisters.POSITION_VALUE_AVG] / 10).ToString("0.0");
            textBoxAnalog1.Text = ((float)registers[(int)SHKModBusRegisters.PEAK_VALUE] * 16 / 100 + 4).ToString("0.0"); // in 4-20 mA   b = 16/100*a + 4 
            textBoxAnalog2.Text = ((float)registers[(int)SHKModBusRegisters.POSITION_VALUE_AVG] * 16 / 1000 + 4).ToString("0.0");

            //update charts
            chart1.Series["Window"].Points.Clear();
            chart1.Series["Threshold"].Points.Clear();

            if (registers[(int)SHKModBusRegisters.SET] < 2)
            {
                chart1.Series["Threshold"].Points.AddXY(0, registers[(int)SHKModBusRegisters.THRESHOLD_SET1] - 5, registers[(int)SHKModBusRegisters.THRESHOLD_SET1] + 5);
                chart1.Series["Threshold"].Points.AddXY(99.9, registers[(int)SHKModBusRegisters.THRESHOLD_SET1] - 5, registers[(int)SHKModBusRegisters.THRESHOLD_SET1] + 5);
            }
            else
            {
                chart1.Series["Threshold"].Points.AddXY(0, registers[(int)SHKModBusRegisters.THRESHOLD_SET2] - 5, registers[(int)SHKModBusRegisters.THRESHOLD_SET2] + 5);
                chart1.Series["Threshold"].Points.AddXY(99.9, registers[(int)SHKModBusRegisters.THRESHOLD_SET2] - 5, registers[(int)SHKModBusRegisters.THRESHOLD_SET2] + 5);
            }

            chart1.Series["Window"].Points.AddXY(0, 100);
            chart1.Series["Window"].Points.AddXY(registers[(int)SHKModBusRegisters.WINDOW_BEGIN], 100);
            chart1.Series["Window"].Points.AddXY(registers[(int)SHKModBusRegisters.WINDOW_BEGIN], 0);
            chart1.Series["Window"].Points.AddXY(registers[(int)SHKModBusRegisters.WINDOW_END], 0);
            chart1.Series["Window"].Points.AddXY(registers[(int)SHKModBusRegisters.WINDOW_END], 100);
            chart1.Series["Window"].Points.AddXY(100, 100);

            chart1.ChartAreas[0].AxisX.Minimum = 100 * (float)registers[(int)SHKModBusRegisters.WINDOW_BEGIN] / ((float)registers[(int)SHKModBusRegisters.WINDOW_BEGIN] - (float)registers[(int)SHKModBusRegisters.WINDOW_END]);
            chart1.ChartAreas[0].AxisX.Maximum = (100 * (100 - chart1.ChartAreas[0].AxisX.Minimum) / (float)registers[(int)SHKModBusRegisters.WINDOW_END]) + chart1.ChartAreas[0].AxisX.Minimum;
            chart1.ChartAreas[0].AxisX.IntervalOffset = -chart1.ChartAreas[0].AxisX.Minimum;

            chart1.Series["Position"].Points.Clear();
            chart1.Series["Position"].Points.AddXY((float)registers[(int)SHKModBusRegisters.POSITION_VALUE] / 10, 100);

            chart1.Series["Signal"].Points.Clear();
            // start from second point to have nice chart
            chart1.Series["Signal"].Points.AddXY(2, ((float)((uint)registers[(int)SHKModBusRegisters.AN_VALUES] >> 8)) * 100 / 256); // MSB to 0-100%
            for (int i = 1; i < ((int)SHKModBusRegisters.MOTOR_TIME_DIFF - (int)SHKModBusRegisters.AN_VALUES); i++)   // MOTOR_TIME_DIFF = AN_VALUES+25
            {
                // AN_VALUES 50 values from analog_buffer[200]: MSB = signal[i*8+4], LSB = signal[i*8] 
                chart1.Series["Signal"].Points.AddXY(i * 4, ((float)((uint)registers[i + (int)SHKModBusRegisters.AN_VALUES] % 256)) * 100 / 256); // LSB to 0-100%
                chart1.Series["Signal"].Points.AddXY(i * 4 + 2, ((float)((uint)registers[i + (int)SHKModBusRegisters.AN_VALUES] >> 8)) * 100 / 256); // MSB to 0-100%
            }
            
            //time chart
            chart1.ChartAreas[1].AxisX.Minimum = minDate.ToOADate();
            chart1.ChartAreas[1].AxisX.Maximum = now.ToOADate();
            chart1.Series["Intensity"].Points.AddXY(now.ToOADate(), registers[(int)SHKModBusRegisters.PEAK_VALUE]);
            chart1.Series["Position Raw"].Points.AddXY(now.ToOADate(), (float)registers[(int)SHKModBusRegisters.POSITION_VALUE] / 10);
            chart1.Series["Position Out"].Points.AddXY(now.ToOADate(), (float)registers[(int)SHKModBusRegisters.POSITION_VALUE_AVG] / 10);
            chart1.Series["Temperature"].Points.AddXY(now.ToOADate(), registers[(int)SHKModBusRegisters.ACT_TEMPERATURE]);
        }



        private void ComboBoxComPorts_DropDown(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            comboBoxComPorts.Items.Clear();
            comboBoxComPorts.Items.AddRange(ports);
            if (comboBoxComPorts.Items.Count > 0) comboBoxComPorts.BackColor = Color.White; else comboBoxComPorts.BackColor = Color.Red;
        }



        private void ComboBoxGain1_SelectedIndexChanged(object sender, EventArgs e)
        {
            settingsChanged = true;
        }


        private void ComboBoxGain2_SelectedIndexChanged(object sender, EventArgs e)
        {
            settingsChanged = true;
        }



        private void ComboBoxSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            settingsChanged = true;
        }



        private void ComboBoxPositionMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            settingsChanged = true;
        }



        private void RadioButtonLaserOn_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonLaserOn.Checked) { radioButtonLaserOn.BackColor = Color.Red; } else { radioButtonLaserOn.BackColor = DefaultBackColor; }

            if (radioButtonLaserOn.Checked && !Convert.ToBoolean(registers[(int)SHKModBusRegisters.IO_STATE] & (1 << (int)IO_Status.IO_LASER)))
            {

                radioButtonLaserOn.BackColor = Color.Red;

                try
                {
                    registers[(int)SHKModBusRegisters.IO_STATE] |= 1 << (int)IO_Status.IO_LASER; // LASER bit to 1
                    master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.IO_STATE, registers[(int)SHKModBusRegisters.IO_STATE]);
                }
                catch (Exception mbe)
                {
                    textBoxLog.AppendText(mbe.Message);
                    textBoxLog.AppendText("\r\n");
                }
            }
        }

        private void RadioButtonLaserOff_CheckedChanged(object sender, EventArgs e)
        {


            if (radioButtonLaserOff.Checked && Convert.ToBoolean(registers[(int)SHKModBusRegisters.IO_STATE] & (1 << (int)IO_Status.IO_LASER)))
            {

                //radioButtonLaserOn.BackColor = DefaultBackColor;

                try
                {
                    int iostate = registers[(int)SHKModBusRegisters.IO_STATE];
                    iostate &= ~(1 << (int)IO_Status.IO_LASER); // LASER bit to 0
                    registers[(int)SHKModBusRegisters.IO_STATE] = (ushort)iostate;
                    master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.IO_STATE, registers[(int)SHKModBusRegisters.IO_STATE]);
                }
                catch (Exception mbe)
                {
                    textBoxLog.AppendText(mbe.Message);
                    textBoxLog.AppendText("\r\n");
                }
            }
        }

        private void RadioButtonTestOn_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTestOn.Checked) { radioButtonTestOn.BackColor = Color.Red; } else { radioButtonTestOn.BackColor = DefaultBackColor; }

            if (radioButtonTestOn.Checked && !Convert.ToBoolean(registers[(int)SHKModBusRegisters.IO_STATE] & (1 << (int)IO_Status.IO_IR_LED)))
            {

                //radioButtonTestOn.BackColor = Color.Red;

                try
                {
                    registers[(int)SHKModBusRegisters.IO_STATE] |= 1 << (int)IO_Status.IO_IR_LED; // IR_LED bit to 1
                    master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.IO_STATE, registers[(int)SHKModBusRegisters.IO_STATE]);
                }
                catch (Exception mbe)
                {
                    textBoxLog.AppendText(mbe.Message);
                    textBoxLog.AppendText("\r\n");
                }
            }
        }

        private void RadioButtonTestOff_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTestOff.Checked && Convert.ToBoolean(registers[(int)SHKModBusRegisters.IO_STATE] & (1 << (int)IO_Status.IO_IR_LED)))
            {

                //radioButtonTestOn.BackColor = DefaultBackColor;

                try
                {
                    int iostate = registers[(int)SHKModBusRegisters.IO_STATE];
                    iostate &= ~(1 << (int)IO_Status.IO_IR_LED); // IR_LED bit to 0
                    registers[(int)SHKModBusRegisters.IO_STATE] = (ushort)iostate;
                    master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.IO_STATE, registers[(int)SHKModBusRegisters.IO_STATE]);
                }
                catch (Exception mbe)
                {
                    textBoxLog.AppendText(mbe.Message);
                    textBoxLog.AppendText("\r\n");
                }
            }
        }



        private void NumericUpDownThre1_ValueChanged(object sender, EventArgs e)
        {
            settingsChanged = true;
        }

        private void NumericUpDownThre2_ValueChanged(object sender, EventArgs e)
        {
            settingsChanged = true;
        }

        private void NumericUpDownWindowBeg_ValueChanged(object sender, EventArgs e)
        {
            settingsChanged = true;
        }

        private void NumericUpDownWindowEnd_ValueChanged(object sender, EventArgs e)
        {
            settingsChanged = true;
        }

        private void NumericUpDownOffset_ValueChanged(object sender, EventArgs e)
        {
            settingsChanged = true;
        }

        private void NumericUpDownFilterPosition_ValueChanged(object sender, EventArgs e)
        {
            settingsChanged = true;
        }

        private void NumericUpDownFilterOn_ValueChanged(object sender, EventArgs e)
        {
            settingsChanged = true;
        }

        private void NumericUpDownFilterOff_ValueChanged(object sender, EventArgs e)
        {
            settingsChanged = true;
        }

        private void ComboBoxAnalogOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            settingsChanged = true;
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            if (buttonLogin.Text.Equals("&Login"))
            {
                FormLogin formLogin = new FormLogin();
                DialogResult loginResult = formLogin.ShowDialog();
                switch (loginResult)
                {
                    case DialogResult.Yes:  // password for calibration/offset
                        buttonLogin.Text = "&Logout";
                        buttonImport.Enabled = true;
                        buttonExport.Enabled = true;
                        if (radioButtonSerial.Checked) buttonSaveSerial.Enabled = true;
                        groupBoxLaser.Enabled = true;
                        groupBoxTest.Enabled = true;
                        groupBoxSensor.Enabled = true;
                        groupBoxAnalog.Enabled = true;
                        numericUpDownOffset.Enabled = true;
                        groupBoxFilters.Enabled = true;
                        timerLogout_counter = 3600;  // automatic logout after 30 min
                        break;
                    case DialogResult.OK: // password for operator
                        buttonLogin.Text = "&Logout";
                        buttonImport.Enabled = true;
                        buttonExport.Enabled = true;
                        if (radioButtonSerial.Checked) buttonSaveSerial.Enabled = true;
                        groupBoxLaser.Enabled = true;
                        groupBoxTest.Enabled = true;
                        groupBoxSensor.Enabled = true;
                        groupBoxAnalog.Enabled = true;
                        numericUpDownOffset.Enabled = false;
                        groupBoxFilters.Enabled = true;
                        timerLogout_counter = 3600; // automatic logout after 30 min
                        break;
                    case DialogResult.Cancel:  // wrong password
                        buttonLogin.Text = "&Login";
                        buttonSaveSerial.Enabled = false;
                        groupBoxLaser.Enabled = false;
                        groupBoxTest.Enabled = false;
                        groupBoxSensor.Enabled = false;
                        groupBoxAnalog.Enabled = false;
                        groupBoxFilters.Enabled = false;
                        //this.Close();
                        break;

                }
            }
            else
            {
                buttonLogin.Text = "&Login";
                buttonImport.Enabled = false;
                buttonExport.Enabled = false;
                buttonSaveSerial.Enabled = false;
                groupBoxLaser.Enabled = false;
                groupBoxTest.Enabled = false;
                groupBoxSensor.Enabled = false;
                groupBoxAnalog.Enabled = false;
                groupBoxFilters.Enabled = false;
                timerLogout_counter = 0;
            }

        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.D)
            {
                logFormatHex = false;
            }

            if (e.KeyData == Keys.H)
            {
                logFormatHex = true;
            }
        }

        private void ButtonImport_Click(object sender, EventArgs e)
        {
            IniFile file = new IniFile(
                                new IniOptions()
                                {
                                    //CommentStarter = IniCommentStarter.Hash,
                                    //KeyDelimiter = IniKeyDelimiter.Colon,
                                    KeySpaceAroundDelimiter = true,
                                    //SectionWrapper = IniSectionWrapper.CurlyBrackets,
                                    //Encoding = Encoding.UTF8

                                });

            openFileDialog1.AddExtension = true;
            openFileDialog1.DefaultExt = ".ini";
            openFileDialog1.Multiselect = false;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (var s = openFileDialog1.OpenFile())
                {
                    file.Load(s);
                    s.Close();
                }

                StringWriter contentWriter = new StringWriter();
                //file.Save(contentWriter);
                //string iniContent = contentWriter.ToString();

                foreach (var section in file.Sections)
                {
                    contentWriter.WriteLine("[{0}]", section.Name);
                    foreach (var key in section.Keys)
                        contentWriter.WriteLine("{0} = {1}", key.Name, key.Value);
                    contentWriter.Write(Environment.NewLine);
                }

                DialogResult dialogResult = MessageBox.Show("Import following settings?" + Environment.NewLine + Environment.NewLine + contentWriter.ToString(), "Import Settings", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    //do something
                    try
                    {

                        comboBoxSet.SelectedIndex = ushort.Parse(file.Sections["Sensor"].Keys["Set"].Value.ToString());
                        comboBoxGain1.SelectedIndex = comboBoxGain1.FindStringExact(file.Sections["Sensor"].Keys["Gain1"].Value.ToString());
                        numericUpDownThre1.Value = ushort.Parse(file.Sections["Sensor"].Keys["Threshold1"].Value.ToString());
                        comboBoxGain2.SelectedIndex = comboBoxGain2.FindStringExact(file.Sections["Sensor"].Keys["Gain2"].Value.ToString());
                        numericUpDownThre2.Value = ushort.Parse(file.Sections["Sensor"].Keys["Threshold2"].Value.ToString());

                        comboBoxAnalogOut.SelectedIndex = ushort.Parse(file.Sections["Analog"].Keys["Analog Outputs"].Value.ToString());
                        comboBoxPositionMode.SelectedIndex = ushort.Parse(file.Sections["Analog"].Keys["Position Mode"].Value.ToString());
                        numericUpDownWindowBeg.Value = ushort.Parse(file.Sections["Analog"].Keys["Window Begin"].Value.ToString());
                        numericUpDownWindowEnd.Value = ushort.Parse(file.Sections["Analog"].Keys["Window End"].Value.ToString());

                        numericUpDownFilterPosition.Value = ushort.Parse(file.Sections["Filters"].Keys["Position Filter"].Value.ToString());
                        numericUpDownFilterOn.Value = ushort.Parse(file.Sections["Filters"].Keys["Signal On Filter"].Value.ToString());
                        numericUpDownFilterOff.Value = ushort.Parse(file.Sections["Filters"].Keys["Signal Off Filter"].Value.ToString());


                    }
                    catch (Exception inie)
                    {
                        MessageBox.Show(inie.Message + Environment.NewLine + inie.Source + Environment.NewLine + inie.TargetSite, "INI file loading error!");
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }



            }
        }

        private void ButtonExport_Click(object sender, EventArgs e)
        {
            // Create new file with a default formatting.
            IniFile file = new IniFile(
                                new IniOptions()
                                {
                                    //CommentStarter = IniCommentStarter.Hash,
                                    //KeyDelimiter = IniKeyDelimiter.Colon,
                                    KeySpaceAroundDelimiter = true,
                                    //SectionWrapper = IniSectionWrapper.CurlyBrackets,
                                    //Encoding = Encoding.UTF8

                                });


            //// Add new section.
            //IniSection section = file.Sections.Add("Section name");
            //// Add trailing comment.
            //section.TrailingComment.Text = "Section trailing comment";

            //// Add new key and its value.
            //IniKey key = section.Keys.Add("Key name", "Key value ");
            //// Add leading comment.
            //key.LeadingComment.Text = "Key leading comment";

            //// Save file.
            //file.Save("Sample.ini"); 


            IniSection sensorIniSection = file.Sections.Add("Sensor");
            sensorIniSection.TrailingComment.Text = "INI file for importing/exporting SHK sensor settings\r\n\r\nSensor Setup";
            //sensorIniSection.TrailingComment.EmptyLinesBefore = 1;
            IniKey setIniKey = sensorIniSection.Keys.Add("Set", registers[(int)SHKModBusRegisters.SET].ToString());
            setIniKey.LeadingComment.Text = " RELAY = 0, MAN1 = 1, MAN2 = 2";
            setIniKey.LeadingComment.LeftIndentation = 4;
            IniKey gain1IniKey = sensorIniSection.Keys.Add("Gain1", registers[(int)SHKModBusRegisters.GAIN_SET1].ToString());
            gain1IniKey.LeadingComment.Text = " valid values 1,2,4,8,16,32,64";
            gain1IniKey.LeadingComment.LeftIndentation = 4;
            IniKey threshold1IniKey = sensorIniSection.Keys.Add("Threshold1", registers[(int)SHKModBusRegisters.THRESHOLD_SET1].ToString());
            threshold1IniKey.LeadingComment.Text = " valid values 20 .. 80 ";
            threshold1IniKey.LeadingComment.LeftIndentation = 4;
            IniKey gain2IniKey = sensorIniSection.Keys.Add("Gain2", registers[(int)SHKModBusRegisters.GAIN_SET2].ToString());
            gain2IniKey.LeadingComment.Text = " valid values 1,2,4,8,16,32,64";
            gain2IniKey.LeadingComment.LeftIndentation = 4;
            IniKey threshold2IniKey = sensorIniSection.Keys.Add("Threshold2", registers[(int)SHKModBusRegisters.THRESHOLD_SET2].ToString());
            threshold2IniKey.LeadingComment.Text = " valid values 20 .. 80 ";
            threshold2IniKey.LeadingComment.LeftIndentation = 4;

            IniSection analogIniSection = file.Sections.Add("Analog");
            analogIniSection.TrailingComment.Text = "Analog Outputs Setup";
            analogIniSection.TrailingComment.EmptyLinesBefore = 1;
            IniKey analogoutIniKey = analogIniSection.Keys.Add("Analog Outputs", registers[(int)SHKModBusRegisters.ANALOG_OUT_MODE].ToString());
            analogoutIniKey.LeadingComment.Text = " 1:Int 2:Pos = 0, 1:Pos 2:Int = 1, 1:Int 2: Int = 2, 1:Pos 2:Pos = 3";
            analogoutIniKey.LeadingComment.LeftIndentation = 4;
            IniKey positionmodeIniKey = analogIniSection.Keys.Add("Position Mode", registers[(int)SHKModBusRegisters.POSITION_MODE].ToString());
            positionmodeIniKey.LeadingComment.Text = " hmd = 0, rising = 1, falling = 2, peak = 3";
            positionmodeIniKey.LeadingComment.LeftIndentation = 4;
            IniKey windowbeginIniKey = analogIniSection.Keys.Add("Window Begin", registers[(int)SHKModBusRegisters.WINDOW_BEGIN].ToString());
            windowbeginIniKey.LeadingComment.Text = " valid values 5..50";
            windowbeginIniKey.LeadingComment.LeftIndentation = 4;
            IniKey windowendIniKey = analogIniSection.Keys.Add("Window End", registers[(int)SHKModBusRegisters.WINDOW_END].ToString());
            windowendIniKey.LeadingComment.Text = " valid values 50..95";
            windowendIniKey.LeadingComment.LeftIndentation = 4;

            IniSection filtersIniSection = file.Sections.Add("Filters");
            filtersIniSection.TrailingComment.Text = "Filters Setup";
            filtersIniSection.TrailingComment.EmptyLinesBefore = 1;
            IniKey filterpositionIniKey = filtersIniSection.Keys.Add("Position Filter", registers[(int)SHKModBusRegisters.FILTER_POSITION].ToString());
            filterpositionIniKey.LeadingComment.Text = " valid values 0..9999 ms";
            filterpositionIniKey.LeadingComment.LeftIndentation = 4;
            IniKey filtersignalonIniKey = filtersIniSection.Keys.Add("Signal On Filter", registers[(int)SHKModBusRegisters.FILTER_ON].ToString());
            filtersignalonIniKey.LeadingComment.Text = " valid values 0..9999 ms";
            filtersignalonIniKey.LeadingComment.LeftIndentation = 4;
            IniKey filtersignaloffIniKey = filtersIniSection.Keys.Add("Signal Off Filter", registers[(int)SHKModBusRegisters.FILTER_OFF].ToString());
            filtersignaloffIniKey.LeadingComment.Text = " valid values 0..9999 ms";
            filtersignaloffIniKey.LeadingComment.LeftIndentation = 4;

            var filename = "SHK_Setup_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".ini";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = ".ini";
            saveFileDialog1.FileName = filename;
            saveFileDialog1.OverwritePrompt = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (var s = saveFileDialog1.OpenFile())
                {
                    file.Save(s);
                    s.Close();
                }
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DoDisconnect();

            // save last used settings
            Properties.Settings.Default.IP = textBoxIP.Text;
            Properties.Settings.Default.Port = textBoxPort.Text;
            Properties.Settings.Default.BaudrateIndex = comboBoxBaudrates.SelectedIndex;
            Properties.Settings.Default.DataBitsIndex = comboBoxDataBits.SelectedIndex;
            Properties.Settings.Default.ParityIndex = comboBoxParity.SelectedIndex;
            Properties.Settings.Default.StopBitsIndex = comboBoxStopBits.SelectedIndex;
            Properties.Settings.Default.SlaveID = (int)numericUpDownID.Value;
            if (comboBoxComPorts.Items.Count > 0) Properties.Settings.Default.ComPort = comboBoxComPorts.SelectedItem.ToString();
            Properties.Settings.Default.Save();
        }

        private void RadioButtonSerial_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSerial.Checked)
            {
                groupBoxSerial.Enabled = true;
                groupBoxTCP.Enabled = false;
            }
            else
            {
                groupBoxSerial.Enabled = false;
                groupBoxTCP.Enabled = true;
            }
        }

        private void RadioButtonTCP_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTCP.Checked)
            {
                groupBoxSerial.Enabled = false;
                groupBoxTCP.Enabled = true;
            }
            else
            {
                groupBoxSerial.Enabled = true;
                groupBoxTCP.Enabled = false;
            }
        }

        private void RadioButtonUDP_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonUDP.Checked)
            {
                groupBoxSerial.Enabled = false;
                groupBoxTCP.Enabled = true;
            }
            else
            {
                groupBoxSerial.Enabled = true;
                groupBoxTCP.Enabled = false;
            }
        }

        private void RadioButtonRTUTCP_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonRTUTCP.Checked)
            {
                groupBoxSerial.Enabled = false;
                groupBoxTCP.Enabled = true;
            }
            else
            {
                groupBoxSerial.Enabled = true;
                groupBoxTCP.Enabled = false;
            }
        }

        private void RadioButtonRTUUDP_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonRTUUDP.Checked)
            {
                groupBoxSerial.Enabled = false;
                groupBoxTCP.Enabled = true;
            }
            else
            {
                groupBoxSerial.Enabled = true;
                groupBoxTCP.Enabled = false;
            }
        }

        private void Chart1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (chart1.Location.X != 0)
            {
                chart1.Location = new System.Drawing.Point(0, chart1.Location.Y);
                chart1.Width = this.ClientRectangle.Width;
                //chart1.Height += 121;
                chart1.BringToFront();
            }
            else
            {
                chart1.Location = new System.Drawing.Point(458, 118);
                chart1.Width = this.ClientRectangle.Width - 458 - 10;
                //chart1.Height -= 121;
            }

        }

        private void Chart1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("double-click to resize", (Control)sender);
        }

        private void TextBoxLog_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("click & press: 'h' to hex; 'd' to decimal", (Control)sender);
        }


        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.ssktrinec.cz");
        }

        private void CreateModbusMaster()
        {
            var factory = new ModbusFactory();


            if (radioButtonSerial.Checked && !serialPort1.IsOpen)
            {
                // configure serial port
                serialPort1.BaudRate = int.Parse(comboBoxBaudrates.SelectedItem.ToString());
                serialPort1.DataBits = 8;

                //switch (comboBoxDataBits.SelectedItem.ToString())
                //{
                //    case "8":
                //        serialPort1.DataBits = 8;
                //        break;
                //    case "7":
                //        serialPort1.DataBits = 7;
                //        break;

                //    default:
                //        serialPort1.DataBits = 8;
                //        break;
                //}


                switch (comboBoxParity.SelectedItem.ToString())
                {
                    case "Even":
                        serialPort1.Parity = Parity.Even;
                        break;
                    case "None":
                        serialPort1.Parity = Parity.None;
                        break;
                    case "Odd":
                        serialPort1.Parity = Parity.Odd;
                        break;
                    default:
                        serialPort1.Parity = Parity.Even;
                        break;
                }


                switch (comboBoxStopBits.SelectedItem.ToString())
                {
                    case "1":
                        serialPort1.StopBits = StopBits.One;
                        break;
                    case "2":
                        serialPort1.StopBits = StopBits.Two;
                        break;

                    default:
                        serialPort1.StopBits = StopBits.One;
                        break;
                }


                try
                {
                    serialPort1.PortName = comboBoxComPorts.SelectedItem.ToString();
                    serialPort1.Open();
                    var adapter = new SerialPortAdapter(serialPort1);

                    // create modbus master
                    master = factory.CreateRtuMaster(adapter);
                    textBoxLog.AppendText($"Connecting using Modbus/RTU via {serialPort1.PortName}\r\n");
                }
                catch
                {
                    MessageBox.Show($"Unable to connect via serial port {serialPort1.PortName}");
                    return;
                }
            }

            if (radioButtonRTUTCP.Checked)
            {

                try
                {
                    tcpclient = new TcpClient(textBoxIP.Text, Int32.Parse(textBoxPort.Text));

                    var adapter = new TcpClientAdapter(tcpclient); // for Modbus/RTU over TCP protocol
                    master = factory.CreateRtuMaster(adapter);

                    textBoxLog.AppendText($"Connecting using Modbus/RTU over TCP to {textBoxIP.Text}:{textBoxPort.Text}\r\n");
                }
                catch
                {
                    MessageBox.Show($"Unable to connect to {textBoxIP.Text}:{textBoxPort.Text} using Modbus/RTU over TCP");
                    return;
                }
            }

            if (radioButtonRTUUDP.Checked)
            {

                try
                {
                    udpclient = new UdpClient();
                    endPoint = new IPEndPoint(IPAddress.Parse(textBoxIP.Text), Int32.Parse(textBoxPort.Text));
                    udpclient.Connect(endPoint);

                    var adapter = new UdpClientAdapter(udpclient); // for Modbus/RTU over UDP protocol
                    master = factory.CreateRtuMaster(adapter);

                    textBoxLog.AppendText($"Connecting using Modbus/RTU over UDP to {textBoxIP.Text}:{textBoxPort.Text}\r\n");
                }
                catch
                {
                    MessageBox.Show($"Unable to connect to {textBoxIP.Text}:{textBoxPort.Text} using Modbus/RTU over TCP");
                    return;
                }
            }

            if (radioButtonTCP.Checked)
            {

                try
                {
                    tcpclient = new TcpClient(textBoxIP.Text, Int32.Parse(textBoxPort.Text));

                    master = factory.CreateMaster(tcpclient); // for Modbus/TCP protocol

                    textBoxLog.AppendText($"Connecting using Modbus/TCP to {textBoxIP.Text}:{textBoxPort.Text}\r\n");
                }
                catch
                {
                    MessageBox.Show($"Unable to connect to {textBoxIP.Text}:{textBoxPort.Text} using Modbus/TCP");
                    return;
                }
            }


            if (radioButtonUDP.Checked)
            {
                try
                {
                    udpclient = new UdpClient();
                    endPoint = new IPEndPoint(IPAddress.Parse(textBoxIP.Text), Int32.Parse(textBoxPort.Text));
                    udpclient.Connect(endPoint);

                    master = factory.CreateMaster(udpclient);

                    textBoxLog.AppendText($"Connecting using Modbus/UDP to {textBoxIP.Text}:{textBoxPort.Text}\r\n");
                }
                catch
                {
                    MessageBox.Show($"Unable to connect to {textBoxIP.Text}:{textBoxPort.Text} using Modbus/UDP");
                    return;
                }
            }

            master.Transport.ReadTimeout = 5000;
            master.Transport.WriteTimeout = 5000;

        }

        private void DoDisconnect()
        {
            if (master != null)
            {
                master.Transport.Dispose();
                master.Dispose();
                master = null;
            }

            if (serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.Close();
                    serialPort1.Dispose();
                }
                catch
                {
                    MessageBox.Show("Unable to close the serial port");
                }
            }

            if (tcpclient != null)
            {
                //tcpclient.GetStream().Close();
                tcpclient.Close();
                tcpclient.Dispose();
                tcpclient = null;
            }

            if (udpclient != null)
            {
                udpclient.Close();
                udpclient.Dispose();
                udpclient = null;
            }

            buttonConnect.Text = "&Connect";
            textBoxLog.AppendText("Disconnected!\r\n");
            groupBoxMode.Enabled = true;
            chart1.Location = new System.Drawing.Point(458, 111);
            chart1.Width = this.ClientRectangle.Width - 458 - 10;
            buttonImport.Enabled = false;
            buttonExport.Enabled = false;
            groupBoxSystemInfo.Enabled = false;
            groupBoxIOStatus.Enabled = false;
            groupBoxActValues.Enabled = false;
            groupBoxSensor.Enabled = false;
            groupBoxAnalog.Enabled = false;
            groupBoxFilters.Enabled = false;
            groupBoxLaser.Enabled = false;
            groupBoxTest.Enabled = false;
            buttonLogin.Enabled = false;
            buttonSaveSerial.Enabled = false;
        }

        private void buttonSaveSerial_Click(object sender, EventArgs e)
        {
            ushort modbusFormat = 0x06;
            ushort[] modbusData = { 0, 0, 0 };

            if (comboBoxStopBits.SelectedIndex == 0)
            {
                switch (comboBoxParity.SelectedItem.ToString())
                {
                    case "Even":
                        modbusFormat = 0x06; // SERIAL_8E1
                        break;
                    case "None":
                        modbusFormat = 0x00; // SERIAL_8N1
                        break;
                    case "Odd":
                        modbusFormat = 0x07; // SERIAL_8O1
                        break;
                    default:
                        break;
                }
            }
            else if ((comboBoxParity.SelectedItem.ToString() == "None") && (comboBoxStopBits.SelectedIndex == 1)) modbusFormat = 0x04; //SERIAL_8N2

            DialogResult dialogResult = MessageBox.Show("Save following settings to sensor?" + Environment.NewLine + Environment.NewLine
                + "Modbus ID:  " + numericUpDownID.Value.ToString() + Environment.NewLine + Environment.NewLine
                + "Baudrate:  " + comboBoxBaudrates.SelectedItem.ToString() + Environment.NewLine
                + "Data Bits:  " + comboBoxDataBits.SelectedItem.ToString() + Environment.NewLine
                + "Parity:  " + comboBoxParity.SelectedItem.ToString() + Environment.NewLine
                + "Stop Bits:  " + comboBoxStopBits.SelectedItem.ToString() + Environment.NewLine
                , "Save Serial Comm. Settings", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                //do something
                try
                {
                    modbusData[0] = ushort.Parse(numericUpDownID.Value.ToString());
                    modbusData[1] = (ushort)(int.Parse(comboBoxBaudrates.SelectedItem.ToString()) / 100);
                    modbusData[2] = modbusFormat;

                    // check changes
                    if ((modbusData[0] != registers[(int)SHKModBusRegisters.MODBUS_ID])
                       || (modbusData[1] != registers[(int)SHKModBusRegisters.MODBUS_SPEED])
                       || (modbusData[2] != registers[(int)SHKModBusRegisters.MODBUS_FORMAT]))
                    {
                        master.WriteMultipleRegisters(slaveId, (ushort)SHKModBusRegisters.MODBUS_ID, modbusData);
                        textBoxLog.AppendText($"Saving new serial communication settings and reconnecting...\r\n");


                        buttonConnect.PerformClick(); // Disconnect
                        buttonConnect.PerformClick(); // Re-connect with new settings
                    }
                }
                catch (Exception mbe)
                {
                    textBoxLog.AppendText(mbe.Message);
                    textBoxLog.AppendText("\r\n");
                }
            }

        }

        private void comboBoxParity_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxStopBits.Items.Clear();
            if (comboBoxParity.SelectedIndex == 1)  // "None"
            {
                comboBoxStopBits.Items.Add("1");
                comboBoxStopBits.Items.Add("2");
            }
            else
            {
                comboBoxStopBits.Items.Add("1");
            }
            comboBoxStopBits.SelectedIndex = 0;
        }
    }
}
