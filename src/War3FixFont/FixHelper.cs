using System;
using System.Windows.Forms;
using War3FixFont.WinAPI;

namespace War3FixFont;

public static class FixHelper
{
    public static IntPtr GetWar3Window()
    {
        var window = API.FindWindowA(null!, "Warcraft III");
        return window;
    }

    public static void Borderless()
    {
        var window = GetWar3Window();
        if (window == IntPtr.Zero)
        {
            return;
        }

        var wndStyle = API.GetWindowLong(window, API.GWL_STYLE);
        wndStyle &= ~(API.WS_BORDER | API.WS_THICKFRAME | API.WS_CAPTION);
        API.SetWindowLong(window, API.GWL_STYLE, wndStyle);

        var exStyle = API.GetWindowLong(window, API.GWL_EXSTYLE);
        exStyle &= ~(API.WS_EX_WINDOWEDGE | API.WS_EX_DLGMODALFRAME);
        API.SetWindowLong(window, API.GWL_EXSTYLE, exStyle);
    }

    public static void Border()
    {
        var window = GetWar3Window();
        if (window == IntPtr.Zero)
        {
            return;
        }

        var wndStyle = API.GetWindowLong(window, API.GWL_STYLE);
        wndStyle |= API.WS_BORDER | API.WS_THICKFRAME | API.WS_CAPTION;
        API.SetWindowLong(window, API.GWL_STYLE, wndStyle);

        var exStyle = API.GetWindowLong(window, API.GWL_EXSTYLE);
        exStyle |= API.WS_EX_WINDOWEDGE | API.WS_EX_DLGMODALFRAME;
        API.SetWindowLong(window, API.GWL_EXSTYLE, exStyle);
    }

    /// <summary>
    /// 全屏化
    /// </summary>
    public static void FullScreen()
    {
        var window = GetWar3Window();
        if (window == IntPtr.Zero)
        {
            return;
        }

        var width = Screen.PrimaryScreen.Bounds.Width;
        var height = Screen.PrimaryScreen.Bounds.Height;

        API.SetWindowPos(window, IntPtr.Zero, 0, 0, width, height, 0);
    }

    /// <summary>
    /// 修复叠字
    /// </summary>
    public static void FixFont()
    {
        var window = GetWar3Window();
        if (window == IntPtr.Zero)
        {
            return;
        }

        API.GetWindowRect(window, out var rect);
        var x = rect.Left;
        var y = rect.Top;
        var width = rect.Right - rect.Left;
        var height = rect.Bottom - rect.Top;

        API.SetWindowPos(window, IntPtr.Zero, x, y, width, height + 1, 0);
        API.SetWindowPos(window, IntPtr.Zero, x, y, width, height, 0);
    }
}