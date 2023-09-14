using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using War3FixFont.WinAPI;

namespace War3FixFont;

/// <summary>
/// 修复器
/// </summary>
[JsonObject]
public class Fixer
{
    public Settings Settings { get; set; }

    public void Fix()
    {
        var window = GetWar3Window();
        if (window == IntPtr.Zero)
        {
            return;
        }

        switch (Settings.WindowMode)
        {
        case WindowMode.FullScreen:
        {
            SetBorderless(window);
            FixFullScreenWindow(window);
            break;
        }
        case WindowMode.Maximum:
        {
            SetBorder(window);
            FixMaxWindow(window);
            break;
        }
        case WindowMode.Ratio43:
        {
            SetBorderless(window);
            FixRatio43(window);
            break;
        }
        case WindowMode.Custom:
        {
            SetBorder(window);
            FixCustomWindow(window);
            break;
        }
        default:
            break;
        }

        LockCursor();
    }

    public void Apply()
    {
        var window = GetWar3Window();
        if (window == IntPtr.Zero)
        {
            return;
        }

        switch (Settings.WindowMode)
        {
        case WindowMode.FullScreen:
        {
            SetBorderless(window);
            SetFullScreenWindow(window);
            break;
        }
        case WindowMode.Maximum:
        {
            SetBorder(window);
            SetMaxWindow(window);
            break;
        }
        case WindowMode.Ratio43:
        {
            SetBorderless(window);
            SetRatio43(window);
            break;
        }
        case WindowMode.Custom:
        {
            SetBorder(window);
            SetCustomWindow(window);
            break;
        }
        default:
            break;
        }

        LockCursor();
    }

    /// <summary>
    /// 获取魔兽3窗口句柄
    /// </summary>
    /// <returns></returns>
    public static IntPtr GetWar3Window()
    {
        var window = API.FindWindowA("Warcraft III", "Warcraft III");
        if (window != IntPtr.Zero)
        {
            return window;
        }

        window = API.FindWindowA("Black Warcraft III", "Warcraft III");
        if (window == IntPtr.Zero)
        {
            return IntPtr.Zero;
        }

        var child = API.GetWindow(window, API.GW_CHILD);
        return child;
    }

    private static IntPtr Get5211War3Window()
    {
        return API.FindWindowA("Black Warcraft III", "Warcraft III");
    }

    /// <summary>
    /// 设置窗口无边框
    /// </summary>
    private static void SetBorderless(IntPtr window)
    {
        var wndStyle = API.GetWindowLong(window, API.GWL_STYLE);
        wndStyle &= ~(API.WS_BORDER | API.WS_THICKFRAME | API.WS_CAPTION);
        API.SetWindowLong(window, API.GWL_STYLE, wndStyle);

        var exStyle = API.GetWindowLong(window, API.GWL_EXSTYLE);
        exStyle &= ~(API.WS_EX_WINDOWEDGE | API.WS_EX_DLGMODALFRAME);
        API.SetWindowLong(window, API.GWL_EXSTYLE, exStyle);
    }

    /// <summary>
    /// 设置窗口边框
    /// </summary>
    private void SetBorder(IntPtr window)
    {
        var wndStyle = API.GetWindowLong(window, API.GWL_STYLE);
        wndStyle |= API.WS_BORDER | API.WS_THICKFRAME | API.WS_CAPTION;
        API.SetWindowLong(window, API.GWL_STYLE, wndStyle);

        var exStyle = API.GetWindowLong(window, API.GWL_EXSTYLE);
        exStyle |= API.WS_EX_WINDOWEDGE | API.WS_EX_DLGMODALFRAME;
        API.SetWindowLong(window, API.GWL_EXSTYLE, exStyle);
    }

    /// <summary>
    /// 应用全屏窗口
    /// </summary>
    private void SetFullScreenWindow(IntPtr window)
    {
        var width = Screen.PrimaryScreen.Bounds.Width;
        var height = Screen.PrimaryScreen.Bounds.Height;

        API.SetWindowPos(window, IntPtr.Zero, 0, 0, width, height, 0);
    }

    /// <summary>
    /// 修复全屏窗口
    /// </summary>
    private void FixFullScreenWindow(IntPtr window)
    {
        if (window == IntPtr.Zero)
        {
            return;
        }

        Task.Run(
            async () =>
            {
                var width = Screen.PrimaryScreen.Bounds.Width;
                var height = Screen.PrimaryScreen.Bounds.Height;

                API.SetWindowPos(window, IntPtr.Zero, 0, 0, width + 1, height, 0);
                await Task.Delay(1000);
                API.SetWindowPos(window, IntPtr.Zero, 0, 0, width, height, 0);
            });
    }

    /// <summary>
    /// 应用自定义窗口
    /// </summary>
    private void SetCustomWindow(IntPtr window)
    {
        var width = Settings.Width;
        var height = Settings.Height;
        int x;
        int y;
        if (Settings.UseCustomPosition)
        {
            x = Settings.X;
            y = Settings.Y;
        }
        else
        {
            x = Screen.PrimaryScreen.Bounds.Width / 2 - width / 2;
            y = Screen.PrimaryScreen.Bounds.Height / 2 - height / 2;
        }

        var border = API.GetBorder();
        API.ShowWindow(window, API.SW_SHOWNORMAL);
        API.SetWindowPos(window, IntPtr.Zero, x - border, y, width, height, 0);
    }

    /// <summary>
    /// 修复自定义窗口
    /// </summary>
    private void FixCustomWindow(IntPtr window)
    {
        if (window == IntPtr.Zero)
        {
            return;
        }

        Task.Run(
            async () =>
            {
                var width = Settings.Width;
                var height = Settings.Height;
                int x;
                int y;
                if (Settings.UseCustomPosition)
                {
                    x = Settings.X;
                    y = Settings.Y;
                }
                else
                {
                    x = Screen.PrimaryScreen.Bounds.Width / 2 - width / 2;
                    y = Screen.PrimaryScreen.Bounds.Height / 2 - height / 2;
                }

                var border = API.GetBorder();
                API.SetWindowPos(window, IntPtr.Zero, x - border, y, width + 1, height, 0);
                await Task.Delay(1000);
                API.SetWindowPos(window, IntPtr.Zero, x - border, y, width, height, 0);
            });
    }

    /// <summary>
    /// 应用最大化窗口
    /// </summary>
    private void SetMaxWindow(IntPtr window)
    {
        API.ShowWindow(window, API.SW_MAXIMIZE);
    }

    /// <summary>
    /// 修复最大化窗口
    /// </summary>
    private void FixMaxWindow(IntPtr window)
    {
        Task.Run(
            async () =>
            {
                API.ShowWindow(window, API.SW_SHOWNORMAL);
                await Task.Delay(100);
                API.ShowWindow(window, API.SW_MAXIMIZE);
            });
    }

    /// <summary>
    /// 应用4:3窗口
    /// </summary>
    /// <param name="window"></param>
    private void SetRatio43(IntPtr window)
    {
        API.SystemParametersInfo(API.SPI_GETWORKAREA, 0, out var rect, 0);
        var width = rect.Right;
        var height = rect.Bottom;
        width = Math.Min(height / 3 * 4, width);

        var x = Screen.PrimaryScreen.Bounds.Width / 2 - width / 2;
        API.ShowWindow(window, API.SW_SHOWNORMAL);
        API.SetWindowPos(window, IntPtr.Zero, x, 0, width, height, 0);
    }

    /// <summary>
    /// 修复4:3窗口
    /// </summary>
    /// <param name="window"></param>
    private void FixRatio43(IntPtr window)
    {
        API.SystemParametersInfo(API.SPI_GETWORKAREA, 0, out var rect, 0);
        var width = rect.Right;
        var height = rect.Bottom;
        width = Math.Min(height / 3 * 4, width);

        var x = Screen.PrimaryScreen.Bounds.Width / 2 - width / 2;
        API.ShowWindow(window, API.SW_SHOWNORMAL);
        Task.Run(
            async () =>
            {
                API.SetWindowPos(window, IntPtr.Zero, x, 0, width + 1, height, 0);
                await Task.Delay(1000);
                API.SetWindowPos(window, IntPtr.Zero, x, 0, width, height, 0);
            });
    }

    /// <summary>
    /// 锁定鼠标范围
    /// </summary>
    public void LockCursor()
    {
        if (!Settings.LockCursor)
        {
            return;
        }

        var war3Window = GetWar3Window();
        if (war3Window == IntPtr.Zero)
        {
            return;
        }

        var handle = API.GetForegroundWindow();
        if (handle != war3Window)
        {
            // 11对战平台的窗口是嵌套的
            var war35211 = Get5211War3Window();
            if (handle == war35211 && handle != IntPtr.Zero)
            {
                handle = API.GetWindow(handle, API.GW_CHILD);
            }
        }

        if (handle == war3Window)
        {
            API.GetWindowRect(war3Window, out var windowRect);
            if (windowRect.Top == -32000)
            {
                // 窗口已最小化
                return;
            }

            if (Settings.WindowMode is WindowMode.Maximum or WindowMode.Custom)
            {
                var height = API.GetTitleBarHeight();
                var border = API.GetBorder();
                var rect = new Rect { Top = windowRect.Top + height, Bottom = windowRect.Bottom - border, Left = windowRect.Left + border, Right = windowRect.Right - border };
                API.ClipCursor(ref rect);
            }
            else
            {
                API.ClipCursor(ref windowRect);
            }
        }
    }
}