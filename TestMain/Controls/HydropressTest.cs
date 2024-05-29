using S7.Net.Types;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using TestMain.Controls;
using TestMain.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace TestMain
{
    public partial class HydropressTest : UIPage
    {
        #region 全局参数

        Label[] arrLabel = new Label[8];
        private List<PointF> arrPonitList = new List<PointF>();
        //定义输出IO的按钮控件
        System.Windows.Forms.Button[] arrButton = new System.Windows.Forms.Button[8];

        XmlHandler<HydropressModel> xmlFileManager = new XmlHandler<HydropressModel>();

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
        private bool startdian;

        //压机
        private ushort cardNo = 0;                   // 例子中的卡号
        private ushort portNum = 1;                  // 端口号
        private ushort actualTorqueAddress = 0x6077; //实际转矩地址
        private ushort targetTorqueAddress = 0x6071; //目标转矩地址
        private ushort dataLen = 4;                  // 数据长度
        private uint actualValue;                   // 实际示例值
        private uint targetValue;             // 目标示例值

        private static bool _shouldStop;
        private Thread thread1;
        private Thread thread2;
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
                if (num <= 0 || num > 8)
                {
                    this.ShowWarningDialog("初始卡失败!出错");
                    Messagetextbox.AppendText("初始卡失败!出错");
                }
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
                Alarmtextbox.AppendText("初始卡失败!出错" + ex + "\n");
                throw ex;
            }

        }

        #region 定时事件
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                LTDMC.nmc_read_rxpdo_extra_uint(cardNo, portNum, actualTorqueAddress, dataLen, ref actualValue);


                uiPanel4.Text = System.DateTime.Now.DateTimeString();

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
            catch (Exception ex)
            {
                Alarmtextbox.AppendText("Timer异常!出错" + ex + "\n");
            }
        }
        private void simtimer_Tick(object sender, EventArgs e)
        {

        }
        #endregion

        #region 按钮事件  
        private void uiButton5_MouseDown(object sender, MouseEventArgs e)
        {
            thread2 = new Thread(Threadstartadd);
            thread2.Start();
            startdian = true;
        }

        private void uiButton5_MouseClick(object sender, MouseEventArgs e)
        {

            startdian = false;
        }

        private void uiButton6_MouseClick(object sender, MouseEventArgs e)
        {
            thread2 = new Thread(Threadstartminus);
            thread2.Start();
            startdian = true;
        }

        private void uiButton6_MouseDown(object sender, MouseEventArgs e)
        {
            startdian = false;
        }
        public void Threadstartadd()
        {
            while (startdian)
            {

                Thread.Sleep(100);
            }
        }
        public void Threadstartminus()
        {
            while (startdian)
            {

                Thread.Sleep(100);
            }
        }
        private void btn_ChangeVel_Click(object sender, EventArgs e)
        {
            try
            {
                double dNewVel = nud_NewVel.Value;               // 新的运行速度
                double dTaccDec = nud_TaccDec.Value;             //变速时间
                Speedunit(dNewVel, dTaccDec);
            }
            catch (Exception ex)
            {
                Alarmtextbox.AppendText("在线变速!出错" + ex + "\n");
            }
        }
        private void button_HardwareReset_Click(object sender, EventArgs e)
        {
            try
            {
                Messagetextbox.AppendText("请勿操作，总线卡硬件复位进行中……预计30s时间" + "\n");
                button_HardwareReset.Enabled = false;
                LTDMC.dmc_board_reset();
                LTDMC.dmc_board_close();

                for (int i = 0; i < 15; i++)//总线卡硬件复位耗时15s左右
                {
                    Application.DoEvents();
                    Thread.Sleep(1000);
                }

                LTDMC.dmc_board_init();
                Messagetextbox.AppendText("总线卡硬件复位完成" + "\n");
                button_HardwareReset.Enabled = true;
                button_HardwareReset.Focus();

                Messagetextbox.AppendText("请勿操作，总线卡软件复位进行中……" + "\n");
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
                Messagetextbox.AppendText("总线卡软件复位完成,请确认总线状态");
                button_HardwareReset.Enabled = true;
                button_HardwareReset.Focus();
            }
            catch (Exception ex)
            {
                Alarmtextbox.AppendText("总线卡复位!出错" + ex + "\n");
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
        // 修改压机参数
        private void uiButton3_Click(object sender, EventArgs e)
        {
            try
            {
                cardNo = Convert.ToUInt16(carnotext.Text);                   // 例子中的卡号
                portNum = Convert.ToUInt16(prottext.Text);                   // 端口号
                actualTorqueAddress = Convert.ToUInt16(actualtext.Text, 16);   //实际转矩地址
                targetTorqueAddress = Convert.ToUInt16(targettext.Text, 16);     //目标转矩地址
                dataLen = Convert.ToUInt16(lenghtleng.Text);                  // 数据长度
                actualValue = Convert.ToUInt16(actualdate.Value);                   // 实际示例值
                targetValue = Convert.ToUInt16(targetdate.Value);                   // 目标示例值
            }
            catch (Exception ex)
            {
                Alarmtextbox.AppendText("修改压机参数!出错" + ex + "\n");
                throw;
            }

        }
        private void btn_Start_Click(object sender, EventArgs e)
        {
            try
            {
                // 防止多次点击启动多个线程
                if (thread1 != null && thread1.IsAlive)
                {
                    return;
                }

                _shouldStop = false;
                arrPonitList.Clear();
                RedingDate();

                thread1 = new Thread(new ThreadStart(ThreadMethod));
                thread1.IsBackground = true; // 设置为后台线程
                thread1.Start();

                //下压速度变化
                LTDMC.dmc_set_profile_unit(_CardID, 0, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);
                //下压
                LTDMC.dmc_vmove(_CardID, 0, 1);

                //等待保压
                thread1.Join();


                //开模关闭压力检测
                simtimer.Stop();
                //开模速度变化
                LTDMC.dmc_set_profile_unit(_CardID, 0, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);
                //上升
                LTDMC.dmc_vmove(_CardID, 0, 0);
            }
            catch (Exception ex)
            {
                Alarmtextbox.AppendText("启动流程!出错" + ex + "\n");
                throw;
            }
        }
        public void ThreadMethod()
        {
            try
            {
                LTDMC.nmc_write_rxpdo_extra_uint(cardNo, portNum, targetTorqueAddress, dataLen, targetValue);
                while (!_shouldStop)
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

                    //等待保压度和设置度
                    for (double i = PressureVD; i <= bhyl;)
                    {
                        i++;
                    }

                    // 等待保压度和设置度，避免忙等待
                    Thread.Sleep(100); // 每100ms检查一次
                }
            }
            catch (Exception ex)
            {
                Alarmtextbox.AppendText("启动子线程!出错" + ex + "\n");
                throw;
            }

        }
        private void StopThread()
        {
            try
            {
                if (thread1 != null && thread1.IsAlive)
                {
                    _shouldStop = true; // 设置标志变量

                    // 使用一个新线程来等待 thread1 退出，避免阻塞UI线程
                    new Thread(() =>
                    {
                        thread1.Join(); // 等待线程安全退出
                        thread1 = null; // 清空线程引用
                    }).Start();
                }
            }
            catch (Exception ex)
            {
                Alarmtextbox.AppendText("暂停程序!出错" + ex + "\n");
                throw;
            }

        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopThread();
            base.OnFormClosing(e);
        }
        private void btn_Stop_Click(object sender, EventArgs e)
        {
            StopThread();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //在线变位
            LTDMC.dmc_reset_target_position_unit(_CardID, 0, uiDoubleUpDown2.Value);
        }


        #endregion

        #region 逻辑处理
        //获取当前位置压力值
        public double Detection()
        {
            try
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
            catch (Exception ex)
            {
                Alarmtextbox.AppendText("获取当前位置压力值!出错" + ex + "\n");
                throw;
            }

        }
        //参数配置
        private void RedingDate()
        {
            try
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
            catch (Exception ex)
            {
                Alarmtextbox.AppendText("参数配置!出错" + ex + "\n");
                throw;
            }

        }
        //电压
        double value_AD(ushort SubIndex, int value)
        {
            try
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
            catch (Exception ex)
            {
                Alarmtextbox.AppendText("电压!出错" + ex + "\n");
                throw;
            }

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



        /// <param name="CardNo">卡号</param>
        /// <param name="PortNum">端口号</param>
        /// <param name="address">地址</param>
        /// <param name="DataLen">数据长度</param>
        /// <param name="Value">读取的值</param>
        private void uiButton4_Click(object sender, EventArgs e)
        {
            LTDMC.nmc_write_rxpdo_extra_uint(cardNo, portNum, 0x6098, 4, 35);
        }

    }
}
