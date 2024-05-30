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
    public partial class AlarmControls : UserControl
    {
        private AlarmLogger logger;
        public AlarmControls()
        {
            InitializeComponent();
        }

        private void AlarmControls_Load(object sender, EventArgs e)
        {
            logger = new AlarmLogger();
            uiRichTextBox1.Clear();
            var logs = logger.GetLogs();
            foreach (var log in logs)
            {
                uiRichTextBox1.AppendText(log + Environment.NewLine);
            }
        }

        public void AppendTextToRichTextBox()
        {
            logger = new AlarmLogger();
            logger.ClearLogs();

            // 刷新控件
            uiRichTextBox1.Refresh();
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            logger = new AlarmLogger();
            logger.ClearLogs();

            uiRichTextBox1.Clear();
            // 刷新控件
            uiRichTextBox1.Refresh();

        }
    }
}
