using Cocona.Filters;
using Ghost.Infrastructure.Services;
using Library;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Ghost.Application.Filters;

public class PreExecutionFilter : ICommandFilter
{
    private readonly IGitService _gitService;
    private readonly GeminiOptions _geminiOptions;

    public PreExecutionFilter(IOptions<GeminiOptions> geminiOptions, IGitService gitService)
    {
        _gitService = gitService;
        _geminiOptions = geminiOptions.Value;
    }

    public async ValueTask<int> OnCommandExecutionAsync(CoconaCommandExecutingContext ctx, CommandExecutionDelegate next)
    {
        if (string.IsNullOrWhiteSpace(_geminiOptions.ApiKey))
            throw new InvalidOperationException("API key is not configured. Please set the API_KEY in appsettings.json or type gitghost config apikey");

        await _gitService.EnsureGitIsInstalledAsync();
        await _gitService.EnsureIsGitRepositoryAsync();

        return await next(ctx);
    }
}

class PreExecutionFilterAttribute : Attribute, IFilterFactory
{
    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
    {
        return ActivatorUtilities.CreateInstance<PreExecutionFilter>(serviceProvider);
    }
}
