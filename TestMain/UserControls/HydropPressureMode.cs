using Sunny.UI;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
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
        private string ConfigFilePath;

        public HydropPressureMode()
        {
            InitializeComponent();

            chart1.MouseDoubleClick += Chart1_MouseDoubleClick;

            pidController = new PIDController(0.1, 0.01, 0.05); // 初始化PID控制器
        }

        private void HydropPressureMode_Load(object sender, EventArgs e)
        {
            // 清除默认的ChartArea和Series
            chart1.ChartAreas.Clear();
            chart1.Series.Clear();

            // 设置Chart控件的背景颜色为纯白色
            chart1.BackColor = System.Drawing.Color.White;
            chart1.ChartAreas.Add(new ChartArea("Default"));
            chart1.ChartAreas[0].BackColor = System.Drawing.Color.White;

            // 隐藏导航栏
            chart1.ChartAreas[0].AxisX.ScrollBar.Enabled = false;
            chart1.ChartAreas[0].AxisY.ScrollBar.Enabled = false;

            // 配置ChartArea
            ChartArea chartArea = chart1.ChartAreas["Default"];
            chartArea.AxisX.Title = "时间";
            chartArea.AxisX.IntervalType = DateTimeIntervalType.Seconds;
            chartArea.AxisX.LabelStyle.Format = "HH:mm:ss";
            chartArea.AxisX.LineColor = System.Drawing.Color.Black;
            chartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;

            chartArea.AxisY.Title = "压力";
            chartArea.AxisY.LineColor = System.Drawing.Color.Blue;
            chartArea.AxisY.LabelStyle.ForeColor = System.Drawing.Color.Blue;
            chartArea.AxisY.LabelStyle.Angle = 90; // 竖直显示标签
            chartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;

            chartArea.AxisY2.Enabled = AxisEnabled.True;
            chartArea.AxisY2.Title = "位置";
            chartArea.AxisY2.LineColor = System.Drawing.Color.Yellow;
            chartArea.AxisY2.LabelStyle.ForeColor = System.Drawing.Color.Yellow;
            chartArea.AxisY2.LabelStyle.Angle = -90; // 竖直显示标签
            chartArea.AxisY2.MajorGrid.LineColor = System.Drawing.Color.LightGray;

            // 从文件中读取范围值并设置轴范围
            LoadAxisConfig(chartArea.AxisY, "压力");
            LoadAxisConfig(chartArea.AxisY2, "位置");

            // 创建Series并绑定到第一个Y轴 (折线图)
            Series pressureSeries = new Series
            {
                Name = "压力",
                ChartType = SeriesChartType.Spline,
                Color = System.Drawing.Color.Blue,
                BorderWidth = 2
            };

            // 创建Series并绑定到第二个Y轴 (样条曲线图)
            Series positionSeries = new Series
            {
                Name = "位置",
                ChartType = SeriesChartType.Spline,
                Color = System.Drawing.Color.Yellow,
                BorderWidth = 2,
                YAxisType = AxisType.Secondary
            };

            // 添加数据点
            DateTime now = DateTime.Now;
            pressureSeries.Points.AddXY(now, 10);
            pressureSeries.Points.AddXY(now.AddSeconds(1), 20);
            pressureSeries.Points.AddXY(now.AddSeconds(2), 30);

            positionSeries.Points.AddXY(now, 5);
            positionSeries.Points.AddXY(now.AddSeconds(1), 15);
            positionSeries.Points.AddXY(now.AddSeconds(2), 25);

            // 将Series添加到Chart
            chart1.Series.Add(pressureSeries);
            chart1.Series.Add(positionSeries);

            // 初始化开始时间
            dateTime = DateTime.Now;

            // 启动定时器
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //真空计
            Vacuometertext.Text = GlobalDate.Vacuometer;


            // 获取当前时间
            DateTime now = DateTime.Now;

            // 生成模拟数据
            double pressureValue = random.Next(100, 1000);
            double positionValue = random.Next(100, 1000);

            // 添加数据点
            chart1.Series["压力"].Points.AddXY(now, pressureValue);
            chart1.Series["位置"].Points.AddXY(now, positionValue);

            // 移动X轴范围以显示最新的数据
            chart1.ChartAreas[0].AxisX.Minimum = dateTime.ToOADate();
            chart1.ChartAreas[0].AxisX.Maximum = DateTime.Now.ToOADate();


            Vacuometertext.Text = GlobalDate.Vacuometer;
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {

        }            
        private void EditAxisRange(Axis axis, string axisTitle)
        {
            using (var inputDialog = new InputDialog($"请输入新的{axisTitle}范围值（最小值,最大值）："))
            {
                if (inputDialog.ShowDialog() == DialogResult.OK)
                {
                    string input = inputDialog.InputText;
                    string[] values = input.Split(',');
                    if (values.Length == 2 && double.TryParse(values[0], out double minValue) && double.TryParse(values[1], out double maxValue))
                    {
                        axis.Minimum = minValue;
                        axis.Maximum = maxValue;
                        SaveAxisConfig(axisTitle, minValue, maxValue);
                    }
                    else
                    {
                        MessageBox.Show("输入无效，请输入有效的范围值（最小值,最大值）。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void Chart1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            HitTestResult hit = chart1.HitTest(e.X, e.Y);
            if (hit.ChartElementType == ChartElementType.AxisLabels)
            {
                if (hit.Axis.AxisName == AxisName.Y)
                {
                    EditAxisRange(hit.Axis, "压力");
                }
                else if (hit.Axis.AxisName == AxisName.Y2)
                {
                    EditAxisRange(hit.Axis, "位置");
                }
            }
        }

        private void SaveAxisConfig(string axisTitle, double minValue, double maxValue)
        {
            var lines = File.Exists(ConfigFilePath) ? File.ReadAllLines(ConfigFilePath).ToList() : new List<string>();
            var newLine = $"{axisTitle},{minValue},{maxValue}";
            var existingLineIndex = lines.FindIndex(line => line.StartsWith($"{axisTitle},"));
            if (existingLineIndex >= 0)
            {
                lines[existingLineIndex] = newLine;
            }
            else
            {
                lines.Add(newLine);
            }
            File.WriteAllLines(ConfigFilePath, lines);
        }
        private void LoadAxisConfig(Axis axis, string axisTitle)
        {
            if (File.Exists(ConfigFilePath))
            {
                var lines = File.ReadAllLines(ConfigFilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 3 && parts[0] == axisTitle && double.TryParse(parts[1], out double minValue) && double.TryParse(parts[2], out double maxValue))
                    {
                        axis.Minimum = minValue;
                        axis.Maximum = maxValue;
                    }
                }
            }
        }

        #region 点击事件
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
            LTDMC.dmc_stop(0, 0, 0);
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
        #endregion
    }
}
