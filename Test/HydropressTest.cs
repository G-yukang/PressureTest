using csLTDMC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class HydropressTest : Form
    {
        private ushort usCardId = 0;
        public HydropressTest()
        {
            InitializeComponent();
        }

        //目前只有一台轴机
        //private ushort GetAxis()
        //{
        //    return decimal.ToUInt16(nud_AxisId.Text);
        //}
        private void btn_ChangeVel_Click(object sender, EventArgs e)
        {
            //ushort usAxis = GetAxis(); //轴号
            double dNewVel = decimal.ToDouble(nud_NewVel.Value);          // 新的运行速度
            double dTaccDec = decimal.ToDouble(nud_TaccDec.Value);        //变速时间
            LTDMC.dmc_change_speed_unit(usCardId, 1, dNewVel, dTaccDec);  //在线变速
        }

        private void HydropressTest_Load(object sender, EventArgs e)
        {

            short sNum = LTDMC.dmc_board_init();//获取卡数量
            if (sNum <= 0 || sNum > 8)
            {
                toolStripStatusLabel3.Text = "初始卡失败!";
                MessageBox.Show("初始卡失败!", "出错");
            }
            ushort usNum = 0;
            ushort[] arrusCardList = new ushort[8];
            uint[] arrunCardTypes = new uint[8];
            short sRtn = LTDMC.dmc_get_CardInfList(ref usNum, arrunCardTypes, arrusCardList);
            if (sRtn != 0)
            {
                MessageBox.Show("获取卡信息失败!");
            }
            usCardId = arrusCardList[0];

            timer1.Start();
            // 使能对应轴    
            sRtn = LTDMC.nmc_set_axis_enable(usCardId, 1);
        }

        private void button_HardwareReset_Click(object sender, EventArgs e)
        {
            statelabe.Text = "请勿操作，总线卡硬件复位进行中……\" + \"\\n\"";

            //Application.DoEvents();
            LTDMC.dmc_board_reset();
            LTDMC.dmc_board_close();

            for (int i = 0; i < 15; i++)//总线卡硬件复位耗时15s左右
            {
                Application.DoEvents();
                Thread.Sleep(1000);
            }

            LTDMC.dmc_board_init();

            statelabe.Text = "总线卡硬件复位完成,请确认总线状态";

            statelabe.Text = "请勿操作，总线卡软件复位进行中……\" + \"\\n\"";
            LTDMC.dmc_soft_reset(usCardId);
            LTDMC.dmc_board_close();

            for (int i = 0; i < 15; i++)//总线卡软件复位耗时15s左右
            {
                Application.DoEvents();
                Thread.Sleep(1000);
            }
            LTDMC.dmc_board_init();

            statelabe.Text = "总线卡软件复位完成";
            button_HardwareReset.Enabled = true;
            button_HardwareReset.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            short sNum = LTDMC.dmc_board_init();//获取卡数量
            if (sNum <= 0 || sNum > 8)
            {
                toolStripStatusLabel3.Text = "初始卡失败!";
            }
            ushort usNum = 0;
            ushort[] arrusCardList = new ushort[8];
            uint[] arrunCardTypes = new uint[8];
            short sRtn = LTDMC.dmc_get_CardInfList(ref usNum, arrunCardTypes, arrusCardList);
            if (sRtn != 0)
            {
                toolStripStatusLabel3.Text = "获取卡信息失败!";
            }
            toolStripStatusLabel3.Text = "连接成功";
            usCardId = arrusCardList[0];
        }
    }
}

