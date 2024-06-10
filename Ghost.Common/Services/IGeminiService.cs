using Ghost.Infrastructure.Models.Enum;

namespace Ghost.Common.Services;

public interface IGeminiService
{
    Task<string> GetCommitMessage(PromptType promptType, string stagedChanges, string? code = null);
}
