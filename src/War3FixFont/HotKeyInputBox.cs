using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace War3FixFont;

/// <summary>
/// 快捷键设置器
/// </summary>
public sealed class HotKeyInputBox : TextBox
{
    #region Properties to hide from the designer

    [Browsable(false)]
    public new string[] Lines
    {
        get { return new string[] { Text }; }
        private set => base.Lines = value;
    }

    [Browsable(false)]
    public override bool Multiline => false;

    [Browsable(false)]
    public new char PasswordChar { get; set; }

    [Browsable(false)]
    public new ScrollBars ScrollBars { get; set; }

    [Browsable(false)]
    public override bool ShortcutsEnabled => false;

    [Browsable(false)]
    public override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    [Browsable(false)]
    public new bool WordWrap { get; set; }

    #endregion

    #region Focus detection - use this to stop hotkeys being triggered in your code

    private static Control FindFocusedControl(Control control)
    {
        var container = control as ContainerControl;
        while (container != null)
        {
            control = container.ActiveControl;
            container = control as ContainerControl;
        }

        return control;
    }

    public bool IsFocused => FindFocusedControl(Form.ActiveForm) == this;

    public static bool TypeIsFocused => FindFocusedControl(Form.ActiveForm) is HotKeyInputBox;

    #endregion

    private HotKey _hotkey = new();

    public Keys KeyCode
    {
        get => _hotkey.KeyCode;
        set => _hotkey.KeyCode = value;
    }

    public bool Control
    {
        get => _hotkey.Control;
        set => _hotkey.Control = value;
    }

    public bool Alt
    {
        get => _hotkey.Alt;
        set => _hotkey.Alt = value;
    }

    public bool Shift
    {
        get => _hotkey.Shift;
        set => _hotkey.Shift = value;
    }

    public void SetHotKey(HotKey hotkey)
    {
        _hotkey = hotkey;
        RefreshText();
    }

    public HotKey GetHotKey()
    {
        return _hotkey.Clone();
    }

    public void Reset()
    {
        KeyCode = Keys.None;
        Control = false;
        Alt = false;
        Shift = false;
        RefreshText();
    }

    private void RefreshText()
    {
        Text = _hotkey.ToString();
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        RefreshText();
        base.OnPaint(e);
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

    const int WM_KEYDOWN = 0x100;

    const int WM_KEYUP = 0x101;

    const int WM_CHAR = 0x102;

    const int WM_SYSCHAR = 0x106;

    const int WM_SYSKEYDOWN = 0x104;

    const int WM_SYSKEYUP = 0x105;

    const int WM_IME_CHAR = 0x286;

    protected override bool ProcessKeyMessage(ref Message m)
    {
        if (m.Msg == WM_KEYUP || m.Msg == WM_SYSKEYUP)
        {
            OnHotKeyChanged(EventArgs.Empty);
        }

        if (m.Msg != WM_CHAR && m.Msg != WM_SYSCHAR && m.Msg != WM_IME_CHAR)
        {
            var e = new KeyEventArgs((Keys)(int)(long)m.WParam | ModifierKeys);

            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                Reset();
            }
            else
            {
                if (m.Msg == WM_KEYDOWN || m.Msg == WM_SYSKEYDOWN || e.KeyCode == Keys.PrintScreen)
                {
                    Control = e.Control;
                    Shift = e.Shift;
                    Alt = e.Alt;

                    if (e.KeyCode != Keys.ShiftKey
                     && e.KeyCode != Keys.ControlKey
                     && e.KeyCode != Keys.Menu)
                    {
                        KeyCode = e.KeyCode;
                    }
                    else
                    {
                        KeyCode = Keys.None;
                    }
                }
            }

            // Pretty readable output
            RefreshText();

            // Select the end of our textbox
            Select(TextLength, 0);
        }

        return true;
    }
}