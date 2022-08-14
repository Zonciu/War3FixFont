using System;
using System.Windows.Forms;
using War3FixFont.WinAPI;

namespace War3FixFont;

/// <summary>
/// 键盘事件参数
/// </summary>
public class KeyPressedEventArgs : EventArgs
{
    internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
    {
        Modifier = modifier;
        Key = key;
    }

    public ModifierKeys Modifier { get; }

    public Keys Key { get; }
}