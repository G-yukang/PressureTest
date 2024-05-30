using S7.Net.Types;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestMain.Common;
using static MongoDB.Driver.WriteConcern;
using static System.Windows.Forms.AxHost;

namespace TestMain.UserControls
{
    public partial class HydropManualMode : UserControl
    {
        #region 声明变量
        private FLineChart fLineChart = new FLineChart();
        private UInt16 cardNo = 0;        // 控制卡的编号，假设为0
        private UInt16 axis = 1;          // 需要设置的轴号，假设为1
        private UInt16 runMode = 3;       // 运行模式，假设3代表转矩控制模式
        private UInt16 portNum = 1;       // 端口号，假设为1
        private UInt16 nodeNum = 1;       // 节点号，假设为1
        private UInt16 index = 0x6071;    // 对象字典的索引，0x6071 通常用于转矩设定值
        private UInt16 subindex = 0x00;   // 对象字典的子索引，通常为0
        private UInt16 valueLength = 4;   // 数据长度，单位为字节，通常为4
        private Int32 torqueValue = 100;  // 设定的转矩值，假设为100

        private ushort MyCardNo;// 控制卡的编号。
        private ushort MyAxis;//轴的编号。
        private ushort Mymode;
        private ushort state;
        private ushort statemachine;//状态机
        private ushort MyPosiMode; // 位置模式，0 表示相对位置，1 表示绝对位置

        private AlarmLogger alarmLogger;
        private System.DateTime dateTime;
        #endregion

        public HydropManualMode()
        {
            InitializeComponent();
        }

        private void HydropManualMode_Load(object sender, EventArgs e)
        {
            short result = LTDMC.nmc_set_axis_run_mode(cardNo, axis, runMode);
            if (result == 0)
            {
                Console.WriteLine("成功设置轴的运行模式为转矩控制模式");
            }
            else
            {
                Console.WriteLine("设置轴运行模式失败，错误代码: " + result);
            }
        }


        private void uiButton1_Click(object sender, EventArgs e)
        {
            // 设置正转
            InitializeMotor();
            MoveAxis(1000); // 正转距离
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            // 设置反转
            InitializeMotor();
            MoveAxis(-1000); // 反转距离
        }
        private void InitializeMotor()
        {
            // 获取当前时间
            dateTime = System.DateTime.Now;

            // 初始化控制卡
            int ret = LTDMC.dmc_board_init();
            if (ret == 0)
            {
                // 设置速度曲线
                double Start_Vel = 0; // 起始速度
                double Max_Vel = 1000; // 最大速度
                double Tacc = 0.1; // 加速时间
                double Tdec = 0.1; // 减速时间
                double Stop_Vel = 0; // 停止速度

                ret = LTDMC.dmc_set_profile_unit(MyCardNo, MyAxis, Start_Vel, Max_Vel, Tacc, Tdec, Stop_Vel);
                if (ret != 0)
                {
                    alarmLogger.LogAlarm($"{dateTime} 手动模式：设置速度曲线失败，错误代码：{ret}");
                }
            }
            else
            {
                alarmLogger.LogAlarm($"{dateTime} 手动模式：初始化失败，错误代码：{ret}");
            }
        }
        private void MoveAxis(double distance)
        {
            LTDMC.dmc_pmove_unit(MyCardNo, MyAxis, distance, MyPosiMode);
            // 等待运动完成
            while (LTDMC.dmc_check_done(MyCardNo, MyAxis) == 0)
            {
                Application.DoEvents();
            }
        }

        private void uiButton8_Click(object sender, EventArgs e)
        {
            AlarmControls alarmControls = new AlarmControls();
            alarmControls.AppendTextToRichTextBox();
        }

        private void uiButton7_Click(object sender, EventArgs e)
        {
            alarmLogger = new AlarmLogger();
            dateTime = System.DateTime.Now;
            MyCardNo = 0;
            MyAxis = 0;
            int MyPosition = 0;

            try
            {
                // 初始卡
                int ret = LTDMC.dmc_board_init();
                if (ret == 0)
                {
                    // 初始化成功，设置当前位置为零点
                    ret = LTDMC.dmc_set_position(MyCardNo, MyAxis, MyPosition);
                    if (ret == 0)
                    {
                        // 设置零点成功，记录日志
                        alarmLogger.LogAlarm($"{dateTime} 手动模式：零点设置成功");
                    }
                    else
                    {
                        // 设置零点失败，记录日志
                        alarmLogger.LogAlarm($"{dateTime} 手动模式：设置当前位置为零点失败，错误代码：{ret}");
                    }
                    //关闭运动卡
                    LTDMC.dmc_board_close();
                }
                else
                {
                    alarmLogger.LogAlarm($"{dateTime} 手动模式：初始化失败，错误代码：{ret}");
                }
            }
            catch (Exception ex)
            {
                alarmLogger.LogAlarm($"{dateTime} 手动模式：设置零点异常：{ex}");
            }
        }

        private void uiButton9_Click(object sender, EventArgs e)
        {
            try
            {
                MyCardNo = 0; // 卡号
                MyAxis = 0; // 轴号
                Mymode = 33; // 回零方式为33

                LTDMC.nmc_get_axis_state_machine(MyCardNo, MyAxis, ref statemachine); // 获取轴状态机
                if (statemachine == 4) // 轴状态机处于准备好状态
                {
                    LTDMC.nmc_set_home_profile(MyCardNo, MyAxis, Mymode, 500, 1000, 0.1, 0.1, 0); // 设置回原点模式
                    LTDMC.nmc_home_move(MyCardNo, MyAxis); // 执行回原点运动
                    while (LTDMC.dmc_check_done(MyCardNo, MyAxis) == 0) // 判断轴运动状态
                    {
                        Application.DoEvents();
                    }
                    LTDMC.dmc_get_home_result(MyCardNo, MyAxis, ref state); // 判断回零是否正常完成
                    if (state == 1)
                    {
                        alarmLogger.LogAlarm($"{dateTime} 手动模式：回零正常");
                    }
                }
            }
            catch (Exception ex)
            {
                alarmLogger.LogAlarm($"{dateTime} 回零异常：{ex}");
            }
        }
    }
}
