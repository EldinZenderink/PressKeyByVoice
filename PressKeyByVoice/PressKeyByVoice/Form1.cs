using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using WindowsInput;
using WindowsInput.Native;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

namespace PressKeyByVoice
{
    public partial class Form1 : Form
    {
        //all the public/shared values
        public Process selectedProcess;
        public Thread audioListener = null;
        public int sensitivity = 1;
        public int treshold = 100;
        public MMDevice device;
        public bool IsHovering = false;
        public VirtualKeyCode key = VirtualKeyCode.VK_V;
        public bool notstop = true;
        public char keyToBePressed = (char)86;
        public int DisableEnableKey = 116;
        public bool DisableEnable = false;

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

            //get all audio devices
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            var devices = enumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
            for(int i =0; i < devices.Count; i++)
            {
                AudioDeviceComboBox.Items.Add(devices[i].ToString());
            }
            //set default defice (first)
            device = (MMDevice)devices[0];

            //Run the listener thread
            audioListener = new Thread(() => AudioListener());
            audioListener.Start();

            //Get all process currently running:
            Process[] processlist = Process.GetProcesses();
            for (int i = 0; i < processlist.Length; i++)
            {
                ProgramComboBox.Items.Add(processlist[i].ProcessName);
            }

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
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            var devices = enumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
            device = (MMDevice)devices[index];
            DebugTextBox.Text = "Selected device " + device.ToString() + " at index " + index;
        }

        /// <summary>
        /// The function that actually performs the comparison on  peak volume received from the audio device, this runs on the main thread due to NAudio having issues with running within non initiated threads at the start
        /// </summary>
        private void UpdatePeakVolumeBar()
        {
            if (PeakVolumeBar.InvokeRequired)
            {
                PeakVolumeBar.Invoke(new MethodInvoker(() => UpdatePeakVolumeBar()));
            }
            else
            {
                if (!IsHovering)
                {
                    int audioVolume = (int)(Math.Round(((device.AudioMeterInformation.MasterPeakValue * 100) * sensitivity)));
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

                   
                    if (audioVolume > treshold)
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
                    } else
                    {
                        KeyPressStatusLabel.Text = "Key " + keyToBePressed.ToString() + " is not being pressed!";
                        StatusBox.BackColor = Color.Red;
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
                    DebugTextBox.Invoke(new MethodInvoker(() => UpdateDebugText(text)));
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
            InputSimulator sim = new InputSimulator();
            sim.Keyboard.KeyDown(key);
            Thread.Sleep(100);
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
                        UpdatePeakVolumeBar();
                    } catch (Exception e)
                    {
                        UpdateDebugText("Issues with audio device: " + e.ToString());
                    }
                }
                Thread.Sleep(30);
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
        /// Trackbar to set the treshold value, value use din UpdatePeakVolumeBar method to execute the key press when the peakvolume, multiplied by the sensitivity exceeds the treshold value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TresholdTrackBar_ValueChanged(object sender, EventArgs e)
        {
            treshold = TresholdTrackBar.Value;
            TresholdLevel.Text = treshold.ToString();
            PeakVolumeBar.Value = treshold;
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
            int val = 86;
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
            audioListener.Abort();
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
                audioListener.Abort();
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
