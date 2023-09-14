using System;
using System.Runtime.InteropServices;
using System.Text;

namespace War3FixFont.WinAPI;

public static class API
{
    /// <summary>
    /// 查找窗口
    /// </summary>
    /// <param name="lpClassName"></param>
    /// <param name="lpWindowName"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern IntPtr FindWindowA(string lpClassName, string lpWindowName);

    /// <summary>
    /// 获取子窗口
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="uCmd"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern IntPtr GetWindow(IntPtr hwnd, uint uCmd);

    /// <summary>
    /// 获取窗口参数
    /// </summary>
    /// <param name="hWnd"></param>
    /// <param name="nIndex"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    /// <summary>
    /// 设置窗口参数
    /// </summary>
    /// <param name="hWnd"></param>
    /// <param name="nIndex"></param>
    /// <param name="dwNewLong"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    /// <summary>
    /// 设置窗口位置
    /// </summary>
    /// <param name="hWnd"></param>
    /// <param name="hWndInsertAfter"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="cx"></param>
    /// <param name="cy"></param>
    /// <param name="uFlags"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    /// <summary>
    /// 获取窗口矩形
    /// </summary>
    /// <param name="hWnd"></param>
    /// <param name="lpRect"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);

    /// <summary>
    /// 显示窗口
    /// </summary>
    /// <param name="hWnd"></param>
    /// <param name="nCmdShow"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    /// <summary>
    /// 设置鼠标范围
    /// </summary>
    /// <param name="lpRect"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern bool ClipCursor(ref Rect lpRect);

    /// <summary>
    /// 解除鼠标范围限制
    /// </summary>
    /// <param name="lpRect"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern bool ClipCursor(IntPtr lpRect);

    /// <summary>
    /// 获取系统参数
    /// </summary>
    /// <param name="nIndex"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern int GetSystemMetrics(int nIndex);

    /// <summary>
    /// 获取系统信息
    /// </summary>
    /// <param name="uiAction"></param>
    /// <param name="uiParam"></param>
    /// <param name="pvParam"></param>
    /// <param name="fWinIni"></param>
    /// <returns></returns>
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool SystemParametersInfo(int uiAction, int uiParam, out Rect pvParam, int fWinIni);

    public delegate void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

    /// <summary>
    /// 设置窗口事件钩子
    /// </summary>
    /// <param name="eventMin"></param>
    /// <param name="eventMax"></param>
    /// <param name="hmodWinEventProc"></param>
    /// <param name="lpfnWinEventProc"></param>
    /// <param name="idProcess"></param>
    /// <param name="idThread"></param>
    /// <param name="dwFlags"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventProc lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

    /// <summary>
    /// 取消窗口事件钩子
    /// </summary>
    /// <param name="hWinEventHook"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern int UnhookWinEvent(IntPtr hWinEventHook);

    /// <summary>
    /// 获取最前面的窗口
    /// </summary>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();

    /// <summary>
    /// 设置窗口在最前
    /// </summary>
    /// <param name="hWnd"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    public const int GWL_STYLE = -16;

    public const int GWL_EXSTYLE = -20;

    public const int WS_EX_WINDOWEDGE = 0x0100;

    public const int WS_EX_DLGMODALFRAME = 0x0001;

    public const int WS_BORDER = 0x00800000;

    public const int WS_THICKFRAME = 0x00040000;

    public const int WS_CAPTION = 0x00C00000;

    public const int SW_MAXIMIZE = 3;

    public const int SW_SHOWNORMAL = 1;

    public const int GW_CHILD = 5;

    public const int SM_CXFRAME = 33;

    public const int SM_CYFRAME = 33;

    public const int SM_CYCAPTION = 4;

    public const int SM_CXPADDEDBORDER = 92;

    public const uint WINEVENT_OUTOFCONTEXT = 0;

    public const uint EVENT_SYSTEM_FOREGROUND = 3;

    public const int SPI_GETWORKAREA = 0x0030;

    public static int GetTitleBarHeight()
        => GetSystemMetrics(SM_CYFRAME)
         + GetSystemMetrics(SM_CYCAPTION)
         + GetSystemMetrics(SM_CXPADDEDBORDER);

    public static int GetBorder()
        => GetSystemMetrics(SM_CXFRAME)
         + GetSystemMetrics(SM_CXPADDEDBORDER);
}