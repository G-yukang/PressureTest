using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMain
{
    public class PressDate
    {
        public ushort _CardID { get; set; }
        public double PressureVD { get; set; }                   // 当前位置  
        public double dCmdPos { get; set; }                      // 指令位置
        public double CurSpeed { get; set; }                     // 当前速度
        public double dEnPos { get; set; }                       // 编码器反馈位置
        public ushort usCardNum { get; set; }                    // IO
        public bool startdian { get; set; }
        public ushort cardNo { get; set; }                      // 卡号
        public ushort portNum { get; set; }                     // 端口号
        public ushort actualTorqueAddress { get; set; }         // 实际转矩地址
        public ushort targetTorqueAddress { get; set; }         // 目标转矩地址
        public ushort dataLen { get; set; }                     // 数据长度
        public uint actualValue { get; set; }                    // 实际示例值
        public uint targetValue { get; set; }                    // 目标示例值
        public double qswz { get; set; }                         // 起始位置
        public double yxwz { get; set; }                         // 运行位置
        public double bhwz { get; set; }                         // 闭合位置
        public double kmwz1 { get; set; }                        // 开模位置
        public double kmwz2 { get; set; }                        // 开模位置
        public double kmwz3 { get; set; }                        // 开模位置
        public double dStartVel { get; set; }                    // 起始速度
        public double dMaxVel { get; set; }                      // 运行速度
        public double dStopVel { get; set; }                     // 停止速度
        public double HdStartVel { get; set; }                   // 开模速度
        public double HdMaxVel { get; set; }                     // 开模速度
        public double HdStopVel { get; set; }                    // 开模速度        
        public double dTacc { get; set; }                        // 加速时间
        public double dTdec { get; set; }                        // 减速时间
        public double qsyl { get; set; }                         // 压力起始
        public double yxyl { get; set; }                         // 压力运行
        public double bhyl { get; set; }                         // 压力闭合
        public double kmyl { get; set; }                         // 压力开模
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
