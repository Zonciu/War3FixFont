using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using War3FixFont.WinAPI;

namespace War3FixFont;

public static class FixHelper
{
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

    public static IntPtr Get5211War3Window()
    {
        return API.FindWindowA("Black Warcraft III", "Warcraft III");
    }

    /// <summary>
    /// 设置窗口无边框
    /// </summary>
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

    /// <summary>
    /// 设置窗口边框
    /// </summary>
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

    public static void NormalWindow()
    {
        var window = GetWar3Window();
        if (window == IntPtr.Zero)
        {
            return;
        }

        API.ShowWindow(window, API.SW_SHOWNORMAL);
    }

    public static void MaxWindow()
    {
        var window = GetWar3Window();
        if (window == IntPtr.Zero)
        {
            return;
        }

        API.ShowWindow(window, API.SW_MAXIMIZE);
    }

    public static void FixCurrentWindow()
    {
        Task.Run(
            async () =>
            {
                var window = GetWar3Window();
                if (window == IntPtr.Zero)
                {
                    return;
                }

                if (!API.GetWindowRect(window, out var rect))
                {
                    return;
                }

                var x = rect.Left;
                var y = rect.Top;
                var width = rect.Right - rect.Left;
                var height = rect.Bottom - rect.Top;

                API.SetWindowPos(window, IntPtr.Zero, x, y, width + 1, height, 0);
                if (!API.GetWindowRect(window, out var rect2))
                {
                    return;
                }

                var width2 = rect2.Right - rect2.Left;
                if (width != width2)
                {
                    await Task.Delay(1000);
                    API.SetWindowPos(window, IntPtr.Zero, x, y, width, height, 0);
                }
                else
                {
                    API.ShowWindow(window, API.SW_SHOWNORMAL);
                    await Task.Delay(100);
                    API.ShowWindow(window, API.SW_MAXIMIZE);
                }
            });
    }

    /// <summary>
    /// 修复普通窗口
    /// </summary>
    public static void FixFullScreenWindow()
    {
        Task.Run(
            async () =>
            {
                var window = GetWar3Window();
                if (window == IntPtr.Zero)
                {
                    return;
                }

                var width = Screen.PrimaryScreen.Bounds.Width;
                var height = Screen.PrimaryScreen.Bounds.Height;

                API.SetWindowPos(window, IntPtr.Zero, 0, 0, width + 1, height, 0);
                await Task.Delay(1000);
                API.SetWindowPos(window, IntPtr.Zero, 0, 0, width, height, 0);
            });
    }

    /// <summary>
    /// 修复最大化窗口
    /// </summary>
    public static void FixMaxWindow()
    {
        var window = GetWar3Window();
        if (window == IntPtr.Zero)
        {
            return;
        }

        Task.Run(
            async () =>
            {
                API.ShowWindow(window, API.SW_SHOWNORMAL);
                await Task.Delay(100);
                API.ShowWindow(window, API.SW_MAXIMIZE);
            });
    }
}