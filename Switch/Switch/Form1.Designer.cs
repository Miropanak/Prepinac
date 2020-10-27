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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_timer)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(40, 361);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(582, 170);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // button_findDevices
            // 
            this.button_findDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_findDevices.Location = new System.Drawing.Point(925, 44);
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
            this.button_startCapture.Location = new System.Drawing.Point(925, 115);
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
            this.button_stopCapture.Location = new System.Drawing.Point(925, 177);
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
            this.checkedListBox_foundDevices.Location = new System.Drawing.Point(40, 49);
            this.checkedListBox_foundDevices.Name = "checkedListBox_foundDevices";
            this.checkedListBox_foundDevices.Size = new System.Drawing.Size(445, 259);
            this.checkedListBox_foundDevices.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(556, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "CAM table";
            // 
            // button_resetStats
            // 
            this.button_resetStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_resetStats.Location = new System.Drawing.Point(925, 386);
            this.button_resetStats.Name = "button_resetStats";
            this.button_resetStats.Size = new System.Drawing.Size(149, 32);
            this.button_resetStats.TabIndex = 7;
            this.button_resetStats.Text = "Reset statistics";
            this.button_resetStats.UseVisualStyleBackColor = true;
            this.button_resetStats.Click += new System.EventHandler(this.button_resetStats_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(35, 332);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Statistics";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(561, 49);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(328, 259);
            this.richTextBox2.TabIndex = 9;
            this.richTextBox2.Text = "";
            // 
            // dev_label
            // 
            this.dev_label.AutoSize = true;
            this.dev_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dev_label.Location = new System.Drawing.Point(35, 9);
            this.dev_label.Name = "dev_label";
            this.dev_label.Size = new System.Drawing.Size(82, 25);
            this.dev_label.TabIndex = 10;
            this.dev_label.Text = "Devices";
            // 
            // button_resetCam
            // 
            this.button_resetCam.Location = new System.Drawing.Point(925, 331);
            this.button_resetCam.Name = "button_resetCam";
            this.button_resetCam.Size = new System.Drawing.Size(149, 32);
            this.button_resetCam.TabIndex = 11;
            this.button_resetCam.Text = "Reset CAM";
            this.button_resetCam.UseVisualStyleBackColor = true;
            this.button_resetCam.Click += new System.EventHandler(this.button_resetCam_Click);
            // 
            // button_setTimer
            // 
            this.button_setTimer.Location = new System.Drawing.Point(925, 271);
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
            this.label3.Location = new System.Drawing.Point(1080, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "sec";
            // 
            // numericUpDown_timer
            // 
            this.numericUpDown_timer.Location = new System.Drawing.Point(925, 239);
            this.numericUpDown_timer.Name = "numericUpDown_timer";
            this.numericUpDown_timer.Size = new System.Drawing.Size(149, 22);
            this.numericUpDown_timer.TabIndex = 15;
            this.numericUpDown_timer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(922, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "Default timer set to 30s";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 566);
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
    }
}

