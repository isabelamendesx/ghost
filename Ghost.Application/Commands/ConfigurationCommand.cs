﻿using Cocona;
using Ghost.Common;
using Ghost.Infrastructure.Utilities;
using System.Text;

namespace Ghost.Infrastructure.Commands;

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
            _ => () => Console.WriteLine("👻 Invalid option. Valid options are: apikey")
        };

        action();
    }

    private void TrySetApiKey()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("👻 Enter your API Key:");
        string apiKey = SecretReader.Read();

        if (!FileManager.Exists(GlobalConfig.ConfigFilePath, "appsettings.json")) return;

        var configFile = JsonFileManager.ReadFileAsJson();

        configFile.TrySetValue("GeminiOptions", "ApiKey", apiKey);
        configFile.WriteInFileInJsonFormat();

        Console.WriteLine("\n👻 API key saved successfully");
    }
}
