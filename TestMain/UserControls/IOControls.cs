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

        public IOControls(string name = "")
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

        }



        #region 卡号，第几个IO，高电平1
        private void uiButton1_Click(object sender, EventArgs e)
        {
            if (uiButton1.Text == "on")
            {
                LTDMC.dmc_write_outbit(0, 8, 1);
                uiButton1.Text = "off";
                outIO1.State= UILightState.Off;
            }
            else
            {
                LTDMC.dmc_write_outbit(0, 8, 0);
                uiButton1.Text = "on";
                outIO1.State= UILightState.On;
            }
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            if (uiButton2.Text == "on")
            {
                LTDMC.dmc_write_outbit(0, 9, 1); 
                uiButton2.Text = "off";
                outIO2.State = UILightState.Off;
            }
            else
            {
                LTDMC.dmc_write_outbit(0, 9, 0); 
                uiButton2.Text = "on";
                outIO2.State = UILightState.On;
            }
        }

        private void uiButton3_Click(object sender, EventArgs e)
        {
            if (uiButton3.Text == "on")
            {
                LTDMC.dmc_write_outbit(0, 10, 1);
                uiButton3.Text = "off";
                outIO3.State = UILightState.Off;
            }
            else
            {
                LTDMC.dmc_write_outbit(0, 10, 0);
                uiButton3.Text = "on";
                outIO3.State = UILightState.On;
            }
        }

        private void uiButton4_Click(object sender, EventArgs e)
        {
            if (uiButton4.Text == "on")
            {
                LTDMC.dmc_write_outbit(0, 11, 1);
                uiButton4.Text = "off";
                outIO4.State = UILightState.Off;
            }
            else
            {
                LTDMC.dmc_write_outbit(0, 11, 0);
                uiButton4.Text = "on";
                outIO4.State = UILightState.On;
            }
        }

        private void uiButton5_Click(object sender, EventArgs e)
        {
            if (uiButton5.Text == "on")
            {
                LTDMC.dmc_write_outbit(0, 12, 1);
                uiButton5.Text = "off";
                outIO5.State = UILightState.Off;
            }
            else
            {
                LTDMC.dmc_write_outbit(0, 12, 0);
                uiButton5.Text = "on";
                outIO5.State = UILightState.On;
            }
        }

        private void uiButton6_Click(object sender, EventArgs e)
        {
            if (uiButton6.Text == "on")
            {
                LTDMC.dmc_write_outbit(0, 13, 1);
                uiButton6.Text = "off";
                outIO6.State= UILightState.Off;
            }
            else
            {
                LTDMC.dmc_write_outbit(0, 13, 0);
                uiButton6.Text = "on";
                outIO6.State = UILightState.On;
            }
        }
        private void uiButton7_Click(object sender, EventArgs e)
        {

            if (uiButton7.Text == "on")
            {
                LTDMC.dmc_write_outbit(0, 14, 1);
                uiButton7.Text = "off";
                outIO7.State = UILightState.Off;
            }
            else
            {
                LTDMC.dmc_write_outbit(0, 14, 0);
                uiButton7.Text = "on";
                outIO7.State = UILightState.On;
            }
        }
        #endregion


    }
}
