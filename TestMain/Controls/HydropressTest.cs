using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using TestMain.Controls;
using TestMain.Model;

namespace TestMain
{
    public partial class HydropressTest : UIPage
    {
        #region 全局参数
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
        private double qsyl;                //压力起始
        private double yxyl;                //压力运行
        private double bhyl;                //压力闭合
        private double kmyl;                //压力开模


        ushort _CardID = 0;                 //轴机ID
        double PressureVD = 0;              //压力      
        double CurrentPos = 0;              //当前位置  
        double dCmdPos = 0;                 //指令位置
        double CurSpeed = 0;                //当前速度
        double dEnPos = 0;                  //编码器反馈位置

        private ushort usCardNum = 0;       //IO

        XmlHandler<HydropressModel> xmlFileManager = new XmlHandler<HydropressModel>();

        Label[] arrLabel = new Label[8];
        private List<PointF> arrPonitList = new List<PointF>();
        System.Windows.Forms.Button[] arrButton = new System.Windows.Forms.Button[8];//定义输出IO的按钮控件
        #endregion

        public HydropressTest()
        {
            InitializeComponent();
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
                Ctr_Build();
                timer1.Start();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region 定时事件
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

            //IO模拟数据监听
            for (ushort i = 0; i < 8; i++)
            {
                if (LTDMC.dmc_read_inbit(usCardNum, i) == 0)
                {
                    arrLabel[i].BackColor = Color.Green;
                }
                else
                {
                    arrLabel[i].BackColor = Color.Red;
                }
            }

            for (ushort i = 0; i < 8; i++)
            {
                if (LTDMC.dmc_read_outbit(usCardNum, i) == 0)
                {
                    arrButton[i].BackColor = Color.Green;
                }
                else
                {
                    arrButton[i].BackColor = Color.Red;
                }
            }

            //IO数量
            ushort usTotalIn = 0;
            ushort usTotalOut = 0;
            LTDMC.nmc_get_total_ionum(usCardNum, ref usTotalIn, ref usTotalOut);
        }
        private void simtimer_Tick(object sender, EventArgs e)
        {
            //设定压力值
            double pressure = qsyl, pressure1 = yxyl, pressure2 = bhyl;
            double pre = Detection();
            switch (pre)
            {
                case 0:
                    if (PressureVD >= pressure)
                    {
                        //压力报警
                    }
                    break;
                case 1:
                    if (PressureVD >= pressure1)
                    {
                        //压力报警
                    }
                    break;
                case 2:
                    if (PressureVD >= pressure2)
                    {
                        //压力报警
                    }
                    break;
            }
        }
        #endregion

        #region 按钮事件
        private void btn_ChangeVel_Click(object sender, EventArgs e)
        {
            double dNewVel = nud_NewVel.Value;               // 新的运行速度
            double dTaccDec = nud_TaccDec.Value;             //变速时间
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
        private void uiButton2_Click(object sender, EventArgs e)
        {
            HydropParmart hydropParmart = new HydropParmart();
            if (hydropParmart.ShowDialog() == DialogResult.OK)
            {

            }
        }
        private void btn_Start_Click(object sender, EventArgs e)
        {
            arrPonitList.Clear();

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
        #endregion

        #region 逻辑处理
        //获取当前位置压力值
        public double Detection()
        {
            double pos = qswz, pos1 = yxwz, pos2 = bhwz;
            //起始-运行  
            if (CurrentPos <= pos && CurrentPos > pos1 && CurrentPos > pos2)
            { return 0; }
            //运行-闭合 
            else if (CurrentPos < pos && CurrentPos <= pos1 && CurrentPos > pos2)
            { return 1; }
            //最初位置 
            else if (CurrentPos < pos && CurrentPos < pos1 && CurrentPos >= pos2)
            { return 2; }

            return 0;
        }
        private void RedingDate()
        {
            HydropressModel dataFromFile = xmlFileManager.ReadFromFile();
            foreach (var item in dataFromFile.Pressurestart)
            {
                qswz = double.Parse(item.Location);     //起始位置
                dStartVel = double.Parse(item.Spleed);  //起始速度
                qsyl = double.Parse(item.Pressure);
            }
            foreach (var item in dataFromFile.PressureOperation)
            {
                yxwz = double.Parse(item.Location);     //运行位置
                dMaxVel = double.Parse(item.Spleed);    //运行速度
                yxyl = double.Parse(item.Pressure);
            }
            foreach (var item in dataFromFile.Pressurestart)
            {
                bhwz = double.Parse(item.Location);     //闭合位置
                dStopVel = double.Parse(item.Spleed);   //停止速度
                bhyl = double.Parse(item.Pressure);
            }
            foreach (var item in dataFromFile.Pressurestart)
            {
                kmwz1 = double.Parse(item.Location);     //开模位置
                HdStartVel = double.Parse(item.Spleed);  //开模速度
                kmyl = double.Parse(item.Pressure);
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
        public void Speedunit(double dNewVel, double dTaccDec)
        {
            LTDMC.dmc_change_speed_unit(_CardID, 0, dNewVel, dTaccDec);        //在线变速
        }
        #endregion

        #region IO模拟测试
        //创建界面控件子程序
        private void Ctr_Build()
        {
            for (int i = 0; i < 8; i++)
            {
                arrLabel[i] = new Label();
                IOIn.Controls.Add(arrLabel[i]);
                arrLabel[i].Size = new Size(40, 25);
                arrLabel[i].Location = new Point(19 + ((i) % 8 * 50), 27 + (i) / 8 * 40);
                arrLabel[i].BackColor = Color.Red;
                arrLabel[i].Text = string.Format("In{0}", i);
                arrLabel[i].TextAlign = ContentAlignment.MiddleCenter;
            }

            for (int i = 0; i < 8; i++)
            {
                arrButton[i] = new System.Windows.Forms.Button();
                IOout.Controls.Add(arrButton[i]);
                arrButton[i].Size = new Size(50, 30);
                arrButton[i].Location = new Point(19 + (i) % 8 * 50, 20 + (i) / 8 * 40);
                arrButton[i].Text = string.Format("Out{0}", i);
            }

            arrButton[0].Click += new System.EventHandler(MyOutBtn0_Click);      //注册Out0按钮事件
            arrButton[1].Click += new System.EventHandler(MyOutBtn1_Click);      //注册Out1按钮事件
            arrButton[2].Click += new System.EventHandler(MyOutBtn2_Click);      //注册Out2按钮事件
            arrButton[3].Click += new System.EventHandler(MyOutBtn3_Click);      //注册Out3按钮事件
            arrButton[4].Click += new System.EventHandler(MyOutBtn4_Click);      //注册Out4按钮事件
            arrButton[5].Click += new System.EventHandler(MyOutBtn5_Click);      //注册Out5按钮事件
            arrButton[6].Click += new System.EventHandler(MyOutBtn6_Click);      //注册Out6按钮事件
            arrButton[7].Click += new System.EventHandler(MyOutBtn7_Click);      //注册Out7按钮事件

        }
        private void MyOutBtn0_Click(object sender, EventArgs e)     //Out0按钮事件
        {
            if (LTDMC.dmc_read_outbit(usCardNum, 0) == 0)   //如果当前为输出低电平
            { LTDMC.dmc_write_outbit(usCardNum, 0, 1); }    //输出高电平
            else
            { LTDMC.dmc_write_outbit(usCardNum, 0, 0); }    //输出低电平
        }

        private void MyOutBtn1_Click(object sender, EventArgs e)     //Out1按钮事件
        {
            if (LTDMC.dmc_read_outbit(usCardNum, 1) == 0)   //如果当前为输出低电平
            { LTDMC.dmc_write_outbit(usCardNum, 1, 1); }    //输出高电平
            else
            { LTDMC.dmc_write_outbit(usCardNum, 1, 0); }    //输出低电平
        }

        private void MyOutBtn2_Click(object sender, EventArgs e)     //Out2按钮事件
        {
            if (LTDMC.dmc_read_outbit(usCardNum, 2) == 0)   //如果当前为输出低电平
            { LTDMC.dmc_write_outbit(usCardNum, 2, 1); }    //输出高电平
            else
            { LTDMC.dmc_write_outbit(usCardNum, 2, 0); }    //输出低电平
        }

        private void MyOutBtn3_Click(object sender, EventArgs e)     //Out3按钮事件
        {
            if (LTDMC.dmc_read_outbit(usCardNum, 3) == 0)   //如果当前为输出低电平
            { LTDMC.dmc_write_outbit(usCardNum, 3, 1); }    //输出高电平
            else
            { LTDMC.dmc_write_outbit(usCardNum, 3, 0); }    //输出低电平
        }

        private void MyOutBtn4_Click(object sender, EventArgs e)     //Out4按钮事件
        {
            if (LTDMC.dmc_read_outbit(usCardNum, 4) == 0)   //如果当前为输出低电平
            { LTDMC.dmc_write_outbit(usCardNum, 4, 1); }    //输出高电平
            else
            { LTDMC.dmc_write_outbit(usCardNum, 4, 0); }    //输出低电平
        }

        private void MyOutBtn5_Click(object sender, EventArgs e)     //Out5按钮事件
        {
            if (LTDMC.dmc_read_outbit(usCardNum, 5) == 0)   //如果当前为输出低电平
            { LTDMC.dmc_write_outbit(usCardNum, 5, 1); }    //输出高电平
            else
            { LTDMC.dmc_write_outbit(usCardNum, 5, 0); }    //输出低电平
        }

        private void MyOutBtn6_Click(object sender, EventArgs e)     //Out6按钮事件
        {
            if (LTDMC.dmc_read_outbit(usCardNum, 6) == 0)   //如果当前为输出低电平
            { LTDMC.dmc_write_outbit(usCardNum, 6, 1); }    //输出高电平
            else
            { LTDMC.dmc_write_outbit(usCardNum, 6, 0); }    //输出低电平
        }

        private void MyOutBtn7_Click(object sender, EventArgs e)     //Out7按钮事件
        {
            if (LTDMC.dmc_read_outbit(usCardNum, 7) == 0)   //如果当前为输出低电平
            { LTDMC.dmc_write_outbit(usCardNum, 7, 1); }    //输出高电平
            else
            { LTDMC.dmc_write_outbit(usCardNum, 7, 0); }    //输出低电平
        }
        #endregion
    }
}
