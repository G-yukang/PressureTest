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
using TestMain.Interfaces;

namespace TestMain
{
    public partial class Main : UIForm2
    {
        private HydropressTest hydropressTest;
        public Main()
        {
            InitializeComponent();


        }
        private void Main_Load(object sender, EventArgs e)
        {

            InitializeNavigationMenu();
        }

        private void InitializeNavigationMenu()
        {
        }



    }
}

