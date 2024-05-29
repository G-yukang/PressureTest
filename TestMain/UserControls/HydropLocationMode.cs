using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestMain.UserControls
{
    public partial class HydropLocationMode : UserControl
    {
        private FLineChart FLineChart;
        public HydropLocationMode()
        {
            InitializeComponent();
        }

        private void HydropLocationMode_Load(object sender, EventArgs e)
        {

        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            LTDMC.dmc_stop(3, 0, 0);
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            FLineChart=new FLineChart();
            FLineChart.FLineChartSer();
        }
    }
}
