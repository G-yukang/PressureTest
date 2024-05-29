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

namespace TestMain.UserControls
{
    public partial class HydropManualMode : UserControl
    {
        FLineChart fLineChart = new FLineChart();
        // 设置轴为转矩控制模式
        private UInt16 cardNo = 0;      // 控制卡的编号，假设为0
        private UInt16 axis = 1;        // 需要设置的轴号，假设为1
        private UInt16 runMode = 3;     // 运行模式，假设3代表转矩控制模式

        // 设置转矩值
        private UInt16 portNum = 1;     // 端口号，假设为1
        private UInt16 nodeNum = 1;     // 节点号，假设为1
        private UInt16 index = 0x6071;  // 对象字典的索引，0x6071 通常用于转矩设定值
        private UInt16 subindex = 0x00; // 对象字典的子索引，通常为0
        private UInt16 valueLength = 4; // 数据长度，单位为字节，通常为4
        private Int32 torqueValue = 100; // 设定的转矩值，假设为100

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

            short result = LTDMC.nmc_set_node_od(cardNo, portNum, nodeNum, index, subindex, valueLength, torqueValue);

            if (result == 0)
            {
                Console.WriteLine("成功设置转矩值: " + torqueValue);
            }
            else
            {
                Console.WriteLine("设置转矩值失败，错误代码: " + result);
            }
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
