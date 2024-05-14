using SharpCompress.Common;
using Sunny.UI;
using Sunny.UI.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestMain.Controls;
using TestMain.Model;
using TestMain.UserControls;
using static MongoDB.Driver.WriteConcern;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TestMain
{
    public partial class HydropressTest : UIPage
    {
        private double qswz;                //起始位置
        private double yxwz;                //运行位置
        private double bhwz;                //闭合位置
        private double kmwz1;               //开模位置
        private double kmwz2;               //开模位置
        private double kmwz3;               //开模位置
        private double dStartVel;           //起始速度
        private double dMaxVel;             //运行速度
        private double dStopVel;            //停止速度
        private double HdStartVel;          //开模速度
        private double HdMaxVel;            //开模速度
        private double HdStopVel;           //开模速度        
        private double dTacc;               //加速时间
        private double dTdec;               //减速时间

        ushort _CardID = 0;                 //轴机ID
        double PressureVD = 0;              //压力      
        double CurrentPos = 0;              //当前位置  
        double dCmdPos = 0;                 //指令位置
        double CurSpeed = 0;                //当前速度
        double dEnPos = 0;                  //编码器反馈位置   

        XmlHandler<HydropressModel> xmlFileManager = new XmlHandler<HydropressModel>();

        public HydropressTest()
        {
            InitializeComponent();
        }
        private void uiButton2_Click(object sender, EventArgs e)
        {
            HydropParmart hydropParmart = new HydropParmart();
            if (hydropParmart.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            uiPanel4.Text = DateTime.Now.DateTimeString();

            LTDMC.dmc_read_current_speed_unit(_CardID, 0, ref CurSpeed);     // 读取轴当前速度
            sun_Speed.Text = CurSpeed.ToString();

            int value0 = 0;
            LTDMC.nmc_read_txpdo_extra(_CardID, 2, 0, 1, ref value0);        //读取压力指令值
            double value00 = value_AD(0, value0);
            textBox5.Text = value00.ToString();
            PressureVD = value00;

            LTDMC.dmc_get_position_unit(_CardID, 0, ref dCmdPos);           //读取指定轴指令位置值
            tb_CurrentPos.Text = dCmdPos.ToString();
            CurrentPos = dCmdPos;

            LTDMC.dmc_get_encoder_unit(_CardID, 0, ref dEnPos);             //读取指定轴的编码器反馈值
            tb_Encoder.Text = dEnPos.ToString();

            if (LTDMC.dmc_check_done(_CardID, 0) == 0)                      //读取指定轴运动状态
            {
                tb_RunState.Text = "运行中";
            }
            else
            {
                tb_RunState.Text = "停止中";
            }

            ushort usAxisStateMachine = 0;
            LTDMC.nmc_get_axis_state_machine(0, 0, ref usAxisStateMachine);
            switch (usAxisStateMachine)
            {
                case 0:
                    textBox_StateMachine.Text = "轴处于未启动状态";
                    textBox_StateMachine.BackColor = Color.Red;
                    break;
                case 1:
                    textBox_StateMachine.Text = "轴处于启动禁止状态";
                    textBox_StateMachine.BackColor = Color.Red;
                    break;
                case 2:
                    textBox_StateMachine.Text = "轴处于准备启动状态";
                    textBox_StateMachine.BackColor = Color.Red;
                    break;
                case 3:
                    textBox_StateMachine.Text = "轴处于启动状态";
                    textBox_StateMachine.BackColor = Color.Red;
                    break;
                case 4:
                    textBox_StateMachine.Text = "轴处于操作使能状态";
                    textBox_StateMachine.BackColor = Color.Green;
                    break;
                case 5:
                    textBox_StateMachine.Text = "轴处于停止状态";
                    textBox_StateMachine.BackColor = Color.Red;
                    break;
                case 6:
                    textBox_StateMachine.Text = "轴处于错误触发状态";
                    textBox_StateMachine.BackColor = Color.Red;
                    break;
                case 7:
                    textBox_StateMachine.Text = "轴处于错误状态";
                    textBox_StateMachine.BackColor = Color.Red;
                    break;
            };

            //读取总线状态
            ushort usErrorCode = 0;
            LTDMC.nmc_get_errcode(0, 2, ref usErrorCode);
            if (usErrorCode == 0)
            {
                textBox_EthercatState.Text = "EtherCAT总线正常";
                textBox_EthercatState.BackColor = Color.Green;
            }
            else
            {
                textBox_EthercatState.Text = "EtherCAT总线出错";
                textBox_EthercatState.BackColor = Color.Red;
            }
        }

        private void RedingDate()
        {
            HydropressModel dataFromFile = xmlFileManager.ReadFromFile();
            foreach (var item in dataFromFile.Pressurestart)
            {
                qswz = double.Parse(item.Location);     //起始位置
                dStartVel = double.Parse(item.Spleed);  //起始速度
            }
            foreach (var item in dataFromFile.PressureOperation)
            {
                yxwz = double.Parse(item.Location);     //运行位置
                dMaxVel = double.Parse(item.Spleed);    //运行速度
            }
            foreach (var item in dataFromFile.Pressurestart)
            {
                bhwz = double.Parse(item.Location);     //闭合位置
                dStopVel = double.Parse(item.Spleed);   //停止速度
            }
            foreach (var item in dataFromFile.Pressurestart)
            {
                kmwz1 = double.Parse(item.Location);     //开模位置
                HdStartVel = double.Parse(item.Spleed);  //开模速度
            }
            foreach (var item in dataFromFile.Pressurestart)
            {
                kmwz2 = double.Parse(item.Location);    //开模位置
                HdMaxVel = double.Parse(item.Spleed);   //开模速度
            }
            foreach (var item in dataFromFile.Pressurestart)
            {
                kmwz3 = double.Parse(item.Location);     //开模位置
                HdStopVel = double.Parse(item.Spleed);   //开模速度
            }
            dTacc = double.Parse(dataFromFile.TimeAcc);    //加速时间
            dTdec = double.Parse(dataFromFile.TimeDec);    //减速时间
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            RedingDate();
            //下压速度变化
            LTDMC.dmc_set_profile_unit(_CardID, 0, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);
            //下压
            LTDMC.dmc_vmove(_CardID, 0, 1);
            //压力实施检测启动
            simtimer.Start();

            //等待保压度和设置度
            for (double i = PressureVD; i <= PressureVD;)
            {

            }

            //开模关闭压力检测
            simtimer.Stop();
            //开模速度变化
            LTDMC.dmc_set_profile_unit(_CardID, 0, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);
            //上升
            LTDMC.dmc_vmove(0, 0, 0);
        }

        private void simtimer_Tick(object sender, EventArgs e)
        {
            int pre = Detection();
            //设定压力值
            double pressure = 0;
            double pressure1 = 0;
            double pressure2 = 0;
            switch (pre)
            {
                case 0:
                    pressure = 0;
                    if (PressureVD >= pressure)
                    {
                        //压力报警
                    }
                    break;
                case 1:
                    pressure = 0;
                    if (PressureVD >= pressure1)
                    {
                        //压力报警
                    }
                    break;
                case 2:
                    pressure = 0;
                    if (PressureVD >= pressure2)
                    {
                        //压力报警
                    }
                    break;
                default:
                    break;
            }
        }

        //获取当前位置压力值
        public int Detection()
        {
            double pos = 0;
            double pos1 = 1;
            double pos2 = 2;

            int z = 0;
            //最小位置
            if (CurrentPos <= pos2)
            {
                z = 0;
            }//运行中
            else if (CurrentPos >= pos1 || CurrentPos < pos2)
            {
                z = 1;
            }//最初位置
            if (CurrentPos >= pos || CurrentPos < pos1 || CurrentPos < pos2)
            {
                z = 2;
            }
            return z;
        }

        //电压
        double value_AD(ushort SubIndex, int value)
        {
            int Value = 0;
            double _value = 1;
            LTDMC.nmc_get_node_od(_CardID, 2, 0, 32768, (ushort)(SubIndex + 1), 8, ref Value);
            if (Value == 0)//电压模式量程±5V
            {
                _value = value * 5 / 32000.0;
            }
            else if (Value == 1)//电压模式量程1-5V
            {
                _value = value * 4 / 32000.0 + 1;
            }
            else if (Value == 2)//电压模式量程±10V
            {
                _value = value * 10 / 32000.0;
            }
            else if (Value == 3)//电压模式量程0-10V
            {
                _value = value * 10 / 32000.0;
            }

            return _value;
        }

        private void HydropressTest_Load(object sender, EventArgs e)
        {
            try
            {
                short num = LTDMC.dmc_board_init();
                //控制卡初始化，获取卡数量
                if (num <= 0 || num > 8) { textBox5.Text = "初始卡失败!\", \"出错"; }
                ushort _num = 0;
                ushort[] cardids = new ushort[8];
                uint[] cardtypes = new uint[8];
                short res = LTDMC.dmc_get_CardInfList(ref _num, cardtypes, cardids);
                //获取卡成功在启动线程
                if (res != 0) { textBox5.Text = "获取卡信息失败"; }

                _CardID = cardids[0];
                timer1.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Speedunit(double dNewVel, double dTaccDec)
        {
            LTDMC.dmc_change_speed_unit(_CardID, 0, dNewVel, dTaccDec);        //在线变速
        }

        private void btn_ChangeVel_Click(object sender, EventArgs e)
        {
            double dNewVel = decimal.ToDouble(nud_NewVel.Value);               // 新的运行速度
            double dTaccDec = decimal.ToDouble(nud_TaccDec.Value);             //变速时间
            Speedunit(dNewVel, dTaccDec);
        }

        private void button_HardwareReset_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox_Message.Text = "";
                richTextBox_Message.AppendText("请勿操作，总线卡硬件复位进行中……" + "\n");
                button_HardwareReset.Enabled = false;
                LTDMC.dmc_board_reset();
                LTDMC.dmc_board_close();

                for (int i = 0; i < 15; i++)//总线卡硬件复位耗时15s左右
                {
                    Application.DoEvents();
                    Thread.Sleep(1000);
                }

                LTDMC.dmc_board_init();
                richTextBox_Message.AppendText("总线卡硬件复位完成,请确认总线状态");
                button_HardwareReset.Enabled = true;
                button_HardwareReset.Focus();

                richTextBox_Message.Text = "";
                richTextBox_Message.AppendText("请勿操作，总线卡软件复位进行中……" + "\n");
                button_HardwareReset.Enabled = false;
                //Application.DoEvents();
                LTDMC.dmc_soft_reset(_CardID);
                LTDMC.dmc_board_close();

                for (int i = 0; i < 15; i++)//总线卡软件复位耗时15s左右
                {
                    Application.DoEvents();
                    Thread.Sleep(1000);
                }
                LTDMC.dmc_board_init();
                richTextBox_Message.AppendText("总线卡软件复位完成,请确认总线状态");
                button_HardwareReset.Enabled = true;
                button_HardwareReset.Focus();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            HydropPattern pattern = new HydropPattern();
            if (pattern.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
