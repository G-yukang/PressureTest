using log4net;
using Sunny.UI;
using System;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestMain.Common;
using TestMain.Interfaces;
using TestMain.Model;
using TestMain.UserControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TestMain
{
    public partial class Main : UIForm2
    {
        #region 全局字段
        private AlarmLogger alarmLogger;
        private UserControl currentControl;
        private double dCmdPos;
        private double CurSpeed;
        private double dEnPos;
        private uint actualValue;
        private uint targetValue;
        private PIDController pidController;
        static SerialPort _serialPort;
        #endregion

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            alarmLogger = new AlarmLogger();
            pidController = new PIDController(1.0, 0.5, 0.1, 0.01); // 初始化PID控制器
            InitializeSportsCard(); // 运动卡初始化
            InitializeNavigationMenu("手动模式"); // 初始化导航菜单为手动模式
            timer1.Start();
            // 启动真空度读取
            StartVacuumReading();
        }

        #region 串口真空
        public void StartVacuumReading()
        {
            string portName = "COM3"; // 根据实际情况更改COM端口
            int baudRate = 38400; // 根据设备设置更改波特率

            Thread readThread = new Thread(() => ReadFromSerialPort(portName, baudRate));
            readThread.IsBackground = true;
            readThread.Start();
        }
        private void ReadFromSerialPort(string portName, int baudRate)
        {
            using (SerialPort serialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One))
            {
                try
                {
                    // 打开串口
                    serialPort.Open();
                    Console.WriteLine("串口打开成功");

                    while (true)
                    {
                        string dataToSend = ":01D45\r";
                        serialPort.WriteLine(dataToSend);
                        Console.WriteLine($"发送数据: {dataToSend}");

                        Thread.Sleep(500); // 根据需要调整等待时间

                        if (serialPort.BytesToRead > 0)
                        {
                            int len = serialPort.BytesToRead; //获取可以读取的字节数
                            byte[] buff = new byte[len];
                            serialPort.Read(buff, 0, len);//把数据读取到数组中
                            string result = Encoding.Default.GetString(buff);//将byte值根据为ASCII值转为string

                            if (!string.IsNullOrEmpty(result))
                            {
                                // 解析和转换数据
                                ProcessReceivedData(result);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"串口通信发生错误: {ex.Message}");
                }
            }
        }
        private void ProcessReceivedData(string data)
        {
            if (data.StartsWith(":01"))
            {
                // 去除换行符
                string trimmedData = data.Trim();

                // 提取科学计数法部分并转换
                string sciNotation = trimmedData.Substring(4, 8); // "1.00E-01"
                string actualValue = ConvertScientificToValue(sciNotation);
                if (!string.IsNullOrEmpty(actualValue))
                {
                    UpdateVacuumText(actualValue);
                }
                else
                {
                    UpdateVacuumText("F.FF");
                }
            }
            else
            {
                Console.WriteLine("接收到的数据格式不正确");
            }
        }
        private string ConvertScientificToValue(string sciNotation)
        {
            if (double.TryParse(sciNotation, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double value))
            {
                return (value).ToString();
            }
            return "";
        }
        private void UpdateVacuumText(string text)
        {
            if (vacuotext.InvokeRequired)
            {
                vacuotext.Invoke(new Action(() => vacuotext.Text = text));
                GlobalDate.Vacuometer = text;
            }
            else
            {
                vacuotext.Text = text;
                GlobalDate.Vacuometer = text;
            }
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                // 从指定地址读取实际转矩值
                PressDate.actualValue = LTDMC.nmc_read_rxpdo_extra_uint(PressDate.cardNo, PressDate.portNum, PressDate.actualTorqueAddress, PressDate.dataLen, ref actualValue);

                // 读取指定轴的当前速度并更新速度显示
                PressDate.CurSpeed = LTDMC.dmc_read_current_speed_unit(PressDate._CardID, 0, ref CurSpeed);
                sun_Speed.Text = PressDate.CurSpeed.ToString();

                // 读取压力指令值并将其转换为有意义的单位
                int value0 = 0;
                LTDMC.nmc_read_txpdo_extra(PressDate._CardID, 2, 0, 1, ref value0);
                double value00 = ConvertVoltageToPressure(value0);
                textBox5.Text = value00.ToString();
                PressDate.PressureVD = Math.Round(value00, 2);

                // 读取指定轴的指令位置值并更新显示
                PressDate.dCmdPos = LTDMC.dmc_get_position_unit(PressDate._CardID, 0, ref dCmdPos);
                tb_CurrentPos.Text = dCmdPos.ToString();
                PressDate.CurrentPos = dCmdPos;

                // 读取指定轴的编码器反馈值并更新显示
                PressDate.dEnPos = LTDMC.dmc_get_encoder_unit(PressDate._CardID, 0, ref dEnPos);
                tb_Encoder.Text = PressDate.dEnPos.ToString();

                // 根据轴的运动状态更新运行状态显示
                tb_RunState.Text = LTDMC.dmc_check_done(PressDate._CardID, 0) == 0 ? "运行中" : "停止中";

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
        private void InitializeSportsCard()
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

                PressDate._CardID = cardids[0];
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

            return (voltage / 10.0) * 30.0;
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
