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
using TestMain.Common;
using TestMain.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TestMain.UserControls
{
    public partial class User : UserControl
    {
        private HydropressTest hydropressTest;
        private LogRecord logRecord;
        private Alarm alarm;
        public User()
        {
            InitializeComponent();
        }

        private void uiNavMenu1_MenuItemClick(TreeNode node, Sunny.UI.NavMenuItem item, int pageIndex)
        {
            Authority(node.Text);
        }

        //权限
        public void Authority(string auth) 
        {
            uiPanel1.Controls.Clear();
            switch (auth)
            {
                case "首页":
                    break;
                case "电压机":
                    hydropressTest = new HydropressTest();
                    uiPanel1.Controls.Add(hydropressTest);
                    hydropressTest.Dock = DockStyle.Fill;
                    hydropressTest.Show();
                    break;
                case "报警信息":
                    logRecord = new LogRecord();
                    uiPanel1.Controls.Add(logRecord);
                    logRecord.Dock = DockStyle.Fill;
                    logRecord.Show();
                    break;
                case "日志记录":
                    alarm = new Alarm();
                    uiPanel1.Controls.Add(alarm);
                    alarm.Dock = DockStyle.Fill;
                    alarm.Show();
                    break;
                case "关于":
                    break;
                default:
                    break;
            }
        }

        private void User_Load(object sender, EventArgs e)
        {
            ExpandAllNodes(uiNavMenu1.Nodes);
        }
        private void ExpandAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Expand();
                ExpandAllNodes(node.Nodes);
            }
        }
    }
}
