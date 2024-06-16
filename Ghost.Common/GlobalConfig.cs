namespace Ghost.Common;

public static class GlobalConfig
{
    public static readonly string BaseDirectory = Directory.GetCurrentDirectory();
    public static readonly string ConfigFilePath = Path.Combine(BaseDirectory, "appsettings.json");
}
