using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using War3FixFont;

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
            using var mutex = new Mutex(true, "eCS", out var mutexCreated);
            if (mutexCreated)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
            else
            {
                var current = Process.GetCurrentProcess();
                if (Process.GetProcessesByName(current.ProcessName).Any(process => process.Id != current.Id))
                {
                    MessageBox.Show("程序已经运行", "魔兽争霸3叠字修复", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}