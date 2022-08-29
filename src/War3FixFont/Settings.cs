using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace War3FixFont;

/// <summary>
/// 程序设置
/// </summary>
[JsonObject]
public class Settings
{
    /// <summary>
    /// 使用快捷键
    /// </summary>
    [JsonProperty]
    public bool UseHotKey { get; set; } = true;

    /// <summary>
    /// 快捷键
    /// </summary>
    [JsonProperty("HotKey")]
    public string HotKeyString { get; set; } = "0,1,1,D";

    /// <summary>
    /// 快捷键
    /// </summary>
    [JsonIgnore]
    public HotKey HotKey
    {
        get => HotKey.Deserialize(HotKeyString);
        set => HotKeyString = value.Serialize();
    }

    /// <summary>
    /// 呼出本窗口快捷键
    /// </summary>
    [JsonProperty("ShowMe")]
    public string ShowMeHotKeyString { get; set; } = "1,0,0,Q";

    /// <summary>
    /// 呼出本窗口快捷键
    /// </summary>
    [JsonIgnore]
    public HotKey ShowMeHotKey
    {
        get => HotKey.Deserialize(ShowMeHotKeyString);
        set => ShowMeHotKeyString = value.Serialize();
    }

    /// <summary>
    /// 窗口模式
    /// </summary>
    [JsonProperty]
    public WindowMode WindowMode { get; set; } = WindowMode.FullScreenWindow;

    /// <summary>
    /// 使用定时修复
    /// </summary>
    [JsonProperty]
    public bool UseTimer { get; set; } = true;

    /// <summary>
    /// 定时修复间隔
    /// </summary>
    [JsonProperty]
    public int TimerInterval { get; set; } = 60;

    /// <summary>
    /// 修复频率限制
    /// </summary>
    [JsonProperty]
    public int FixThreshold { get; set; } = 1;

    /// <summary>
    /// 自动应用窗口模式
    /// </summary>
    [JsonProperty]
    public bool UseAutoWindow { get; set; } = true;

    /// <summary>
    /// 锁定鼠标
    /// </summary>
    [JsonProperty]
    public bool LockCursor { get; set; } = true;
}