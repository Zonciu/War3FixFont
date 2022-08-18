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
    /// 使用无边框全屏
    /// </summary>
    [JsonProperty]
    public bool UseFullScreen { get; set; } = true;

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
    /// 使用模式2
    /// </summary>
    [JsonProperty]
    public bool UseMode2 { get; set; } = false;

    /// <summary>
    /// 修复频率限制
    /// </summary>
    [JsonProperty]
    public int FixThreshold { get; set; } = 5;

    /// <summary>
    /// 修复方向
    /// </summary>
    [JsonProperty]
    [JsonConverter(typeof(StringEnumConverter))]
    public FixDirection FixDirection { get; set; } = FixDirection.Width;
}