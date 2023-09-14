using System;
using System.Diagnostics;
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
                if (!Debugger.IsAttached)
                {
                    // Add the event handler for handling UI thread exceptions to the event.
                    Application.ThreadException += UIThreadException;

                    // Set the unhandled exception mode to force all Windows Forms errors to go through
                    // our handler.
                    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                    // Add the event handler for handling non-UI thread exceptions to the event.
                    AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
            else
            {
                MessageBox.Show("程序已经运行", "魔兽争霸3叠字修复", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Handle the UI exceptions by showing a dialog box, and asking the user whether
        // or not they wish to abort execution.
        private static void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            var result = DialogResult.Cancel;
            try
            {
                result = ShowThreadExceptionDialog("程序异常", t.Exception);
            }
            catch
            {
                try
                {
                    MessageBox.Show(
                        "程序异常",
                        "程序出错",
                        MessageBoxButtons.AbortRetryIgnore,
                        MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }

            if (result == DialogResult.Abort)
            {
                Application.Exit();
            }
        }

        private static void LogToEventLog(string message)
        {
            const string SourceName = "War3FixFont";
            if (!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, "Application");
            }

            var eventLog = new EventLog();
            eventLog.Source = SourceName;
            eventLog.WriteEntry(message);
        }

        // Handle the UI exceptions by showing a dialog box, and asking the user whether
        // or not they wish to abort execution.
        // NOTE: This exception cannot be kept from terminating the application - it can only
        // log the event, and inform the user about it.
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var ex = (Exception)e.ExceptionObject;
                const string ErrorMsg = "程序异常:\n\n";
                LogToEventLog(ErrorMsg + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace);
            }
            catch (Exception exc)
            {
                try
                {
                    MessageBox.Show(
                        "日志异常",
                        "无法记录错误到日志: "
                      + exc.Message,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }

        // Creates the error message and displays it.
        private static DialogResult ShowThreadExceptionDialog(string title, Exception e)
        {
            var errorMsg = "程序异常:\n\n";
            errorMsg = errorMsg + e.Message + "\n\nStack Trace:\n" + e.StackTrace;
            LogToEventLog(errorMsg);
            return MessageBox.Show(
                errorMsg,
                title,
                MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Stop);
        }
    }
}