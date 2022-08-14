using System;
using System.Runtime.InteropServices;

namespace War3FixFont.WinAPI;

public static class API
{
    [DllImport("user32.dll")]
    public static extern IntPtr FindWindowA(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    public static extern long GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    public static extern long SetWindowLong(IntPtr hWnd, int nIndex, long dwNewLong);

    [DllImport("user32.dll")]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);

    /// <summary>
    /// 设置目标窗体大小，位置
    /// </summary>
    /// <param name="hWnd">目标句柄</param>
    /// <param name="x">目标窗体新位置X轴坐标</param>
    /// <param name="y">目标窗体新位置Y轴坐标</param>
    /// <param name="nWidth">目标窗体新宽度</param>
    /// <param name="nHeight">目标窗体新高度</param>
    /// <param name="BRePaint">是否刷新窗体</param>
    /// <returns></returns>
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);

    public const int GWL_STYLE = -16;

    public const int GWL_EXSTYLE = -20;

    public const long WS_EX_WINDOWEDGE = 0x0100;

    public const long WS_EX_DLGMODALFRAME = 0x0001;

    public const long WS_BORDER = 0x00800000L;

    public const long WS_THICKFRAME = 0x00040000L;

    public const long WS_CAPTION = 0x00C00000L;
}