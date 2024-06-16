using Cocona;
using Ghost.Infrastructure;
using Ghost.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Ghost.Application.UseCases.PickMessage;
using Ghost.Application.Filters;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = CoconaApp.CreateBuilder();

        builder.Services.RegisterInfrastructureLayerServices(builder.Configuration);

        builder.Services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(PickMessageHandler).Assembly);
        });

        builder.Logging.AddFilter("System.Net.Http", LogLevel.Warning);

        builder.Configuration.AddJsonFile(GlobalConfig.ConfigFilePath, optional: false, reloadOnChange: true);

        var app = builder.Build();

        app.UseFilter(new ExceptionHandlingFilterAttribute());

        app.RegisterApplicationLayerCommands();

        app.Run();
    }
}

