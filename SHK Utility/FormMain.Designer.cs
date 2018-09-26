namespace SHK_Utility
{
    partial class FormMain
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Wymagana metoda obsługi projektanta — nie należy modyfikować 
        /// zawartość tej metody z edytorem kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonConnect = new System.Windows.Forms.Button();
            this.comboBoxComPorts = new System.Windows.Forms.ComboBox();
            this.comboBoxBaudrates = new System.Windows.Forms.ComboBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.comboBoxGain1 = new System.Windows.Forms.ComboBox();
            this.labelGain1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxParity = new System.Windows.Forms.ComboBox();
            this.comboBoxStopBits = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxDataBits = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxGain2 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBoxSet = new System.Windows.Forms.ComboBox();
            this.comboBoxPositionMode = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBoxSerial = new System.Windows.Forms.GroupBox();
            this.checkedListBoxIOStatus = new System.Windows.Forms.CheckedListBox();
            this.groupBoxLaser = new System.Windows.Forms.GroupBox();
            this.radioButtonLaserOff = new System.Windows.Forms.RadioButton();
            this.radioButtonLaserOn = new System.Windows.Forms.RadioButton();
            this.groupBoxIOStatus = new System.Windows.Forms.GroupBox();
            this.groupBoxTest = new System.Windows.Forms.GroupBox();
            this.radioButtonTestOff = new System.Windows.Forms.RadioButton();
            this.radioButtonTestOn = new System.Windows.Forms.RadioButton();
            this.numericUpDownThre1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownID = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownThre2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownWindowBeg = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownWindowEnd = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownOffset = new System.Windows.Forms.NumericUpDown();
            this.groupBoxSensor = new System.Windows.Forms.GroupBox();
            this.groupBoxAnalog = new System.Windows.Forms.GroupBox();
            this.comboBoxAnalogOut = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBoxFilters = new System.Windows.Forms.GroupBox();
            this.numericUpDownFilterOff = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownFilterOn = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.numericUpDownFilterPosition = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBoxActValues = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.labelAnalog1 = new System.Windows.Forms.Label();
            this.labelAnalog2 = new System.Windows.Forms.Label();
            this.textBoxAnalog2 = new System.Windows.Forms.TextBox();
            this.textBoxAnalog1 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textBoxPos = new System.Windows.Forms.TextBox();
            this.textBoxInt = new System.Windows.Forms.TextBox();
            this.groupBoxSystemInfo = new System.Windows.Forms.GroupBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.textBoxMaxTemp = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.textBoxFW = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.textBoxTotalTime = new System.Windows.Forms.TextBox();
            this.textBoxActTemp = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.textBoxSerial = new System.Windows.Forms.TextBox();
            this.textBoxModel = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBoxMode = new System.Windows.Forms.GroupBox();
            this.radioButtonRTUUDP = new System.Windows.Forms.RadioButton();
            this.radioButtonRTUTCP = new System.Windows.Forms.RadioButton();
            this.radioButtonUDP = new System.Windows.Forms.RadioButton();
            this.radioButtonTCP = new System.Windows.Forms.RadioButton();
            this.radioButtonSerial = new System.Windows.Forms.RadioButton();
            this.groupBoxTCP = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBoxSerial.SuspendLayout();
            this.groupBoxLaser.SuspendLayout();
            this.groupBoxIOStatus.SuspendLayout();
            this.groupBoxTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThre1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThre2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWindowBeg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWindowEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOffset)).BeginInit();
            this.groupBoxSensor.SuspendLayout();
            this.groupBoxAnalog.SuspendLayout();
            this.groupBoxFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterOff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterPosition)).BeginInit();
            this.groupBoxActValues.SuspendLayout();
            this.groupBoxSystemInfo.SuspendLayout();
            this.groupBoxMode.SuspendLayout();
            this.groupBoxTCP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart1.BackColor = System.Drawing.SystemColors.Menu;
            this.chart1.BorderlineColor = System.Drawing.SystemColors.ControlLight;
            chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisX.Interval = 10D;
            chartArea1.AxisX.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.IncreaseFont | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.DecreaseFont)));
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX.Maximum = 100D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea1.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisX2.Interval = 10D;
            chartArea1.AxisX2.IsStartedFromZero = false;
            chartArea1.AxisX2.LabelAutoFitMaxFontSize = 8;
            chartArea1.AxisX2.MajorGrid.Interval = 10D;
            chartArea1.AxisX2.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX2.Maximum = 100D;
            chartArea1.AxisX2.Minimum = 0D;
            chartArea1.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisY.Interval = 10D;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.Maximum = 100D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.AxisY.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea1.AxisY.ToolTip = "%";
            chartArea1.AxisY2.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.Name = "ChartArea1";
            chartArea2.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea2.AxisY.Interval = 10D;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.Maximum = 100D;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.Name = "ChartArea2";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.ChartAreas.Add(chartArea2);
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.DockedToChartArea = "ChartArea1";
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.IsDockedInsideChartArea = false;
            legend1.Name = "Legend1";
            legend2.Alignment = System.Drawing.StringAlignment.Center;
            legend2.DockedToChartArea = "ChartArea2";
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend2.IsDockedInsideChartArea = false;
            legend2.Name = "Legend2";
            this.chart1.Legends.Add(legend1);
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(458, 118);
            this.chart1.Name = "chart1";
            series1.BorderColor = System.Drawing.Color.SandyBrown;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Range;
            series1.Color = System.Drawing.Color.PeachPuff;
            series1.Legend = "Legend1";
            series1.Name = "Threshold";
            series1.XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series1.YValuesPerPoint = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            series2.Color = System.Drawing.Color.Beige;
            series2.Legend = "Legend1";
            series2.Name = "Window";
            series2.XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series3.BorderWidth = 0;
            series3.ChartArea = "ChartArea1";
            series3.Color = System.Drawing.Color.DarkSeaGreen;
            series3.CustomProperties = "PointWidth=0.3";
            series3.Legend = "Legend1";
            series3.LegendText = "Position (Unfiltered)";
            series3.Name = "Position";
            series3.XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Color = System.Drawing.Color.SteelBlue;
            series4.Legend = "Legend1";
            series4.Name = "Signal";
            series4.XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series5.ChartArea = "ChartArea2";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = System.Drawing.Color.SteelBlue;
            series5.Legend = "Legend2";
            series5.Name = "Intensity";
            series5.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series6.ChartArea = "ChartArea2";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = System.Drawing.Color.DarkSeaGreen;
            series6.Legend = "Legend2";
            series6.LegendText = "Pos. (Unfiltered)";
            series6.Name = "Position Raw";
            series7.ChartArea = "ChartArea2";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Color = System.Drawing.Color.DarkGreen;
            series7.Legend = "Legend2";
            series7.LegendText = "Pos. (Output)";
            series7.Name = "Position Out";
            series8.ChartArea = "ChartArea2";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series8.Color = System.Drawing.Color.Salmon;
            series8.Legend = "Legend2";
            series8.LegendText = "Internal Temp.";
            series8.Name = "Temperature";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Series.Add(series5);
            this.chart1.Series.Add(series6);
            this.chart1.Series.Add(series7);
            this.chart1.Series.Add(series8);
            this.chart1.Size = new System.Drawing.Size(494, 422);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart1";
            this.chart1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Chart1_MouseDoubleClick);
            this.chart1.MouseHover += new System.EventHandler(this.Chart1_MouseHover);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(839, 71);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(113, 36);
            this.buttonConnect.TabIndex = 5;
            this.buttonConnect.Text = "&Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.ButtonConnect_Click);
            // 
            // comboBoxComPorts
            // 
            this.comboBoxComPorts.AllowDrop = true;
            this.comboBoxComPorts.AutoCompleteCustomSource.AddRange(new string[] {
            "COM1",
            "COM2"});
            this.comboBoxComPorts.FormattingEnabled = true;
            this.comboBoxComPorts.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4"});
            this.comboBoxComPorts.Location = new System.Drawing.Point(73, 16);
            this.comboBoxComPorts.Name = "comboBoxComPorts";
            this.comboBoxComPorts.Size = new System.Drawing.Size(69, 21);
            this.comboBoxComPorts.Sorted = true;
            this.comboBoxComPorts.TabIndex = 7;
            this.comboBoxComPorts.DropDown += new System.EventHandler(this.ComboBoxComPorts_DropDown);
            // 
            // comboBoxBaudrates
            // 
            this.comboBoxBaudrates.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxBaudrates.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxBaudrates.FormattingEnabled = true;
            this.comboBoxBaudrates.Items.AddRange(new object[] {
            "1200",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.comboBoxBaudrates.Location = new System.Drawing.Point(207, 16);
            this.comboBoxBaudrates.Name = "comboBoxBaudrates";
            this.comboBoxBaudrates.Size = new System.Drawing.Size(68, 21);
            this.comboBoxBaudrates.TabIndex = 8;
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 19200;
            this.serialPort1.Parity = System.IO.Ports.Parity.Even;
            // 
            // textBoxLog
            // 
            this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLog.ForeColor = System.Drawing.Color.Black;
            this.textBoxLog.Location = new System.Drawing.Point(150, 546);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLog.Size = new System.Drawing.Size(802, 76);
            this.textBoxLog.TabIndex = 10;
            this.textBoxLog.WordWrap = false;
            this.textBoxLog.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox1_KeyDown);
            this.textBoxLog.MouseHover += new System.EventHandler(this.TextBoxLog_MouseHover);
            // 
            // comboBoxGain1
            // 
            this.comboBoxGain1.AllowDrop = true;
            this.comboBoxGain1.AutoCompleteCustomSource.AddRange(new string[] {
            "1..247"});
            this.comboBoxGain1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxGain1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxGain1.FormattingEnabled = true;
            this.comboBoxGain1.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "8",
            "16",
            "32",
            "64"});
            this.comboBoxGain1.Location = new System.Drawing.Point(70, 52);
            this.comboBoxGain1.Name = "comboBoxGain1";
            this.comboBoxGain1.Size = new System.Drawing.Size(60, 21);
            this.comboBoxGain1.TabIndex = 12;
            this.comboBoxGain1.SelectedIndexChanged += new System.EventHandler(this.ComboBoxGain1_SelectedIndexChanged);
            // 
            // labelGain1
            // 
            this.labelGain1.AutoSize = true;
            this.labelGain1.Location = new System.Drawing.Point(6, 55);
            this.labelGain1.Name = "labelGain1";
            this.labelGain1.Size = new System.Drawing.Size(38, 13);
            this.labelGain1.TabIndex = 13;
            this.labelGain1.Text = "Gain1:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Port Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Baudrate:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(379, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Parity:";
            // 
            // comboBoxParity
            // 
            this.comboBoxParity.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxParity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxParity.FormattingEnabled = true;
            this.comboBoxParity.Items.AddRange(new object[] {
            "1200",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.comboBoxParity.Location = new System.Drawing.Point(421, 16);
            this.comboBoxParity.Name = "comboBoxParity";
            this.comboBoxParity.Size = new System.Drawing.Size(49, 21);
            this.comboBoxParity.TabIndex = 16;
            // 
            // comboBoxStopBits
            // 
            this.comboBoxStopBits.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxStopBits.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxStopBits.FormattingEnabled = true;
            this.comboBoxStopBits.Items.AddRange(new object[] {
            "1200",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.comboBoxStopBits.Location = new System.Drawing.Point(534, 16);
            this.comboBoxStopBits.Name = "comboBoxStopBits";
            this.comboBoxStopBits.Size = new System.Drawing.Size(33, 21);
            this.comboBoxStopBits.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(476, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Stop Bits:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(281, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Data Bits:";
            // 
            // comboBoxDataBits
            // 
            this.comboBoxDataBits.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDataBits.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDataBits.FormattingEnabled = true;
            this.comboBoxDataBits.Items.AddRange(new object[] {
            "1200",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.comboBoxDataBits.Location = new System.Drawing.Point(340, 16);
            this.comboBoxDataBits.Name = "comboBoxDataBits";
            this.comboBoxDataBits.Size = new System.Drawing.Size(33, 21);
            this.comboBoxDataBits.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(836, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Slave ID:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Offset:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Threshold1:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 135);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Threshold2:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 109);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "Gain2:";
            // 
            // comboBoxGain2
            // 
            this.comboBoxGain2.AllowDrop = true;
            this.comboBoxGain2.AutoCompleteCustomSource.AddRange(new string[] {
            "1..247"});
            this.comboBoxGain2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxGain2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxGain2.FormattingEnabled = true;
            this.comboBoxGain2.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "8",
            "16",
            "32",
            "64"});
            this.comboBoxGain2.Location = new System.Drawing.Point(70, 106);
            this.comboBoxGain2.Name = "comboBoxGain2";
            this.comboBoxGain2.Size = new System.Drawing.Size(60, 21);
            this.comboBoxGain2.TabIndex = 27;
            this.comboBoxGain2.SelectedIndexChanged += new System.EventHandler(this.ComboBoxGain2_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 109);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 13);
            this.label11.TabIndex = 32;
            this.label11.Text = "Window End:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 82);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 13);
            this.label12.TabIndex = 34;
            this.label12.Text = "Window Beg:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(26, 13);
            this.label13.TabIndex = 35;
            this.label13.Text = "Set:";
            // 
            // comboBoxSet
            // 
            this.comboBoxSet.AllowDrop = true;
            this.comboBoxSet.AutoCompleteCustomSource.AddRange(new string[] {
            "1..247"});
            this.comboBoxSet.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxSet.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxSet.FormattingEnabled = true;
            this.comboBoxSet.Items.AddRange(new object[] {
            "RELAY",
            "SET1",
            "SET2"});
            this.comboBoxSet.Location = new System.Drawing.Point(70, 25);
            this.comboBoxSet.Name = "comboBoxSet";
            this.comboBoxSet.Size = new System.Drawing.Size(60, 21);
            this.comboBoxSet.TabIndex = 36;
            this.comboBoxSet.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSet_SelectedIndexChanged);
            // 
            // comboBoxPositionMode
            // 
            this.comboBoxPositionMode.AllowDrop = true;
            this.comboBoxPositionMode.AutoCompleteCustomSource.AddRange(new string[] {
            "1..247"});
            this.comboBoxPositionMode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxPositionMode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxPositionMode.FormattingEnabled = true;
            this.comboBoxPositionMode.Items.AddRange(new object[] {
            "HMD",
            "Rising",
            "Falling",
            "Peak"});
            this.comboBoxPositionMode.Location = new System.Drawing.Point(80, 51);
            this.comboBoxPositionMode.Name = "comboBoxPositionMode";
            this.comboBoxPositionMode.Size = new System.Drawing.Size(86, 21);
            this.comboBoxPositionMode.TabIndex = 37;
            this.comboBoxPositionMode.SelectedIndexChanged += new System.EventHandler(this.ComboBoxPositionMode_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 55);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 13);
            this.label14.TabIndex = 38;
            this.label14.Text = "Position Mode:";
            // 
            // groupBoxSerial
            // 
            this.groupBoxSerial.Controls.Add(this.comboBoxDataBits);
            this.groupBoxSerial.Controls.Add(this.comboBoxComPorts);
            this.groupBoxSerial.Controls.Add(this.comboBoxBaudrates);
            this.groupBoxSerial.Controls.Add(this.label1);
            this.groupBoxSerial.Controls.Add(this.label2);
            this.groupBoxSerial.Controls.Add(this.comboBoxParity);
            this.groupBoxSerial.Controls.Add(this.label3);
            this.groupBoxSerial.Controls.Add(this.comboBoxStopBits);
            this.groupBoxSerial.Controls.Add(this.label4);
            this.groupBoxSerial.Controls.Add(this.label5);
            this.groupBoxSerial.Location = new System.Drawing.Point(230, 12);
            this.groupBoxSerial.Name = "groupBoxSerial";
            this.groupBoxSerial.Size = new System.Drawing.Size(600, 47);
            this.groupBoxSerial.TabIndex = 39;
            this.groupBoxSerial.TabStop = false;
            this.groupBoxSerial.Text = "Serial Port";
            // 
            // checkedListBoxIOStatus
            // 
            this.checkedListBoxIOStatus.BackColor = System.Drawing.SystemColors.Menu;
            this.checkedListBoxIOStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBoxIOStatus.FormattingEnabled = true;
            this.checkedListBoxIOStatus.Items.AddRange(new object[] {
            "LASER",
            "IR_LED",
            "TEST_IN",
            "SET_IN",
            "ALARM_OUT",
            "SIGNAL_OUT"});
            this.checkedListBoxIOStatus.Location = new System.Drawing.Point(6, 29);
            this.checkedListBoxIOStatus.Name = "checkedListBoxIOStatus";
            this.checkedListBoxIOStatus.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.checkedListBoxIOStatus.Size = new System.Drawing.Size(106, 90);
            this.checkedListBoxIOStatus.TabIndex = 41;
            // 
            // groupBoxLaser
            // 
            this.groupBoxLaser.Controls.Add(this.radioButtonLaserOff);
            this.groupBoxLaser.Controls.Add(this.radioButtonLaserOn);
            this.groupBoxLaser.Enabled = false;
            this.groupBoxLaser.Location = new System.Drawing.Point(10, 285);
            this.groupBoxLaser.Name = "groupBoxLaser";
            this.groupBoxLaser.Size = new System.Drawing.Size(134, 43);
            this.groupBoxLaser.TabIndex = 42;
            this.groupBoxLaser.TabStop = false;
            this.groupBoxLaser.Text = "Laser";
            // 
            // radioButtonLaserOff
            // 
            this.radioButtonLaserOff.AutoSize = true;
            this.radioButtonLaserOff.Location = new System.Drawing.Point(89, 16);
            this.radioButtonLaserOff.Name = "radioButtonLaserOff";
            this.radioButtonLaserOff.Size = new System.Drawing.Size(39, 17);
            this.radioButtonLaserOff.TabIndex = 1;
            this.radioButtonLaserOff.TabStop = true;
            this.radioButtonLaserOff.Text = "Off";
            this.radioButtonLaserOff.UseVisualStyleBackColor = true;
            this.radioButtonLaserOff.CheckedChanged += new System.EventHandler(this.RadioButtonLaserOff_CheckedChanged);
            // 
            // radioButtonLaserOn
            // 
            this.radioButtonLaserOn.AutoSize = true;
            this.radioButtonLaserOn.Location = new System.Drawing.Point(25, 16);
            this.radioButtonLaserOn.Name = "radioButtonLaserOn";
            this.radioButtonLaserOn.Size = new System.Drawing.Size(39, 17);
            this.radioButtonLaserOn.TabIndex = 0;
            this.radioButtonLaserOn.TabStop = true;
            this.radioButtonLaserOn.Text = "On";
            this.radioButtonLaserOn.UseVisualStyleBackColor = true;
            this.radioButtonLaserOn.CheckedChanged += new System.EventHandler(this.RadioButtonLaserOn_CheckedChanged);
            // 
            // groupBoxIOStatus
            // 
            this.groupBoxIOStatus.Controls.Add(this.checkedListBoxIOStatus);
            this.groupBoxIOStatus.Enabled = false;
            this.groupBoxIOStatus.Location = new System.Drawing.Point(328, 236);
            this.groupBoxIOStatus.Name = "groupBoxIOStatus";
            this.groupBoxIOStatus.Size = new System.Drawing.Size(124, 136);
            this.groupBoxIOStatus.TabIndex = 43;
            this.groupBoxIOStatus.TabStop = false;
            this.groupBoxIOStatus.Text = "I/O Status";
            // 
            // groupBoxTest
            // 
            this.groupBoxTest.Controls.Add(this.radioButtonTestOff);
            this.groupBoxTest.Controls.Add(this.radioButtonTestOn);
            this.groupBoxTest.Enabled = false;
            this.groupBoxTest.Location = new System.Drawing.Point(10, 236);
            this.groupBoxTest.Name = "groupBoxTest";
            this.groupBoxTest.Size = new System.Drawing.Size(134, 43);
            this.groupBoxTest.TabIndex = 44;
            this.groupBoxTest.TabStop = false;
            this.groupBoxTest.Text = "Test";
            // 
            // radioButtonTestOff
            // 
            this.radioButtonTestOff.AutoSize = true;
            this.radioButtonTestOff.Location = new System.Drawing.Point(89, 19);
            this.radioButtonTestOff.Name = "radioButtonTestOff";
            this.radioButtonTestOff.Size = new System.Drawing.Size(39, 17);
            this.radioButtonTestOff.TabIndex = 1;
            this.radioButtonTestOff.TabStop = true;
            this.radioButtonTestOff.Text = "Off";
            this.radioButtonTestOff.UseVisualStyleBackColor = true;
            this.radioButtonTestOff.CheckedChanged += new System.EventHandler(this.RadioButtonTestOff_CheckedChanged);
            // 
            // radioButtonTestOn
            // 
            this.radioButtonTestOn.AutoSize = true;
            this.radioButtonTestOn.Location = new System.Drawing.Point(25, 19);
            this.radioButtonTestOn.Name = "radioButtonTestOn";
            this.radioButtonTestOn.Size = new System.Drawing.Size(39, 17);
            this.radioButtonTestOn.TabIndex = 0;
            this.radioButtonTestOn.TabStop = true;
            this.radioButtonTestOn.Text = "On";
            this.radioButtonTestOn.UseVisualStyleBackColor = true;
            this.radioButtonTestOn.CheckedChanged += new System.EventHandler(this.RadioButtonTestOn_CheckedChanged);
            // 
            // numericUpDownThre1
            // 
            this.numericUpDownThre1.Location = new System.Drawing.Point(70, 79);
            this.numericUpDownThre1.Maximum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.numericUpDownThre1.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownThre1.Name = "numericUpDownThre1";
            this.numericUpDownThre1.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownThre1.TabIndex = 45;
            this.numericUpDownThre1.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownThre1.ValueChanged += new System.EventHandler(this.NumericUpDownThre1_ValueChanged);
            // 
            // numericUpDownID
            // 
            this.numericUpDownID.Location = new System.Drawing.Point(893, 29);
            this.numericUpDownID.Maximum = new decimal(new int[] {
            247,
            0,
            0,
            0});
            this.numericUpDownID.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownID.Name = "numericUpDownID";
            this.numericUpDownID.Size = new System.Drawing.Size(59, 20);
            this.numericUpDownID.TabIndex = 46;
            this.numericUpDownID.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownThre2
            // 
            this.numericUpDownThre2.Location = new System.Drawing.Point(70, 133);
            this.numericUpDownThre2.Maximum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.numericUpDownThre2.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownThre2.Name = "numericUpDownThre2";
            this.numericUpDownThre2.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownThre2.TabIndex = 47;
            this.numericUpDownThre2.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownThre2.ValueChanged += new System.EventHandler(this.NumericUpDownThre2_ValueChanged);
            // 
            // numericUpDownWindowBeg
            // 
            this.numericUpDownWindowBeg.Location = new System.Drawing.Point(80, 80);
            this.numericUpDownWindowBeg.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownWindowBeg.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownWindowBeg.Name = "numericUpDownWindowBeg";
            this.numericUpDownWindowBeg.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownWindowBeg.TabIndex = 48;
            this.numericUpDownWindowBeg.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownWindowBeg.ValueChanged += new System.EventHandler(this.NumericUpDownWindowBeg_ValueChanged);
            // 
            // numericUpDownWindowEnd
            // 
            this.numericUpDownWindowEnd.Location = new System.Drawing.Point(80, 107);
            this.numericUpDownWindowEnd.Maximum = new decimal(new int[] {
            95,
            0,
            0,
            0});
            this.numericUpDownWindowEnd.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownWindowEnd.Name = "numericUpDownWindowEnd";
            this.numericUpDownWindowEnd.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownWindowEnd.TabIndex = 49;
            this.numericUpDownWindowEnd.Value = new decimal(new int[] {
            95,
            0,
            0,
            0});
            this.numericUpDownWindowEnd.ValueChanged += new System.EventHandler(this.NumericUpDownWindowEnd_ValueChanged);
            // 
            // numericUpDownOffset
            // 
            this.numericUpDownOffset.Location = new System.Drawing.Point(80, 133);
            this.numericUpDownOffset.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownOffset.Name = "numericUpDownOffset";
            this.numericUpDownOffset.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownOffset.TabIndex = 50;
            this.numericUpDownOffset.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.numericUpDownOffset.ValueChanged += new System.EventHandler(this.NumericUpDownOffset_ValueChanged);
            // 
            // groupBoxSensor
            // 
            this.groupBoxSensor.Controls.Add(this.comboBoxGain2);
            this.groupBoxSensor.Controls.Add(this.comboBoxGain1);
            this.groupBoxSensor.Controls.Add(this.labelGain1);
            this.groupBoxSensor.Controls.Add(this.label8);
            this.groupBoxSensor.Controls.Add(this.numericUpDownThre2);
            this.groupBoxSensor.Controls.Add(this.label10);
            this.groupBoxSensor.Controls.Add(this.label9);
            this.groupBoxSensor.Controls.Add(this.numericUpDownThre1);
            this.groupBoxSensor.Controls.Add(this.label13);
            this.groupBoxSensor.Controls.Add(this.comboBoxSet);
            this.groupBoxSensor.Enabled = false;
            this.groupBoxSensor.Location = new System.Drawing.Point(10, 378);
            this.groupBoxSensor.Name = "groupBoxSensor";
            this.groupBoxSensor.Size = new System.Drawing.Size(134, 162);
            this.groupBoxSensor.TabIndex = 51;
            this.groupBoxSensor.TabStop = false;
            this.groupBoxSensor.Text = "Sensor";
            // 
            // groupBoxAnalog
            // 
            this.groupBoxAnalog.Controls.Add(this.comboBoxAnalogOut);
            this.groupBoxAnalog.Controls.Add(this.label18);
            this.groupBoxAnalog.Controls.Add(this.label12);
            this.groupBoxAnalog.Controls.Add(this.label7);
            this.groupBoxAnalog.Controls.Add(this.label11);
            this.groupBoxAnalog.Controls.Add(this.numericUpDownOffset);
            this.groupBoxAnalog.Controls.Add(this.comboBoxPositionMode);
            this.groupBoxAnalog.Controls.Add(this.numericUpDownWindowEnd);
            this.groupBoxAnalog.Controls.Add(this.label14);
            this.groupBoxAnalog.Controls.Add(this.numericUpDownWindowBeg);
            this.groupBoxAnalog.Enabled = false;
            this.groupBoxAnalog.Location = new System.Drawing.Point(150, 378);
            this.groupBoxAnalog.Name = "groupBoxAnalog";
            this.groupBoxAnalog.Size = new System.Drawing.Size(172, 162);
            this.groupBoxAnalog.TabIndex = 48;
            this.groupBoxAnalog.TabStop = false;
            this.groupBoxAnalog.Text = "Analog";
            // 
            // comboBoxAnalogOut
            // 
            this.comboBoxAnalogOut.AllowDrop = true;
            this.comboBoxAnalogOut.AutoCompleteCustomSource.AddRange(new string[] {
            "1..247"});
            this.comboBoxAnalogOut.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxAnalogOut.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxAnalogOut.FormattingEnabled = true;
            this.comboBoxAnalogOut.Items.AddRange(new object[] {
            "1:Int 2:Pos",
            "1:Pos 2:Int",
            "1:Int 2:Int",
            "1:Pos 2:Pos"});
            this.comboBoxAnalogOut.Location = new System.Drawing.Point(80, 25);
            this.comboBoxAnalogOut.Name = "comboBoxAnalogOut";
            this.comboBoxAnalogOut.Size = new System.Drawing.Size(86, 21);
            this.comboBoxAnalogOut.TabIndex = 51;
            this.comboBoxAnalogOut.SelectedIndexChanged += new System.EventHandler(this.ComboBoxAnalogOut_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 28);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(68, 13);
            this.label18.TabIndex = 52;
            this.label18.Text = "Analog Outs:";
            // 
            // groupBoxFilters
            // 
            this.groupBoxFilters.Controls.Add(this.numericUpDownFilterOff);
            this.groupBoxFilters.Controls.Add(this.numericUpDownFilterOn);
            this.groupBoxFilters.Controls.Add(this.label15);
            this.groupBoxFilters.Controls.Add(this.label16);
            this.groupBoxFilters.Controls.Add(this.numericUpDownFilterPosition);
            this.groupBoxFilters.Controls.Add(this.label17);
            this.groupBoxFilters.Enabled = false;
            this.groupBoxFilters.Location = new System.Drawing.Point(328, 378);
            this.groupBoxFilters.Name = "groupBoxFilters";
            this.groupBoxFilters.Size = new System.Drawing.Size(124, 162);
            this.groupBoxFilters.TabIndex = 0;
            this.groupBoxFilters.TabStop = false;
            this.groupBoxFilters.Text = "Filters";
            // 
            // numericUpDownFilterOff
            // 
            this.numericUpDownFilterOff.Location = new System.Drawing.Point(58, 80);
            this.numericUpDownFilterOff.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDownFilterOff.Name = "numericUpDownFilterOff";
            this.numericUpDownFilterOff.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownFilterOff.TabIndex = 56;
            this.numericUpDownFilterOff.ValueChanged += new System.EventHandler(this.NumericUpDownFilterOff_ValueChanged);
            // 
            // numericUpDownFilterOn
            // 
            this.numericUpDownFilterOn.Location = new System.Drawing.Point(58, 52);
            this.numericUpDownFilterOn.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDownFilterOn.Name = "numericUpDownFilterOn";
            this.numericUpDownFilterOn.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownFilterOn.TabIndex = 55;
            this.numericUpDownFilterOn.ValueChanged += new System.EventHandler(this.NumericUpDownFilterOn_ValueChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 28);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 13);
            this.label15.TabIndex = 53;
            this.label15.Text = "Position:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 82);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(56, 13);
            this.label16.TabIndex = 51;
            this.label16.Text = "Signal Off:";
            // 
            // numericUpDownFilterPosition
            // 
            this.numericUpDownFilterPosition.Location = new System.Drawing.Point(58, 26);
            this.numericUpDownFilterPosition.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDownFilterPosition.Name = "numericUpDownFilterPosition";
            this.numericUpDownFilterPosition.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownFilterPosition.TabIndex = 54;
            this.numericUpDownFilterPosition.ValueChanged += new System.EventHandler(this.NumericUpDownFilterPosition_ValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 55);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(56, 13);
            this.label17.TabIndex = 52;
            this.label17.Text = "Signal On:";
            // 
            // groupBoxActValues
            // 
            this.groupBoxActValues.Controls.Add(this.label23);
            this.groupBoxActValues.Controls.Add(this.label24);
            this.groupBoxActValues.Controls.Add(this.label25);
            this.groupBoxActValues.Controls.Add(this.label26);
            this.groupBoxActValues.Controls.Add(this.labelAnalog1);
            this.groupBoxActValues.Controls.Add(this.labelAnalog2);
            this.groupBoxActValues.Controls.Add(this.textBoxAnalog2);
            this.groupBoxActValues.Controls.Add(this.textBoxAnalog1);
            this.groupBoxActValues.Controls.Add(this.label20);
            this.groupBoxActValues.Controls.Add(this.label19);
            this.groupBoxActValues.Controls.Add(this.textBoxPos);
            this.groupBoxActValues.Controls.Add(this.textBoxInt);
            this.groupBoxActValues.Enabled = false;
            this.groupBoxActValues.Location = new System.Drawing.Point(150, 236);
            this.groupBoxActValues.Name = "groupBoxActValues";
            this.groupBoxActValues.Size = new System.Drawing.Size(172, 136);
            this.groupBoxActValues.TabIndex = 52;
            this.groupBoxActValues.TabStop = false;
            this.groupBoxActValues.Text = "Actual Values";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(142, 80);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(22, 13);
            this.label23.TabIndex = 66;
            this.label23.Text = "mA";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(142, 106);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(22, 13);
            this.label24.TabIndex = 65;
            this.label24.Text = "mA";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(142, 28);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(15, 13);
            this.label25.TabIndex = 64;
            this.label25.Text = "%";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(142, 54);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(15, 13);
            this.label26.TabIndex = 63;
            this.label26.Text = "%";
            // 
            // labelAnalog1
            // 
            this.labelAnalog1.AutoSize = true;
            this.labelAnalog1.Location = new System.Drawing.Point(6, 80);
            this.labelAnalog1.Name = "labelAnalog1";
            this.labelAnalog1.Size = new System.Drawing.Size(68, 13);
            this.labelAnalog1.TabIndex = 62;
            this.labelAnalog1.Text = "An. Intensity:";
            // 
            // labelAnalog2
            // 
            this.labelAnalog2.AutoSize = true;
            this.labelAnalog2.Location = new System.Drawing.Point(6, 106);
            this.labelAnalog2.Name = "labelAnalog2";
            this.labelAnalog2.Size = new System.Drawing.Size(66, 13);
            this.labelAnalog2.TabIndex = 61;
            this.labelAnalog2.Text = "An. Position:";
            // 
            // textBoxAnalog2
            // 
            this.textBoxAnalog2.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxAnalog2.Location = new System.Drawing.Point(80, 103);
            this.textBoxAnalog2.Name = "textBoxAnalog2";
            this.textBoxAnalog2.ReadOnly = true;
            this.textBoxAnalog2.Size = new System.Drawing.Size(60, 20);
            this.textBoxAnalog2.TabIndex = 60;
            this.textBoxAnalog2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxAnalog1
            // 
            this.textBoxAnalog1.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxAnalog1.Location = new System.Drawing.Point(80, 77);
            this.textBoxAnalog1.Name = "textBoxAnalog1";
            this.textBoxAnalog1.ReadOnly = true;
            this.textBoxAnalog1.Size = new System.Drawing.Size(60, 20);
            this.textBoxAnalog1.TabIndex = 59;
            this.textBoxAnalog1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 28);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(49, 13);
            this.label20.TabIndex = 58;
            this.label20.Text = "Intensity:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 54);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(47, 13);
            this.label19.TabIndex = 57;
            this.label19.Text = "Position:";
            // 
            // textBoxPos
            // 
            this.textBoxPos.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxPos.Location = new System.Drawing.Point(80, 51);
            this.textBoxPos.Name = "textBoxPos";
            this.textBoxPos.ReadOnly = true;
            this.textBoxPos.Size = new System.Drawing.Size(60, 20);
            this.textBoxPos.TabIndex = 1;
            this.textBoxPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxInt
            // 
            this.textBoxInt.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxInt.Location = new System.Drawing.Point(80, 25);
            this.textBoxInt.Name = "textBoxInt";
            this.textBoxInt.ReadOnly = true;
            this.textBoxInt.Size = new System.Drawing.Size(60, 20);
            this.textBoxInt.TabIndex = 0;
            this.textBoxInt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBoxSystemInfo
            // 
            this.groupBoxSystemInfo.Controls.Add(this.label35);
            this.groupBoxSystemInfo.Controls.Add(this.label28);
            this.groupBoxSystemInfo.Controls.Add(this.label29);
            this.groupBoxSystemInfo.Controls.Add(this.textBoxMaxTemp);
            this.groupBoxSystemInfo.Controls.Add(this.label30);
            this.groupBoxSystemInfo.Controls.Add(this.textBoxFW);
            this.groupBoxSystemInfo.Controls.Add(this.label27);
            this.groupBoxSystemInfo.Controls.Add(this.label31);
            this.groupBoxSystemInfo.Controls.Add(this.label32);
            this.groupBoxSystemInfo.Controls.Add(this.textBoxTotalTime);
            this.groupBoxSystemInfo.Controls.Add(this.textBoxActTemp);
            this.groupBoxSystemInfo.Controls.Add(this.label33);
            this.groupBoxSystemInfo.Controls.Add(this.label34);
            this.groupBoxSystemInfo.Controls.Add(this.textBoxSerial);
            this.groupBoxSystemInfo.Controls.Add(this.textBoxModel);
            this.groupBoxSystemInfo.Enabled = false;
            this.groupBoxSystemInfo.Location = new System.Drawing.Point(150, 121);
            this.groupBoxSystemInfo.Name = "groupBoxSystemInfo";
            this.groupBoxSystemInfo.Size = new System.Drawing.Size(302, 99);
            this.groupBoxSystemInfo.TabIndex = 67;
            this.groupBoxSystemInfo.TabStop = false;
            this.groupBoxSystemInfo.Text = "System Info";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(282, 21);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(13, 13);
            this.label35.TabIndex = 73;
            this.label35.Text = "h";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(282, 73);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(18, 13);
            this.label28.TabIndex = 72;
            this.label28.Text = "°C";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(147, 73);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(63, 13);
            this.label29.TabIndex = 71;
            this.label29.Text = "Max. Temp:";
            // 
            // textBoxMaxTemp
            // 
            this.textBoxMaxTemp.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxMaxTemp.Location = new System.Drawing.Point(220, 70);
            this.textBoxMaxTemp.Name = "textBoxMaxTemp";
            this.textBoxMaxTemp.ReadOnly = true;
            this.textBoxMaxTemp.Size = new System.Drawing.Size(60, 20);
            this.textBoxMaxTemp.TabIndex = 70;
            this.textBoxMaxTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(6, 73);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(52, 13);
            this.label30.TabIndex = 69;
            this.label30.Text = "Firmware:";
            // 
            // textBoxFW
            // 
            this.textBoxFW.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxFW.Location = new System.Drawing.Point(80, 70);
            this.textBoxFW.Name = "textBoxFW";
            this.textBoxFW.ReadOnly = true;
            this.textBoxFW.Size = new System.Drawing.Size(60, 20);
            this.textBoxFW.TabIndex = 68;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(282, 47);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(18, 13);
            this.label27.TabIndex = 67;
            this.label27.Text = "°C";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(147, 47);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(67, 13);
            this.label31.TabIndex = 62;
            this.label31.Text = "Intern.Temp:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(147, 21);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(49, 13);
            this.label32.TabIndex = 61;
            this.label32.Text = "Runtime:";
            // 
            // textBoxTotalTime
            // 
            this.textBoxTotalTime.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxTotalTime.Location = new System.Drawing.Point(220, 19);
            this.textBoxTotalTime.Name = "textBoxTotalTime";
            this.textBoxTotalTime.ReadOnly = true;
            this.textBoxTotalTime.Size = new System.Drawing.Size(60, 20);
            this.textBoxTotalTime.TabIndex = 60;
            this.textBoxTotalTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxActTemp
            // 
            this.textBoxActTemp.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxActTemp.Location = new System.Drawing.Point(220, 44);
            this.textBoxActTemp.Name = "textBoxActTemp";
            this.textBoxActTemp.ReadOnly = true;
            this.textBoxActTemp.Size = new System.Drawing.Size(60, 20);
            this.textBoxActTemp.TabIndex = 59;
            this.textBoxActTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 21);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(39, 13);
            this.label33.TabIndex = 58;
            this.label33.Text = "Model:";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(6, 47);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(56, 13);
            this.label34.TabIndex = 57;
            this.label34.Text = "Serial No.:";
            // 
            // textBoxSerial
            // 
            this.textBoxSerial.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxSerial.Location = new System.Drawing.Point(80, 44);
            this.textBoxSerial.Name = "textBoxSerial";
            this.textBoxSerial.ReadOnly = true;
            this.textBoxSerial.Size = new System.Drawing.Size(60, 20);
            this.textBoxSerial.TabIndex = 1;
            // 
            // textBoxModel
            // 
            this.textBoxModel.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxModel.Location = new System.Drawing.Point(80, 18);
            this.textBoxModel.Name = "textBoxModel";
            this.textBoxModel.ReadOnly = true;
            this.textBoxModel.Size = new System.Drawing.Size(60, 20);
            this.textBoxModel.TabIndex = 0;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Enabled = false;
            this.buttonLogin.Location = new System.Drawing.Point(10, 130);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(134, 36);
            this.buttonLogin.TabIndex = 68;
            this.buttonLogin.Text = "&Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.ButtonLogin_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.Enabled = false;
            this.buttonImport.Location = new System.Drawing.Point(10, 175);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(64, 36);
            this.buttonImport.TabIndex = 69;
            this.buttonImport.Text = "&Import Settings";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.ButtonImport_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Enabled = false;
            this.buttonExport.Location = new System.Drawing.Point(80, 175);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(64, 36);
            this.buttonExport.TabIndex = 70;
            this.buttonExport.Text = "&Export Settings";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.ButtonExport_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "ini";
            this.openFileDialog1.FileName = "SHK_Setup_";
            this.openFileDialog1.Filter = "INI files|*.ini|All files|*.*";
            this.openFileDialog1.InitialDirectory = ".";
            this.openFileDialog1.Title = "Open INI file";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "ini";
            this.saveFileDialog1.Filter = "INI files|*.ini|All files|*.*";
            this.saveFileDialog1.InitialDirectory = ".";
            this.saveFileDialog1.Title = "Save as INI file";
            // 
            // groupBoxMode
            // 
            this.groupBoxMode.Controls.Add(this.radioButtonRTUUDP);
            this.groupBoxMode.Controls.Add(this.radioButtonRTUTCP);
            this.groupBoxMode.Controls.Add(this.radioButtonUDP);
            this.groupBoxMode.Controls.Add(this.radioButtonTCP);
            this.groupBoxMode.Controls.Add(this.radioButtonSerial);
            this.groupBoxMode.Location = new System.Drawing.Point(10, 12);
            this.groupBoxMode.Name = "groupBoxMode";
            this.groupBoxMode.Size = new System.Drawing.Size(212, 100);
            this.groupBoxMode.TabIndex = 71;
            this.groupBoxMode.TabStop = false;
            this.groupBoxMode.Text = "Modbus Mode";
            // 
            // radioButtonRTUUDP
            // 
            this.radioButtonRTUUDP.AutoSize = true;
            this.radioButtonRTUUDP.Location = new System.Drawing.Point(7, 76);
            this.radioButtonRTUUDP.Name = "radioButtonRTUUDP";
            this.radioButtonRTUUDP.Size = new System.Drawing.Size(98, 17);
            this.radioButtonRTUUDP.TabIndex = 4;
            this.radioButtonRTUUDP.Text = "RTU over UDP";
            this.radioButtonRTUUDP.UseVisualStyleBackColor = true;
            this.radioButtonRTUUDP.CheckedChanged += new System.EventHandler(this.RadioButtonRTUUDP_CheckedChanged);
            // 
            // radioButtonRTUTCP
            // 
            this.radioButtonRTUTCP.AutoSize = true;
            this.radioButtonRTUTCP.Location = new System.Drawing.Point(7, 53);
            this.radioButtonRTUTCP.Name = "radioButtonRTUTCP";
            this.radioButtonRTUTCP.Size = new System.Drawing.Size(96, 17);
            this.radioButtonRTUTCP.TabIndex = 3;
            this.radioButtonRTUTCP.Text = "RTU over TCP";
            this.radioButtonRTUTCP.UseVisualStyleBackColor = true;
            this.radioButtonRTUTCP.CheckedChanged += new System.EventHandler(this.RadioButtonRTUTCP_CheckedChanged);
            // 
            // radioButtonUDP
            // 
            this.radioButtonUDP.AutoSize = true;
            this.radioButtonUDP.Location = new System.Drawing.Point(113, 76);
            this.radioButtonUDP.Name = "radioButtonUDP";
            this.radioButtonUDP.Size = new System.Drawing.Size(91, 17);
            this.radioButtonUDP.TabIndex = 2;
            this.radioButtonUDP.Text = "Modbus/UDP";
            this.radioButtonUDP.UseVisualStyleBackColor = true;
            this.radioButtonUDP.CheckedChanged += new System.EventHandler(this.RadioButtonUDP_CheckedChanged);
            // 
            // radioButtonTCP
            // 
            this.radioButtonTCP.AutoSize = true;
            this.radioButtonTCP.Location = new System.Drawing.Point(113, 53);
            this.radioButtonTCP.Name = "radioButtonTCP";
            this.radioButtonTCP.Size = new System.Drawing.Size(89, 17);
            this.radioButtonTCP.TabIndex = 1;
            this.radioButtonTCP.Text = "Modbus/TCP";
            this.radioButtonTCP.UseVisualStyleBackColor = true;
            this.radioButtonTCP.CheckedChanged += new System.EventHandler(this.RadioButtonTCP_CheckedChanged);
            // 
            // radioButtonSerial
            // 
            this.radioButtonSerial.AutoSize = true;
            this.radioButtonSerial.Checked = true;
            this.radioButtonSerial.Location = new System.Drawing.Point(7, 17);
            this.radioButtonSerial.Name = "radioButtonSerial";
            this.radioButtonSerial.Size = new System.Drawing.Size(99, 17);
            this.radioButtonSerial.TabIndex = 0;
            this.radioButtonSerial.TabStop = true;
            this.radioButtonSerial.Text = "Serial Port RTU";
            this.radioButtonSerial.UseVisualStyleBackColor = true;
            this.radioButtonSerial.CheckedChanged += new System.EventHandler(this.RadioButtonSerial_CheckedChanged);
            // 
            // groupBoxTCP
            // 
            this.groupBoxTCP.Controls.Add(this.label22);
            this.groupBoxTCP.Controls.Add(this.label21);
            this.groupBoxTCP.Controls.Add(this.textBoxPort);
            this.groupBoxTCP.Controls.Add(this.textBoxIP);
            this.groupBoxTCP.Location = new System.Drawing.Point(230, 65);
            this.groupBoxTCP.Name = "groupBoxTCP";
            this.groupBoxTCP.Size = new System.Drawing.Size(600, 47);
            this.groupBoxTCP.TabIndex = 72;
            this.groupBoxTCP.TabStop = false;
            this.groupBoxTCP.Text = "Network";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(305, 18);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(29, 13);
            this.label22.TabIndex = 3;
            this.label22.Text = "Port:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 18);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(61, 13);
            this.label21.TabIndex = 2;
            this.label21.Text = "IP Address:";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(340, 15);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(84, 20);
            this.textBoxPort.TabIndex = 1;
            this.textBoxPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(73, 15);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(202, 20);
            this.textBoxIP.TabIndex = 0;
            this.textBoxIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 546);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(134, 65);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 73;
            this.pictureBox1.TabStop = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(32, 609);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(90, 13);
            this.linkLabel1.TabIndex = 74;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "www.ssktrinec.cz";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 631);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.groupBoxTCP);
            this.Controls.Add(this.groupBoxMode);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.groupBoxSystemInfo);
            this.Controls.Add(this.groupBoxActValues);
            this.Controls.Add(this.groupBoxAnalog);
            this.Controls.Add(this.groupBoxFilters);
            this.Controls.Add(this.groupBoxSensor);
            this.Controls.Add(this.numericUpDownID);
            this.Controls.Add(this.groupBoxTest);
            this.Controls.Add(this.groupBoxIOStatus);
            this.Controls.Add(this.groupBoxLaser);
            this.Controls.Add(this.groupBoxSerial);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.buttonConnect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(980, 670);
            this.Name = "FormMain";
            this.Text = "SHK Utility 1.2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBoxSerial.ResumeLayout(false);
            this.groupBoxSerial.PerformLayout();
            this.groupBoxLaser.ResumeLayout(false);
            this.groupBoxLaser.PerformLayout();
            this.groupBoxIOStatus.ResumeLayout(false);
            this.groupBoxTest.ResumeLayout(false);
            this.groupBoxTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThre1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThre2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWindowBeg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWindowEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOffset)).EndInit();
            this.groupBoxSensor.ResumeLayout(false);
            this.groupBoxSensor.PerformLayout();
            this.groupBoxAnalog.ResumeLayout(false);
            this.groupBoxAnalog.PerformLayout();
            this.groupBoxFilters.ResumeLayout(false);
            this.groupBoxFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterOff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterPosition)).EndInit();
            this.groupBoxActValues.ResumeLayout(false);
            this.groupBoxActValues.PerformLayout();
            this.groupBoxSystemInfo.ResumeLayout(false);
            this.groupBoxSystemInfo.PerformLayout();
            this.groupBoxMode.ResumeLayout(false);
            this.groupBoxMode.PerformLayout();
            this.groupBoxTCP.ResumeLayout(false);
            this.groupBoxTCP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.ComboBox comboBoxComPorts;
        private System.Windows.Forms.ComboBox comboBoxBaudrates;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.ComboBox comboBoxGain1;
        private System.Windows.Forms.Label labelGain1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxParity;
        private System.Windows.Forms.ComboBox comboBoxStopBits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxDataBits;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxGain2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBoxSet;
        private System.Windows.Forms.ComboBox comboBoxPositionMode;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBoxSerial;
        private System.Windows.Forms.CheckedListBox checkedListBoxIOStatus;
        private System.Windows.Forms.GroupBox groupBoxLaser;
        private System.Windows.Forms.RadioButton radioButtonLaserOff;
        private System.Windows.Forms.RadioButton radioButtonLaserOn;
        private System.Windows.Forms.GroupBox groupBoxIOStatus;
        private System.Windows.Forms.GroupBox groupBoxTest;
        private System.Windows.Forms.RadioButton radioButtonTestOff;
        private System.Windows.Forms.RadioButton radioButtonTestOn;
        private System.Windows.Forms.NumericUpDown numericUpDownThre1;
        private System.Windows.Forms.NumericUpDown numericUpDownID;
        private System.Windows.Forms.NumericUpDown numericUpDownThre2;
        private System.Windows.Forms.NumericUpDown numericUpDownWindowBeg;
        private System.Windows.Forms.NumericUpDown numericUpDownWindowEnd;
        private System.Windows.Forms.NumericUpDown numericUpDownOffset;
        private System.Windows.Forms.GroupBox groupBoxSensor;
        private System.Windows.Forms.GroupBox groupBoxAnalog;
        private System.Windows.Forms.GroupBox groupBoxFilters;
        private System.Windows.Forms.NumericUpDown numericUpDownFilterOff;
        private System.Windows.Forms.NumericUpDown numericUpDownFilterOn;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown numericUpDownFilterPosition;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox comboBoxAnalogOut;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBoxActValues;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label labelAnalog1;
        private System.Windows.Forms.Label labelAnalog2;
        private System.Windows.Forms.TextBox textBoxAnalog2;
        private System.Windows.Forms.TextBox textBoxAnalog1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBoxPos;
        private System.Windows.Forms.TextBox textBoxInt;
        private System.Windows.Forms.GroupBox groupBoxSystemInfo;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox textBoxTotalTime;
        private System.Windows.Forms.TextBox textBoxActTemp;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox textBoxSerial;
        private System.Windows.Forms.TextBox textBoxModel;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox textBoxMaxTemp;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox textBoxFW;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBoxMode;
        private System.Windows.Forms.RadioButton radioButtonUDP;
        private System.Windows.Forms.RadioButton radioButtonTCP;
        private System.Windows.Forms.RadioButton radioButtonSerial;
        private System.Windows.Forms.GroupBox groupBoxTCP;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.RadioButton radioButtonRTUTCP;
        private System.Windows.Forms.RadioButton radioButtonRTUUDP;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

