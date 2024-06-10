using Cocona;
using Ghost.Application;
using Ghost.Common;
using Ghost.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = CoconaApp.CreateBuilder();

        builder.Logging.AddFilter("System.Net.Http", LogLevel.Warning);

        builder.Configuration.AddJsonFile(GlobalConfig.ConfigFilePath, optional: false, reloadOnChange: true);

        builder.Services.RegisterInfrastructureLayerServices(builder.Configuration);

        var app = builder.Build();

        //app.UseFilter(new ExceptionHandlingFilterAttribute());

        app.RegisterApplicationLayerCommands();

        app.Run();
    }
}

