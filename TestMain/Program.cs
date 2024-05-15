using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestMain
{
    internal static class Program
    {
        static Mutex mutex = new Mutex(true, "Main");
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
            else
            {
                MessageBox.Show("另一个程序正在运行，请勿重复打开！", "消息提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
