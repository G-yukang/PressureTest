using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TestMain.Common;

namespace TestMain.UserControls
{
    public partial class HydropAutomatic : UserControl
    {
        #region 全局变量
        private double setpoint = 50; // 设置值，即期望的压力值
        private double processValue = 0; // 初始过程值，即当前的压力值
        private Random random = new Random();
        private int index = 0;
        private PIDController pidController; // PID控制器实例
        private FlowPanel flowPanel;
        private AlarmLogger alarmLogger;
        private System.DateTime dateTime;
        private ushort MyCardNo, MyAxis, Mymode, state, statemachine, MyDir;
        private string ConfigFilePath;
        #endregion

        public HydropAutomatic()
        {
            InitializeComponent();
            chart1.MouseDoubleClick += Chart1_MouseDoubleClick;

            // 设置配置文件路径
            ConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin\\FlowConfig\\chartParameter.ini");
            Directory.CreateDirectory(Path.GetDirectoryName(ConfigFilePath));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
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
        }

        private void HydropAutomatic_Load(object sender, EventArgs e)
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
            chartArea.AxisY2.LineColor = System.Drawing.Color.Lime;
            chartArea.AxisY2.LabelStyle.ForeColor = System.Drawing.Color.Lime;
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
                Color = System.Drawing.Color.Lime,
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
        //启动
        private void uiButton1_Click(object sender, EventArgs e)
        {
            ushort MyCardNo = 0;
            ushort MyAxis = 0;
            ushort MyDir = 1; // 正转方向
            // 启动电机
            LTDMC.dmc_vmove(MyCardNo, MyAxis, MyDir);
            timer1.Start();
        }
        //停止
        private void uiButton2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            LTDMC.dmc_stop(0, 0, 0);
        }
        //设置零点
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
        //清除报警
        private void uiButton8_Click(object sender, EventArgs e)
        {
            AlarmControls alarmControls = new AlarmControls();
            alarmControls.AppendTextToRichTextBox();
        }
        //回零
        private void uiButton9_Click(object sender, EventArgs e)
        {
            try
            {
                MyCardNo = 0; //卡号
                MyAxis = 0; //轴号
                state = 0;
                statemachine = 0;
                Mymode = 33; //回零方式为 33
                LTDMC.nmc_get_axis_state_machine(MyCardNo, MyAxis, ref statemachine);//获取轴状态机
                if (statemachine == 4) //监控轴状态机的值，该值等于 4 表示轴状态机处于准备好状态
                {
                    LTDMC.nmc_set_home_profile(MyCardNo, MyAxis, Mymode, 500, 1000, 0.1, 0.1, 0);
                    //设置回原点模式 ,设置 0 号轴梯形速度曲线参数
                    LTDMC.nmc_home_move(MyCardNo, MyAxis);//执行回原点运动
                    while (LTDMC.dmc_check_done(MyCardNo, MyAxis) == 0)// 判断轴运动状态，等待回零运动完成
                    {
                        Application.DoEvents();
                    }
                    LTDMC.dmc_get_home_result(MyCardNo, MyAxis, ref state);//判断回零是否正常完成。
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
        //参数配置
        private void uiButton10_Click(object sender, EventArgs e)
        {
            flowPanel = new FlowPanel();
            uiPanel3.Controls.Clear();

            uiPanel3.Controls.Add(flowPanel);
            flowPanel.Dock = DockStyle.Fill;
            flowPanel.Show();
        }
        #endregion
    }

    public class InputDialog : Form
    {
        private TextBox inputBox;
        public string InputText => inputBox.Text;

        public InputDialog(string prompt)
        {
            Text = "输入范围值";
            StartPosition = FormStartPosition.CenterParent;

            Label textLabel = new Label { Left = 50, Top = 20, Text = prompt, AutoSize = true };
            inputBox = new TextBox { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button { Text = "确认", Left = 350, Width = 100, Top = 80, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { Close(); };

            Controls.Add(textLabel);
            Controls.Add(inputBox);
            Controls.Add(confirmation);
            ClientSize = new System.Drawing.Size(500, 150);
            AcceptButton = confirmation;
        }
    }
}
