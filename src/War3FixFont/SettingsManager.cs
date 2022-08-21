using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace War3FixFont;

public class SettingsManager
{
    private string _path;

    private readonly string _exe = Assembly.GetExecutingAssembly().GetName().Name;

    public Settings Settings { get; private set; }

    public void Load(string path = null)
    {
        _path = new FileInfo(path ?? _exe + ".json").FullName;
        if (File.Exists(_path))
        {
            var json = File.ReadAllText(_path);
            Settings = JsonConvert.DeserializeObject<Settings>(json);
        }
        else
        {
            Settings = new();
        }
    }

    public void Save()
    {
        var json = JsonConvert.SerializeObject(Settings, Formatting.Indented);
        File.WriteAllText(_path, json, Encoding.UTF8);
    }
}