using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using War3FixFont.WinAPI;
using Timer = System.Timers.Timer;

namespace War3FixFont;

public partial class Main : Form
{
    private readonly KeyboardHook _hook = new();

    private readonly Timer _timer = new();

    private const string Url = "https://github.com/Zonciu/War3FixFont";

    /// <summary>
    /// 修复频率限制
    /// </summary>
    private readonly long _fixThresholdTicks;

    /// <summary>
    /// 最后一次修复时间刻
    /// </summary>
    private long _lastFixTicks;

    private const long ToggleWindowThresholdTicks = 1 * 100 * 10000;

    private long _lastToggleWindowTicks;

    private const int UNLOCKED = 0;

    private const int LOCKED = 1;

    /// <summary>
    /// 修复锁
    /// </summary>
    private int _locker = UNLOCKED;

    /// <summary>
    /// 窗口事件钩子
    /// </summary>
    private IntPtr _winEventHook;

    /// <summary>
    /// 魔兽3进程扫描
    /// </summary>
    private readonly Timer _war3RunningMonitor = new();

    private bool _isSetAutoWindow;

    private const int FixHotKeyId = 1;

    private int _fixHotKeyId;

    private const int ShowMeHotKeyId = 2;

    private int _showMeHotKeyId;

    public readonly SettingsManager SettingsManager = new();

    public Settings Settings => SettingsManager.Settings;

    public Main()
    {
        SettingsManager.Load();
        _fixThresholdTicks = Settings.FixThreshold * 1000 * 10000;

        InitializeComponent();

        VersionLabel.Text = @$"v{Application.ProductVersion}";

        _hook.KeyPressed += HotKeyEvent;
        _timer.Elapsed += TimerFix;

        // 读取定时配置
        var interval = Settings.TimerInterval;
        IntervalInput.Value = interval > 0 ? interval : 60;
        EnableTimerFixCheckBox.Checked = Settings.UseTimer;

        // 读取窗口配置
        WindowModeSelect.DisplayMember = "Name";
        WindowModeSelect.ValueMember = "Value";
        var windowModeSource = Enum.GetValues(typeof(WindowMode))
                                   .Cast<WindowMode>()
                                   .Select(
                                        value => new ComboBoxItem<WindowMode>
                                        {
                                            Name = (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute)!.Description,
                                            Value = value
                                        })
                                   .OrderBy(item => item.Value)
                                   .ToList();
        WindowModeSelect.DataSource = windowModeSource;
        WindowModeSelect.SelectedItem = windowModeSource.Single(e => e.Value == Settings.WindowMode);

        // 读取修复快捷键配置
        var hotKey = Settings.HotKey;
        if (hotKey.IsValid)
        {
            HotKeyInputBox.HotKey = hotKey;
        }
        else
        {
            Settings.HotKey = HotKey.DefaultFixHotKey;
            HotKeyInputBox.HotKey = Settings.HotKey;
            SettingsManager.Save();
        }

        HotKeyInputBox.HotKeyEditing += (_, _) =>
        {
            if (_fixHotKeyId != 0)
            {
                _hook.UnregisterHotKey(_fixHotKeyId);
                _fixHotKeyId = 0;
            }
        };
        EnableHotKeyCheckBox.Checked = Settings.UseHotKey;
        UpdateFixHotKey();

        // 读取显示窗口快捷键配置
        if (Settings.ShowMeHotKey.IsValid)
        {
            ShowMeHotKeyInputBox.HotKey = Settings.ShowMeHotKey;
        }
        else
        {
            Settings.ShowMeHotKey = HotKey.DefaultShowMeHotKey;
            ShowMeHotKeyInputBox.HotKey = Settings.ShowMeHotKey;
            SettingsManager.Save();
        }

        ShowMeHotKeyInputBox.HotKeyEditing += (_, _) =>
        {
            if (_showMeHotKeyId != 0)
            {
                _hook.UnregisterHotKey(_showMeHotKeyId);
                _showMeHotKeyId = 0;
            }
        };
        UpdateShowMeHotKey();

        // 窗口切换事件
        LockCursorCheckBox.Checked = Settings.LockCursor;
        if (Settings.LockCursor)
        {
            LockCursor();
        }

        // 扫描魔兽3进程
        AutoWindowCheckBox.Checked = Settings.UseAutoWindow;
        _war3RunningMonitor.Interval = 2000;
        _war3RunningMonitor.Elapsed += CheckWar3Process;
        if (Settings.UseAutoWindow)
        {
            _war3RunningMonitor.Start();
        }
    }

    /// <summary>
    /// 关闭程序
    /// </summary>
    /// <param name="e"></param>
    protected override void OnClosing(CancelEventArgs e)
    {
        _war3RunningMonitor.Stop();
        UnlockCursor();
        SettingsManager.Save();
        _hook.Dispose();
        base.OnClosing(e);
    }

    /// <summary>
    /// 快捷键事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void HotKeyEvent(object sender, KeyPressedEventArgs args)
    {
        if (Settings.HotKey.SameAs(args))
        {
            HotKeyFix();
        }
        else if (Settings.ShowMeHotKey.SameAs(args))
        {
            ToggleWindow();
        }
    }

    /// <summary>
    /// 快捷键修复
    /// </summary>
    private void HotKeyFix()
    {
        if (!EnableHotKeyCheckBox.Checked)
        {
            return;
        }

        if (HotKeyInputBox.IsEditing)
        {
            return;
        }

        FixFont(true);
    }

    /// <summary>
    /// 手动修复
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void FixButton_Click(object sender, EventArgs args)
    {
        FixFont(true);
    }

    /// <summary>
    /// 定时修复
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void TimerFix(object sender, EventArgs args)
    {
        FixFont(false);
    }

    /// <summary>
    /// 重置修复定时器
    /// </summary>
    private void ResetTimer()
    {
        if (Settings.UseTimer)
        {
            _timer.Stop();
            _timer.Start();
        }
    }

    /// <summary>
    /// 修复叠字
    /// </summary>
    private void FixFont(bool resetTimer)
    {
        if (Interlocked.CompareExchange(ref _locker, LOCKED, UNLOCKED) != UNLOCKED)
        {
            return;
        }

        try
        {
            var nowTicks = DateTime.Now.Ticks;
            if (nowTicks - _lastFixTicks >= _fixThresholdTicks)
            {
                _lastFixTicks = nowTicks;

                switch (Settings.WindowMode)
                {
                case WindowMode.KeepCurrent:
                    FixHelper.FixCurrentWindow();
                    break;
                case WindowMode.MaxWindows:
                    FixHelper.Border();
                    FixHelper.FixMaxWindow();
                    break;
                case WindowMode.FullScreenWindow:
                default:
                    FixHelper.Borderless();
                    FixHelper.FixFullScreenWindow();
                    break;
                }
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
        finally
        {
            if (resetTimer)
            {
                ResetTimer();
            }

            Interlocked.Exchange(ref _locker, UNLOCKED);
        }
    }

    /// <summary>
    /// 更新快捷键
    /// </summary>
    private void UpdateFixHotKey()
    {
        if (_fixHotKeyId != 0)
        {
            _hook.UnregisterHotKey(_fixHotKeyId);
            _fixHotKeyId = 0;
        }

        if (EnableHotKeyCheckBox.Checked && HotKeyInputBox.HotKey.IsValid)
        {
            var id = _hook.RegisterHotKey(HotKeyInputBox.HotKey.Modifier, HotKeyInputBox.HotKey.KeyCode, FixHotKeyId);
            if (id.HasValue)
            {
                _fixHotKeyId = id.Value;
                ActiveControl = null;
            }
            else
            {
                MessageBox.Show("注册修复快捷键失败");
            }
        }
    }

    /// <summary>
    /// 点击仓库链接
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        var ps = new ProcessStartInfo(Url)
        {
            UseShellExecute = true,
            Verb = "open"
        };
        Process.Start(ps);
    }

    /// <summary>
    /// 有边框全屏
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BorderMaxWindowButton_Click(object sender, EventArgs e)
    {
        FixHelper.Border();
        FixHelper.NormalWindow();
        FixHelper.MaxWindow();
    }

    /// <summary>
    /// 无边框全屏
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FullScreenButton_Click(object sender, EventArgs e)
    {
        FixHelper.Borderless();
        FixHelper.FullScreen();
    }

    /// <summary>
    /// 更新定时间隔
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void IntervalInput_ValueChanged(object sender, EventArgs e)
    {
        Settings.TimerInterval = (int)IntervalInput.Value;
        SettingsManager.Save();
        if (_timer.Enabled)
        {
            _timer.Stop();
            _timer.Interval = (int)(IntervalInput.Value * 1000);
            _timer.Start();
        }
    }

    /// <summary>
    /// 更改定时器启用状态
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void EnableTimerFixCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        Settings.UseTimer = EnableTimerFixCheckBox.Checked;
        SettingsManager.Save();
        if (!EnableTimerFixCheckBox.Checked)
        {
            if (_timer.Enabled)
            {
                _timer.Stop();
            }
        }
        else
        {
            if (_timer.Enabled)
            {
                _timer.Stop();
            }

            _timer.Interval = (int)(IntervalInput.Value * 1000);
            _timer.Start();
        }
    }

    /// <summary>
    /// 更改修复快捷键启用状态
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void EnableHotKeyCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        Settings.UseHotKey = EnableHotKeyCheckBox.Checked;
        SettingsManager.Save();
        UpdateFixHotKey();
    }

    /// <summary>
    /// 输入修复快捷键
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void HotKeyInputBox_HotKeyChanged(object sender, EventArgs e)
    {
        var hotkey = HotKeyInputBox.HotKey;
        Settings.HotKey = hotkey.IsValid ? hotkey : HotKey.Empty;
        SettingsManager.Save();
        UpdateFixHotKey();
    }

    /// <summary>
    /// 使用说明
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ManualButton_Click(object sender, EventArgs e)
    {
        using var manual = new Manual();
        manual.StartPosition = FormStartPosition.CenterParent;
        manual.ShowDialog();
    }

    /// <summary>
    /// 窗口模式改变
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void WindowModeSelect_SelectionChangeCommitted(object sender, EventArgs e)
    {
        var mode = (ComboBoxItem<WindowMode>)WindowModeSelect.SelectedItem;
        Settings.WindowMode = mode.Value;
        SettingsManager.Save();
        if (Settings.UseAutoWindow)
        {
            ApplyWindowMode();
        }
    }

    #region 系统托盘

    /// <summary>
    /// 从托盘显示
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            if (ShowInTaskbar)
            {
                HideWindow();
            }
            else
            {
                ShowWindow();
            }
        }
    }

    /// <summary>
    /// 窗口最小化到托盘
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Main_SizeChanged(object sender, EventArgs e)
    {
        if (WindowState == FormWindowState.Minimized)
        {
            HideWindow();
        }
    }

    /// <summary>
    /// 点击显示窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ShowWindowMenuItem_Click(object sender, EventArgs e)
    {
        ShowWindow();
    }

    private void ExitApplication(object sender, EventArgs e)
    {
        Close();
    }

    private void ShowWindow()
    {
        ShowInTaskbar = true;                 //设置程序允许显示在任务栏
        Visible = true;                       //设置窗口可见
        WindowState = FormWindowState.Normal; //设置窗口状态
        Activate();                           //设置窗口为活动状态，防止被其他窗口遮挡。
    }

    private void HideWindow()
    {
        Visible = false;       //隐藏窗口
        ShowInTaskbar = false; //将程序从任务栏移除显示
    }

    private void ToggleWindow()
    {
        if (ShowMeHotKeyInputBox.IsEditing)
        {
            return;
        }

        var nowTicks = DateTime.Now.Ticks;
        if (nowTicks - _lastToggleWindowTicks >= ToggleWindowThresholdTicks)
        {
            _lastToggleWindowTicks = nowTicks;
            if (Visible)
            {
                HideWindow();
            }
            else
            {
                ShowWindow();
            }
        }
    }

    /// <summary>
    /// 显示本窗口快捷键改变
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ShowMeHotKeyInputBox_HotKeyChanged(object sender, EventArgs e)
    {
        var hotkey = ShowMeHotKeyInputBox.HotKey;
        Settings.ShowMeHotKey = hotkey.IsValid ? hotkey : HotKey.DefaultShowMeHotKey;
        SettingsManager.Save();
        UpdateShowMeHotKey();
    }

    /// <summary>
    /// 更新显示窗口快捷键
    /// </summary>
    private void UpdateShowMeHotKey()
    {
        if (_showMeHotKeyId != 0)
        {
            _hook.UnregisterHotKey(_showMeHotKeyId);
            _showMeHotKeyId = 0;
        }

        var hotKey = ShowMeHotKeyInputBox.HotKey;
        if (hotKey.IsValid)
        {
            var id = _hook.RegisterHotKey(hotKey.Modifier, hotKey.KeyCode, ShowMeHotKeyId);
            if (id.HasValue)
            {
                _showMeHotKeyId = id.Value;
                ActiveControl = null;
            }
            else
            {
                MessageBox.Show("注册显示窗口快捷键失败");
            }
        }
    }

    #endregion

    #region 自动窗口模式

    /// <summary>
    /// 自动应用窗口模式
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AutoWindowCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        Settings.UseAutoWindow = AutoWindowCheckBox.Checked;
        SettingsManager.Save();
        if (Settings.UseAutoWindow)
        {
            if (_isSetAutoWindow)
            {
                ApplyWindowMode();
            }

            _war3RunningMonitor.Start();
        }
        else
        {
            _war3RunningMonitor.Stop();
        }
    }

    /// <summary>
    /// 扫描魔兽3进程
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CheckWar3Process(object sender, ElapsedEventArgs e)
    {
        if (!Settings.UseAutoWindow)
        {
            _isSetAutoWindow = false;
            return;
        }

        var window = FixHelper.GetWar3Window();
        if (window == IntPtr.Zero)
        {
            _isSetAutoWindow = false;
        }
        else
        {
            if (!_isSetAutoWindow)
            {
                _isSetAutoWindow = true;
                Task.Run(
                    async () =>
                    {
                        await Task.Delay(800);
                        ApplyWindowMode();
                    });
            }
        }
    }

    /// <summary>
    /// 应用窗口模式
    /// </summary>
    private void ApplyWindowMode()
    {
        switch (Settings.WindowMode)
        {
        case WindowMode.FullScreenWindow:
            FixHelper.Borderless();
            FixHelper.FullScreen();
            break;
        case WindowMode.MaxWindows:
            FixHelper.Border();
            FixHelper.MaxWindow();
            break;
        case WindowMode.KeepCurrent:
        default:
            break;
        }

        SetCursor();
    }

    #endregion

    #region 锁定鼠标

    /// <summary>
    /// 锁定鼠标
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LockCursorCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        Settings.LockCursor = LockCursorCheckBox.Checked;
        SettingsManager.Save();
    }

    /// <summary>
    /// 锁定鼠标范围
    /// </summary>
    private void SetCursor()
    {
        var war3Window = FixHelper.GetWar3Window();
        if (war3Window == IntPtr.Zero)
        {
            return;
        }

        var handle = API.GetForegroundWindow();
        if (handle != war3Window)
        {
            var war35211 = FixHelper.Get5211War3Window();
            if (handle == war35211 && handle != IntPtr.Zero)
            {
                handle = API.GetWindow(handle, API.GW_CHILD);
            }
        }

        if (handle == war3Window && Settings.LockCursor)
        {
            API.GetWindowRect(war3Window, out var windowRect);
            if (windowRect.Top == -32000)
            {
                // 窗口已最小化
                return;
            }

            if (Settings.WindowMode != WindowMode.FullScreenWindow)
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

    /// <summary>
    /// 窗口改变事件，设置鼠标锁定
    /// </summary>
    /// <param name="hWinEventHook"></param>
    /// <param name="eventType"></param>
    /// <param name="hwnd"></param>
    /// <param name="idObject"></param>
    /// <param name="idChild"></param>
    /// <param name="dwEventThread"></param>
    /// <param name="dwmsEventTime"></param>
    public void LockCursorEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
    {
        SetCursor();
    }

    /// <summary>
    /// 锁定鼠标
    /// </summary>
    private void LockCursor()
    {
        _winEventHook = API.SetWinEventHook(API.EVENT_SYSTEM_FOREGROUND, API.EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, LockCursorEventProc, 0, 0, API.WINEVENT_OUTOFCONTEXT);
    }

    /// <summary>
    /// 解锁鼠标
    /// </summary>
    private void UnlockCursor()
    {
        if (_winEventHook != IntPtr.Zero)
        {
            API.UnhookWinEvent(_winEventHook);
            _winEventHook = IntPtr.Zero;
        }
    }

    #endregion
}