using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMain.Common
{
    public class Parameter
    {
        /// <summary>
        /// 控制卡卡号
        /// </summary>
        public ushort MyCardNo { get; set; }
        /// <summary>
        /// 轴号
        /// </summary>
        public ushort MyAxis { get; set; }
        /// <summary>
        /// 运动模式
        /// </summary>
        public ushort MyMode { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public ushort State { get; set; }
        /// <summary>
        /// 状态机状态
        /// </summary>
        public ushort Statemachine { get; set; }

        /// 速度曲线参数 
        /// <summary>
        /// 起始速度
        /// </summary>
        private double Start_Vel = 0;
        /// <summary>
        /// 最大速度
        /// </summary>
        private double Max_Vel = 1000;
        /// <summary>
        /// 加速时间
        /// </summary>
        private double Tacc = 0.1;
        /// <summary>
        /// 减速时间
        /// </summary>
        private double Tdec = 0.1;
        /// <summary>
        /// 停止速度
        /// </summary>
        private double Stop_Vel = 0;

        /// <summary>
        /// 报警记录器
        /// </summary>
        private AlarmLogger alarmLogger = new AlarmLogger();
        /// <summary>
        /// 日期时间变量
        /// </summary>
        private System.DateTime dateTime;

        /// <summary>
        /// 初始化控制卡和设置速度曲线
        /// </summary>
        public void InitializeMotor()
        {
            // 获取当前时间
            dateTime = System.DateTime.Now;

            // 初始化控制卡
            int ret = LTDMC.dmc_board_init();
            if (ret == 0)
            {
                // 设置速度曲线
                ret = LTDMC.dmc_set_profile_unit(MyCardNo, MyAxis, Start_Vel, Max_Vel, Tacc, Tdec, Stop_Vel);
                if (ret != 0)
                {
                    alarmLogger.LogAlarm($"{dateTime} 设置速度曲线失败，错误代码：{ret}");
                }
            }
            else
            {
                alarmLogger.LogAlarm($"{dateTime} 初始化失败，错误代码：{ret}");
            }
        }

        // 启动电机
        public void StartMotor()
        {
            // 获取当前时间
            dateTime = System.DateTime.Now;

            // 初始化电机
            InitializeMotor();

            // 启动电机
            ushort MyDir = 1; // 正转方向
            int ret = LTDMC.dmc_vmove(MyCardNo, MyAxis, MyDir);
            if (ret == 0)
            {
                alarmLogger.LogAlarm($"{dateTime} 启动指令发送成功");
            }
            else
            {
                alarmLogger.LogAlarm($"{dateTime} 启动指令发送失败，错误代码：{ret}");
            }
        }

        // 暂停电机
        public void StopMotor()
        {
            // 获取当前时间
            dateTime = System.DateTime.Now;

            // 暂停电机
            ushort MyStopMode = 0; // 减速停止
            int ret = LTDMC.dmc_stop(MyCardNo, MyAxis, MyStopMode);
            if (ret == 0)
            {
                alarmLogger.LogAlarm($"{dateTime} 电机已暂停");
            }
            else
            {
                alarmLogger.LogAlarm($"{dateTime} 暂停指令发送失败，错误代码：{ret}");
            }
        }
    }
}
