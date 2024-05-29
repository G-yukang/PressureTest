using Amazon.Runtime.Internal.Util;
using S7.Net.Types;
using SharpCompress.Common;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace TestMain.UserControls
{
    public partial class FlowPanel : UserControl
    {
        private string filePath;
        List<bool> valuesList = new List<bool>();
        List<string> wzluesList = new List<string>();
        List<string> sdluesList = new List<string>();
        List<string> ylluesList = new List<string>();
        List<string> ioluesList = new List<string>();

        public FlowPanel()
        {
            InitializeComponent();
        }
        private void FlowPanel_Load(object sender, EventArgs e)
        {
            this.filePath = AppDomain.CurrentDomain.BaseDirectory + "bin\\FlowConfig\\PressParameter.ini";
            ReadyDate();
        }
        private void uiButton5_Click(object sender, EventArgs e)
        {
            string[] wzArray = { wz001.Text, wz002.Text, wz003.Text, wz004.Text, wz005.Text, wz006.Text, wz007.Text, wz008.Text, wz009.Text, wz0010.Text };
            string[] sdArray = { sd001.Text, sd002.Text, sd003.Text, sd004.Text, sd005.Text, sd006.Text, sd007.Text, sd008.Text, sd009.Text, sd0010.Text };
            string[] ylArray = { yl001.Text, yl002.Text, yl003.Text, yl004.Text, yl005.Text, yl006.Text, yl007.Text, yl008.Text, yl009.Text, yl0010.Text };
            string[] ioArray = { com001.Text, com002.Text, com003.Text, com004.Text, com005.Text, com006.Text, com007.Text, com008.Text, com009.Text, com0010.Text };

            for (int i = 0; i < 10; i++)
            {
                string controlName = "uiTitlePanel" + (i + 1);
                WriteValue(controlName, "location", wzArray[i]);
            }
            for (int i = 0; i < 10; i++)
            {
                string controlName = "uiTitlePanel" + (i + 1);
                WriteValue(controlName, "speed", sdArray[i]);
            }
            for (int i = 0; i < 10; i++)
            {
                string controlName = "uiTitlePanel" + (i + 1);
                WriteValue(controlName, "pressure", ylArray[i]);
            }
            for (int i = 0; i < 10; i++)
            {
                string controlName = "uiTitlePanel" + (i + 1);
                WriteValue(controlName, "io", ioArray[i]);
            }

            WriteValue("Pressurize", "speed", uiTextBox2.Text);
            WriteValue("Pressurize", "time", uiTextBox1.Text);
            WriteValue("Pressurize", "pressure", uiTextBox3.Text);
            WriteValue("Pressurize", "io", uiComboTreeView5.Text);
            WriteValue("OtherParameters", "location", CurrentPosition.Text);
            WriteValue("OtherParameters", "speed", CurrentSpeed.Text);
            WriteValue("OtherParameters", "recipe", Recipe.Text);
            WriteValue("OtherParameters", "OpeningSpeed", TripSpeed.Text);
        }
        private void ReadyDate()
        {
            for (int i = 1; i <= 10; i++)
            {
                bool value = bool.Parse(ReadValue("uiTitlePanel" + i, "viewshow"));
                valuesList.Add(value);
            }
            for (int i = 1; i <= 10; i++)
            {
                string value = ReadValue("uiTitlePanel" + i, "location");
                wzluesList.Add(value);
            }
            for (int i = 1; i <= 10; i++)
            {
                string value = ReadValue("uiTitlePanel" + i, "speed");
                sdluesList.Add(value);
            }
            for (int i = 1; i <= 10; i++)
            {
                string value = ReadValue("uiTitlePanel" + i, "pressure");
                ylluesList.Add(value);
            }
            for (int i = 1; i <= 10; i++)
            {
                string value = ReadValue("uiTitlePanel" + i, "io");
                ioluesList.Add(value);
            }
            ReadControlData();
            ReadSwitchData();
            ReadViewData();
        }
        private void ReadControlData()
        {
            com001.Text = ioluesList[0];
            com002.Text = ioluesList[1];
            com003.Text = ioluesList[2];
            com004.Text = ioluesList[3];
            com005.Text = ioluesList[4];
            com006.Text = ioluesList[5];
            com007.Text = ioluesList[6];
            com008.Text = ioluesList[7];
            com009.Text = ioluesList[8];
            com0010.Text = ioluesList[9];

            sd001.Text = sdluesList[0];
            sd002.Text = sdluesList[1];
            sd003.Text = sdluesList[2];
            sd004.Text = sdluesList[3];
            sd005.Text = sdluesList[4];
            sd006.Text = sdluesList[5];
            sd007.Text = sdluesList[6];
            sd008.Text = sdluesList[7];
            sd009.Text = sdluesList[8];
            sd0010.Text = sdluesList[9];

            yl001.Text = ylluesList[0];
            yl002.Text = ylluesList[1];
            yl003.Text = ylluesList[2];
            yl004.Text = ylluesList[3];
            yl005.Text = ylluesList[4];
            yl006.Text = ylluesList[5];
            yl007.Text = ylluesList[6];
            yl008.Text = ylluesList[7];
            yl009.Text = ylluesList[8];
            yl0010.Text = ylluesList[9];

            wz001.Text = wzluesList[0];
            wz002.Text = wzluesList[1];
            wz003.Text = wzluesList[2];
            wz004.Text = wzluesList[3];
            wz005.Text = wzluesList[4];
            wz006.Text = wzluesList[5];
            wz007.Text = wzluesList[6];
            wz008.Text = wzluesList[7];
            wz009.Text = wzluesList[8];
            wz0010.Text = wzluesList[9];

            uiComboTreeView5.Text = ReadValue("Pressurize", "location");
            uiTextBox2.Text = ReadValue("Pressurize", "speed");
            uiTextBox1.Text = ReadValue("Pressurize", "time");
            uiTextBox3.Text = ReadValue("Pressurize", "pressure");
            uiComboTreeView5.Text = ReadValue("Pressurize", "io");
            CurrentPosition.Text = ReadValue("OtherParameters", "location");
            CurrentSpeed.Text = ReadValue("OtherParameters", "speed");
            Recipe.Text = ReadValue("OtherParameters", "recipe");
            TripSpeed.Text = ReadValue("OtherParameters", "OpeningSpeed");
        }

        private void ReadViewData()
        {
            TitlePanel1.io = ioluesList[0];
            TitlePanel2.io = ioluesList[1];
            TitlePanel3.io = ioluesList[2];
            TitlePanel4.io = ioluesList[3];
            TitlePanel5.io = ioluesList[4];
            TitlePanel6.io = ioluesList[5];
            TitlePanel7.io = ioluesList[6];
            TitlePanel8.io = ioluesList[7];
            TitlePanel9.io = ioluesList[8];
            TitlePanel10.io = ioluesList[9];

            TitlePanel1.speed = sdluesList[0];
            TitlePanel2.speed = sdluesList[1];
            TitlePanel3.speed = sdluesList[2];
            TitlePanel4.speed = sdluesList[3];
            TitlePanel5.speed = sdluesList[4];
            TitlePanel6.speed = sdluesList[5];
            TitlePanel7.speed = sdluesList[6];
            TitlePanel8.speed = sdluesList[7];
            TitlePanel9.speed = sdluesList[8];
            TitlePanel10.speed = sdluesList[9];

            TitlePanel1.pressure = ylluesList[0];
            TitlePanel2.pressure = ylluesList[1];
            TitlePanel3.pressure = ylluesList[2];
            TitlePanel4.pressure = ylluesList[3];
            TitlePanel5.pressure = ylluesList[4];
            TitlePanel6.pressure = ylluesList[5];
            TitlePanel7.pressure = ylluesList[6];
            TitlePanel8.pressure = ylluesList[7];
            TitlePanel9.pressure = ylluesList[8];
            TitlePanel10.pressure = ylluesList[9];

            TitlePanel1.location = wzluesList[0];
            TitlePanel2.location = wzluesList[1];
            TitlePanel3.location = wzluesList[2];
            TitlePanel4.location = wzluesList[3];
            TitlePanel5.location = wzluesList[4];
            TitlePanel6.location = wzluesList[5];
            TitlePanel7.location = wzluesList[6];
            TitlePanel8.location = wzluesList[7];
            TitlePanel9.location = wzluesList[8];
            TitlePanel10.location = wzluesList[9];

            PressurizeView.speed = ReadValue("Pressurize", "speed");
            PressurizeView.time = ReadValue("Pressurize", "time");
            PressurizeView.pressure = ReadValue("Pressurize", "pressure");
            PressurizeView.io = ReadValue("Pressurize", "io");
            OtherParametersView.location = ReadValue("OtherParameters", "location");
            OtherParametersView.speed = ReadValue("OtherParameters", "speed");
            OtherParametersView.recipe = ReadValue("OtherParameters", "recipe");
            OtherParametersView.OpeningSpeed = ReadValue("OtherParameters", "OpeningSpeed");
        }

        private void ReadSwitchData()
        {
            uiSwitch001.Active = valuesList[0];
            uiSwitch002.Active = valuesList[1];
            uiSwitch003.Active = valuesList[2];
            uiSwitch004.Active = valuesList[3];
            uiSwitch005.Active = valuesList[4];
            uiSwitch006.Active = valuesList[5];
            uiSwitch007.Active = valuesList[6];
            uiSwitch008.Active = valuesList[7];
            uiSwitch009.Active = valuesList[8];
            uiSwitch0010.Active = valuesList[9];
            ToggleControlsReadOnly(uiSwitch001.Active, (wz001, null), (sd001, null), (yl001, null), (com001, null));
            ToggleControlsReadOnly(uiSwitch002.Active, (wz002, null), (sd002, null), (yl002, null), (com002, null));
            ToggleControlsReadOnly(uiSwitch003.Active, (sd003, null), (wz003, null), (yl003, null), (com003, null));
            ToggleControlsReadOnly(uiSwitch004.Active, (wz004, null), (sd004, null), (yl004, null), (com004, null));
            ToggleControlsReadOnly(uiSwitch005.Active, (wz005, null), (sd005, null), (yl005, null), (com005, null));
            ToggleControlsReadOnly(uiSwitch006.Active, (wz006, null), (sd006, null), (yl006, null), (com006, null));
            ToggleControlsReadOnly(uiSwitch007.Active, (sd007, com007), (yl007, null), (wz007, null));
            ToggleControlsReadOnly(uiSwitch008.Active, (sd008, com008), (yl008, null), (wz008, null));
            ToggleControlsReadOnly(uiSwitch009.Active, (sd009, com009), (yl009, null), (wz009, null));
            ToggleControlsReadOnly(uiSwitch0010.Active, (sd0010, com0010), (yl0010, null), (wz0010, null));
            ToggleControlsReadOnly(uiSwitch7.Active, (CurrentPosition, null), (CurrentSpeed, null), (Recipe, null), (TripSpeed, null));
        }

        #region 读取写入ini文件
        public string ReadValue(string section, string key)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    if (File.Exists(filePath))
                    {
                        List<string> lines = new List<string>(File.ReadAllLines(filePath));
                        string searchKey = "[" + section + "]";
                        int sectionIndex = lines.FindIndex(line => line.Contains(searchKey));

                        if (sectionIndex != -1)
                        {
                            string keySearch = key + "=";
                            int keyIndex = lines.FindIndex(sectionIndex + 1, line => line.Trim().StartsWith(keySearch));

                            if (keyIndex != -1)
                            {
                                return lines[keyIndex].Substring(keySearch.Length).Trim();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while reading configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }
        public void WriteValue(string section, string key, string value)
        {
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            string searchKey = "[" + section + "]";
            int sectionIndex = lines.FindIndex(line => line.Contains(searchKey));

            if (sectionIndex == -1)
            {
                lines.Add("[" + section + "]");
                sectionIndex = lines.Count - 1;
            }

            string keySearch = key + "=";
            int keyIndex = lines.FindIndex(sectionIndex + 1, line => line.Trim().StartsWith(keySearch));

            if (keyIndex != -1)
            {
                lines[keyIndex] = key + "=" + value;
            }
            else
            {
                lines.Insert(sectionIndex + 1, key + "=" + value);
            }

            File.WriteAllLines(filePath, lines);
        }
        #endregion

        #region 启用关闭
        private void uiSwitch4_ValueChanged(object sender, bool value)
        {
            ToggleControlsReadOnly(uiSwitch001.Active, (wz001, null), (sd001, null), (yl001, null), (com001, null));
            WriteValue("uiTitlePanel1", "viewshow", uiSwitch001.Active.ToString());
        }
        private void uiSwitch1_ValueChanged(object sender, bool value)
        {
            ToggleControlsReadOnly(uiSwitch002.Active, (wz002, null), (sd002, null), (yl002, null), (com002, null));
            WriteValue("uiTitlePanel2", "viewshow", uiSwitch002.Active.ToString());
        }
        private void uiSwitch8_ValueChanged(object sender, bool value)
        {
            ToggleControlsReadOnly(uiSwitch003.Active, (sd003, null), (wz003, null), (yl003, null), (com003, null));
            WriteValue("uiTitlePanel3", "viewshow", uiSwitch003.Active.ToString());
        }
        private void uiSwitch9_ValueChanged(object sender, bool value)
        {
            ToggleControlsReadOnly(uiSwitch004.Active, (wz004, null), (sd004, null), (yl004, null), (com004, null));
            WriteValue("uiTitlePanel4", "viewshow", uiSwitch004.Active.ToString());
        }
        private void uiSwitch10_ValueChanged(object sender, bool value)
        {
            ToggleControlsReadOnly(uiSwitch005.Active, (wz005, null), (sd005, null), (yl005, null), (com005, null));
            WriteValue("uiTitlePanel5", "viewshow", uiSwitch005.Active.ToString());
        }
        private void uiSwitch11_ValueChanged(object sender, bool value)
        {
            ToggleControlsReadOnly(uiSwitch006.Active, (wz006, null), (sd006, null), (yl006, null), (com006, null));
            WriteValue("uiTitlePanel6", "viewshow", uiSwitch006.Active.ToString());
        }
        private void uiSwitch2_ValueChanged(object sender, bool value)
        {
            ToggleControlsReadOnly(uiSwitch007.Active, (sd007, com007), (yl007, null), (wz007, null));
            WriteValue("uiTitlePanel7", "viewshow", uiSwitch007.Active.ToString());
        }
        private void uiSwitch3_ValueChanged(object sender, bool value)
        {
            ToggleControlsReadOnly(uiSwitch008.Active, (sd008, com008), (yl008, null), (wz008, null));
            WriteValue("uiTitlePanel8", "viewshow", uiSwitch008.Active.ToString());
        }
        private void uiSwitch5_ValueChanged(object sender, bool value)
        {
            ToggleControlsReadOnly(uiSwitch009.Active, (sd009, com009), (yl009, null), (wz009, null));
            WriteValue("uiTitlePanel9", "viewshow", uiSwitch009.Active.ToString());
        }
        private void uiSwitch6_ValueChanged(object sender, bool value)
        {
            ToggleControlsReadOnly(uiSwitch0010.Active, (sd0010, com0010), (yl0010, null), (wz0010, null));
            WriteValue("uiTitlePanel10", "viewshow", uiSwitch0010.Active.ToString());
        }
        private void uiSwitch7_ValueChanged(object sender, bool value)
        {
            ToggleControlsReadOnly(uiSwitch7.Active, (CurrentPosition, null), (CurrentSpeed, null), (Recipe, null), (TripSpeed, null));
            WriteValue("OtherParameters", "viewshow", uiSwitch007.Active.ToString());
        }
        private void ToggleControlsReadOnly(bool isReadOnly, params (Control, Control)[] controlPairs)
        {
            foreach (var (textBox, comboTreeView) in controlPairs)
            {
                textBox.Enabled = isReadOnly;
                if (comboTreeView != null)
                {
                    comboTreeView.Enabled = isReadOnly;
                }
            }
        }

        #endregion

    }
}
