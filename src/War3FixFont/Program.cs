using System;
using System.Threading;
using System.Windows.Forms;

namespace War3FixFont
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using var mutex = new Mutex(true, "War3FixFont", out var mutexCreated);
            if (mutexCreated)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
            else
            {
                MessageBox.Show("程序已经运行", "魔兽争霸3叠字修复", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}