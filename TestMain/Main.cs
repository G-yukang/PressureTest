using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using TestMain.Common;
using TestMain.Interfaces;
using TestMain.Model;
using TestMain.UserControls;

namespace TestMain
{
    public partial class Main : UIForm2
    {
        #region 全局变量
        private readonly Label[] arrLabel = new Label[8];
        private readonly List<PointF> arrPointList = new List<PointF>();
        private readonly Button[] arrButton = new Button[8];
        private AlarmLogger alarmLogger;
        private readonly XmlHandler<HydropressModel> xmlFileManager = new XmlHandler<HydropressModel>();
        private UserControl currentControl;

        private double qswz;                // 起始位置
        private double yxwz;                // 运行位置
        private double bhwz;                // 闭合位置
        private double kmwz1;               // 开模位置
        private double kmwz2;               // 开模位置
        private double kmwz3;               // 开模位置
        private double dStartVel;           // 起始速度
        private double dMaxVel;             // 运行速度
        private double dStopVel;            // 停止速度
        private double HdStartVel;          // 开模速度
        private double HdMaxVel;            // 开模速度
        private double HdStopVel;           // 开模速度        
        private double dTacc;               // 加速时间
        private double dTdec;               // 减速时间
        private double qsyl;                // 压力起始
        private double yxyl;                // 压力运行
        private double bhyl;                // 压力闭合
        private double kmyl;                // 压力开模

        private ushort _CardID;
        private double PressureVD;          // 压力      
        private double CurrentPos;          // 当前位置  
        private double dCmdPos;             // 指令位置
        private double CurSpeed;            // 当前速度
        private double dEnPos;              // 编码器反馈位置
        private ushort usCardNum;           // IO
        private bool startdian;

        // 压机
        private readonly ushort cardNo = 0;                   // 卡号
        private readonly ushort portNum = 1;                  // 端口号
        private readonly ushort actualTorqueAddress = 0x6077; // 实际转矩地址
        private readonly ushort targetTorqueAddress = 0x6071; // 目标转矩地址
        private readonly ushort dataLen = 4;                  // 数据长度
        private uint actualValue;                            // 实际示例值
        private uint targetValue;                            // 目标示例值

        private static bool _shouldStop;
        private Thread thread1;
        private Thread thread2;

        private double setpoint = 50; // 设置值，即期望的压力值
        private double processValue = 0; // 初始过程值，即当前的压力值
        private Random random = new Random();
        private int index = 0;
        private PIDController pidController; // PID控制器实例
        #endregion

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            alarmLogger = new AlarmLogger();
            pidController = new PIDController(1.0, 0.5, 0.1, 0.01); // 初始化PID控制器
            //运动卡初始化
            SportsCard();
            FLineChartSer();
            GlobalDate();

            InitializeNavigationMenu("手动模式");
            uiButton10.Visible = false;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                LTDMC.nmc_read_rxpdo_extra_uint(cardNo, portNum, actualTorqueAddress, dataLen, ref actualValue);



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
                //更新时间
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(() => datetimepanel.Text = DateTime.Now.ToString()));
                }
                else
                {
                    datetimepanel.Text = DateTime.Now.ToString();
                }
            }
            catch (Exception)
            {
            }
        }
        private void Linecharttimer_Tick(object sender, EventArgs e)
        {

        }

        #region 逻辑处理
        //运动卡初始化
        private void SportsCard()
        {
            try
            {
                short num = LTDMC.dmc_board_init();
                if (num <= 0 || num > 8)
                {
                    this.ShowWarningDialog("初始卡失败!出错");
                    alarmLogger.LogAlarm("初始卡失败!出错");
                }

                ushort _num = 0;
                ushort[] cardids = new ushort[8];
                uint[] cardtypes = new uint[8];
                short res = LTDMC.dmc_get_CardInfList(ref _num, cardtypes, cardids);

                if (res != 0)
                {
                    this.ShowWarningDialog("初始卡失败!出错");
                    alarmLogger.LogAlarm("初始卡失败!出错");
                }

                _CardID = cardids[0];
                timer1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误: {ex.Message}");

                alarmLogger.LogAlarm($"发生错误: {ex.Message}");
            }

        }
        private void GlobalDate()
        {
            
        }
        public void FLineChartSer()
        {

        }
        public void FLineChartstop()
        {
            timer1.Stop(); // 启动定时器
        }
        private double value_AD(ushort SubIndex, int value)
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
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Event Handlers 点击事件
        // 报警复位逻辑
        private void uiButton8_Click(object sender, EventArgs e)
        {
            AlarmControls alarmControls = new AlarmControls();
            alarmControls.AppendTextToRichTextBox();

        }
        // 矫正置零逻辑
        private void uiButton7_Click(object sender, EventArgs e)
        {
            LTDMC.nmc_write_rxpdo_extra_uint(cardNo, portNum, 0x6098, 4, 35);
        }
        //设置零点
        private void uiButton9_Click(object sender, EventArgs e)
        {
            LTDMC.nmc_write_rxpdo_extra_uint(cardNo, portNum, 0x6098, 4, 35);
        }

        private void uiButton10_Click(object sender, EventArgs e)
        {
            InitializeNavigationMenu("参数配置");
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            InitializeNavigationMenu("手动模式");
            uiButton10.Visible = false;
        }

        private void uiButton6_Click(object sender, EventArgs e)
        {
            InitializeNavigationMenu("报警信息");
            uiButton10.Visible = false;
        }

        private void uiButton3_Click(object sender, EventArgs e)
        {
            InitializeNavigationMenu("位置模式");
            uiButton10.Visible = true;
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            InitializeNavigationMenu("压力模式");
            uiButton10.Visible = false;
        }

        private void uiButton4_Click(object sender, EventArgs e)
        {
            InitializeNavigationMenu("自动模式");
            uiButton10.Visible = true;
        }

        #endregion

        #region Private Methods权限

        private void InitializeNavigationMenu(string type)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(InitializeNavigationMenu), type);
                return;
            }
            try
            {
                titlepanel.Text = string.Empty;

                // 移除并释放当前控件
                if (currentControl != null)
                {
                    uiPanel8.Controls.Remove(currentControl);
                    uiPanel4.Controls.Clear();
                    currentControl.Dispose();
                    currentControl = null;
                }

                // 创建并添加新控件
                currentControl = CreateControlByType(type);
                if (currentControl != null)
                {
                    uiPanel8.Controls.Add(currentControl);
                    currentControl.Dock = DockStyle.Fill;
                    currentControl.Show();
                }

                titlepanel.Text = "当前模式: " + type;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误: {ex.Message}");
            }
        }

        private UserControl CreateControlByType(string type)
        {
            switch (type)
            {
                case "手动模式":
                    var uiControls = new IOControls();
                    uiPanel4.Controls.Add(uiControls);
                    uiControls.Dock = DockStyle.Fill;
                    uiControls.Show();
                    return new HydropManualMode();
                case "自动模式":
                    // 自动模式逻辑
                    return null;
                case "压力模式":
                    var uiControls1 = new IOControls();
                    uiPanel4.Controls.Add(uiControls1);
                    uiControls1.Dock = DockStyle.Fill;
                    uiControls1.Show();
                    return new HydropPressureMode();
                case "位置模式":
                    var uiControls2 = new IOControls();
                    uiPanel4.Controls.Add(uiControls2);
                    uiControls2.Dock = DockStyle.Fill;
                    uiControls2.Show();
                    return new HydropLocationMode();
                case "参数配置":
                    return new FlowPanel();
                case "报警信息":
                    return new AlarmControls();
                default:
                    return new HydropManualMode();
            }
        }

        #endregion


    }
}
