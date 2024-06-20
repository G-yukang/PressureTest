using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMain
{
    public class GlobalDate
    {
        /// <summary>
        /// 真空计
        /// </summary>
        /// <param name="Vacuometer">真空计</param>
        public static string Vacuometer { get; set; }
    }
    public class PressDate
    {
        /// <summary>
        /// 卡号
        /// </summary>
        /// <param name="cardNo">卡号</param>
        public static ushort cardNo { get; set; } = 0;
        /// <summary>
        /// 端口号
        /// </summary>
        /// <param name="portNum">端口号</param>
        public static ushort portNum { get; set; } = 1;
        /// <summary>
        /// 实际转矩地址
        /// </summary>
        /// <param name="actualTorqueAddress">实际转矩地址</param>
        public static ushort actualTorqueAddress { get; set; } = 0x6077;
        /// <summary>
        /// 目标转矩地址
        /// </summary>
        /// <param name="targetTorqueAddress">目标转矩地址</param>
        public static ushort targetTorqueAddress { get; set; } = 0x6071;
        /// <summary>
        /// 数据长度
        /// </summary>
        /// <param name="dataLen">数据长度</param>
        public static ushort dataLen { get; set; } = 4;
        /// <summary>
        /// 数据长度
        /// </summary>
        /// <param name="actualValue">数据长度</param>
        public static short actualValue { get; set; } = 4;
        /// <summary>
        /// 数据长度
        /// </summary>
        /// <param name="TargetValue">数据长度</param>
        public static uint TargetValue { get; set; } = 4;
        /// <summary>
        /// ID
        /// </summary>
        /// <param name="_CardID">ID</param>
        public static ushort _CardID { get; set; }
        /// <summary>
        /// 压力
        /// </summary>
        /// <param name="PressureVD">压力</param>
        public static double PressureVD { get; set; }
        /// <summary>
        /// 当前位置
        /// </summary>
        /// <param name="CurrentPos">当前位置</param>
        public static double CurrentPos { get; set; }
        /// <summary>
        /// 指令位置
        /// </summary>
        /// <param name="dCmdPos">指令位置</param>
        public static double dCmdPos { get; set; }
        /// <summary>
        /// 当前速度
        /// </summary>
        /// <param name="CurSpeed">当前速度</param>
        public static double CurSpeed { get; set; }
        /// <summary>
        /// 编码器反馈位置
        /// </summary>
        /// <param name="dEnPos">编码器反馈位置</param>
        public static double dEnPos { get; set; }
        /// <summary>
        /// 起始位置
        /// </summary>
        /// <param name="InitialPosition">起始位置</param>
        public static double InitialPosition { get; set; }
        /// <summary>
        /// 运行位置
        /// </summary>
        /// <param name="RunningPosition">运行位置</param>
        public static double RunningPosition { get; set; }
        /// <summary>
        /// 闭合位置
        /// </summary>
        /// <param name="OnPosition">闭合位置</param>
        public static double OnPosition { get; set; }
        /// <summary>
        /// 开模位置1
        /// </summary>
        /// <param name="OpeningPosition1">开模位置1</param>
        public static double OpeningPosition1 { get; set; }
        /// <summary>
        /// 开模位置2
        /// </summary>
        /// <param name="OpeningPosition2">开模位置2</param>
        public static double OpeningPosition2 { get; set; }
        /// <summary>
        /// 开模位置3
        /// </summary>
        /// <param name="OpeningPosition3">开模位置3</param>
        public static double OpeningPosition3 { get; set; }
        /// <summary>
        /// 起始速度
        /// </summary>
        /// <param name="StartSpeed">起始速度</param>
        public static double StartSpeed { get; set; }
        /// <summary>
        /// 运行速度
        /// </summary>
        /// <param name="RunningSpeed">运行速度</param>
        public static double RunningSpeed { get; set; }
        /// <summary>
        /// 停止速度
        /// </summary>
        /// <param name="StopSpeed">停止速度</param>
        public static double StopSpeed { get; set; }
        /// <summary>
        /// 开模速度
        /// </summary>
        /// <param name="OpeningSpeed1">开模速度</param>
        public static double OpeningSpeed1 { get; set; }
        /// <summary>
        /// 开模速度
        /// </summary>
        /// <param name="OpeningSpeed2">开模速度</param>
        public static double OpeningSpeed2 { get; set; }
        /// <summary>
        /// 开模速度
        /// </summary>
        /// <param name="OpeningSpeed3">开模速度</param>
        public static double OpeningSpeed3 { get; set; }
        /// <summary>
        /// 加速时间
        /// </summary>
        /// <param name="AccelerationTime">加速时间</param>        
        public static double AccelerationTime { get; set; }
        /// <summary>
        /// 减速时间
        /// </summary>
        /// <param name="StopTime">减速时间</param>
        public static double StopTime { get; set; }
        /// <summary>
        /// 压力起始
        /// </summary>
        /// <param name="PressureInitiation">压力起始</param>
        public static double PressureInitiation { get; set; }
        /// <summary>
        /// 压力运行
        /// </summary>
        /// <param name="PressureRunning">压力运行</param>
        public static double PressureRunning { get; set; }
        /// <summary>
        /// 压力闭合
        /// </summary>
        /// <param name="PressureOn">压力闭合</param>
        public static double PressureOn { get; set; }
        /// <summary>
        /// 压力开模
        /// </summary>
        /// <param name="PressureOpening">压力开模</param>
        public static double PressureOpening { get; set; }
    }

    public class TitlePanel1
    {
        public static string speed { get; set; }
        public static string location { get; set; }
        public static string pressure { get; set; }
        public static string io { get; set; }
        public static bool viewshow { get; set; }
    }
    public class TitlePanel2
    {
        public static string speed { get; set; }
        public static string location { get; set; }
        public static string pressure { get; set; }
        public static string io { get; set; }
        public static bool viewshow { get; set; }
    }
    public class TitlePanel3
    {
        public static string speed { get; set; }
        public static string location { get; set; }
        public static string pressure { get; set; }
        public static string io { get; set; }
        public static bool viewshow { get; set; }
    }
    public class TitlePanel4
    {
        public static string speed { get; set; }
        public static string location { get; set; }
        public static string pressure { get; set; }
        public static string io { get; set; }
        public static bool viewshow { get; set; }
    }
    public class TitlePanel5
    {
        public static string speed { get; set; }
        public static string location { get; set; }
        public static string pressure { get; set; }
        public static string io { get; set; }
        public static bool viewshow { get; set; }
    }
    public class TitlePanel6
    {
        public static string speed { get; set; }
        public static string location { get; set; }
        public static string pressure { get; set; }
        public static string io { get; set; }
        public static bool viewshow { get; set; }
    }
    public class TitlePanel7
    {
        public static string speed { get; set; }
        public static string location { get; set; }
        public static string pressure { get; set; }
        public static string io { get; set; }
        public static bool viewshow { get; set; }
    }
    public class TitlePanel8
    {
        public static string speed { get; set; }
        public static string location { get; set; }
        public static string pressure { get; set; }
        public static string io { get; set; }
        public static bool viewshow { get; set; }
    }
    public class TitlePanel9
    {
        public static string speed { get; set; }
        public static string location { get; set; }
        public static string pressure { get; set; }
        public static string io { get; set; }
        public static bool viewshow { get; set; }
    }
    public class TitlePanel10
    {
        public static string speed { get; set; }
        public static string location { get; set; }
        public static string pressure { get; set; }
        public static string io { get; set; }
        public static bool viewshow { get; set; }
    }
    public class PressurizeView
    {
        public static string speed { get; set; }
        public static string time { get; set; }
        public static string pressure { get; set; }
        public static string io { get; set; }
    }
    public class OtherParametersView
    {
        public static string location { get; set; }
        public static string speed { get; set; }
        public static string recipe { get; set; }
        public static string OpeningSpeed { get; set; }
        public static bool viewshow { get; set; }
    }

}
