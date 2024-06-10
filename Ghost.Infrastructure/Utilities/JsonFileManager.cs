using Ghost.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ghost.Infrastructure.Utilities;

public static class JsonFileManager
{
    public static JObject ReadFileAsJson()
    {
        var content = FileManager.Read(GlobalConfig.ConfigFilePath);
        return JObject.Parse(content);
    }

    public static void TrySetValue(this JObject config, string section, string key, string value)
    {
        if (config[section] != null)
            config[section]![key] = value;

        throw new InvalidOperationException($"The section {section} is not configured in the configuration file.");
    }

    public static void WriteInFileInJsonFormat(this JObject config)
    {
        var updatedJson = JsonConvert.SerializeObject(config, Formatting.Indented);
        FileManager.Write(GlobalConfig.ConfigFilePath, updatedJson);
    }
}
