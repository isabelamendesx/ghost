using Cocona;
using Ghost.Common;
using Ghost.Infrastructure.Utilities;

namespace Ghost.Application.Commands;

public sealed class ConfigurationCommand
{
    [Command("config", Description = "Handles configuration settings for the application.")]
    public void Handle(
        [Argument(Description = "The configuration option to set. Valid options are: apikey.")]
        string option
        )
    {
        Action action = option.ToLower() switch
        {
            "apikey" => () => TrySetApiKey(),
            _ => () => Console.WriteLine("Invalid option. Valid options are: apikey")
        };

        action();
    }

    private void TrySetApiKey()
    {
        Console.WriteLine("enter your API Key:");
        string apiKey = SecretReader.Read();

        if (!FileManager.Exists(GlobalConfig.ConfigFilePath, "appsettings.json")) return;

        var configFile = JsonFileManager.ReadFileAsJson();

        configFile.TrySetValue("GeminiOptions", "ApiKey", apiKey);
        configFile.WriteInFileInJsonFormat();

        Console.WriteLine("\nAPI key saved successfully");
    }
}
