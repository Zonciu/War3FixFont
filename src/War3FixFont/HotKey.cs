using System;
using System.Text;
using System.Windows.Forms;
using War3FixFont.WinAPI;

namespace War3FixFont;

public record HotKey
{
    public const string DefaultString = "0,1,1,D";

    public static readonly HotKey Default = new(DefaultString);

    public Keys KeyCode { get; set; }

    public bool Control { get; set; }

    public bool Alt { get; set; }

    public bool Shift { get; set; }

    public bool IsValid => Control | Shift | Alt && KeyCode != Keys.None && KeyCode != Keys.Back && KeyCode != Keys.Delete && KeyCode != Keys.PrintScreen;

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
}