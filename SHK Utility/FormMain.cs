using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using NModbus;
using NModbus.Serial;
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

        MOTOR_TIME_DIFF,
        IO_STATE,

        PEAK_VALUE,
        POSITION_VALUE,
        POSITION_VALUE_AVG,

        AN_VALUES, // 25 registers
        AN_VALUES_END = AN_VALUES + 25,
        EXEC_TIME,
        TOTAL_ERRORS,
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

        private SerialPortAdapter adapter;
        private ModbusFactory factory;
        private byte slaveId = 1;
        private IModbusSerialMaster master;
        private ushort[] registers;
        private ushort startAddress = 0;
        private ushort numRegisters = 1;
        private int timer_counter = 0;
        private bool logFormatHex = false;
        private bool settingsChanged = false;


        public FormMain()
        {
            InitializeComponent();


            string[] ports = SerialPort.GetPortNames();
            comboBoxPorts.Items.Clear();
            comboBoxPorts.Items.AddRange(ports);
            if (ports.Length > 0)
            {
                comboBoxPorts.SelectedIndex = 0;
            }

            comboBoxBaudrates.SelectedIndex = 3;

            comboBoxDataBits.Items.Clear();
            comboBoxDataBits.Items.Add("8");
            comboBoxDataBits.Items.Add("7");
            comboBoxDataBits.SelectedIndex = 0;

            comboBoxParity.Items.Clear();
            comboBoxParity.Items.Add(Parity.Even.ToString());
            comboBoxParity.Items.Add(Parity.None.ToString());
            comboBoxParity.Items.Add(Parity.Odd.ToString());
            comboBoxParity.SelectedIndex = 0;

            comboBoxStopBits.Items.Clear();
            comboBoxStopBits.Items.Add("1");
            comboBoxStopBits.Items.Add("2");
            comboBoxStopBits.SelectedIndex = 0;


            chart1.ChartAreas[1].AxisX.LabelStyle.Format = "HH:mm:ss";



            //comboBoxGain1.Enabled = false;
            //comboBoxPositionOffset.Enabled = false;


        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (buttonConnect.Text.Equals("&Connect"))
            {

                if (!serialPort1.IsOpen)
                {
                    // configure serial port
                    serialPort1.BaudRate = int.Parse(comboBoxBaudrates.SelectedItem.ToString());


                    switch (comboBoxDataBits.SelectedItem.ToString())
                    {
                        case "8":
                            serialPort1.DataBits = 8;
                            break;
                        case "7":
                            serialPort1.DataBits = 7;
                            break;

                        default:
                            serialPort1.DataBits = 8;
                            break;
                    }


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
                        serialPort1.PortName = comboBoxPorts.SelectedItem.ToString();
                        serialPort1.Open();
                    }
                    catch
                    {
                        MessageBox.Show("Unable to open the requested serial port");
                        return;
                    }


                    slaveId = byte.Parse(numericUpDownID.Value.ToString());

                    adapter = new SerialPortAdapter(serialPort1);

                    factory = new ModbusFactory();

                    // create modbus master
                    master = factory.CreateRtuMaster(adapter);

                    startAddress = 0;
                    numRegisters = 1;


                    //slaveId = 2;

                    

                    master.Transport.ReadTimeout = 1000;
                    try
                    {
                        // read total number of registers first - SHKModBusRegisters.ENUM_SIZE
                        registers = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);

                        numRegisters = registers[0];
                        
                        textBox1.AppendText($"Connected, number of registers: {numRegisters}\r\n");

                        // enable groups
                        buttonConnect.Text = "&Disconnect";
                        groupBoxSystemInfo.Enabled = true;
                        groupBoxIOStatus.Enabled = true;
                        groupBoxActValues.Enabled = true;
                        groupBoxSensor.Enabled = false;
                        groupBoxAnalog.Enabled = false;
                        groupBoxFilters.Enabled = false;
                        groupBoxLaser.Enabled = false;
                        groupBoxTest.Enabled = false;
                        buttonLogin.Enabled = true;
                        buttonLogin.Text = "&Login";
                        buttonLogin.Focus();

                        if (serialPort1.BaudRate < 19200)
                        {
                            timer1.Interval = 2000;
                        }
                        else
                        {
                            timer1.Interval = 500;
                        }
                        timer1.Start();
                    }
                    catch (Exception mbe)
                    {
                        master.Dispose();

                        textBox1.AppendText(mbe.Message);
                        textBox1.AppendText("\r\n");

                        return;
                    }
                }
            }
            else
            {
                if (serialPort1.IsOpen)
                {
                    try
                    {
                        serialPort1.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Unable to close the serial port");
                    }
                }
                timer1.Stop();

                master.Dispose();

                buttonConnect.Text = "&Connect";
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
                buttonLogin.Text = "&Login";
                //chart1.ChartAreas[0].Visible = false;
                //chart1.ChartAreas[1].Visible = false;

            }

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            
            DateTime now = DateTime.Now;
            DateTime minDate = DateTime.Now.AddMinutes(-5);
            //DateTime maxDate = DateTime.Now.AddSeconds(1);

            if (settingsChanged) // update only once per tick, to avoid hang-ups
            {
                // check Sensor settings
                if (registers[(int)SHKModBusRegisters.SET] != ushort.Parse(comboBoxSet.SelectedIndex.ToString()))
                {
                    try
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.SET, ushort.Parse(comboBoxSet.SelectedIndex.ToString()));
                        textBox1.AppendText("Set saved!\r\n");
                    }
                    catch (Exception mbe)
                    {
                        textBox1.AppendText(mbe.Message);
                        textBox1.AppendText("\r\n");
                    }
                }

                if (registers[(int)SHKModBusRegisters.GAIN_SET1] != ushort.Parse(comboBoxGain1.SelectedItem.ToString()))
                {
                    try
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.GAIN_SET1, ushort.Parse(comboBoxGain1.SelectedItem.ToString()));
                        textBox1.AppendText("Gain1 saved!\r\n");
                    }
                    catch (Exception mbe)
                    {
                        textBox1.AppendText(mbe.Message);
                        textBox1.AppendText("\r\n");
                    }
                }

                if (registers[(int)SHKModBusRegisters.THRESHOLD_SET1] != ushort.Parse(numericUpDownThre1.Value.ToString()))
                {
                    try
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.THRESHOLD_SET1, ushort.Parse(numericUpDownThre1.Value.ToString()));
                        textBox1.AppendText("Threshold1 saved!\r\n");
                    }
                    catch (Exception mbe)
                    {
                        textBox1.AppendText(mbe.Message);
                        textBox1.AppendText("\r\n");
                    }
                }

                if (registers[(int)SHKModBusRegisters.GAIN_SET2] != ushort.Parse(comboBoxGain2.SelectedItem.ToString()))
                {
                    try
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.GAIN_SET2, ushort.Parse(comboBoxGain2.SelectedItem.ToString()));
                        textBox1.AppendText("Gain2 saved!\r\n");
                    }
                    catch (Exception mbe)
                    {
                        textBox1.AppendText(mbe.Message);
                        textBox1.AppendText("\r\n");
                    }
                }

                if (registers[(int)SHKModBusRegisters.THRESHOLD_SET2] != ushort.Parse(numericUpDownThre2.Value.ToString()))
                {
                    try
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.THRESHOLD_SET2, ushort.Parse(numericUpDownThre2.Value.ToString()));
                        textBox1.AppendText("Threshold2 saved!\r\n");
                    }
                    catch (Exception mbe)
                    {
                        textBox1.AppendText(mbe.Message);
                        textBox1.AppendText("\r\n");
                    }
                }

                //check Analog settings
                if (registers[(int)SHKModBusRegisters.ANALOG_OUT_MODE] != ushort.Parse(comboBoxAnalogOut.SelectedIndex.ToString()))
                {
                    try
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.ANALOG_OUT_MODE, ushort.Parse(comboBoxAnalogOut.SelectedIndex.ToString()));
                        textBox1.AppendText("Analog Out Mode saved!\r\n");
                    }
                    catch (Exception mbe)
                    {
                        textBox1.AppendText(mbe.Message);
                        textBox1.AppendText("\r\n");
                    }
                }

                if (registers[(int)SHKModBusRegisters.POSITION_MODE] != ushort.Parse(comboBoxPositionMode.SelectedIndex.ToString()))
                {
                    try
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.POSITION_MODE, ushort.Parse(comboBoxPositionMode.SelectedIndex.ToString()));
                        textBox1.AppendText("Position Mode saved!\r\n");
                    }
                    catch (Exception mbe)
                    {
                        textBox1.AppendText(mbe.Message);
                        textBox1.AppendText("\r\n");
                    }
                }

                if (registers[(int)SHKModBusRegisters.WINDOW_BEGIN] != ushort.Parse(numericUpDownWindowBeg.Value.ToString()))
                {
                    try
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.WINDOW_BEGIN, ushort.Parse(numericUpDownWindowBeg.Value.ToString()));
                        textBox1.AppendText("Window Begin saved!\r\n");
                    }
                    catch (Exception mbe)
                    {
                        textBox1.AppendText(mbe.Message);
                        textBox1.AppendText("\r\n");
                    }
                }

                if (registers[(int)SHKModBusRegisters.WINDOW_END] != ushort.Parse(numericUpDownWindowEnd.Value.ToString()))
                {
                    try
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.WINDOW_END, ushort.Parse(numericUpDownWindowEnd.Value.ToString()));
                        textBox1.AppendText("Window End saved!\r\n");
                    }
                    catch (Exception mbe)
                    {
                        textBox1.AppendText(mbe.Message);
                        textBox1.AppendText("\r\n");
                    }
                }

                if (registers[(int)SHKModBusRegisters.POSITION_OFFSET] != ushort.Parse(numericUpDownOffset.Value.ToString()))
                {
                    try
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.POSITION_OFFSET, ushort.Parse(numericUpDownOffset.Value.ToString()));
                        textBox1.AppendText("Offset saved!\r\n");
                    }
                    catch (Exception mbe)
                    {
                        textBox1.AppendText(mbe.Message);
                        textBox1.AppendText("\r\n");
                    }
                }

                //check Filter settings
                if (registers[(int)SHKModBusRegisters.FILTER_POSITION] != ushort.Parse(numericUpDownFilterPosition.Value.ToString()))
                {
                    try
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.FILTER_POSITION, ushort.Parse(numericUpDownFilterPosition.Value.ToString()));
                        textBox1.AppendText("Filter Position saved!\r\n");
                    }
                    catch (Exception mbe)
                    {
                        textBox1.AppendText(mbe.Message);
                        textBox1.AppendText("\r\n");
                    }
                }

                if (registers[(int)SHKModBusRegisters.FILTER_ON] != ushort.Parse(numericUpDownFilterOn.Value.ToString()))
                {
                    try
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.FILTER_ON, ushort.Parse(numericUpDownFilterOn.Value.ToString()));
                        textBox1.AppendText("Filter Signal On saved!\r\n");
                    }
                    catch (Exception mbe)
                    {
                        textBox1.AppendText(mbe.Message);
                        textBox1.AppendText("\r\n");
                    }
                }

                if (registers[(int)SHKModBusRegisters.FILTER_OFF] != ushort.Parse(numericUpDownFilterOff.Value.ToString()))
                {
                    try
                    {
                        master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.FILTER_OFF, ushort.Parse(numericUpDownFilterOff.Value.ToString()));
                        textBox1.AppendText("Filter Signal Off saved!\r\n");
                    }
                    catch (Exception mbe)
                    {
                        textBox1.AppendText(mbe.Message);
                        textBox1.AppendText("\r\n");
                    }
                }

                settingsChanged = false;
            }

            try
            {
                registers = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);
            }
            catch (Exception mbe)
            {
                textBox1.AppendText(mbe.Message);
                textBox1.AppendText("\r\n");
            }

            // update log
            for (int i = 0; i < numRegisters; i++)
            {
                //textBox1.AppendText($"R{startAddress + i}={registers[i]} ");
                if (logFormatHex)
                {
                    textBox1.AppendText($"{registers[i]:X4} "); // hex 
                }
                else
                {
                    textBox1.AppendText($"{registers[i]} ");
                }
            }
            textBox1.AppendText("\r\n");

            // update setup values every 10 seconds
            if (timer_counter == 0)
            {
                // load setup from registers
                textBoxModel.Text = "SHK01-" + registers[(int)SHKModBusRegisters.MB_MODEL_TYPE].ToString();
                textBoxSerial.Text = registers[(int)SHKModBusRegisters.MB_MODEL_SERIAL_NUMBER].ToString();
                textBoxFW.Text = registers[(int)SHKModBusRegisters.MB_FW_VERSION].ToString();


                comboBoxSet.SelectedIndex = registers[(int)SHKModBusRegisters.SET];
                comboBoxPositionMode.SelectedIndex = registers[(int)SHKModBusRegisters.POSITION_MODE];
                comboBoxAnalogOut.SelectedIndex = registers[(int)SHKModBusRegisters.ANALOG_OUT_MODE];

                comboBoxGain1.SelectedIndex = comboBoxGain1.FindStringExact(registers[(int)SHKModBusRegisters.GAIN_SET1].ToString());
                numericUpDownThre1.Value = registers[(int)SHKModBusRegisters.THRESHOLD_SET1];

                comboBoxGain2.SelectedIndex = comboBoxGain2.FindStringExact(registers[(int)SHKModBusRegisters.GAIN_SET2].ToString());
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


            textBoxInt.Text = ((float)registers[(int)SHKModBusRegisters.PEAK_VALUE] * 100 / 256).ToString("0.0");
            textBoxPos.Text = ((float)registers[(int)SHKModBusRegisters.POSITION_VALUE_AVG] / 10).ToString("0.0");
            textBoxAnalog1.Text = ((float)registers[(int)SHKModBusRegisters.PEAK_VALUE] / 16 + 4).ToString("0.0"); // in 4-20 mA   b = 16/100*a + 4 
            textBoxAnalog2.Text = ((float)registers[(int)SHKModBusRegisters.POSITION_VALUE_AVG] * 16 / 1000 + 4).ToString("0.0");
            //update charts
            chart1.Series["Window"].Points.Clear();
            chart1.Series["Threshold"].Points.Clear();

            chart1.Series["Window"].Points.AddXY(0, 100);
            chart1.Series["Window"].Points.AddXY(registers[(int)SHKModBusRegisters.WINDOW_BEGIN], 100);
            chart1.Series["Window"].Points.AddXY(registers[(int)SHKModBusRegisters.WINDOW_BEGIN], 0);
            chart1.Series["Window"].Points.AddXY(registers[(int)SHKModBusRegisters.WINDOW_END], 0);
            chart1.Series["Window"].Points.AddXY(registers[(int)SHKModBusRegisters.WINDOW_END], 100);
            chart1.Series["Window"].Points.AddXY(100, 100);

            chart1.ChartAreas[0].AxisX2.Minimum = 100 * (float)registers[(int)SHKModBusRegisters.WINDOW_BEGIN] / ((float)registers[(int)SHKModBusRegisters.WINDOW_BEGIN] - (float)registers[(int)SHKModBusRegisters.WINDOW_END]);
            chart1.ChartAreas[0].AxisX2.Maximum = (100 * (100 - chart1.ChartAreas[0].AxisX2.Minimum) / (float)registers[(int)SHKModBusRegisters.WINDOW_END]) + chart1.ChartAreas[0].AxisX2.Minimum;
            chart1.ChartAreas[0].AxisX2.IntervalOffset = -chart1.ChartAreas[0].AxisX2.Minimum;

            //textBoxAnalog1.Text = chart1.ChartAreas[0].AxisX2.Minimum.ToString();
            //textBoxAnalog2.Text = chart1.ChartAreas[0].AxisX2.Maximum.ToString();
            if (registers[(int)SHKModBusRegisters.SET] < 2)
            {
                chart1.Series["Threshold"].Points.AddXY(0, registers[(int)SHKModBusRegisters.THRESHOLD_SET1] - 5, registers[(int)SHKModBusRegisters.THRESHOLD_SET1] + 5);
                chart1.Series["Threshold"].Points.AddXY(100, registers[(int)SHKModBusRegisters.THRESHOLD_SET1] - 5, registers[(int)SHKModBusRegisters.THRESHOLD_SET1] + 5);
            }
            else
            {
                chart1.Series["Threshold"].Points.AddXY(0, registers[(int)SHKModBusRegisters.THRESHOLD_SET2] - 5, registers[(int)SHKModBusRegisters.THRESHOLD_SET2] + 5);
                chart1.Series["Threshold"].Points.AddXY(100, registers[(int)SHKModBusRegisters.THRESHOLD_SET2] - 5, registers[(int)SHKModBusRegisters.THRESHOLD_SET2] + 5);
            }


            chart1.Series["Position"].Points.Clear();
            chart1.Series["Position"].Points.AddXY((float)registers[(int)SHKModBusRegisters.POSITION_VALUE] / 10, 100);
            //chart1.Series["Reg11"].Points.AddY(registers[(int)SHKModBusRegisters.THRESHOLD_SET1]);
            chart1.Series["Signal"].Points.Clear();
            for (int i = 0; i < ((int)SHKModBusRegisters.AN_VALUES_END - (int)SHKModBusRegisters.AN_VALUES); i++)
            {
                // AN_VALUES 50 values from analog_buffer[200]: MSB = signal[i*8+4], LSB = signal[i*8] 
                chart1.Series["Signal"].Points.AddXY(i * 4, ((float)((uint)registers[i + (int)SHKModBusRegisters.AN_VALUES] % 256)) * 100 / 256); // LSB to 0-100%
                chart1.Series["Signal"].Points.AddXY(i * 4 + 2, ((float)((uint)registers[i + (int)SHKModBusRegisters.AN_VALUES] >> 8)) * 100 / 256); // MSB to 0-100%
            }

            //time chart
            chart1.ChartAreas[1].AxisX.Minimum = minDate.ToOADate();
            chart1.ChartAreas[1].AxisX.Maximum = now.ToOADate();
            chart1.Series["Intensity"].Points.AddXY(now.ToOADate(), (float)registers[(int)SHKModBusRegisters.PEAK_VALUE] * 100 / 256);
            chart1.Series["Position Raw"].Points.AddXY(now.ToOADate(), (float)registers[(int)SHKModBusRegisters.POSITION_VALUE] / 10);
            chart1.Series["Position Out"].Points.AddXY(now.ToOADate(), (float)registers[(int)SHKModBusRegisters.POSITION_VALUE_AVG] / 10);
            chart1.Series["Temperature"].Points.AddXY(now.ToOADate(), registers[(int)SHKModBusRegisters.ACT_TEMPERATURE]);
        }





        private void ComboBoxPorts_DropDown(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            comboBoxPorts.Items.Clear();
            comboBoxPorts.Items.AddRange(ports);
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
            if (radioButtonLaserOn.Checked == true) { radioButtonLaserOn.BackColor = Color.Red; } else { radioButtonLaserOn.BackColor = DefaultBackColor; }

            if (radioButtonLaserOn.Checked == true && !Convert.ToBoolean(registers[(int)SHKModBusRegisters.IO_STATE] & (1 << (int)IO_Status.IO_LASER)))
            {

                radioButtonLaserOn.BackColor = Color.Red;

                try
                {
                    registers[(int)SHKModBusRegisters.IO_STATE] |= 1 << (int)IO_Status.IO_LASER; // LASER bit to 1
                    master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.IO_STATE, registers[(int)SHKModBusRegisters.IO_STATE]);
                }
                catch (Exception mbe)
                {
                    textBox1.AppendText(mbe.Message);
                    textBox1.AppendText("\r\n");
                }
            }
        }

        private void RadioButtonLaserOff_CheckedChanged(object sender, EventArgs e)
        {


            if (radioButtonLaserOff.Checked == true && Convert.ToBoolean(registers[(int)SHKModBusRegisters.IO_STATE] & (1 << (int)IO_Status.IO_LASER)))
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
                    textBox1.AppendText(mbe.Message);
                    textBox1.AppendText("\r\n");
                }
            }
        }

        private void RadioButtonTestOn_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTestOn.Checked == true) { radioButtonTestOn.BackColor = Color.Red; } else { radioButtonTestOn.BackColor = DefaultBackColor; }

            if (radioButtonTestOn.Checked == true && !Convert.ToBoolean(registers[(int)SHKModBusRegisters.IO_STATE] & (1 << (int)IO_Status.IO_IR_LED)))
            {

                //radioButtonTestOn.BackColor = Color.Red;

                try
                {
                    registers[(int)SHKModBusRegisters.IO_STATE] |= 1 << (int)IO_Status.IO_IR_LED; // IR_LED bit to 1
                    master.WriteSingleRegister(slaveId, (ushort)SHKModBusRegisters.IO_STATE, registers[(int)SHKModBusRegisters.IO_STATE]);
                }
                catch (Exception mbe)
                {
                    textBox1.AppendText(mbe.Message);
                    textBox1.AppendText("\r\n");
                }
            }
        }

        private void RadioButtonTestOff_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTestOff.Checked == true && Convert.ToBoolean(registers[(int)SHKModBusRegisters.IO_STATE] & (1 << (int)IO_Status.IO_IR_LED)))
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
                    textBox1.AppendText(mbe.Message);
                    textBox1.AppendText("\r\n");
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
                    case DialogResult.Yes:
                        buttonLogin.Text = "&Logout";
                        buttonImport.Enabled = true;
                        buttonExport.Enabled = true;
                        groupBoxLaser.Enabled = true;
                        groupBoxTest.Enabled = true;
                        groupBoxSensor.Enabled = true;
                        groupBoxAnalog.Enabled = true;
                        numericUpDownOffset.Enabled = true;
                        groupBoxFilters.Enabled = true;
                        break;
                    case DialogResult.OK:
                        //do stuff
                        buttonLogin.Text = "&Logout";
                        buttonImport.Enabled = true;
                        buttonExport.Enabled = true;
                        groupBoxLaser.Enabled = true;
                        groupBoxTest.Enabled = true;
                        groupBoxSensor.Enabled = true;
                        groupBoxAnalog.Enabled = true;
                        numericUpDownOffset.Enabled = false;
                        groupBoxFilters.Enabled = true;

                        break;
                    case DialogResult.Cancel:
                        buttonLogin.Text = "&Login";
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
                groupBoxLaser.Enabled = false;
                groupBoxTest.Enabled = false;
                groupBoxSensor.Enabled = false;
                groupBoxAnalog.Enabled = false;
                groupBoxFilters.Enabled = false;
            }

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
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

                DialogResult dialogResult = MessageBox.Show("Import Settings: " + Environment.NewLine + Environment.NewLine + contentWriter.ToString(), "Import Settings", MessageBoxButtons.YesNo);

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
    }
}
