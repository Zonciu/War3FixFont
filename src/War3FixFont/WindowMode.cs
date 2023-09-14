using System.ComponentModel;

namespace War3FixFont;

/// <summary>
/// 窗口模式
/// </summary>
public enum WindowMode
{
    /// <summary>
    /// 全屏窗口
    /// </summary>
    [Description("无边框全屏")]
    FullScreen,

    /// <summary>
    /// 最大化窗口
    /// </summary>
    [Description("最大化窗口")]
    Maximum,

    /// <summary>
    /// 4:3窗口
    /// </summary>
    [Description("4:3窗口")]
    Ratio43,

    /// <summary>
    /// 自定义窗口尺寸
    /// </summary>
    [Description("自定义窗口")]
    Custom
}