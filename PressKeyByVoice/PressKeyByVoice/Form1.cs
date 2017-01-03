using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public Process selectedProcess;
        public Thread audioListener = null;
        public int sensitivity = 1;
        public int treshold = 100;
        public MMDevice device;
        public bool IsHovering = false;
        public VirtualKeyCode key = VirtualKeyCode.VK_V;
        public bool notstop = true;
        public char keyToBePressed = (char)86;

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

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
            device = (MMDevice)devices[0];


            audioListener = new Thread(() => AudioListener());
            audioListener.Start();
            //get all process currently running:
            Process[] processlist = Process.GetProcesses();
            for (int i = 0; i < processlist.Length; i++)
            {
                ProgramComboBox.Items.Add(processlist[i].ProcessName);
            }

            TresholdTrackBar.Value = 100;
        }

        private void SetKeyButton_Click(object sender, EventArgs e)
        {

        }

        private void AudioDeviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            int index = comboBox.SelectedIndex;
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            var devices = enumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
            device = (MMDevice)devices[index];
            DebugTextBox.Text = "Selected device " + device.ToString() + " at index " + index;
        }

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

        private void KeyReleaser()
        {
            InputSimulator sim = new InputSimulator();
            sim.Keyboard.KeyDown(key);
            Thread.Sleep(100);
        }

        private void AudioListener()
        {
            while (notstop)
            {
                try
                {

                   // int audioVolume = (int)((device.AudioMeterInformation.MasterPeakValue * 100) * sensitivity);
                    UpdatePeakVolumeBar();
                } catch
                {

                }


                Thread.Sleep(30);
            }
        }

        private void SensitivityTrackBar_ValueChanged(object sender, EventArgs e)
        {
            sensitivity = SensitivityTrackBar.Value;
            SensLevel.Text = sensitivity.ToString();
        }

        private void DebutTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void TresholdTrackBar_ValueChanged(object sender, EventArgs e)
        {
            treshold = TresholdTrackBar.Value;
            TresholdLevel.Text = treshold.ToString();
            PeakVolumeBar.Value = treshold;
        }

        private void TresholdTrackBar_MouseHover(object sender, EventArgs e)
        {
            ModifyProgressBarColor.SetState(PeakVolumeBar, 3);
            PeakVolumeBar.Value = treshold;
            IsHovering = true;
        }

        private void TresholdTrackBar_MouseLeave(object sender, EventArgs e)
        {
            ModifyProgressBarColor.SetState(PeakVolumeBar, 1);
            IsHovering = false;
        }

        private uint GetActiveProcessID()
        {
            uint pID;
            IntPtr handle = GetForegroundWindow();
            GetWindowThreadProcessId(handle, out pID);
            return pID;
        }

        private void ProgramComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string processName = comboBox.SelectedItem.ToString();
            selectedProcess = Process.GetProcessesByName(processName)[0];
        }

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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            audioListener.Abort();
            notstop = true;
        }

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

        private void ProgramComboBox_Click(object sender, EventArgs e)
        {

            Process[] processlist = Process.GetProcesses();
            for (int i = 0; i < processlist.Length; i++)
            {
                ProgramComboBox.Items.Add(processlist[i].ProcessName);
            }
        }
    }

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
