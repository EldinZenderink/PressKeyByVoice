namespace PressKeyByVoice
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
            this.PeakVolumeBar = new System.Windows.Forms.ProgressBar();
            this.AudioDeviceComboBox = new System.Windows.Forms.ComboBox();
            this.ProgramComboBox = new System.Windows.Forms.ComboBox();
            this.KeyTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CurrentKey = new System.Windows.Forms.Label();
            this.SensitivityTrackBar = new System.Windows.Forms.TrackBar();
            this.TresholdTrackBar = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TresholdLevel = new System.Windows.Forms.Label();
            this.SensLevel = new System.Windows.Forms.Label();
            this.PeakVolumeLabel = new System.Windows.Forms.Label();
            this.DebugTextBox = new System.Windows.Forms.TextBox();
            this.StatusBox = new System.Windows.Forms.PictureBox();
            this.KeyPressStatusLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SensitivityTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TresholdTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusBox)).BeginInit();
            this.SuspendLayout();
            // 
            // PeakVolumeBar
            // 
            this.PeakVolumeBar.Location = new System.Drawing.Point(26, 12);
            this.PeakVolumeBar.Name = "PeakVolumeBar";
            this.PeakVolumeBar.Size = new System.Drawing.Size(268, 47);
            this.PeakVolumeBar.TabIndex = 0;
            // 
            // AudioDeviceComboBox
            // 
            this.AudioDeviceComboBox.FormattingEnabled = true;
            this.AudioDeviceComboBox.Location = new System.Drawing.Point(29, 219);
            this.AudioDeviceComboBox.Name = "AudioDeviceComboBox";
            this.AudioDeviceComboBox.Size = new System.Drawing.Size(271, 21);
            this.AudioDeviceComboBox.TabIndex = 1;
            this.AudioDeviceComboBox.Text = "Select your audio device here!";
            this.AudioDeviceComboBox.SelectedIndexChanged += new System.EventHandler(this.AudioDeviceComboBox_SelectedIndexChanged);
            // 
            // ProgramComboBox
            // 
            this.ProgramComboBox.FormattingEnabled = true;
            this.ProgramComboBox.Location = new System.Drawing.Point(29, 260);
            this.ProgramComboBox.Name = "ProgramComboBox";
            this.ProgramComboBox.Size = new System.Drawing.Size(271, 21);
            this.ProgramComboBox.TabIndex = 2;
            this.ProgramComboBox.Text = "Select the program that will receive the keypress!";
            this.ProgramComboBox.SelectedIndexChanged += new System.EventHandler(this.ProgramComboBox_SelectedIndexChanged);
            this.ProgramComboBox.Click += new System.EventHandler(this.ProgramComboBox_Click);
            // 
            // KeyTextBox
            // 
            this.KeyTextBox.Location = new System.Drawing.Point(29, 300);
            this.KeyTextBox.Name = "KeyTextBox";
            this.KeyTextBox.Size = new System.Drawing.Size(142, 20);
            this.KeyTextBox.TabIndex = 3;
            this.KeyTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyTextBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Audio Device:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 244);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Program";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 284);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Key To Be Pressed:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(186, 303);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Current Key:";
            // 
            // CurrentKey
            // 
            this.CurrentKey.AutoSize = true;
            this.CurrentKey.Location = new System.Drawing.Point(257, 303);
            this.CurrentKey.Name = "CurrentKey";
            this.CurrentKey.Size = new System.Drawing.Size(13, 13);
            this.CurrentKey.TabIndex = 9;
            this.CurrentKey.Text = "v";
            // 
            // SensitivityTrackBar
            // 
            this.SensitivityTrackBar.Location = new System.Drawing.Point(26, 78);
            this.SensitivityTrackBar.Maximum = 100;
            this.SensitivityTrackBar.Minimum = 1;
            this.SensitivityTrackBar.Name = "SensitivityTrackBar";
            this.SensitivityTrackBar.Size = new System.Drawing.Size(268, 45);
            this.SensitivityTrackBar.TabIndex = 10;
            this.SensitivityTrackBar.Value = 1;
            this.SensitivityTrackBar.ValueChanged += new System.EventHandler(this.SensitivityTrackBar_ValueChanged);
            // 
            // TresholdTrackBar
            // 
            this.TresholdTrackBar.BackColor = System.Drawing.SystemColors.Control;
            this.TresholdTrackBar.Location = new System.Drawing.Point(26, 124);
            this.TresholdTrackBar.Maximum = 100;
            this.TresholdTrackBar.Minimum = 1;
            this.TresholdTrackBar.Name = "TresholdTrackBar";
            this.TresholdTrackBar.Size = new System.Drawing.Size(268, 45);
            this.TresholdTrackBar.TabIndex = 11;
            this.TresholdTrackBar.Value = 100;
            this.TresholdTrackBar.ValueChanged += new System.EventHandler(this.TresholdTrackBar_ValueChanged);
            this.TresholdTrackBar.MouseLeave += new System.EventHandler(this.TresholdTrackBar_MouseLeave);
            this.TresholdTrackBar.MouseHover += new System.EventHandler(this.TresholdTrackBar_MouseHover);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Sensitivity:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Treshold:";
            // 
            // TresholdLevel
            // 
            this.TresholdLevel.AutoSize = true;
            this.TresholdLevel.Location = new System.Drawing.Point(83, 108);
            this.TresholdLevel.Name = "TresholdLevel";
            this.TresholdLevel.Size = new System.Drawing.Size(25, 13);
            this.TresholdLevel.TabIndex = 14;
            this.TresholdLevel.Text = "100";
            // 
            // SensLevel
            // 
            this.SensLevel.AutoSize = true;
            this.SensLevel.Location = new System.Drawing.Point(83, 62);
            this.SensLevel.Name = "SensLevel";
            this.SensLevel.Size = new System.Drawing.Size(13, 13);
            this.SensLevel.TabIndex = 15;
            this.SensLevel.Text = "1";
            // 
            // PeakVolumeLabel
            // 
            this.PeakVolumeLabel.AutoSize = true;
            this.PeakVolumeLabel.BackColor = System.Drawing.Color.Transparent;
            this.PeakVolumeLabel.Location = new System.Drawing.Point(155, 29);
            this.PeakVolumeLabel.Name = "PeakVolumeLabel";
            this.PeakVolumeLabel.Size = new System.Drawing.Size(13, 13);
            this.PeakVolumeLabel.TabIndex = 16;
            this.PeakVolumeLabel.Text = "0";
            // 
            // DebugTextBox
            // 
            this.DebugTextBox.Location = new System.Drawing.Point(26, 363);
            this.DebugTextBox.Name = "DebugTextBox";
            this.DebugTextBox.Size = new System.Drawing.Size(271, 20);
            this.DebugTextBox.TabIndex = 17;
            this.DebugTextBox.TextChanged += new System.EventHandler(this.DebutTextBox_TextChanged);
            // 
            // StatusBox
            // 
            this.StatusBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.StatusBox.Location = new System.Drawing.Point(32, 166);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Size = new System.Drawing.Size(252, 25);
            this.StatusBox.TabIndex = 18;
            this.StatusBox.TabStop = false;
            // 
            // KeyPressStatusLabel
            // 
            this.KeyPressStatusLabel.AutoSize = true;
            this.KeyPressStatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.KeyPressStatusLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KeyPressStatusLabel.Location = new System.Drawing.Point(92, 172);
            this.KeyPressStatusLabel.Name = "KeyPressStatusLabel";
            this.KeyPressStatusLabel.Size = new System.Drawing.Size(134, 13);
            this.KeyPressStatusLabel.TabIndex = 19;
            this.KeyPressStatusLabel.Text = "Key v is not being pressed!";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 395);
            this.Controls.Add(this.KeyPressStatusLabel);
            this.Controls.Add(this.StatusBox);
            this.Controls.Add(this.DebugTextBox);
            this.Controls.Add(this.PeakVolumeLabel);
            this.Controls.Add(this.SensLevel);
            this.Controls.Add(this.TresholdLevel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TresholdTrackBar);
            this.Controls.Add(this.SensitivityTrackBar);
            this.Controls.Add(this.CurrentKey);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KeyTextBox);
            this.Controls.Add(this.ProgramComboBox);
            this.Controls.Add(this.AudioDeviceComboBox);
            this.Controls.Add(this.PeakVolumeBar);
            this.Name = "Form1";
            this.Text = "PressKeyByVoice";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.SensitivityTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TresholdTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar PeakVolumeBar;
        private System.Windows.Forms.ComboBox AudioDeviceComboBox;
        private System.Windows.Forms.ComboBox ProgramComboBox;
        private System.Windows.Forms.TextBox KeyTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label CurrentKey;
        private System.Windows.Forms.TrackBar SensitivityTrackBar;
        private System.Windows.Forms.TrackBar TresholdTrackBar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label TresholdLevel;
        private System.Windows.Forms.Label SensLevel;
        private System.Windows.Forms.Label PeakVolumeLabel;
        private System.Windows.Forms.TextBox DebugTextBox;
        private System.Windows.Forms.PictureBox StatusBox;
        private System.Windows.Forms.Label KeyPressStatusLabel;
    }
}

