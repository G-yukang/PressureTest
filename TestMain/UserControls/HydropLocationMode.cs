using S7.Net.Types;
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
using System.Windows.Forms.DataVisualization.Charting;
using TestMain.Common;
using static MongoDB.Driver.WriteConcern;
using static System.Windows.Forms.AxHost;

namespace TestMain.UserControls
{
    public partial class HydropLocationMode : UserControl
    {
        private double setpoint = 50; // 设置值，即期望的压力值
        private double processValue = 0; // 初始过程值，即当前的压力值
        private Random random = new Random();
        private int index = 0;
        private PIDController pidController; // PID控制器实例
        private FlowPanel flowPanel;
        private AlarmLogger alarmLogger;
        private System.DateTime dateTime;
        private ushort MyCardNo, Myaxis, Mymode, state, statemachine;
        private Parameter parameter;
        private ushort _CardID;
        private double PressureVD;          // 压力      
        private double dCmdPos;             // 当前位置
        private double CurSpeed;            // 当前速度

        public HydropLocationMode()
        {
            InitializeComponent();
            pidController = new PIDController(0.1, 0.01, 0.05); // 初始化PID控制器
        }

        private void HydropLocationMode_Load(object sender, EventArgs e)
        {
            index = 0;

            System.DateTime dt = new System.DateTime(2024, 5, 28);
            UILineOption option = new UILineOption
            {
                Title = new UITitle { Text = "压力实时图" }, // 设置图表标题
                XAxisType = UIAxisType.DateTime, // X轴类型为日期时间
                ToolTip = { Visible = true }, // 启用工具提示
                GreaterWarningArea = new UILineWarningArea(100), // 设置高于50的警告区域
                LessWarningArea = new UILineWarningArea(20, Color.Gold), // 设置低于20的警告区域，颜色为金色
                YAxisScaleLines = { new UIScaleLine("上限", 100, Color.Red), new UIScaleLine("下限", 10, Color.Gold) }, // 设置Y轴标尺线，上限为50，颜色为红色，下限为20，颜色为金色
                XAxis = { Name = "日期", AxisLabel = { DateTimeFormat = "yyyy-MM-dd HH:mm", DecimalPlaces = 1 } }, // 设置X轴名称和标签格式
                YAxis = { Name = "压力", AxisLabel = { DecimalPlaces = 1 } } // 设置Y轴名称和标签小数位数
            };

            var series = option.AddSeries(new UILineSeries("Line1")); // 添加数据系列
            series.SetMaxCount(300); // 设置数据系列最大数量

            uiLineChart1.SetOption(option); // 应用图表选项
        }
        //停止
        private void uiButton2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            LTDMC.dmc_stop(0, 0, 0);
        }
        //启动
        private void uiButton1_Click(object sender, EventArgs e)
        {
            // 调用电机控制器的启动方法
            parameter.StartMotor();

            //开始读取实时压力
            timer1.Start();
            //画线
            Cracc();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //实时数据
            RealTime();

            // 获取当前时间作为X轴值
            System.DateTime currentTime = System.DateTime.Now;

            uiLineChart1.Option.AddData("Line1", currentTime, PressureVD);

            uiLineChart1.Refresh(); // 刷新图表

            //模拟数据
            // SimulatedData();
        }
        private void RealTime()
        {
            // 读取指定轴的当前速度并更新速度显示
            LTDMC.dmc_read_current_speed_unit(_CardID, 0, ref CurSpeed);
            SpeedTextBox.Text = CurSpeed.ToString();

            // 读取压力指令值并将其转换为有意义的单位
            int value0 = 0;
            LTDMC.nmc_read_txpdo_extra(_CardID, 2, 0, 1, ref value0);
            double value00 = ConvertVoltageToPressure(value0);
            PressureVD = Math.Round(value00, 2);

            // 读取指定轴的指令位置值并更新显示
            LTDMC.dmc_get_position_unit(_CardID, 0, ref dCmdPos);
            LocationTextBox.Text = dCmdPos.ToString();
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
        //画线
        private void Cracc()
        {
            try
            {
                index = 0;
                System.DateTime dt = new System.DateTime(2024, 5, 28);
                UILineOption option = new UILineOption
                {
                    Title = new UITitle { Text = "压力实时图" }, // 设置图表标题
                    XAxisType = UIAxisType.DateTime, // X轴类型为日期时间
                    ToolTip = { Visible = true }, // 启用工具提示
                    GreaterWarningArea = new UILineWarningArea(100), // 设置高于50的警告区域
                    LessWarningArea = new UILineWarningArea(20, Color.Gold), // 设置低于20的警告区域，颜色为金色
                    YAxisScaleLines = { new UIScaleLine("上限", 100, Color.Red), new UIScaleLine("下限", 10, Color.Gold) }, // 设置Y轴标尺线，上限为50，颜色为红色，下限为20，颜色为金色
                    XAxis = { Name = "日期", AxisLabel = { DateTimeFormat = "yyyy-MM-dd HH:mm", DecimalPlaces = 1 } }, // 设置X轴名称和标签格式
                    YAxis = { Name = "压力", AxisLabel = { DecimalPlaces = 1 } } // 设置Y轴名称和标签小数位数
                };

                var series = option.AddSeries(new UILineSeries("Line1")); // 添加数据系列
                series.SetMaxCount(300); // 设置数据系列最大数量

                uiLineChart1.SetOption(option); // 应用图表选项

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        //模拟数据
        private void SimulatedData()
        {
            // 生成一个随机的模拟过程值，模拟一些噪声
            double noise = (random.NextDouble() - 0.5) * 2.5; // 调整噪声范围为-2.5到2.5
            double simulatedProcessValue = processValue + noise;

            // 平滑启动：逐步增加设定值
            double smoothSetpoint = setpoint * (index / 100.0);
            if (smoothSetpoint > setpoint)
            {
                smoothSetpoint = setpoint;
            }

            // 计算PID控制器的输出
            double output = pidController.CalculateOutput(smoothSetpoint, simulatedProcessValue);

            // 更新过程值
            processValue += output;
            processValue = Math.Max(0, Math.Min(100, processValue)); // 限制processValue在0到100之间

            // 获取当前时间作为X轴值
            System.DateTime currentTime = System.DateTime.Now;

            uiLineChart1.Option.AddData("Line1", currentTime, processValue);

            uiLineChart1.Refresh(); // 刷新图表
            index++;

            if (index > 50)
            {
                // uiLineChart1.Option.XAxis.SetRange(index - 50, index + 20); // 设置X轴显示范围
            }



            // 动态调整PID参数示例
            if (processValue > 90) // 如果过程值超过90
            {
                pidController.UpdateParameters(1.0, 0.01, 0.02); // 更新PID参数
            }

            // 动态调整输出限制示例
            if (processValue < 10) // 如果过程值低于10
            {
                pidController.UpdateOutputLimits(-50, 50); // 更新输出限制
            }
        }
        private double value_AD(ushort SubIndex, int value)
        {
            try
            {
                // 根据不同电压模式量程转换值
                int Value = 0;
                double _value = 1;
                LTDMC.nmc_get_node_od(_CardID, 2, 0, 32768, (ushort)(SubIndex + 1), 8, ref Value);
                switch (Value)
                {
                    case 0: // 电压模式量程±5V
                        _value = value * 5 / 32000.0;
                        break;
                    case 1: // 电压模式量程1-5V
                        _value = value * 4 / 32000.0 + 1;
                        break;
                    case 2: // 电压模式量程±10V
                        _value = value * 10 / 32000.0;
                        break;
                    case 3: // 电压模式量程0-10V
                        _value = value * 10 / 32000.0;
                        break;
                }
                return _value;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void uiButton8_Click(object sender, EventArgs e)
        {
            AlarmControls alarmControls = new AlarmControls();
            alarmControls.AppendTextToRichTextBox();
        }
        private void uiButton9_Click(object sender, EventArgs e)
        {
            try
            {
                MyCardNo = 0; //卡号
                Myaxis = 0; //轴号
                state = 0;
                statemachine = 0;
                Mymode = 33; //回零方式为 33
                LTDMC.nmc_get_axis_state_machine(MyCardNo, Myaxis, ref statemachine);//获取轴状态机
                if (statemachine == 4) //监控轴状态机的值，该值等于 4 表示轴状态机处于准备好状态
                {
                    LTDMC.nmc_set_home_profile(MyCardNo, Myaxis, Mymode, 500, 1000, 0.1, 0.1, 0);
                    //设置回原点模式 ,设置 0 号轴梯形速度曲线参数
                    LTDMC.nmc_home_move(MyCardNo, Myaxis);//执行回原点运动
                    while (LTDMC.dmc_check_done(MyCardNo, Myaxis) == 0)// 判断轴运动状态，等待回零运动完成
                    {
                        Application.DoEvents();
                    }
                    LTDMC.dmc_get_home_result(MyCardNo, Myaxis, ref state);//判断回零是否正常完成。
                    if (state == 1) //回零正常完成
                    {
                        MessageBox.Show("回零完成");
                    }
                }
            }
            catch (Exception ex)
            {
                alarmLogger.SystemLogAlarm(dateTime + "位置模式：回零异常：" + ex);
            }

        }

        private void uiButton7_Click(object sender, EventArgs e)
        {
            alarmLogger = new AlarmLogger();
            dateTime = System.DateTime.Now;
            ushort MyCardNo = 0;
            ushort MyAxis = 0;
            int MyPosition = 0;
            try
            {
                // 初始化板卡
                int ret = LTDMC.dmc_board_init();
                if (ret == 0)
                {
                    // 设置当前位置为零点
                    ret = LTDMC.dmc_set_position(MyCardNo, MyAxis, MyPosition);
                    if (ret == 0)
                    {
                        alarmLogger.SystemLogAlarm(dateTime + "位置模式：零点设置成功");
                    }
                    else
                    {
                        alarmLogger.SystemLogAlarm(dateTime + "位置模式：设置当前位置为零点失败，错误代码：" + ret);
                    }

                    // 释放板卡资源
                    LTDMC.dmc_board_close();
                }
                else
                {
                    alarmLogger.SystemLogAlarm(dateTime + "位置模式：初始化失败，错误代码：" + ret);
                }
            }
            catch (Exception ex)
            {
                alarmLogger.SystemLogAlarm(dateTime + "位置模式：设置零点异常：" + ex);
            }
        }
    }
}
