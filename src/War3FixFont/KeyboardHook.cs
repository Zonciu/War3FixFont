using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using War3FixFont.WinAPI;

namespace War3FixFont;

/// <summary>
/// 键盘监听
/// </summary>
public sealed class KeyboardHook : IDisposable
{
    [DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    /// <summary>
    /// 内部窗体，用于获取全局热键事件
    /// </summary>
    private sealed class Window : NativeWindow, IDisposable
    {
        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        private const int WM_HOTKEY = 0x0312;

        public Window()
        {
            CreateHandle(new CreateParams());
        }

        /// <summary>
        /// 窗体消息接收
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_HOTKEY)
            {
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                ModifierKeys modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);

                KeyPressed?.Invoke(this, new KeyPressedEventArgs(modifier, key));
            }
        }

        public void Dispose()
        {
            DestroyHandle();
        }
    }

    private readonly Window _window = new();

    private int _hotKeyId = 1;

    private HashSet<int> _ids = new();

    public KeyboardHook()
    {
        //注册键盘事件
        _window.KeyPressed += delegate(object _, KeyPressedEventArgs args) { KeyPressed?.Invoke(this, args); };
    }

    /// <summary>
    /// 注册热键到系统
    /// </summary>
    /// <param name="modifier">修饰按键</param>
    /// <param name="key">主要按键</param>
    /// <param name="id">快捷键ID</param>
    public int? RegisterHotKey(ModifierKeys modifier, Keys key, int id = 0)
    {
        if (id == 0)
        {
            id = _hotKeyId++;
        }
        else
        {
            if (_ids.Contains(id))
            {
                MessageBox.Show("快捷键ID冲突");
            }
            else
            {
                _ids.Add(id);
            }
        }

        var result = RegisterHotKey(_window.Handle, id, (uint)modifier, (uint)key);
        return result ? id : null;
    }

    public bool UnregisterHotKey(int hotKeyId)
    {
        _ids.Remove(hotKeyId);
        return UnregisterHotKey(_window.Handle, hotKeyId);
    }

    /// <summary>
    /// 热键按下事件处理
    /// </summary>
    public event EventHandler<KeyPressedEventArgs> KeyPressed;

    public void Dispose()
    {
        // 注销热键
        for (int i = 0; i < _hotKeyId; i++)
        {
            UnregisterHotKey(_window.Handle, i);
        }

        foreach (var id in _ids)
        {
            UnregisterHotKey(_window.Handle, id);
        }

        // 释放窗体
        _window.Dispose();
    }
}