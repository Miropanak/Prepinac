namespace Switch
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button_findDevices = new System.Windows.Forms.Button();
            this.button_startCapture = new System.Windows.Forms.Button();
            this.button_stopCapture = new System.Windows.Forms.Button();
            this.checkedListBox_foundDevices = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_resetStats = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.dev_label = new System.Windows.Forms.Label();
            this.button_resetCam = new System.Windows.Forms.Button();
            this.button_setTimer = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_timer = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.button_editRule = new System.Windows.Forms.Button();
            this.button_createRule = new System.Windows.Forms.Button();
            this.button_deleteRule = new System.Windows.Forms.Button();
            this.label_permitDeny = new System.Windows.Forms.Label();
            this.label_port = new System.Windows.Forms.Label();
            this.label_srcMAC = new System.Windows.Forms.Label();
            this.textBox_srcMAC = new System.Windows.Forms.TextBox();
            this.textBox_srcIP = new System.Windows.Forms.TextBox();
            this.textBox_dstMAC = new System.Windows.Forms.TextBox();
            this.textBox_dstIP = new System.Windows.Forms.TextBox();
            this.label_dstMAC = new System.Windows.Forms.Label();
            this.label_srcIP = new System.Windows.Forms.Label();
            this.label_dstIP = new System.Windows.Forms.Label();
            this.label_srcPort = new System.Windows.Forms.Label();
            this.textBox_srcPort = new System.Windows.Forms.TextBox();
            this.label_dstPort = new System.Windows.Forms.Label();
            this.textBox_dstPort = new System.Windows.Forms.TextBox();
            this.button_selectRule = new System.Windows.Forms.Button();
            this.comboBox_permitDeny = new System.Windows.Forms.ComboBox();
            this.comboBox_port = new System.Windows.Forms.ComboBox();
            this.listView_rules = new System.Windows.Forms.ListView();
            this.column_type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_port = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_srcMAC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_srcIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_dstMAC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_dstIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_srcPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_dstPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_inOut = new System.Windows.Forms.ComboBox();
            this.textBox_protocol = new System.Windows.Forms.TextBox();
            this.label_Protocol = new System.Windows.Forms.Label();
            this.column_inOut = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_protocol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_timer)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(39, 627);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(902, 123);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // button_findDevices
            // 
            this.button_findDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_findDevices.Location = new System.Drawing.Point(1131, 43);
            this.button_findDevices.Name = "button_findDevices";
            this.button_findDevices.Size = new System.Drawing.Size(149, 32);
            this.button_findDevices.TabIndex = 1;
            this.button_findDevices.Text = "Find devices";
            this.button_findDevices.UseVisualStyleBackColor = true;
            this.button_findDevices.Click += new System.EventHandler(this.button_findDevices_Click);
            // 
            // button_startCapture
            // 
            this.button_startCapture.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_startCapture.Location = new System.Drawing.Point(1133, 100);
            this.button_startCapture.Name = "button_startCapture";
            this.button_startCapture.Size = new System.Drawing.Size(149, 32);
            this.button_startCapture.TabIndex = 2;
            this.button_startCapture.Text = "Start Capture";
            this.button_startCapture.UseVisualStyleBackColor = true;
            this.button_startCapture.Click += new System.EventHandler(this.button2_Click);
            // 
            // button_stopCapture
            // 
            this.button_stopCapture.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_stopCapture.Location = new System.Drawing.Point(1133, 156);
            this.button_stopCapture.Name = "button_stopCapture";
            this.button_stopCapture.Size = new System.Drawing.Size(149, 32);
            this.button_stopCapture.TabIndex = 3;
            this.button_stopCapture.Text = "Stop Capture";
            this.button_stopCapture.UseVisualStyleBackColor = true;
            this.button_stopCapture.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkedListBox_foundDevices
            // 
            this.checkedListBox_foundDevices.FormattingEnabled = true;
            this.checkedListBox_foundDevices.Location = new System.Drawing.Point(39, 43);
            this.checkedListBox_foundDevices.Name = "checkedListBox_foundDevices";
            this.checkedListBox_foundDevices.Size = new System.Drawing.Size(445, 259);
            this.checkedListBox_foundDevices.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(547, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "CAM table";
            // 
            // button_resetStats
            // 
            this.button_resetStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_resetStats.Location = new System.Drawing.Point(962, 627);
            this.button_resetStats.Name = "button_resetStats";
            this.button_resetStats.Size = new System.Drawing.Size(149, 32);
            this.button_resetStats.TabIndex = 7;
            this.button_resetStats.Text = "Reset Statistics";
            this.button_resetStats.UseVisualStyleBackColor = true;
            this.button_resetStats.Click += new System.EventHandler(this.button_resetStats_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(35, 599);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 24);
            this.label2.TabIndex = 8;
            this.label2.Text = "Statistics";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(551, 42);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(371, 259);
            this.richTextBox2.TabIndex = 9;
            this.richTextBox2.Text = "";
            // 
            // dev_label
            // 
            this.dev_label.AutoSize = true;
            this.dev_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dev_label.Location = new System.Drawing.Point(34, 15);
            this.dev_label.Name = "dev_label";
            this.dev_label.Size = new System.Drawing.Size(77, 24);
            this.dev_label.TabIndex = 10;
            this.dev_label.Text = "Devices";
            // 
            // button_resetCam
            // 
            this.button_resetCam.Location = new System.Drawing.Point(942, 44);
            this.button_resetCam.Name = "button_resetCam";
            this.button_resetCam.Size = new System.Drawing.Size(149, 32);
            this.button_resetCam.TabIndex = 11;
            this.button_resetCam.Text = "Reset CAM";
            this.button_resetCam.UseVisualStyleBackColor = true;
            this.button_resetCam.Click += new System.EventHandler(this.button_resetCam_Click);
            // 
            // button_setTimer
            // 
            this.button_setTimer.Location = new System.Drawing.Point(942, 156);
            this.button_setTimer.Name = "button_setTimer";
            this.button_setTimer.Size = new System.Drawing.Size(149, 32);
            this.button_setTimer.TabIndex = 12;
            this.button_setTimer.Text = "Set Timer";
            this.button_setTimer.UseVisualStyleBackColor = true;
            this.button_setTimer.Click += new System.EventHandler(this.button_setTimer_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1097, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "sec";
            // 
            // numericUpDown_timer
            // 
            this.numericUpDown_timer.Location = new System.Drawing.Point(942, 128);
            this.numericUpDown_timer.Name = "numericUpDown_timer";
            this.numericUpDown_timer.Size = new System.Drawing.Size(149, 22);
            this.numericUpDown_timer.TabIndex = 15;
            this.numericUpDown_timer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(939, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "Default timer set to 30s";
            // 
            // button_editRule
            // 
            this.button_editRule.Location = new System.Drawing.Point(1100, 403);
            this.button_editRule.Name = "button_editRule";
            this.button_editRule.Size = new System.Drawing.Size(151, 32);
            this.button_editRule.TabIndex = 20;
            this.button_editRule.Text = "Edit Rule";
            this.button_editRule.UseVisualStyleBackColor = true;
            this.button_editRule.Click += new System.EventHandler(this.button_editRule_Click);
            // 
            // button_createRule
            // 
            this.button_createRule.Location = new System.Drawing.Point(1098, 354);
            this.button_createRule.Name = "button_createRule";
            this.button_createRule.Size = new System.Drawing.Size(151, 32);
            this.button_createRule.TabIndex = 21;
            this.button_createRule.Text = "Create Rule";
            this.button_createRule.UseVisualStyleBackColor = true;
            this.button_createRule.Click += new System.EventHandler(this.button_createRule_Click);
            // 
            // button_deleteRule
            // 
            this.button_deleteRule.Location = new System.Drawing.Point(1100, 506);
            this.button_deleteRule.Name = "button_deleteRule";
            this.button_deleteRule.Size = new System.Drawing.Size(149, 32);
            this.button_deleteRule.TabIndex = 22;
            this.button_deleteRule.Text = "Delete Rule";
            this.button_deleteRule.UseVisualStyleBackColor = true;
            this.button_deleteRule.Click += new System.EventHandler(this.button_deleteRule_Click);
            // 
            // label_permitDeny
            // 
            this.label_permitDeny.AutoSize = true;
            this.label_permitDeny.Location = new System.Drawing.Point(36, 336);
            this.label_permitDeny.Name = "label_permitDeny";
            this.label_permitDeny.Size = new System.Drawing.Size(85, 17);
            this.label_permitDeny.TabIndex = 24;
            this.label_permitDeny.Text = "Permit/Deny";
            this.label_permitDeny.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_port
            // 
            this.label_port.AutoSize = true;
            this.label_port.Location = new System.Drawing.Point(141, 336);
            this.label_port.Name = "label_port";
            this.label_port.Size = new System.Drawing.Size(34, 17);
            this.label_port.TabIndex = 26;
            this.label_port.Text = "Port";
            this.label_port.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_srcMAC
            // 
            this.label_srcMAC.AutoSize = true;
            this.label_srcMAC.Location = new System.Drawing.Point(267, 334);
            this.label_srcMAC.Name = "label_srcMAC";
            this.label_srcMAC.Size = new System.Drawing.Size(66, 17);
            this.label_srcMAC.TabIndex = 27;
            this.label_srcMAC.Text = "Src. MAC";
            this.label_srcMAC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_srcMAC
            // 
            this.textBox_srcMAC.Location = new System.Drawing.Point(270, 354);
            this.textBox_srcMAC.Name = "textBox_srcMAC";
            this.textBox_srcMAC.Size = new System.Drawing.Size(139, 22);
            this.textBox_srcMAC.TabIndex = 28;
            // 
            // textBox_srcIP
            // 
            this.textBox_srcIP.Location = new System.Drawing.Point(415, 354);
            this.textBox_srcIP.Name = "textBox_srcIP";
            this.textBox_srcIP.Size = new System.Drawing.Size(139, 22);
            this.textBox_srcIP.TabIndex = 29;
            // 
            // textBox_dstMAC
            // 
            this.textBox_dstMAC.Location = new System.Drawing.Point(560, 354);
            this.textBox_dstMAC.Name = "textBox_dstMAC";
            this.textBox_dstMAC.Size = new System.Drawing.Size(139, 22);
            this.textBox_dstMAC.TabIndex = 31;
            // 
            // textBox_dstIP
            // 
            this.textBox_dstIP.Location = new System.Drawing.Point(705, 354);
            this.textBox_dstIP.Name = "textBox_dstIP";
            this.textBox_dstIP.Size = new System.Drawing.Size(139, 22);
            this.textBox_dstIP.TabIndex = 32;
            // 
            // label_dstMAC
            // 
            this.label_dstMAC.AutoSize = true;
            this.label_dstMAC.Location = new System.Drawing.Point(557, 334);
            this.label_dstMAC.Name = "label_dstMAC";
            this.label_dstMAC.Size = new System.Drawing.Size(66, 17);
            this.label_dstMAC.TabIndex = 33;
            this.label_dstMAC.Text = "Dst. MAC";
            this.label_dstMAC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_srcIP
            // 
            this.label_srcIP.AutoSize = true;
            this.label_srcIP.Location = new System.Drawing.Point(412, 334);
            this.label_srcIP.Name = "label_srcIP";
            this.label_srcIP.Size = new System.Drawing.Size(49, 17);
            this.label_srcIP.TabIndex = 34;
            this.label_srcIP.Text = "Src. IP";
            this.label_srcIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_dstIP
            // 
            this.label_dstIP.AutoSize = true;
            this.label_dstIP.Location = new System.Drawing.Point(702, 334);
            this.label_dstIP.Name = "label_dstIP";
            this.label_dstIP.Size = new System.Drawing.Size(49, 17);
            this.label_dstIP.TabIndex = 35;
            this.label_dstIP.Text = "Dst. IP";
            this.label_dstIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_srcPort
            // 
            this.label_srcPort.AutoSize = true;
            this.label_srcPort.Location = new System.Drawing.Point(922, 334);
            this.label_srcPort.Name = "label_srcPort";
            this.label_srcPort.Size = new System.Drawing.Size(63, 17);
            this.label_srcPort.TabIndex = 37;
            this.label_srcPort.Text = "Src. Port";
            this.label_srcPort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_srcPort
            // 
            this.textBox_srcPort.Location = new System.Drawing.Point(925, 354);
            this.textBox_srcPort.Name = "textBox_srcPort";
            this.textBox_srcPort.Size = new System.Drawing.Size(69, 22);
            this.textBox_srcPort.TabIndex = 36;
            // 
            // label_dstPort
            // 
            this.label_dstPort.AutoSize = true;
            this.label_dstPort.Location = new System.Drawing.Point(997, 334);
            this.label_dstPort.Name = "label_dstPort";
            this.label_dstPort.Size = new System.Drawing.Size(63, 17);
            this.label_dstPort.TabIndex = 39;
            this.label_dstPort.Text = "Dst. Port";
            this.label_dstPort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_dstPort
            // 
            this.textBox_dstPort.Location = new System.Drawing.Point(1000, 354);
            this.textBox_dstPort.Name = "textBox_dstPort";
            this.textBox_dstPort.Size = new System.Drawing.Size(69, 22);
            this.textBox_dstPort.TabIndex = 38;
            // 
            // button_selectRule
            // 
            this.button_selectRule.Location = new System.Drawing.Point(1100, 451);
            this.button_selectRule.Name = "button_selectRule";
            this.button_selectRule.Size = new System.Drawing.Size(151, 32);
            this.button_selectRule.TabIndex = 40;
            this.button_selectRule.Text = "Select Rule";
            this.button_selectRule.UseVisualStyleBackColor = true;
            this.button_selectRule.Click += new System.EventHandler(this.button_selectRule_Click);
            // 
            // comboBox_permitDeny
            // 
            this.comboBox_permitDeny.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_permitDeny.FormattingEnabled = true;
            this.comboBox_permitDeny.Items.AddRange(new object[] {
            "Permit",
            "Deny"});
            this.comboBox_permitDeny.Location = new System.Drawing.Point(38, 354);
            this.comboBox_permitDeny.Name = "comboBox_permitDeny";
            this.comboBox_permitDeny.Size = new System.Drawing.Size(99, 24);
            this.comboBox_permitDeny.TabIndex = 41;
            // 
            // comboBox_port
            // 
            this.comboBox_port.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_port.FormattingEnabled = true;
            this.comboBox_port.Items.AddRange(new object[] {
            "any",
            "0",
            "1"});
            this.comboBox_port.Location = new System.Drawing.Point(144, 354);
            this.comboBox_port.Name = "comboBox_port";
            this.comboBox_port.Size = new System.Drawing.Size(57, 24);
            this.comboBox_port.TabIndex = 42;
            // 
            // listView_rules
            // 
            this.listView_rules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_type,
            this.column_port,
            this.column_inOut,
            this.column_srcMAC,
            this.column_srcIP,
            this.column_dstMAC,
            this.column_dstIP,
            this.column_protocol,
            this.column_srcPort,
            this.column_dstPort});
            this.listView_rules.FullRowSelect = true;
            this.listView_rules.GridLines = true;
            this.listView_rules.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView_rules.HideSelection = false;
            this.listView_rules.Location = new System.Drawing.Point(38, 399);
            this.listView_rules.MultiSelect = false;
            this.listView_rules.Name = "listView_rules";
            this.listView_rules.Size = new System.Drawing.Size(1042, 189);
            this.listView_rules.TabIndex = 43;
            this.listView_rules.UseCompatibleStateImageBehavior = false;
            this.listView_rules.View = System.Windows.Forms.View.Details;
            // 
            // column_type
            // 
            this.column_type.Text = "Type";
            this.column_type.Width = 75;
            // 
            // column_port
            // 
            this.column_port.Text = "Port";
            this.column_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.column_port.Width = 48;
            // 
            // column_srcMAC
            // 
            this.column_srcMAC.Text = "Src. MAC";
            this.column_srcMAC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.column_srcMAC.Width = 108;
            // 
            // column_srcIP
            // 
            this.column_srcIP.Text = "Src. IP";
            this.column_srcIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.column_srcIP.Width = 108;
            // 
            // column_dstMAC
            // 
            this.column_dstMAC.Text = "Dst. MAC";
            this.column_dstMAC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.column_dstMAC.Width = 108;
            // 
            // column_dstIP
            // 
            this.column_dstIP.Text = "Dst.IP";
            this.column_dstIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.column_dstIP.Width = 108;
            // 
            // column_srcPort
            // 
            this.column_srcPort.Text = "Src. Port";
            this.column_srcPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // column_dstPort
            // 
            this.column_dstPort.Text = "Dst. Port";
            this.column_dstPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(204, 334);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 17);
            this.label5.TabIndex = 45;
            this.label5.Text = "In/Out";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox_inOut
            // 
            this.comboBox_inOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_inOut.FormattingEnabled = true;
            this.comboBox_inOut.Items.AddRange(new object[] {
            "any",
            "IN",
            "OUT"});
            this.comboBox_inOut.Location = new System.Drawing.Point(207, 354);
            this.comboBox_inOut.Name = "comboBox_inOut";
            this.comboBox_inOut.Size = new System.Drawing.Size(57, 24);
            this.comboBox_inOut.TabIndex = 46;
            // 
            // textBox_protocol
            // 
            this.textBox_protocol.Location = new System.Drawing.Point(850, 354);
            this.textBox_protocol.Name = "textBox_protocol";
            this.textBox_protocol.Size = new System.Drawing.Size(69, 22);
            this.textBox_protocol.TabIndex = 47;
            // 
            // label_Protocol
            // 
            this.label_Protocol.AutoSize = true;
            this.label_Protocol.Location = new System.Drawing.Point(847, 334);
            this.label_Protocol.Name = "label_Protocol";
            this.label_Protocol.Size = new System.Drawing.Size(60, 17);
            this.label_Protocol.TabIndex = 48;
            this.label_Protocol.Text = "Protocol";
            this.label_Protocol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // column_inOut
            // 
            this.column_inOut.Text = "in/out";
            this.column_inOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.column_inOut.Width = 48;
            // 
            // column_protocol
            // 
            this.column_protocol.Text = "Protocol";
            this.column_protocol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1330, 818);
            this.Controls.Add(this.label_Protocol);
            this.Controls.Add(this.textBox_protocol);
            this.Controls.Add(this.comboBox_inOut);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listView_rules);
            this.Controls.Add(this.comboBox_port);
            this.Controls.Add(this.comboBox_permitDeny);
            this.Controls.Add(this.button_selectRule);
            this.Controls.Add(this.label_dstPort);
            this.Controls.Add(this.textBox_dstPort);
            this.Controls.Add(this.label_srcPort);
            this.Controls.Add(this.textBox_srcPort);
            this.Controls.Add(this.label_dstIP);
            this.Controls.Add(this.label_srcIP);
            this.Controls.Add(this.label_dstMAC);
            this.Controls.Add(this.textBox_dstIP);
            this.Controls.Add(this.textBox_dstMAC);
            this.Controls.Add(this.textBox_srcIP);
            this.Controls.Add(this.textBox_srcMAC);
            this.Controls.Add(this.label_srcMAC);
            this.Controls.Add(this.label_port);
            this.Controls.Add(this.label_permitDeny);
            this.Controls.Add(this.button_deleteRule);
            this.Controls.Add(this.button_createRule);
            this.Controls.Add(this.button_editRule);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDown_timer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_setTimer);
            this.Controls.Add(this.button_resetCam);
            this.Controls.Add(this.dev_label);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_resetStats);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkedListBox_foundDevices);
            this.Controls.Add(this.button_stopCapture);
            this.Controls.Add(this.button_startCapture);
            this.Controls.Add(this.button_findDevices);
            this.Controls.Add(this.richTextBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Switch";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_timer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_findDevices;
        private System.Windows.Forms.Button button_startCapture;
        private System.Windows.Forms.Button button_stopCapture;
        private System.Windows.Forms.CheckedListBox checkedListBox_foundDevices;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_resetStats;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label dev_label;
        private System.Windows.Forms.Button button_resetCam;
        private System.Windows.Forms.Button button_setTimer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown_timer;
        public System.Windows.Forms.RichTextBox richTextBox1;
        public System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_editRule;
        private System.Windows.Forms.Button button_createRule;
        private System.Windows.Forms.Button button_deleteRule;
        private System.Windows.Forms.Label label_permitDeny;
        private System.Windows.Forms.Label label_port;
        private System.Windows.Forms.Label label_srcMAC;
        private System.Windows.Forms.TextBox textBox_srcMAC;
        private System.Windows.Forms.TextBox textBox_srcIP;
        private System.Windows.Forms.TextBox textBox_dstMAC;
        private System.Windows.Forms.TextBox textBox_dstIP;
        private System.Windows.Forms.Label label_dstMAC;
        private System.Windows.Forms.Label label_srcIP;
        private System.Windows.Forms.Label label_dstIP;
        private System.Windows.Forms.Label label_srcPort;
        private System.Windows.Forms.TextBox textBox_srcPort;
        private System.Windows.Forms.Label label_dstPort;
        private System.Windows.Forms.TextBox textBox_dstPort;
        private System.Windows.Forms.Button button_selectRule;
        private System.Windows.Forms.ComboBox comboBox_permitDeny;
        private System.Windows.Forms.ComboBox comboBox_port;
        private System.Windows.Forms.ListView listView_rules;
        private System.Windows.Forms.ColumnHeader column_type;
        private System.Windows.Forms.ColumnHeader column_port;
        private System.Windows.Forms.ColumnHeader column_srcMAC;
        private System.Windows.Forms.ColumnHeader column_dstMAC;
        private System.Windows.Forms.ColumnHeader column_srcIP;
        private System.Windows.Forms.ColumnHeader column_dstIP;
        private System.Windows.Forms.ColumnHeader column_srcPort;
        private System.Windows.Forms.ColumnHeader column_dstPort;
        private System.Windows.Forms.ColumnHeader column_inOut;
        private System.Windows.Forms.ColumnHeader column_protocol;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_inOut;
        private System.Windows.Forms.TextBox textBox_protocol;
        private System.Windows.Forms.Label label_Protocol;
    }
}

