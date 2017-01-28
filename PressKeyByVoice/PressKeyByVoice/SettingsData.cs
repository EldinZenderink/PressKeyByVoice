using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

namespace PressKeyByVoice
{
    public class SettingsData
    {

        public int sensitivity = 1;
        public int treshold = 100;
        public int maxTreshold = 100;
        public int recordSpeed = 100;
        public int DisableEnableKey = 116;
        public int keyPressDuration = 1000;
        public int waveDeviceId = 0;
        public int peakDeviceId = 0;
        public char keyToBePressed = (char)86;
        public bool smoothing = false;
        public bool waveMode = true;
        public bool peakMode = false;
        public string settingsPath = "\\Settings.ini";

        public SettingsData()
        {
            settingsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PressKeyByVoice\\Settings.ini";

            if(!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PressKeyByVoice"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PressKeyByVoice");
            }
            if (!File.Exists(settingsPath))
            {
                //File.Create(settingsPath);
                int character = keyToBePressed;
                string defaultSettings = @"sensitivity = " + sensitivity.ToString() + ";treshold = " + treshold.ToString() + ";maxTreshold = " + maxTreshold.ToString() + ";recordSpeed = " + recordSpeed.ToString() + ";DisableEnableKey = " + DisableEnableKey.ToString() + ";keyPressDuration = " + keyPressDuration.ToString() + ";waveDeviceId = " + waveDeviceId.ToString() + ";peakDeviceId = " + peakDeviceId.ToString() + ";keyToBePressed = " + character.ToString() + ";smoothing = " + smoothing.ToString() + ";waveMode = " + waveMode.ToString() + ";peakMode = " + peakMode.ToString() + ";";

                TextWriter tw = new StreamWriter(settingsPath, true);
                tw.WriteLine(defaultSettings);
                tw.Close();
            } else
            {
                string userSettings = File.ReadAllText(settingsPath);
                string[] perSetting = userSettings.Split(';');

                bool parseSucces = false;
                string currentSetting = "";
                foreach (string setting in perSetting)
                {
                    
                    if (setting.Contains("sensitivity"))
                    {
                        int value = 0;
                        parseSucces = int.TryParse(setting.Split('=')[1].Trim(), out value);
                        sensitivity = value;
                    }
                    else if (setting.Contains("treshold"))
                    {
                        int value = 0;
                        parseSucces = int.TryParse(setting.Split('=')[1].Trim(), out value);
                        treshold = value;
                    }
                    else if (setting.Contains("maxTreshold"))
                    {
                        int value = 0;
                        parseSucces = int.TryParse(setting.Split('=')[1].Trim(), out value);
                        maxTreshold = value;
                    }
                    else if (setting.Contains("recordSpeed"))
                    {
                        int value = 0;
                        parseSucces = int.TryParse(setting.Split('=')[1].Trim(), out value);
                        recordSpeed = value;
                    }
                    else if (setting.Contains("DisableEnableKey"))
                    {
                        int value = 0;
                        parseSucces = int.TryParse(setting.Split('=')[1].Trim(), out value);
                        DisableEnableKey = value;
                    }
                    else if (setting.Contains("keyPressDuration"))
                    {
                        int value = 0;
                        parseSucces = int.TryParse(setting.Split('=')[1].Trim(), out value);
                        keyPressDuration = value;
                    }
                    else if (setting.Contains("waveDeviceId"))
                    {
                        int value = 0;
                        parseSucces = int.TryParse(setting.Split('=')[1].Trim(), out value);
                        waveDeviceId = value;
                    }
                    else if (setting.Contains("peakDeviceId"))
                    {
                        int value = 0;
                        parseSucces = int.TryParse(setting.Split('=')[1].Trim(), out value);
                        peakDeviceId = value;
                    }
                    else if (setting.Contains("keyToBePressed"))
                    {
                        int value = 0;
                        parseSucces = int.TryParse(setting.Split('=')[1].Trim(), out value);
                        keyToBePressed = (char)value;
                    }
                    else if (setting.Contains("smoothing"))
                    {
                        try
                        {
                            smoothing = Convert.ToBoolean(setting.Split('=')[1].Trim());
                            parseSucces = true;
                        }
                        catch (FormatException)
                        {
                            smoothing = false;
                            parseSucces = false;
                        }
                    }
                    else if (setting.Contains("waveMode"))
                    {
                        try
                        {
                            waveMode = Convert.ToBoolean(setting.Split('=')[1].Trim());
                            parseSucces = true;
                        }
                        catch (FormatException)
                        {
                            waveMode = false;
                            parseSucces = false;
                        }
                    }
                    else if (setting.Contains("peakMode"))
                    {
                        try
                        {
                            peakMode = Convert.ToBoolean(setting.Split('=')[1].Trim());
                            parseSucces = true;
                        }
                        catch (FormatException)
                        {
                            peakMode = false;
                            parseSucces = false;
                        }
                    }

                    if (!parseSucces)
                    {
                        currentSetting = setting + ",@" + currentSetting;
                    }
                }
                if (!parseSucces)
                {
                    MessageBox.Show("Something went wrong when loading your settings, error happend at: " + currentSetting);
                }
            }
        }

        public void SaveData()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PressKeyByVoice"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PressKeyByVoice");
            }
            if (!File.Exists(settingsPath))
            {
                File.CreateText(settingsPath);
                int character = keyToBePressed;
                string defaultSettings = @"sensitivity = " + sensitivity.ToString() + ";treshold = " + treshold.ToString() + ";maxTreshold = " + maxTreshold.ToString() + ";recordSpeed = " + recordSpeed.ToString() + ";DisableEnableKey = " + DisableEnableKey.ToString() + ";keyPressDuration = " + keyPressDuration.ToString() + ";waveDeviceId = " + waveDeviceId.ToString() + ";peakDeviceId = " + peakDeviceId.ToString() + ";keyToBePressed = " + character.ToString() + ";smoothing = " + smoothing.ToString() + ";waveMode = " + waveMode.ToString() + ";peakMode = " + peakMode.ToString() + ";";
                TextWriter tw = new StreamWriter(settingsPath);
                tw.WriteLine(defaultSettings);
                tw.Close();
            }
            else
            {

                int character = keyToBePressed;
                string defaultSettings = @"sensitivity = " + sensitivity.ToString() + ";treshold = " + treshold.ToString() + ";maxTreshold = " + maxTreshold.ToString() + ";recordSpeed = " + recordSpeed.ToString() + ";DisableEnableKey = " + DisableEnableKey.ToString() + ";keyPressDuration = " + keyPressDuration.ToString() + ";waveDeviceId = " + waveDeviceId.ToString() + ";peakDeviceId = " + peakDeviceId.ToString() + ";keyToBePressed = " + character.ToString() + ";smoothing = " + smoothing.ToString() + ";waveMode = " + waveMode.ToString() + ";peakMode = " + peakMode.ToString() + ";";
                TextWriter tw = new StreamWriter(settingsPath);
                tw.WriteLine(defaultSettings);
                tw.Close();
            }
        }
    }
}
