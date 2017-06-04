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
            this.ToggleKeyLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.ToggleKeyInputBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SmoothingCheckbox = new System.Windows.Forms.CheckBox();
            this.TresholdMaxLabel = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.TresholdMaxTrackBar = new System.Windows.Forms.TrackBar();
            this.WaveModeCheckbox = new System.Windows.Forms.CheckBox();
            this.PeakModeCheckbox = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.KeyPressDelayLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.KeyPressDelayTrackBar = new System.Windows.Forms.TrackBar();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ChunksPerSecondTrackBar = new System.Windows.Forms.TrackBar();
            this.TimesPerSecondLabel = new System.Windows.Forms.Label();
            this.BufferLabel = new System.Windows.Forms.Label();
            this.BufferTimesLabel = new System.Windows.Forms.Label();
            this.BufferTrackBar = new System.Windows.Forms.TrackBar();
            this.ChunksPerSecondLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SensitivityTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TresholdTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TresholdMaxTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KeyPressDelayTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChunksPerSecondTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BufferTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // PeakVolumeBar
            // 
            this.PeakVolumeBar.Location = new System.Drawing.Point(26, 72);
            this.PeakVolumeBar.Name = "PeakVolumeBar";
            this.PeakVolumeBar.Size = new System.Drawing.Size(530, 47);
            this.PeakVolumeBar.TabIndex = 0;
            // 
            // AudioDeviceComboBox
            // 
            this.AudioDeviceComboBox.FormattingEnabled = true;
            this.AudioDeviceComboBox.Location = new System.Drawing.Point(574, 40);
            this.AudioDeviceComboBox.Name = "AudioDeviceComboBox";
            this.AudioDeviceComboBox.Size = new System.Drawing.Size(271, 21);
            this.AudioDeviceComboBox.TabIndex = 1;
            this.AudioDeviceComboBox.Text = "Select your audio device here!";
            this.AudioDeviceComboBox.SelectedIndexChanged += new System.EventHandler(this.AudioDeviceComboBox_SelectedIndexChanged);
            // 
            // ProgramComboBox
            // 
            this.ProgramComboBox.FormattingEnabled = true;
            this.ProgramComboBox.Location = new System.Drawing.Point(574, 81);
            this.ProgramComboBox.Name = "ProgramComboBox";
            this.ProgramComboBox.Size = new System.Drawing.Size(271, 21);
            this.ProgramComboBox.TabIndex = 2;
            this.ProgramComboBox.Text = "Select the program that will receive the keypress!";
            this.ProgramComboBox.SelectedIndexChanged += new System.EventHandler(this.ProgramComboBox_SelectedIndexChanged);
            this.ProgramComboBox.Click += new System.EventHandler(this.ProgramComboBox_Click);
            // 
            // KeyTextBox
            // 
            this.KeyTextBox.Location = new System.Drawing.Point(574, 190);
            this.KeyTextBox.Name = "KeyTextBox";
            this.KeyTextBox.Size = new System.Drawing.Size(142, 20);
            this.KeyTextBox.TabIndex = 3;
            this.KeyTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyTextBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(574, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Audio Device:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(574, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Program";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(574, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(233, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Key To Be Pressed (only lowercase characters):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(731, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Current Key:";
            // 
            // CurrentKey
            // 
            this.CurrentKey.AutoSize = true;
            this.CurrentKey.Location = new System.Drawing.Point(802, 193);
            this.CurrentKey.Name = "CurrentKey";
            this.CurrentKey.Size = new System.Drawing.Size(13, 13);
            this.CurrentKey.TabIndex = 9;
            this.CurrentKey.Text = "v";
            // 
            // SensitivityTrackBar
            // 
            this.SensitivityTrackBar.Location = new System.Drawing.Point(23, 181);
            this.SensitivityTrackBar.Maximum = 100;
            this.SensitivityTrackBar.Minimum = 1;
            this.SensitivityTrackBar.Name = "SensitivityTrackBar";
            this.SensitivityTrackBar.Size = new System.Drawing.Size(533, 45);
            this.SensitivityTrackBar.TabIndex = 10;
            this.SensitivityTrackBar.Value = 1;
            this.SensitivityTrackBar.ValueChanged += new System.EventHandler(this.SensitivityTrackBar_ValueChanged);
            // 
            // TresholdTrackBar
            // 
            this.TresholdTrackBar.BackColor = System.Drawing.SystemColors.Control;
            this.TresholdTrackBar.Location = new System.Drawing.Point(23, 227);
            this.TresholdTrackBar.Maximum = 100;
            this.TresholdTrackBar.Minimum = 1;
            this.TresholdTrackBar.Name = "TresholdTrackBar";
            this.TresholdTrackBar.Size = new System.Drawing.Size(533, 45);
            this.TresholdTrackBar.TabIndex = 11;
            this.TresholdTrackBar.Value = 100;
            this.TresholdTrackBar.ValueChanged += new System.EventHandler(this.TresholdTrackBar_ValueChanged);
            this.TresholdTrackBar.MouseLeave += new System.EventHandler(this.TresholdTrackBar_MouseLeave);
            this.TresholdTrackBar.MouseHover += new System.EventHandler(this.TresholdTrackBar_MouseHover);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Sensitivity:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 211);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Treshold - Min:";
            // 
            // TresholdLevel
            // 
            this.TresholdLevel.AutoSize = true;
            this.TresholdLevel.Location = new System.Drawing.Point(103, 211);
            this.TresholdLevel.Name = "TresholdLevel";
            this.TresholdLevel.Size = new System.Drawing.Size(25, 13);
            this.TresholdLevel.TabIndex = 14;
            this.TresholdLevel.Text = "100";
            // 
            // SensLevel
            // 
            this.SensLevel.AutoSize = true;
            this.SensLevel.Location = new System.Drawing.Point(80, 165);
            this.SensLevel.Name = "SensLevel";
            this.SensLevel.Size = new System.Drawing.Size(13, 13);
            this.SensLevel.TabIndex = 15;
            this.SensLevel.Text = "1";
            // 
            // PeakVolumeLabel
            // 
            this.PeakVolumeLabel.AutoSize = true;
            this.PeakVolumeLabel.BackColor = System.Drawing.Color.Transparent;
            this.PeakVolumeLabel.Location = new System.Drawing.Point(287, 89);
            this.PeakVolumeLabel.Name = "PeakVolumeLabel";
            this.PeakVolumeLabel.Size = new System.Drawing.Size(13, 13);
            this.PeakVolumeLabel.TabIndex = 16;
            this.PeakVolumeLabel.Text = "0";
            // 
            // DebugTextBox
            // 
            this.DebugTextBox.Location = new System.Drawing.Point(574, 284);
            this.DebugTextBox.Name = "DebugTextBox";
            this.DebugTextBox.Size = new System.Drawing.Size(271, 20);
            this.DebugTextBox.TabIndex = 17;
            // 
            // StatusBox
            // 
            this.StatusBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.StatusBox.Location = new System.Drawing.Point(26, 125);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Size = new System.Drawing.Size(530, 37);
            this.StatusBox.TabIndex = 18;
            this.StatusBox.TabStop = false;
            // 
            // KeyPressStatusLabel
            // 
            this.KeyPressStatusLabel.AutoSize = true;
            this.KeyPressStatusLabel.BackColor = System.Drawing.SystemColors.Window;
            this.KeyPressStatusLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KeyPressStatusLabel.Location = new System.Drawing.Point(136, 137);
            this.KeyPressStatusLabel.Name = "KeyPressStatusLabel";
            this.KeyPressStatusLabel.Size = new System.Drawing.Size(134, 13);
            this.KeyPressStatusLabel.TabIndex = 19;
            this.KeyPressStatusLabel.Text = "Key v is not being pressed!";
            // 
            // ToggleKeyLabel
            // 
            this.ToggleKeyLabel.AutoSize = true;
            this.ToggleKeyLabel.Location = new System.Drawing.Point(802, 236);
            this.ToggleKeyLabel.Name = "ToggleKeyLabel";
            this.ToggleKeyLabel.Size = new System.Drawing.Size(19, 13);
            this.ToggleKeyLabel.TabIndex = 23;
            this.ToggleKeyLabel.Text = "F5";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(731, 236);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Current Key:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(574, 217);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(135, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Disable/Enable toggle key:";
            // 
            // ToggleKeyInputBox
            // 
            this.ToggleKeyInputBox.Location = new System.Drawing.Point(574, 233);
            this.ToggleKeyInputBox.Name = "ToggleKeyInputBox";
            this.ToggleKeyInputBox.Size = new System.Drawing.Size(142, 20);
            this.ToggleKeyInputBox.TabIndex = 20;
            this.ToggleKeyInputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ToggleKeyInputBox_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(571, 268);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Debug textbox, ignore this...";
            // 
            // SmoothingCheckbox
            // 
            this.SmoothingCheckbox.AutoSize = true;
            this.SmoothingCheckbox.Location = new System.Drawing.Point(26, 383);
            this.SmoothingCheckbox.Name = "SmoothingCheckbox";
            this.SmoothingCheckbox.Size = new System.Drawing.Size(244, 17);
            this.SmoothingCheckbox.TabIndex = 28;
            this.SmoothingCheckbox.Text = "Smoothing (to reduce false postive activation).";
            this.SmoothingCheckbox.UseVisualStyleBackColor = true;
            this.SmoothingCheckbox.CheckedChanged += new System.EventHandler(this.SmoothingCheckbox_CheckedChanged);
            this.SmoothingCheckbox.MouseLeave += new System.EventHandler(this.SmoothingCheckbox_MouseLeave);
            this.SmoothingCheckbox.MouseHover += new System.EventHandler(this.SmoothingCheckbox_MouseHover);
            // 
            // TresholdMaxLabel
            // 
            this.TresholdMaxLabel.AutoSize = true;
            this.TresholdMaxLabel.Location = new System.Drawing.Point(103, 259);
            this.TresholdMaxLabel.Name = "TresholdMaxLabel";
            this.TresholdMaxLabel.Size = new System.Drawing.Size(25, 13);
            this.TresholdMaxLabel.TabIndex = 34;
            this.TresholdMaxLabel.Text = "100";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(23, 259);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 13);
            this.label14.TabIndex = 33;
            this.label14.Text = "Treshold - Max:";
            // 
            // TresholdMaxTrackBar
            // 
            this.TresholdMaxTrackBar.BackColor = System.Drawing.SystemColors.Control;
            this.TresholdMaxTrackBar.Location = new System.Drawing.Point(23, 275);
            this.TresholdMaxTrackBar.Maximum = 100;
            this.TresholdMaxTrackBar.Minimum = 1;
            this.TresholdMaxTrackBar.Name = "TresholdMaxTrackBar";
            this.TresholdMaxTrackBar.Size = new System.Drawing.Size(533, 45);
            this.TresholdMaxTrackBar.TabIndex = 32;
            this.TresholdMaxTrackBar.Value = 100;
            this.TresholdMaxTrackBar.ValueChanged += new System.EventHandler(this.TresholdMaxTrackBar_ValueChanged);
            this.TresholdMaxTrackBar.MouseLeave += new System.EventHandler(this.TresholdMaxTrackBar_MouseLeave);
            this.TresholdMaxTrackBar.MouseHover += new System.EventHandler(this.TresholdMaxTrackBar_MouseHover);
            // 
            // WaveModeCheckbox
            // 
            this.WaveModeCheckbox.AutoSize = true;
            this.WaveModeCheckbox.Location = new System.Drawing.Point(26, 40);
            this.WaveModeCheckbox.Name = "WaveModeCheckbox";
            this.WaveModeCheckbox.Size = new System.Drawing.Size(85, 17);
            this.WaveModeCheckbox.TabIndex = 35;
            this.WaveModeCheckbox.Text = "Wave Mode";
            this.WaveModeCheckbox.UseVisualStyleBackColor = true;
            this.WaveModeCheckbox.Click += new System.EventHandler(this.WaveModeCheckbox_Click);
            this.WaveModeCheckbox.MouseLeave += new System.EventHandler(this.WaveModeCheckbox_MouseLeave);
            this.WaveModeCheckbox.MouseHover += new System.EventHandler(this.WaveModeCheckbox_MouseHover);
            // 
            // PeakModeCheckbox
            // 
            this.PeakModeCheckbox.AutoSize = true;
            this.PeakModeCheckbox.Location = new System.Drawing.Point(165, 40);
            this.PeakModeCheckbox.Name = "PeakModeCheckbox";
            this.PeakModeCheckbox.Size = new System.Drawing.Size(135, 17);
            this.PeakModeCheckbox.TabIndex = 36;
            this.PeakModeCheckbox.Text = "Peak Mode (Old mode)";
            this.PeakModeCheckbox.UseVisualStyleBackColor = true;
            this.PeakModeCheckbox.Click += new System.EventHandler(this.PeakModeCheckbox_Click);
            this.PeakModeCheckbox.MouseLeave += new System.EventHandler(this.PeakModeCheckbox_MouseLeave);
            this.PeakModeCheckbox.MouseHover += new System.EventHandler(this.PeakModeCheckbox_MouseHover);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(129, 13);
            this.label10.TabIndex = 37;
            this.label10.Text = "Select Listening Methods:";
            // 
            // KeyPressDelayLabel
            // 
            this.KeyPressDelayLabel.AutoSize = true;
            this.KeyPressDelayLabel.Location = new System.Drawing.Point(677, 110);
            this.KeyPressDelayLabel.Name = "KeyPressDelayLabel";
            this.KeyPressDelayLabel.Size = new System.Drawing.Size(31, 13);
            this.KeyPressDelayLabel.TabIndex = 40;
            this.KeyPressDelayLabel.Text = "1000";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(574, 110);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 13);
            this.label12.TabIndex = 39;
            this.label12.Text = "Key Press Delay";
            // 
            // KeyPressDelayTrackBar
            // 
            this.KeyPressDelayTrackBar.BackColor = System.Drawing.SystemColors.Control;
            this.KeyPressDelayTrackBar.LargeChange = 500;
            this.KeyPressDelayTrackBar.Location = new System.Drawing.Point(574, 126);
            this.KeyPressDelayTrackBar.Maximum = 5000;
            this.KeyPressDelayTrackBar.Minimum = 100;
            this.KeyPressDelayTrackBar.Name = "KeyPressDelayTrackBar";
            this.KeyPressDelayTrackBar.Size = new System.Drawing.Size(268, 45);
            this.KeyPressDelayTrackBar.SmallChange = 100;
            this.KeyPressDelayTrackBar.TabIndex = 500;
            this.KeyPressDelayTrackBar.Value = 1000;
            this.KeyPressDelayTrackBar.ValueChanged += new System.EventHandler(this.KeyPressDelayTrackBar_ValueChanged);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(573, 320);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(272, 23);
            this.SaveButton.TabIndex = 501;
            this.SaveButton.Text = "Save Current Settings";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ChunksPerSecondTrackBar
            // 
            this.ChunksPerSecondTrackBar.BackColor = System.Drawing.SystemColors.Control;
            this.ChunksPerSecondTrackBar.Location = new System.Drawing.Point(23, 332);
            this.ChunksPerSecondTrackBar.Maximum = 1000;
            this.ChunksPerSecondTrackBar.Minimum = 1;
            this.ChunksPerSecondTrackBar.Name = "ChunksPerSecondTrackBar";
            this.ChunksPerSecondTrackBar.Size = new System.Drawing.Size(268, 45);
            this.ChunksPerSecondTrackBar.TabIndex = 25;
            this.ChunksPerSecondTrackBar.Value = 100;
            this.ChunksPerSecondTrackBar.ValueChanged += new System.EventHandler(this.ChunksPerSecondTrackBar_ValueChanged);
            // 
            // TimesPerSecondLabel
            // 
            this.TimesPerSecondLabel.AutoSize = true;
            this.TimesPerSecondLabel.Location = new System.Drawing.Point(23, 316);
            this.TimesPerSecondLabel.Name = "TimesPerSecondLabel";
            this.TimesPerSecondLabel.Size = new System.Drawing.Size(97, 13);
            this.TimesPerSecondLabel.TabIndex = 26;
            this.TimesPerSecondLabel.Text = "Times Per Second:";
            // 
            // BufferLabel
            // 
            this.BufferLabel.AutoSize = true;
            this.BufferLabel.Location = new System.Drawing.Point(212, 307);
            this.BufferLabel.Name = "BufferLabel";
            this.BufferLabel.Size = new System.Drawing.Size(13, 13);
            this.BufferLabel.TabIndex = 504;
            this.BufferLabel.Text = "1";
            // 
            // BufferTimesLabel
            // 
            this.BufferTimesLabel.AutoSize = true;
            this.BufferTimesLabel.Location = new System.Drawing.Point(23, 316);
            this.BufferTimesLabel.Name = "BufferTimesLabel";
            this.BufferTimesLabel.Size = new System.Drawing.Size(62, 13);
            this.BufferTimesLabel.TabIndex = 503;
            this.BufferTimesLabel.Text = "Buffer times";
            // 
            // BufferTrackBar
            // 
            this.BufferTrackBar.BackColor = System.Drawing.SystemColors.Control;
            this.BufferTrackBar.Location = new System.Drawing.Point(23, 332);
            this.BufferTrackBar.Minimum = 1;
            this.BufferTrackBar.Name = "BufferTrackBar";
            this.BufferTrackBar.Size = new System.Drawing.Size(533, 45);
            this.BufferTrackBar.TabIndex = 502;
            this.BufferTrackBar.Value = 1;
            this.BufferTrackBar.ValueChanged += new System.EventHandler(this.BufferTrackBar_ValueChanged);
            // 
            // ChunksPerSecondLabel
            // 
            this.ChunksPerSecondLabel.AutoSize = true;
            this.ChunksPerSecondLabel.Location = new System.Drawing.Point(227, 307);
            this.ChunksPerSecondLabel.Name = "ChunksPerSecondLabel";
            this.ChunksPerSecondLabel.Size = new System.Drawing.Size(25, 13);
            this.ChunksPerSecondLabel.TabIndex = 27;
            this.ChunksPerSecondLabel.Text = "100";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 402);
            this.Controls.Add(this.BufferLabel);
            this.Controls.Add(this.BufferTimesLabel);
            this.Controls.Add(this.BufferTrackBar);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.SmoothingCheckbox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.PeakModeCheckbox);
            this.Controls.Add(this.WaveModeCheckbox);
            this.Controls.Add(this.TresholdMaxLabel);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.ChunksPerSecondLabel);
            this.Controls.Add(this.TimesPerSecondLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ToggleKeyLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ToggleKeyInputBox);
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
            this.Controls.Add(this.TresholdMaxTrackBar);
            this.Controls.Add(this.KeyPressDelayLabel);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.KeyPressDelayTrackBar);
            this.Controls.Add(this.ChunksPerSecondTrackBar);
            this.Name = "Form1";
            this.Text = "PressKeyByVoice";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.SensitivityTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TresholdTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TresholdMaxTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KeyPressDelayTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChunksPerSecondTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BufferTrackBar)).EndInit();
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
        private System.Windows.Forms.Label ToggleKeyLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox ToggleKeyInputBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox SmoothingCheckbox;
        private System.Windows.Forms.Label TresholdMaxLabel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TrackBar TresholdMaxTrackBar;
        private System.Windows.Forms.CheckBox WaveModeCheckbox;
        private System.Windows.Forms.CheckBox PeakModeCheckbox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label KeyPressDelayLabel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TrackBar KeyPressDelayTrackBar;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TrackBar ChunksPerSecondTrackBar;
        private System.Windows.Forms.Label TimesPerSecondLabel;
        private System.Windows.Forms.Label BufferLabel;
        private System.Windows.Forms.Label BufferTimesLabel;
        private System.Windows.Forms.TrackBar BufferTrackBar;
        private System.Windows.Forms.Label ChunksPerSecondLabel;
    }
}

