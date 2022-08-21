using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
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

    private long _lastFixTicks;

    private const int Idle = 0;

    private const int Fixing = 1;

    private int _fixLock = Idle;

    public readonly SettingsManager SettingsManager = new();

    public Settings Settings => SettingsManager.Settings;

    public Main()
    {
        SettingsManager.Load();
        _fixThresholdTicks = Settings.FixThreshold * 1000 * 10000;

        InitializeComponent();

        VersionLabel.Text = @$"v{Application.ProductVersion}";

        _hook.KeyPressed += HotKeyFix;
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

        // 读取快捷键配置
        var hotKey = Settings.HotKey;
        if (hotKey.IsValid)
        {
            HotKeyInputBox.HotKey = hotKey;
        }
        else
        {
            HotKeyInputBox.Reset();
            Settings.HotKey = HotKey.Default;
            SettingsManager.Save();
        }

        EnableHotKeyCheckBox.Checked = Settings.UseHotKey;
        HotKeyInputBox.Enabled = EnableHotKeyCheckBox.Checked;
        UpdateHotKey();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        SettingsManager.Save();
        _hook.Dispose();
        base.OnClosing(e);
    }

    private void HotKeyFix(object sender, EventArgs args)
    {
        if (!EnableHotKeyCheckBox.Checked)
        {
            return;
        }

        if (Interlocked.CompareExchange(ref _fixLock, Fixing, Idle) != Idle)
        {
            return;
        }

        try
        {
            var now = DateTime.Now;
            if (now.Ticks - _lastFixTicks >= _fixThresholdTicks)
            {
                _lastFixTicks = now.Ticks;
                FixFont();
                ResetTimer();
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
        finally
        {
            Interlocked.Exchange(ref _fixLock, Idle);
        }
    }

    private void TimerFix(object sender, EventArgs args)
    {
        if (Interlocked.CompareExchange(ref _fixLock, Fixing, Idle) != Idle)
        {
            return;
        }

        try
        {
            var now = DateTime.Now;
            if (now.Ticks - _lastFixTicks >= _fixThresholdTicks)
            {
                _lastFixTicks = now.Ticks;
                FixFont();
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
        finally
        {
            Interlocked.Exchange(ref _fixLock, Idle);
        }
    }

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
    private void FixFont()
    {
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

    /// <summary>
    /// 更新快捷键
    /// </summary>
    private void UpdateHotKey()
    {
        if (_hook.Registered)
        {
            _hook.UnregisterHotKey();
        }

        if (EnableHotKeyCheckBox.Checked)
        {
            var hotKey = HotKeyInputBox.HotKey;
            if (hotKey.IsValid)
            {
                var success = _hook.RegisterHotKey(hotKey.Modifier, hotKey.KeyCode);
                if (!success)
                {
                    MessageBox.Show("快捷键注册失败");
                }
            }
        }
    }

    /// <summary>
    /// 手动修复
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FixButton_Click(object sender, EventArgs e)
    {
        FixFont();
        ResetTimer();
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
    /// 更改快捷键启用状态
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void EnableHotKeyCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        Settings.UseHotKey = EnableHotKeyCheckBox.Checked;
        SettingsManager.Save();
        HotKeyInputBox.Enabled = EnableHotKeyCheckBox.Checked;
        UpdateHotKey();
    }

    /// <summary>
    /// 输入快捷键
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void HotKeyInputBox_HotKeyChanged(object sender, EventArgs e)
    {
        var hotkey = HotKeyInputBox.HotKey;
        if (!hotkey.IsValid)
        {
            HotKeyInputBox.Reset();
        }
        else
        {
            Settings.HotKey = hotkey;
            SettingsManager.Save();
        }

        UpdateHotKey();
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

    private void ShowWindow(object sender, EventArgs e)
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
        ShowInTaskbar = false; //将程序从任务栏移除显示
        Visible = false;       //隐藏窗口
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
    }
}