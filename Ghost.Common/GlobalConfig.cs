namespace Ghost.Common;

public static class GlobalConfig
{
    public static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
    public static readonly string ConfigFilePath = Path.Combine(BaseDirectory, "appsettings.json");
}
