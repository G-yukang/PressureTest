using Sunny.UI;
using System;
using System.Drawing;
using System.Windows.Forms;
using TestMain.Common;

namespace TestMain.UserControls
{
    public partial class FLineChart : UserControl
    {
        private double setpoint = 50; // 设置值，即期望的压力值
        private double processValue = 0; // 初始过程值，即当前的压力值
        private Random random = new Random();
        private int index = 0;
        private PIDController pidController; // PID控制器实例

        public FLineChart()
        {
            InitializeComponent();
            pidController = new PIDController(0.1, 0.01, 0.05); // 初始化PID控制器
        }


        private void FLineChart_Load(object sender, EventArgs e)
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
                FLineChartSer();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        public void FLineChartSer()
        {
            if (!timer2.Enabled)
            {
                timer2.Start(); // 启动定时器
                Console.WriteLine("Timer started."); // 调试输出
            }
        }

        public void FLineChartstop()
        {
            if (timer2.Enabled)
            {
                timer2.Stop(); // 停止定时器
                Console.WriteLine("Timer stopped."); // 调试输出
            }
        }
        public void Timechart(double smoothSetpoint, double simulatedProcessValue)
        {
            // 生成一个随机的模拟过程值，模拟一些噪声
            double noise = (random.NextDouble() - 0.5) * 2.5; // 调整噪声范围为-2.5到2.5
            simulatedProcessValue = processValue + noise;

            // 平滑启动：逐步增加设定值
            smoothSetpoint = setpoint * (index / 100.0);
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

            // 将新的数据点添加到图表
            Console.WriteLine($"Adding data: Time={currentTime}, Value={processValue}"); // 调试输出
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

        private void timer2_Tick(object sender, EventArgs e)
        {
            // 调用Timechart方法更新图表
            Timechart(setpoint, processValue);
        }
    }
}
