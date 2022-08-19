using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using War3FixFont.WinAPI;

namespace War3FixFont;

public static class FixHelper
{
    public static IntPtr GetWar3Window()
    {
        var window = API.FindWindowA("Warcraft III", "Warcraft III");
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
    public static void FixFont(FixDirection direction)
    {
        Task.Run(
            async () =>
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
                var widthDelta = 0;
                var heightDelta = 0;
                switch (direction)
                {
                case FixDirection.Width:
                    widthDelta = 1;
                    break;
                case FixDirection.Height:
                    heightDelta = 1;
                    break;
                case FixDirection.Both:
                    widthDelta = 1;
                    heightDelta = 1;
                    break;
                default:
                    widthDelta = 1;
                    break;
                }

                API.SetWindowPos(window, IntPtr.Zero, x, y, width + widthDelta, height + heightDelta, 0);
                await Task.Delay(1000);
                API.SetWindowPos(window, IntPtr.Zero, x, y, width, height, 0);
            });
    }

    /// <summary>
    /// 修复叠字模式2
    /// </summary>
    public static void FixFont2(Mode2 mode2, FixDirection direction)
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
        var widthDelta = 0;
        var heightDelta = 0;
        switch (direction)
        {
        case FixDirection.Width:
            widthDelta = 1;
            break;
        case FixDirection.Height:
            heightDelta = 1;
            break;
        case FixDirection.Both:
            widthDelta = 1;
            heightDelta = 1;
            break;
        default:
            widthDelta = 1;
            break;
        }

        if (mode2 == Mode2.Fixing)
        {
            API.SetWindowPos(window, IntPtr.Zero, x, y, width + widthDelta, height + heightDelta, 0);
        }
        else
        {
            API.SetWindowPos(window, IntPtr.Zero, x, y, width, height, 0);
        }
    }

    /// <summary>
    /// 修复最大化窗口
    /// </summary>
    public static void FixMaxWindow(FixDirection direction)
    {
        Task.Run(
            async () =>
            {
                var window = GetWar3Window();
                if (window == IntPtr.Zero)
                {
                    return;
                }

                var isMaxWindow = API.IsZoomed(window);
                if (isMaxWindow)
                {
                    API.ShowWindow(window, 1);
                    await Task.Delay(100);
                    API.ShowWindow(window, 3);
                }
                else
                {
                    API.GetWindowRect(window, out var rect);
                    var x = rect.Left;
                    var y = rect.Top;
                    var width = rect.Right - rect.Left;
                    var height = rect.Bottom - rect.Top;
                    var widthDelta = 0;
                    var heightDelta = 0;
                    switch (direction)
                    {
                    case FixDirection.Width:
                        widthDelta = 1;
                        break;
                    case FixDirection.Height:
                        heightDelta = 1;
                        break;
                    case FixDirection.Both:
                        widthDelta = 1;
                        heightDelta = 1;
                        break;
                    default:
                        widthDelta = 1;
                        break;
                    }

                    API.SetWindowPos(window, IntPtr.Zero, x, y, width + widthDelta, height + heightDelta, 0);
                    await Task.Delay(1000);
                    API.SetWindowPos(window, IntPtr.Zero, x, y, width, height, 0);
                }
            });
    }
}