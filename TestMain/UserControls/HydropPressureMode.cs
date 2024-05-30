using Sunny.UI;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestMain.Common;

namespace TestMain.UserControls
{
    public partial class HydropPressureMode : UserControl
    {
        private double setpoint = 50; // 设置值，即期望的压力值
        private double processValue = 0; // 初始过程值，即当前的压力值
        private Random random = new Random();
        private int index = 0;
        private PIDController pidController; // PID控制器实例
        private AlarmLogger alarmLogger;
        private DateTime dateTime;
        private ushort MyCardNo, Myaxis, Mymode, state, statemachine;

        public HydropPressureMode()
        {
            InitializeComponent();
            pidController = new PIDController(0.1, 0.01, 0.05); // 初始化PID控制器
        }

        private void HydropPressureMode_Load(object sender, EventArgs e)
        {
            index = 0;

            DateTime dt = new DateTime(2024, 5, 28);
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

        private void uiButton1_Click(object sender, EventArgs e)
        {
            try
            {
                index = 0;

                DateTime dt = new DateTime(2024, 5, 28);
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
                timer1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }
        //CardNo: 控制卡卡号  axis: 轴号  home_mode: 回零模式  Low_Vel: 回零低速
        //High_Vel: 回零高速  Tacc: 回零加速时间  Tdec: 回零减速时间 Offsetpos: 回零偏移
        private void uiButton7_Click(object sender, EventArgs e)
        {
            alarmLogger = new AlarmLogger();
            dateTime = DateTime.Now;
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
                        alarmLogger.SystemLogAlarm(dateTime + "压力模式：零点设置成功");
                    }
                    else
                    {
                        alarmLogger.SystemLogAlarm(dateTime + "压力模式：设置当前位置为零点失败，错误代码：" + ret);
                    }

                    // 释放板卡资源
                    LTDMC.dmc_board_close();
                }
                else
                {
                    alarmLogger.SystemLogAlarm(dateTime + "压力模式：初始化失败，错误代码：" + ret);
                }
            }
            catch (Exception ex)
            {
                alarmLogger.SystemLogAlarm(dateTime + "压力模式：设置零点异常：" + ex);
            }
            
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            LTDMC.dmc_stop(0, 0, 1);
        }

        private void timer1_Tick(object sender, EventArgs e)
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
            DateTime currentTime = DateTime.Now;

            uiLineChart1.Option.AddData("Line1", currentTime, processValue);

            index++;

            if (index > 50)
            {
                // uiLineChart1.Option.XAxis.SetRange(index - 50, index + 20); // 设置X轴显示范围
            }

            uiLineChart1.Refresh(); // 刷新图表

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

        private void uiButton8_Click(object sender, EventArgs e)
        {
            AlarmControls alarmControls = new AlarmControls();
            alarmControls.AppendTextToRichTextBox();
        }

        private void uiButton9_Click(object sender, EventArgs e)
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
                    alarmLogger.SystemLogAlarm(dateTime + "压力模式：回零正常完成");
                }
            }
        }
    }
}
