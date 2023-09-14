using Newtonsoft.Json;

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
    /// 修复快捷键
    /// </summary>
    [JsonProperty("HotKey")]
    public string HotKeyString { get; set; } = "0,1,1,D";

    /// <summary>
    /// 修复快捷键
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
    public WindowMode WindowMode { get; set; } = WindowMode.FullScreen;

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
    public bool AutoApplyWindow { get; set; } = true;

    /// <summary>
    /// 锁定鼠标
    /// </summary>
    [JsonProperty]
    public bool LockCursor { get; set; } = true;

    /// <summary>
    /// 自定义宽度
    /// </summary>
    [JsonProperty]
    public int Width { get; set; }

    /// <summary>
    /// 自定义高度
    /// </summary>
    [JsonProperty]
    public int Height { get; set; }

    /// <summary>
    /// 启用自定义位置
    /// </summary>
    [JsonProperty]
    public bool UseCustomPosition { get; set; }

    /// <summary>
    /// 自定义位置X
    /// </summary>
    [JsonProperty]
    public int X { get; set; }

    /// <summary>
    /// 自定义位置Y
    /// </summary>
    [JsonProperty]
    public int Y { get; set; }
}