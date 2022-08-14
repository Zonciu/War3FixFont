using System;

namespace War3FixFont.WinAPI;

[Flags]
public enum ModifierKeys : uint
{
    Alt = 1,

    Control = 2,

    Shift = 4,

    Win = 8
}