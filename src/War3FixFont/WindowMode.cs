using System.ComponentModel;

namespace War3FixFont;

/// <summary>
/// 窗口模式
/// </summary>
public enum WindowMode
{
    /// <summary>
    /// 保持当前窗口
    /// </summary>
    [Description("保持当前")]
    KeepCurrent,

    /// <summary>
    /// 全屏窗口
    /// </summary>
    [Description("无边框全屏")]
    FullScreenWindow,

    /// <summary>
    /// 最大化窗口
    /// </summary>
    [Description("有边框最大化")]
    MaxWindows
}