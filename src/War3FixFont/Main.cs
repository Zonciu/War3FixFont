using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace War3FixFont;

public partial class Main : Form
{
    private readonly KeyboardHook _hook = new();

    private readonly Timer _timer = new();

    private const string Url = "https://github.com/Zonciu/War3FixFont";

    protected override void OnClosing(CancelEventArgs e)
    {
        _hook.Dispose();
        base.OnClosing(e);
    }

    public Main()
    {
        InitializeComponent();

        _hook.KeyPressed += (_, _) =>
        {
            if (EnableHotKeyCheckBox.Checked)
            {
                FixFont();
            }
        };

        _timer.Elapsed += (_, _) => FixFont();

        // 读取定时配置
        var interval = Properties.Settings.Default.Interval;
        IntervalInput.Value = interval > 0 ? interval : 60;
        EnableTimerFixCheckBox.Checked = Properties.Settings.Default.UseTimer;

        // 读取全屏配置
        EnableFullScreenCheckBox.Checked = Properties.Settings.Default.FullScreen;

        // 读取快捷键配置
        var hotKeyString = Properties.Settings.Default.HotKey;
        if (!string.IsNullOrWhiteSpace(hotKeyString))
        {
            var hotKey = HotKey.Deserialize(hotKeyString);
            if (hotKey.IsValid)
            {
                HotKeyInputBox.SetHotKey(hotKey);
            }
            else
            {
                HotKeyInputBox.Reset();
                Properties.Settings.Default.HotKey = string.Empty;
                Properties.Settings.Default.Save();
            }
        }

        EnableHotKeyCheckBox.Checked = Properties.Settings.Default.UseHotKey;
        HotKeyInputBox.Enabled = EnableHotKeyCheckBox.Checked;
        UpdateHotKey();
    }

    /// <summary>
    /// 修复叠字
    /// </summary>
    private void FixFont()
    {
        if (EnableFullScreenCheckBox.Checked)
        {
            FixHelper.Borderless();
            FixHelper.FullScreen();
        }
        else
        {
            FixHelper.Border();
        }

        FixHelper.FixFont();
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
            var hotKey = HotKeyInputBox.GetHotKey();
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
    /// 恢复边框
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SetBorderButton_Click(object sender, EventArgs e)
    {
        FixHelper.Border();
        FixHelper.FixFont();
    }

    /// <summary>
    /// 更改无边框全屏启用状态
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void EnableFullScreenCheckBox_Click(object sender, EventArgs e)
    {
        Properties.Settings.Default.FullScreen = EnableFullScreenCheckBox.Checked;
        Properties.Settings.Default.Save();
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
        FixHelper.FixFont();
    }

    /// <summary>
    /// 更新定时间隔
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void IntervalInput_ValueChanged(object sender, EventArgs e)
    {
        Properties.Settings.Default.Interval = (int)IntervalInput.Value;
        Properties.Settings.Default.Save();
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
        Properties.Settings.Default.UseTimer = EnableTimerFixCheckBox.Checked;
        Properties.Settings.Default.Save();
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
        Properties.Settings.Default.UseHotKey = EnableHotKeyCheckBox.Checked;
        Properties.Settings.Default.Save();
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
        var hotkey = HotKeyInputBox.GetHotKey();
        if (!hotkey.IsValid)
        {
            HotKeyInputBox.Reset();
        }
        else
        {
            Properties.Settings.Default.HotKey = hotkey.Serialize();
            Properties.Settings.Default.Save();
        }

        UpdateHotKey();
    }
}