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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TestMain.UserControls
{
    public partial class User : UserControl
    {
        private HydropressTest hydropressTest;
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
            switch (auth)
            {
                case "首页":
                    break;
                case "液压机":
                    hydropressTest = new HydropressTest();
                    uiPanel1.Controls.Add(hydropressTest);
                    hydropressTest.Dock = DockStyle.Fill;
                    hydropressTest.Show();
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
