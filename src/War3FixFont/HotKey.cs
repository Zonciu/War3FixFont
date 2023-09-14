using System;
using System.Text;
using System.Windows.Forms;
using War3FixFont.WinAPI;

namespace War3FixFont;

public class HotKey
{
    public static readonly HotKey DefaultFixHotKey = new("0,1,1,D");

    public static readonly HotKey DefaultShowMeHotKey = new("1,0,0,Q");

    public static readonly HotKey Empty = new("");

    public Keys KeyCode { get; set; }

    public bool Control { get; set; }

    public bool Alt { get; set; }

    public bool Shift { get; set; }

    public bool IsValid => Control | Shift | Alt
                        && KeyCode != Keys.None
                        && KeyCode != Keys.Back
                        && KeyCode != Keys.Delete
                        && KeyCode != Keys.Escape
                        && KeyCode != Keys.PrintScreen;

    public ModifierKeys Modifier
    {
        get
        {
            ModifierKeys k = 0;
            if (Control)
            {
                k |= ModifierKeys.Control;
            }

            if (Shift)
            {
                k |= ModifierKeys.Shift;
            }

            if (Alt)
            {
                k |= ModifierKeys.Alt;
            }

            return k;
        }
    }

    public HotKey()
    { }

    public HotKey(ModifierKeys modifierKeys, Keys keyCode)
    {
        Control = (modifierKeys & ModifierKeys.Control) != 0;
        Shift = (modifierKeys & ModifierKeys.Shift) != 0;
        Alt = (modifierKeys & ModifierKeys.Alt) != 0;
        KeyCode = keyCode;
    }

    public HotKey(string keys)
    {
        var settings = keys.Split(',');
        if (settings.Length == 4)
        {
            if (int.TryParse(settings[0], out var control)
             && int.TryParse(settings[1], out var shift)
             && int.TryParse(settings[2], out var alt)
             && Enum.TryParse<Keys>(settings[3], true, out var key)
               )
            {
                Control = control == 1;
                Shift = shift == 1;
                Alt = alt == 1;
                KeyCode = key;
            }
        }
    }

    public override string ToString()
    {
        if (!Control && !Shift && !Alt && KeyCode == Keys.None)
        {
            return string.Empty;
        }

        var stb = new StringBuilder();

        stb.Append(Control ? "Ctrl" : "");

        if (Shift)
        {
            if (stb.Length > 0)
            {
                stb.Append(" + ");
            }

            stb.Append("Shift");
        }

        if (Alt)
        {
            if (stb.Length > 0)
            {
                stb.Append(" + ");
            }

            stb.Append("Alt");
        }

        if (KeyCode != Keys.None)
        {
            if (stb.Length > 0)
            {
                stb.Append(" + ");
            }

            stb.Append(KeyCode);
        }

        return stb.ToString();
    }

    public string Serialize()
    {
        return $"{(Control ? "1" : "0")},{(Shift ? "1" : "0")},{(Alt ? "1" : "0")},{KeyCode:G}";
    }

    public static HotKey Deserialize(string text)
    {
        var hotKey = new HotKey();
        var settings = text.Split(',');
        if (settings.Length == 4)
        {
            if (int.TryParse(settings[0], out var control)
             && int.TryParse(settings[1], out var shift)
             && int.TryParse(settings[2], out var alt)
             && Enum.TryParse<Keys>(settings[3], true, out var key)
               )
            {
                hotKey.Control = control == 1;
                hotKey.Shift = shift == 1;
                hotKey.Alt = alt == 1;
                hotKey.KeyCode = key;
            }
        }

        return hotKey;
    }

    public HotKey Clone()
    {
        return new()
        {
            Control = Control,
            Shift = Shift,
            Alt = Alt,
            KeyCode = KeyCode
        };
    }

    public bool SameAs(KeyPressedEventArgs args)
    {
        return args.Modifier == Modifier && args.Key == KeyCode;
    }

    public override bool Equals(object obj)
    {
        if (obj is not HotKey h)
        {
            return false;
        }

        return Equals(h);
    }

    protected bool Equals(HotKey other)
    {
        return KeyCode == other.KeyCode
            && Control == other.Control
            && Alt == other.Alt
            && Shift == other.Shift;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = (int)KeyCode;
            hashCode = (hashCode * 397) ^ Control.GetHashCode();
            hashCode = (hashCode * 397) ^ Alt.GetHashCode();
            hashCode = (hashCode * 397) ^ Shift.GetHashCode();
            return hashCode;
        }
    }

    public static bool operator ==(HotKey key1, HotKey key2)
    {
        return key1 != null && key2 != null && key1.Equals(key2);
    }

    public static bool operator !=(HotKey key1, HotKey key2)
    {
        return !(key1 == key2);
    }
}