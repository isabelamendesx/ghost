using Ghost.Common.Services;
using Ghost.Infrastructure.Models.Enum;
using Ghost.Infrastructure.Services;

namespace Ghost.Infrastructure.Factories;

public static class MessageFormatterFactory
{
    public static IMessageFormatter GetFormatter(PromptType promptType)
    {
        return promptType switch
        {
            PromptType.StartingWithCode => new StartingWithCodeFormatter(),
            PromptType.Custom => new CustomFormatter(),
            PromptType.Conventional => new ConventionalFormatter(),
            _ => throw new ArgumentException("Invalid prompt type")
        };
    }
}
