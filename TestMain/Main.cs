using log4net;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
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
        private uint actualValue;                             // 实际示例值
        private uint targetValue;                             // 目标示例值

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
            SportsCard(); // 运动卡初始化
            InitializeNavigationMenu("手动模式"); // 初始化导航菜单为手动模式
            //真空值

            Vacouot();
        }

        public async Task Vacouot()
        {
            // 设置串口参数
            string portName = "COM3"; // 请根据实际情况更改COM端口
            int baudRate = 38400; // 请根据设备设置更改波特率

            using (SerialPort serialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One))
            {
                try
                {
                    // 打开串口
                    serialPort.Open();
                    serialPort.ReadTimeout = 5000; // 设置读取超时为5000毫秒
                    Console.WriteLine("串口打开成功");

                    while (true)
                    {
                        await ReadVacuumAsync(serialPort, 1);
                        await Task.Delay(1000);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("串口通信发生错误: " + ex.Message);
                }
            }
        }

        private static async Task ReadVacuumAsync(SerialPort serialPort, int deviceAddress)
        {
            string address = deviceAddress.ToString("D2");

            string command = $"{address}D";
            string checksum = CalculateChecksum(command);
            command = $":{command}{checksum}\r";

            serialPort.WriteLine(command);

            try
            {
                string response = await Task.Run(() =>
                {
                    try
                    {
                        return serialPort.ReadLine();
                    }
                    catch (TimeoutException)
                    {
                        return null;
                    }
                });

                if (response == null)
                {
                    Console.WriteLine("读取超时");
                    return;
                }

                Console.WriteLine("设备响应: " + response);

                // 处理响应数据并验证校验和
                if (response.StartsWith($":{address}") && response.Length > 1)
                {
                    string responseData = response.Substring(1, response.Length - 4); // 去掉冒号和最后的校验和与CR
                    string responseChecksum = response.Substring(response.Length - 3, 2); // 提取响应中的校验和
                    string calculatedChecksum = CalculateChecksum(responseData);

                    if (responseChecksum.Equals(calculatedChecksum, StringComparison.OrdinalIgnoreCase))
                    {
                        // 提取真空度值
                        int startIndex = response.IndexOf('D') + 1;
                        int endIndex = response.IndexOf(' ', startIndex);
                        if (endIndex == -1) endIndex = response.Length - 4; // 如果没有空格，则到结尾前4个字符
                        string vacuumReading = response.Substring(startIndex, endIndex - startIndex);
                        Console.WriteLine("真空度: " + vacuumReading);
                    }
                    else
                    {
                        Console.WriteLine("校验和不匹配");
                    }
                }
                else
                {
                    Console.WriteLine("无效的响应数据或地址不匹配");
                }
            }
            catch (TimeoutException)
            {
                Console.WriteLine("读取超时");
            }
            catch (Exception ex)
            {
                Console.WriteLine("处理响应时发生错误: " + ex.Message);
            }
        }

        // 计算校验和
        private static string CalculateChecksum(string input)
        {
            int checksum = 0;
            foreach (char c in input)
            {
                checksum ^= c;
            }
            return checksum.ToString("X2");
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                // 从指定地址读取实际转矩值
                LTDMC.nmc_read_rxpdo_extra_uint(cardNo, portNum, actualTorqueAddress, dataLen, ref actualValue);

                // 读取指定轴的当前速度并更新速度显示
                LTDMC.dmc_read_current_speed_unit(_CardID, 0, ref CurSpeed);
                sun_Speed.Text = CurSpeed.ToString();

                // 读取压力指令值并将其转换为有意义的单位
                int value0 = 0;
                LTDMC.nmc_read_txpdo_extra(_CardID, 2, 0, 1, ref value0);
                double value00 = ConvertVoltageToPressure(value0);
                textBox5.Text = value00.ToString();
                PressureVD = Math.Round(value00, 2);

                // 读取指定轴的指令位置值并更新显示
                LTDMC.dmc_get_position_unit(_CardID, 0, ref dCmdPos);
                tb_CurrentPos.Text = dCmdPos.ToString();
                CurrentPos = dCmdPos;

                // 读取指定轴的编码器反馈值并更新显示
                LTDMC.dmc_get_encoder_unit(_CardID, 0, ref dEnPos);
                tb_Encoder.Text = dEnPos.ToString();

                // 根据轴的运动状态更新运行状态显示
                tb_RunState.Text = LTDMC.dmc_check_done(_CardID, 0) == 0 ? "运行中" : "停止中";

                // 读取轴状态机的状态并更新显示
                ushort usAxisStateMachine = 0;
                LTDMC.nmc_get_axis_state_machine(0, 0, ref usAxisStateMachine);
                UpdateAxisState(usAxisStateMachine);

                // 读取EtherCAT总线状态并更新显示
                ushort usErrorCode = 0;
                LTDMC.nmc_get_errcode(0, 2, ref usErrorCode);
                UpdateEthercatState(usErrorCode);

                // 更新当前时间显示
                datetimepanel.Text = DateTime.Now.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误: {ex.Message}");
                alarmLogger.LogAlarm($"发生错误: {ex.Message}");
            }
        }

        private void UpdateAxisState(ushort usAxisStateMachine)
        {
            // 更新轴状态机状态的文本和颜色
            switch (usAxisStateMachine)
            {
                case 0:
                    SetStateMachineText("轴处于未启动状态", Color.Red);
                    break;
                case 1:
                    SetStateMachineText("轴处于启动禁止状态", Color.Red);
                    break;
                case 2:
                    SetStateMachineText("轴处于准备启动状态", Color.Red);
                    break;
                case 3:
                    SetStateMachineText("轴处于启动状态", Color.Red);
                    break;
                case 4:
                    SetStateMachineText("轴处于操作使能状态", Color.Green);
                    break;
                case 5:
                    SetStateMachineText("轴处于停止状态", Color.Red);
                    break;
                case 6:
                    SetStateMachineText("轴处于错误触发状态", Color.Red);
                    break;
                case 7:
                    SetStateMachineText("轴处于错误状态", Color.Red);
                    break;
            }
        }

        private void SetStateMachineText(string text, Color color)
        {
            // 设置状态机文本和背景颜色
            textBox_StateMachine.Text = text;
            textBox_StateMachine.BackColor = color;
        }

        private void UpdateEthercatState(ushort usErrorCode)
        {
            // 更新EtherCAT总线状态的文本和颜色
            textBox_EthercatState.Text = usErrorCode == 0 ? "EtherCAT总线正常" : "EtherCAT总线出错";
            textBox_EthercatState.BackColor = usErrorCode == 0 ? Color.Green : Color.Red;
        }

        #region 逻辑处理
        private void SportsCard()
        {
            try
            {
                // 初始化运动卡
                short num = LTDMC.dmc_board_init();
                if (num <= 0 || num > 8)
                {
                    ShowInitializationError();
                    return;
                }

                // 获取运动卡信息
                ushort _num = 0;
                ushort[] cardids = new ushort[8];
                uint[] cardtypes = new uint[8];
                short res = LTDMC.dmc_get_CardInfList(ref _num, cardtypes, cardids);

                if (res != 0)
                {
                    ShowInitializationError();
                    return;
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

        private void ShowInitializationError()
        {
            // 显示初始化错误信息
            this.ShowWarningDialog("初始卡失败!出错");
            alarmLogger.LogAlarm("初始卡失败!出错");
        }
        public static double ConvertVoltageToPressure(double voltage)
        {
            if (voltage < 0 || voltage > 10)
            {
                return 0;
            }

            double pressure = (voltage / 10.0) * 30.0;
            return pressure;
        }
        #endregion

        #region 点击事件
        private void uiButton1_Click(object sender, EventArgs e)
        {
            InitializeNavigationMenu("手动模式");
        }

        private void uiButton6_Click(object sender, EventArgs e)
        {
            InitializeNavigationMenu("报警信息");
        }

        private void uiButton3_Click(object sender, EventArgs e)
        {
            InitializeNavigationMenu("位置模式");
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            InitializeNavigationMenu("压力模式");
        }

        private void uiButton4_Click(object sender, EventArgs e)
        {
            InitializeNavigationMenu("自动模式");
        }
        #endregion

        #region 权限处理
        public void InitializeNavigationMenu(string type)
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
                    AddControlToPanel(new IOControls());
                    return new HydropManualMode();
                case "自动模式":
                    AddControlToPanel(new IOControls("自动模式"));
                    return new HydropAutomatic();
                case "压力模式":
                    AddControlToPanel(new IOControls());
                    return new HydropPressureMode();
                case "位置模式":
                    AddControlToPanel(new IOControls());
                    return new HydropLocationMode();
                case "参数配置":
                    return new FlowPanel();
                case "报警信息":
                    return new AlarmControls();
                default:
                    return new HydropManualMode();
            }
        }

        private void AddControlToPanel(UserControl control)
        {
            // 清理面板上的现有控件
            uiPanel4.Controls.Clear();

            // 添加新的控件到面板
            uiPanel4.Controls.Add(control);
            control.Dock = DockStyle.Fill;
            control.Show();
        }
        #endregion
    }
}
