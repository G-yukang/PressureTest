using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestMain.Common;

namespace TestMain.UserControls
{
    public partial class IOControls : UserControl
    {
        private AlarmLogger alarmLogger;
        DateTime dateTime;

        public IOControls(string name="")
        {
            InitializeComponent();
            timer1.Start();
            if (name == "自动模式")
            {
                uiCheckBoxGroup1.Visible = false;
            }
            else { uiCheckBoxGroup1.Visible = true; }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                dateTime = DateTime.Now;
                // 定义卡号
                ushort cardNo = 0;

                // 定义输入端口和输出端口数组
                int[] inputPorts = { 1, 2, 3, 4, 5, 6 };
                UILightState[] outputStates = new UILightState[6];

                // 读取输入端口的电平值并设置输出状态
                for (int i = 0; i < inputPorts.Length; i++)
                {
                    short inputValue = LTDMC.dmc_read_inbit(cardNo, (ushort)inputPorts[i]);
                    outputStates[i] = inputValue != 0 ? UILightState.Off : UILightState.On;
                }

                // 设置对应的输出端口状态
                outIO1.State = outputStates[0];
                outIO2.State = outputStates[1];
                outIO3.State = outputStates[2];
                outIO4.State = outputStates[3];
                outIO5.State = outputStates[4];
                outIO6.State = outputStates[5];
            }
            catch (Exception ex)
            {
                alarmLogger.SystemLogAlarm(dateTime + "IO读取报警:" + ex);
            }

        }



        #region 卡号，第几个IO，高电平1
        private void uiButton1_Click(object sender, EventArgs e)
        {
            LTDMC.dmc_write_outbit(0, 1, 1);
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            LTDMC.dmc_write_outbit(0, 2, 1);
        }

        private void uiButton3_Click(object sender, EventArgs e)
        {
            LTDMC.dmc_write_outbit(0, 3, 1);
        }

        private void uiButton4_Click(object sender, EventArgs e)
        {
            LTDMC.dmc_write_outbit(0, 4, 1);
        }

        private void uiButton5_Click(object sender, EventArgs e)
        {
            LTDMC.dmc_write_outbit(0, 5, 1);
        }

        private void uiButton6_Click(object sender, EventArgs e)
        {
            LTDMC.dmc_write_outbit(0, 6, 1);
        }
        #endregion
    }
}
