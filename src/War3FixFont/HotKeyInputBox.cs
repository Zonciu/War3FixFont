using System;
using System.Windows.Forms;

namespace War3FixFont;

/// <summary>
/// 快捷键设置器
/// </summary>
public sealed class HotKeyInputBox : TextBox
{
    private Hotkey _hotkey;

    public Hotkey Hotkey
    {
        get => _hotkey;
        set
        {
            _hotkey = value;
            RefreshText();
            OnHotKeyChanged(EventArgs.Empty);
        }
    }

    public void Reset()
    {
        Hotkey = default;
    }

    private void RefreshText()
    {
        Text = _hotkey.ToString();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (e.Button != MouseButtons.None)
        {
            SelectAll();
        }

        base.OnMouseMove(e);
    }

    public event EventHandler HotKeyChanged;

    private void OnHotKeyChanged(EventArgs e)
    {
        HotKeyChanged?.Invoke(this, e);
    }

    public event EventHandler HotKeyEditing;

    private void OnHotKeyEditing(EventArgs e)
    {
        HotKeyEditing?.Invoke(this, e);
    }

    const int WM_KEYDOWN = 0x100;

    const int WM_KEYUP = 0x101;

    const int WM_CHAR = 0x102;

    const int WM_SYSCHAR = 0x106;

    const int WM_SYSKEYDOWN = 0x104;

    const int WM_SYSKEYUP = 0x105;

    const int WM_IME_CHAR = 0x286;

    public bool IsEditing { get; private set; }

    private Hotkey _tempHotkey;

    protected override bool ProcessKeyMessage(ref Message m)
    {
        if (m.Msg == WM_KEYUP || m.Msg == WM_SYSKEYUP)
        {
            if (!_tempHotkey.IsValid)
            {
                RefreshText();
                return true;
            }

            IsEditing = false;
            Hotkey = _tempHotkey;
        }
        else if (m.Msg != WM_CHAR && m.Msg != WM_SYSCHAR && m.Msg != WM_IME_CHAR)
        {
            OnHotKeyEditing(EventArgs.Empty);
            var e = new KeyEventArgs((Keys)(int)(long)m.WParam | ModifierKeys);

            if (e.KeyCode is Keys.Delete or Keys.Back or Keys.Escape)
            {
                Reset();
            }
            else
            {
                if (m.Msg is WM_KEYDOWN or WM_SYSKEYDOWN)
                {
                    IsEditing = true;
                    var keyCode = Keys.None;
                    if (e.KeyCode != Keys.ShiftKey && e.KeyCode != Keys.ControlKey && e.KeyCode != Keys.Menu)
                    {
                        keyCode = e.KeyCode;
                    }

                    _tempHotkey = new(keyCode, e.Control, e.Alt, e.Shift);
                    Text = _tempHotkey.ToString();
                }
            }

            Select(TextLength, 0);
        }

        return true;
    }
}