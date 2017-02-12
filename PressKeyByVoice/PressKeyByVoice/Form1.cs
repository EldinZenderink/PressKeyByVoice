using System;
using System.Drawing;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using WindowsInput;
using WindowsInput.Native;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Linq;
using NAudio.Wave;

namespace PressKeyByVoice
{
    public partial class Form1 : Form
    {
        //all the public/shared values
        public Process selectedProcess;
        public Thread audioListener = null;
        public Thread keyReleaser = null;
        public int sensitivity = 1;
        public int treshold = 100;
        public int maxTreshold = 100;
        public int recordSpeed = 100;
        public MMDevice device;
        public WaveInEvent sourceStream = new WaveInEvent();
        public bool IsHovering = false;
        public VirtualKeyCode key = VirtualKeyCode.VK_V;
        public bool notstop = true;
        public char keyToBePressed = (char)86;
        public int DisableEnableKey = 116;
        public bool DisableEnable = false;
        public int[] captureThemAll = new int[100];
        public int gottaKnowWhenToStop = 0;
        public bool smoothing = false;
        public int keyPressDuration = 1000;
        public bool waveMode = true;
        public bool peakMode = false;
        public int waveDeviceId = 0;
        public int peakDeviceId = 0;
        public InputSimulator sim = new InputSimulator();
        public SettingsData data = null;
        public bool keyReleaserRunning = false;
        public int keepingTrack = 0;
        public int globalCounter = 0;
        public int globalBuffer = 1;

        //all the extern methods imported

        //Get the process handle running in the foreground
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        //Get the process id through the handle
        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        //Set/Unset hotkey for disabling / enabeling the listener
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public Form1()
        {
            InitializeComponent();
            data = new SettingsData();
            sensitivity = data.sensitivity;
            treshold = data.treshold;
            maxTreshold = data.maxTreshold;
            recordSpeed = data.recordSpeed;
            DisableEnableKey = data.DisableEnableKey; 
            keyPressDuration = data.keyPressDuration;
            waveDeviceId = data.waveDeviceId;
            peakDeviceId = data.peakDeviceId;
            keyToBePressed = data.keyToBePressed;
            smoothing = data.smoothing;
            waveMode = data.waveMode;
            peakMode = data.peakMode;
            globalBuffer = data.globalBuffer;

            int val = keyToBePressed;
            foreach (VirtualKeyCode vk in Enum.GetValues(typeof(VirtualKeyCode)))
            {
                int x = (int)vk;
                if (x == val)
                {
                    key = vk;
                }
            }
            SensLevel.Text = sensitivity.ToString();
            SensitivityTrackBar.Value = sensitivity;
            TresholdLevel.Text = treshold.ToString();
            TresholdTrackBar.Value = treshold;
            TresholdMaxLabel.Text = maxTreshold.ToString();
            TresholdMaxTrackBar.Value = maxTreshold;
            ChunksPerSecondLabel.Text = recordSpeed.ToString();
            ChunksPerSecondTrackBar.Value = recordSpeed;
            KeyPressDelayLabel.Text = keyPressDuration.ToString();
            KeyPressDelayTrackBar.Value = keyPressDuration;
            SmoothingCheckbox.Checked = smoothing;
            BufferLabel.Text = globalBuffer.ToString();
            BufferTrackBar.Value = globalBuffer;
            CurrentKey.Text = char.ToLower(keyToBePressed).ToString();
            KeyTextBox.Text = char.ToLower(keyToBePressed).ToString();
            ToggleKeyLabel.Text = ((VirtualKeyCode)DisableEnableKey).ToString();
            ToggleKeyInputBox.Text = ((VirtualKeyCode)DisableEnableKey).ToString();
            if (waveMode)
            {
                int waveInDevices = WaveIn.DeviceCount;
                for (int waveInDevice = 0; waveInDevice < waveInDevices; waveInDevice++)
                {
                    WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(waveInDevice);
                    AudioDeviceComboBox.Items.Add(deviceInfo.ProductName);
                }
                AudioDeviceComboBox.SelectedIndex = waveDeviceId;
                sourceStream = new WaveInEvent();
                sourceStream.DeviceNumber = waveDeviceId;
                sourceStream.WaveFormat = new WaveFormat(44100, WaveIn.GetCapabilities(waveDeviceId).Channels);
                sourceStream.DataAvailable += new EventHandler<WaveInEventArgs>(sourceStream_DataAvailable);
                sourceStream.StartRecording();
            } else if (peakMode)
            {
                MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
                var devices = enumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
                for (int i = 0; i < devices.Count; i++)
                {
                    AudioDeviceComboBox.Items.Add(devices[i].ToString());
                }
                AudioDeviceComboBox.SelectedIndex = peakDeviceId;

                //set default defice (first)
                device = (MMDevice)devices[peakDeviceId];
            }

            WaveModeCheckbox.Checked = waveMode;
            PeakModeCheckbox.Checked = peakMode;

            if (waveMode)
            {
                ChunksPerSecondTrackBar.Enabled = false;
                ChunksPerSecondTrackBar.Visible = false;
                ChunksPerSecondLabel.Visible = false;
                TimesPerSecondLabel.Visible = false;
                SmoothingCheckbox.Enabled = false;
                SmoothingCheckbox.Visible = false;
            } else
            {
                BufferLabel.Visible = false;
                BufferTrackBar.Visible = false;
                BufferTimesLabel.Visible = false;
                BufferTrackBar.Enabled = false;
            }
            

            //Get all process currently running:
            Process[] processlist = Process.GetProcesses();
            for (int i = 0; i < processlist.Length; i++)
            {
                ProgramComboBox.Items.Add(processlist[i].ProcessName);
            }
            ProgramComboBox.Sorted = true;

            //Set the default key for enabeling / disabeling the program
            RegisterHotKey(this.Handle, DisableEnableKey, 0, (int)Keys.F5);
        }

        
        /// <summary>
        /// Method for selecting Audio device
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AudioDeviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            int index = comboBox.SelectedIndex;
            if (peakMode)
            {
                MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
                var devices = enumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
                device = (MMDevice)devices[index];

                //Run the listener thread

                if(audioListener != null && audioListener.IsAlive)
                {
                    audioListener.Abort();
                }
                audioListener = new Thread(() => AudioListener());
                audioListener.Start();
            } else if (waveMode)
            {
                if (audioListener != null && audioListener.IsAlive)
                {
                    audioListener.Abort();
                }
                if(sourceStream != null)
                {
                    try
                    {
                        sourceStream.StopRecording();
                    }
                    catch
                    {
                    }
                }
                waveDeviceId = index;
                sourceStream = new WaveInEvent();
                sourceStream.DeviceNumber = waveDeviceId;
                sourceStream.WaveFormat = new WaveFormat(48000, WaveIn.GetCapabilities(index).Channels);
                sourceStream.DataAvailable += new EventHandler<WaveInEventArgs>(sourceStream_DataAvailable);
                sourceStream.StartRecording();
                DebugTextBox.Text = "Selected device: " + index;
            }
        }

        private void sourceStream_DataAvailable(object sender, WaveInEventArgs e)
        {
           
            if (!keyReleaserRunning)
            {
                float[] amplitudes = new float[e.BytesRecorded];
                byte[] buffer = e.Buffer;

                int counter = 0;
                for (int index = 0; index < e.BytesRecorded; index += 2)
                {
                    short sample = (short)((buffer[index + 1] << 8) |
                                            buffer[index + 0]);
                    float sample32 = sample / 32768f;
                    amplitudes[counter] = sample32;
                    counter++;
                }
                //int volume = (int)(((((bytesAsInts.Average() / 10) - 10) * 100) - 200) / (1000 / sensitivity));
                try
                {
                    int volume = (int)(amplitudes.Max() * 10 * sensitivity);
                    keepingTrack = keepingTrack + volume;
                    if(globalCounter > (globalBuffer - 1))
                    {
                        volume = keepingTrack / globalBuffer;
                        if (volume > treshold && volume < maxTreshold)
                        {
                            if (selectedProcess != null)
                            {
                                if (selectedProcess.Id == GetActiveProcessID())
                                {
                                    /* keyReleaser = new Thread(new ThreadStart(() => KeyReleaser()));
                                     try { keyReleaser.Start(); } catch { };*/
                                    DateTime _desired = DateTime.Now.AddMilliseconds(keyPressDuration);
                                    while (DateTime.Now < _desired)
                                    {
                                        sim.Keyboard.KeyDown(key);
                                        Thread.Sleep(1);
                                    }
                                    UpdateStatusBoxColor(Color.Red);
                                }
                            }
                        }
                        UpdatePeakVolumeBar(volume);
                        keepingTrack = 0;
                        globalCounter = 0;
                    }
                   
                    globalCounter++;
                } catch(Exception ex)
                {
                    UpdateDebugText("Couldnt update volume bar:" + ex.ToString());
                }
            }
        }

        private void UpdateKeyPressStatusText(string text)
        {
            if (KeyPressStatusLabel.InvokeRequired)
            {
                KeyPressStatusLabel.BeginInvoke(new MethodInvoker(() => UpdateKeyPressStatusText(text)));
            } else
            {
                KeyPressStatusLabel.Text = text;
            }
        }

        private void UpdateStatusBoxColor(Color color)
        {
            if (StatusBox.InvokeRequired)
            {
                StatusBox.BeginInvoke(new MethodInvoker(() => UpdateStatusBoxColor(color)));
            } else
            {
                StatusBox.BackColor = color;
            }
        }

        /// <summary>
        /// The function that actually performs the comparison on  peak volume received from the audio device, this runs on the main thread due to NAudio having issues with running within non initiated threads at the start
        /// </summary>
        private void UpdatePeakVolumeBar(int volume)
        {            
            if (PeakVolumeBar.InvokeRequired)
            {
                PeakVolumeBar.BeginInvoke(new MethodInvoker(() => UpdatePeakVolumeBar(volume)));
            }
            else
            {
                if (!IsHovering)
                {
                    if (peakMode)
                    {
                        int audioVolume = (int)(Math.Round(((device.AudioMeterInformation.MasterPeakValue * 100) * sensitivity)));

                        if (gottaKnowWhenToStop >= (int)Math.Round((double)(100 / recordSpeed)) && smoothing)
                        {
                            int sum = captureThemAll.Sum();
                            int avg = sum / (100 / recordSpeed);
                            captureThemAll = new int[100];
                            gottaKnowWhenToStop = 0;
                            UpdateDebugText("treshold: " + treshold + ", avg: " + avg);
                            if (avg > treshold && avg < maxTreshold)
                            {
                                UpdateDebugText("you should activate :X");
                                if (selectedProcess != null)
                                {
                                    if (selectedProcess.Id == GetActiveProcessID())
                                    {

                                        if (keyReleaser == null || !keyReleaser.IsAlive)
                                        {
                                            keyReleaser = new Thread(new ThreadStart(() => KeyReleaser()));
                                            keyReleaser.Start();
                                        }
                                        KeyPressStatusLabel.Text = "Key " + keyToBePressed.ToString() + " is currently being pressed!";
                                    }
                                }
                                StatusBox.BackColor = Color.Green;
                            }
                            else
                            {
                                KeyPressStatusLabel.Text = "Key " + keyToBePressed.ToString() + " is not being pressed!";
                                StatusBox.BackColor = Color.Red;
                            }

                            if (avg < 101)
                            {
                                PeakVolumeLabel.Text = avg.ToString();
                                PeakVolumeBar.Value = avg;
                            }
                            else
                            {
                                PeakVolumeLabel.Text = avg.ToString();
                                PeakVolumeBar.Value = 100;
                            }

                        }
                        else if (smoothing)
                        {
                            captureThemAll[gottaKnowWhenToStop] = audioVolume;
                            gottaKnowWhenToStop++;
                        }
                        else
                        {
                            if (audioVolume > treshold && audioVolume < maxTreshold)
                            {
                                if (selectedProcess != null)
                                {
                                    if (selectedProcess.Id == GetActiveProcessID())
                                    {
                                        Thread keyReleaser = new Thread(new ThreadStart(() => KeyReleaser()));
                                        keyReleaser.Start();
                                        KeyPressStatusLabel.Text = "Key " + keyToBePressed.ToString() + " is currently being pressed!";
                                    }
                                }
                                StatusBox.BackColor = Color.Green;
                            }
                            else
                            {
                                KeyPressStatusLabel.Text = "Key " + keyToBePressed.ToString() + " is not being pressed!";
                                StatusBox.BackColor = Color.Red;
                                sim.Keyboard.KeyUp(key);
                            }

                            if (audioVolume < 101)
                            {
                                PeakVolumeLabel.Text = audioVolume.ToString();
                                PeakVolumeBar.Value = audioVolume;
                            }
                            else
                            {
                                PeakVolumeLabel.Text = audioVolume.ToString();
                                PeakVolumeBar.Value = 100;
                            }
                        }
                    } else if (waveMode)
                    {
                        if(volume < 101 && volume > -1) {

                            PeakVolumeLabel.Text = volume.ToString();
                            PeakVolumeBar.Value = volume;
                        } else if(volume < 1)
                        {
                            PeakVolumeLabel.Text = volume.ToString();
                            PeakVolumeBar.Value = 0;
                        } else 
                        {

                            PeakVolumeLabel.Text = volume.ToString();
                            PeakVolumeBar.Value = 100;
                        }
                    }              
                }
            }
        }

        /// <summary>
        /// Invoker method for setting debug text in the debug text box from a thread
        /// </summary>
        /// <param name="text"></param>
        private void UpdateDebugText(string text)
        {
            if (DebugTextBox.InvokeRequired)
            {
                try
                {
                    DebugTextBox.BeginInvoke(new MethodInvoker(() => UpdateDebugText(text)));
                } catch
                {
                    //Well.. cant really do much here, if the invokers fails :(.
                }
            } else
            {
                DebugTextBox.Text = text;
            }
        }


        /// <summary>
        /// Simple one time exectuting method to be runned in a seperate thread, keeping the keypress alive for x amount of time
        /// </summary>
        private void KeyReleaser()
        {
            keyReleaserRunning = true;
            UpdateStatusBoxColor(Color.Green);
            DateTime _desired = DateTime.Now.AddMilliseconds(keyPressDuration);
            while (DateTime.Now < _desired)
            {
                sim.Keyboard.KeyDown(key);
                Thread.Sleep(10);
            }
            UpdateStatusBoxColor(Color.Red);
            keyReleaserRunning = false;
        }

        
        /// <summary>
        /// While loop to constantly receive the peakvolume and performing actions upon it.
        /// </summary>
        private void AudioListener()
        {
            while (notstop)
            {               
                if (!DisableEnable)
                {
                    try
                    {
                        UpdatePeakVolumeBar(0);
                    } catch (Exception e)
                    {
                        UpdateDebugText("Issues with audio device: " + e.ToString());
                    }
                }
                
                Thread.Sleep(recordSpeed);
            }
        }

        /// <summary>
        /// Trackbar to set the sensitivity, value used in UpdatePeakVolumeBar method to multiply the peakvolume output
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SensitivityTrackBar_ValueChanged(object sender, EventArgs e)
        {
            sensitivity = SensitivityTrackBar.Value;
            SensLevel.Text = sensitivity.ToString();
        }

        /// <summary>
        /// Trackbar to set the minimum treshold value, value use din UpdatePeakVolumeBar method to execute the key press when the peakvolume, multiplied by the sensitivity exceeds the treshold value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TresholdTrackBar_ValueChanged(object sender, EventArgs e)
        {
            treshold = TresholdTrackBar.Value;
            TresholdLevel.Text = treshold.ToString();
            PeakVolumeBar.Value = treshold;
            PeakVolumeLabel.Text = "Min Treshold: " + treshold.ToString();
        }


        /// <summary>
        /// Mouse hover event for the treshold trackbar, when the mouse hangs over the trackbar, the value of the trackbar is shown on the ProgressBar (peakvolume) to get a visual representation of the treshold.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TresholdTrackBar_MouseHover(object sender, EventArgs e)
        {
            ModifyProgressBarColor.SetState(PeakVolumeBar, 3);
            PeakVolumeBar.Value = treshold;
            IsHovering = true;
        }

        /// <summary>
        /// Mouse hover event for the treshold trackbar, when the mouse leaves the trackbar, the peakvolume progressbar should show its normal value again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TresholdTrackBar_MouseLeave(object sender, EventArgs e)
        {
            ModifyProgressBarColor.SetState(PeakVolumeBar, 1);
            IsHovering = false;
        }


        /// <summary>
        /// Receives the process id from the progress in foreground.
        /// </summary>
        /// <returns></returns>
        private uint GetActiveProcessID()
        {
            uint pID;
            IntPtr handle = GetForegroundWindow();
            GetWindowThreadProcessId(handle, out pID);
            return pID;
        }

        /// <summary>
        /// Sets the process where the key press should be send to, used in UpdatePeakVolumeBar to compare to the retreived Active Process id.
        /// </summary>
        /// <returns></returns>
        private void ProgramComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string processName = comboBox.SelectedItem.ToString();
            selectedProcess = Process.GetProcessesByName(processName)[0];
        }


        /// <summary>
        /// Sets the key to be send to the process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            int val = keyToBePressed;
            if (!char.IsUpper(e.KeyChar))
            {
                CurrentKey.Text = e.KeyChar.ToString();
                keyToBePressed = e.KeyChar;
                val = (int)(char.ToUpper(e.KeyChar));
            } else
            {
                CurrentKey.Text = char.ToLower(e.KeyChar).ToString();
                keyToBePressed = char.ToLower(e.KeyChar);
                val = (int)e.KeyChar;
            }
            DebugTextBox.Text = val.ToString();
            foreach (VirtualKeyCode vk in Enum.GetValues(typeof(VirtualKeyCode)))
            {
                int x = (int)vk;
                if (x == val)
                {
                    key = vk;
                    DebugTextBox.Text = e.KeyChar.ToString();
                }
            }
        }

        /// <summary>
        /// Stop the thread that listens to audio device, to ensure there are no running threads when the form closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (peakMode)
            {
                if (audioListener != null && audioListener.IsAlive)
                {
                    audioListener.Abort();
                }
            }
            notstop = true;
        }

        /// <summary>
        /// Another "just to be sure" method to close the thread listening to the audio device... if running.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

                if (audioListener != null && audioListener.IsAlive)
                {
                    audioListener.Abort();
                }
            } catch
            {
                notstop = true;
            }
        }

        /// <summary>
        /// Receives current processes running when you click on the combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgramComboBox_Click(object sender, EventArgs e)
        {
            ProgramComboBox.Items.Clear();
            Process[] processlist = Process.GetProcesses();
            for (int i = 0; i < processlist.Length; i++)
            {
                ProgramComboBox.Items.Add(processlist[i].ProcessName);
            }
        }


        /// <summary>
        /// Listens to the keyboard input stream, used to disable or enable the listning part of this program, even when focus lies on another process.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                if(m.WParam.ToInt32() == DisableEnableKey)
                {

                    DebugTextBox.Text = "Enabled/Disabled pressed, key int: " + DisableEnableKey.ToString();

                    if (!DisableEnable)
                    {
                        DisableEnable = true;
                        StatusBox.BackColor = Color.Black;
                        KeyPressStatusLabel.Text = "DISABLED";
                    } else
                    {
                        DisableEnable = false;
                        StatusBox.BackColor = Color.Red;
                        KeyPressStatusLabel.Text = "ENABLED";
                    }
                } else
                {
                    DebugTextBox.Text = "A key was pressed, but not the one you want: " + DisableEnableKey.ToString() + " instead: " + m.WParam.ToInt32().ToString() + " was pressed";
                }
            }
            base.WndProc(ref m);
        }


        /// <summary>
        /// Set the key that enables/disables the listening part of this program. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleKeyInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            ToggleKeyInputBox.Text = e.KeyCode.ToString();
            ToggleKeyLabel.Text = e.KeyCode.ToString();
            DebugTextBox.Text = "Key pressed: " + e.KeyCode.ToString();
            UnregisterHotKey(this.Handle, DisableEnableKey);
            DisableEnableKey = (int)e.KeyCode;
            RegisterHotKey(this.Handle, DisableEnableKey, 0, DisableEnableKey);           
        }

        private void ChunksPerSecondTrackBar_ValueChanged(object sender, EventArgs e)
        {            
            recordSpeed = (int)Math.Round((double)(1000 / ChunksPerSecondTrackBar.Value));
            ChunksPerSecondLabel.Text = ChunksPerSecondTrackBar.Value.ToString();
        }


        private void BufferTrackBar_ValueChanged(object sender, EventArgs e)
        {
            globalBuffer = BufferTrackBar.Value;
            BufferLabel.Text = globalBuffer.ToString();
        }

        private void SmoothingCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (SmoothingCheckbox.Checked)
            {
                if(ChunksPerSecondTrackBar.Value < 100)
                {
                    ChunksPerSecondTrackBar.Value = 100;
                }
                ChunksPerSecondTrackBar.Minimum = 100;
                smoothing = true;
            } else
            {
                ChunksPerSecondTrackBar.Minimum = 1;
                smoothing = false;
            }
        }

        private void TresholdMaxTrackBar_ValueChanged(object sender, EventArgs e)
        {
            maxTreshold = TresholdMaxTrackBar.Value;
            TresholdMaxLabel.Text = maxTreshold.ToString();
            PeakVolumeBar.Value = maxTreshold;
            PeakVolumeLabel.Text = "Max Treshold: " + maxTreshold.ToString();
        }

        private void TresholdMaxTrackBar_MouseHover(object sender, EventArgs e)
        {
            ModifyProgressBarColor.SetState(PeakVolumeBar, 2);
            PeakVolumeBar.Value = maxTreshold;
            IsHovering = true;
        }

        private void TresholdMaxTrackBar_MouseLeave(object sender, EventArgs e)
        {
            ModifyProgressBarColor.SetState(PeakVolumeBar, 1);
            IsHovering = false;
        }

        private void WaveModeCheckbox_Click(object sender, EventArgs e)
        {
            if (PeakModeCheckbox.Checked)
            {
                PeakModeCheckbox.Checked = false;
                peakMode = false;
            }
            ChunksPerSecondTrackBar.Enabled = false;
            ChunksPerSecondTrackBar.Visible = false;
            ChunksPerSecondLabel.Visible = false;
            TimesPerSecondLabel.Visible = false;
            SmoothingCheckbox.Enabled = false;
            SmoothingCheckbox.Visible = false;
            BufferLabel.Visible = true;
            BufferTrackBar.Visible = true;
            BufferTimesLabel.Visible = true;
            BufferTrackBar.Enabled = true;
            waveMode = WaveModeCheckbox.Checked;

            int waveInDevices = WaveIn.DeviceCount;
            AudioDeviceComboBox.Items.Clear();
            for (int waveInDevice = 0; waveInDevice < waveInDevices; waveInDevice++)
            {
                WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(waveInDevice);
                AudioDeviceComboBox.Items.Add(deviceInfo.ProductName);
            }
            if(audioListener != null && audioListener.IsAlive)
            {
                audioListener.Abort();
            }
            DebugTextBox.Text = "Wave mode clicked";
        }

        private void PeakModeCheckbox_Click(object sender, EventArgs e)
        {

            MessageBox.Show("WARNING: This is the old mode and should not be used, it can be a bit glitchy. \n If the sound bar isn't moving, you need to open the following window: \n -Right click on the speaker/volume button in the bottom right corner of your screen. \n -Then hit: recording devices. \n Keep this window open in the background!. \n This mode allows for a bit more customizing by adding smoothing, but it shouldn't be needed and is a bit of a gimmick.");

            if (WaveModeCheckbox.Checked)
            {
                WaveModeCheckbox.Checked = false;
                waveMode = false;
            }

            ChunksPerSecondTrackBar.Enabled = true;
            ChunksPerSecondTrackBar.Visible = true;
            ChunksPerSecondLabel.Visible = true;
            TimesPerSecondLabel.Visible = true;
            SmoothingCheckbox.Enabled = true;
            SmoothingCheckbox.Visible = true;
            BufferLabel.Visible = false;
            BufferTrackBar.Visible = false;
            BufferTimesLabel.Visible = false;
            BufferTrackBar.Enabled = false;
            peakMode = PeakModeCheckbox.Checked;


            if (sourceStream != null)
            {
                try
                {
                    sourceStream.StopRecording();
                }
                catch
                {

                }
            }
            DebugTextBox.Text = "Peak mode clicked";
            //get all audio devices
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            var devices = enumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
            AudioDeviceComboBox.Items.Clear();
            for (int i = 0; i < devices.Count; i++)
            {
                AudioDeviceComboBox.Items.Add(devices[i].ToString());
            }
            //set default defice (first)
            device = (MMDevice)devices[0];
        }

      
        private void WaveModeCheckbox_MouseHover(object sender, EventArgs e)
        {
           
            IsHovering = true;
            stopEverything();
        }

        private void WaveModeCheckbox_MouseLeave(object sender, EventArgs e)
        {
            IsHovering = false;
            restartEverything();
        }

        private void PeakModeCheckbox_MouseHover(object sender, EventArgs e)
        {
            IsHovering = true;
            stopEverything();
        }

        private void PeakModeCheckbox_MouseLeave(object sender, EventArgs e)
        {
            IsHovering = false;
            restartEverything();
        }


        private void SmoothingCheckbox_MouseHover(object sender, EventArgs e)
        {
            IsHovering = true;
            stopEverything();
        }

        private void SmoothingCheckbox_MouseLeave(object sender, EventArgs e)
        {
            IsHovering = false;
            restartEverything();
        }

        private void KeyPressDelayTrackBar_ValueChanged(object sender, EventArgs e)
        {
            keyPressDuration = KeyPressDelayTrackBar.Value;
            KeyPressDelayLabel.Text = keyPressDuration.ToString();
        }

        private void restartEverything()
        {
            if (sourceStream != null)
            {
                try
                {
                    sourceStream.StartRecording();
                }
                catch
                {
                }
            }


            if (audioListener != null)
            {
                try
                {
                    audioListener.Start();
                }
                catch
                {

                }
            }
        }

        private void stopEverything()
        {
            if (sourceStream != null)
            {
                try
                {
                    sourceStream.StopRecording();
                }
                catch
                {
                }
            }

            if (audioListener != null && audioListener.IsAlive)
            {
                try
                {

                    audioListener.Abort();
                }
                catch
                {

                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {

            data.sensitivity = sensitivity;
            data.treshold = treshold;
            data.maxTreshold = maxTreshold;
            data.recordSpeed = recordSpeed;
            data.DisableEnableKey = DisableEnableKey;
            data.keyPressDuration = keyPressDuration;
            data.waveDeviceId = waveDeviceId;
            data.peakDeviceId = peakDeviceId;
            data.keyToBePressed = keyToBePressed;
            data.smoothing = smoothing;
            data.globalBuffer = globalBuffer;
            data.waveMode = waveMode;
            data.peakMode = peakMode;
            data.SaveData();
            MessageBox.Show("Succesfully saved your settings!");
        }

        public int GetMaxPartialIterate(int[] arr)
        {
            var max = arr[0];
            var idx = 0;
            for (int i = arr.Length / 2; i < arr.Length; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                }

                if (arr[idx] > max)
                {
                    max = arr[idx];
                }
                idx++;
            }
            return max;
        }

    }


    /// <summary>
    /// Used to change the color of the progressbar without loosing its visual effects.
    /// </summary>
    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
    }
}
