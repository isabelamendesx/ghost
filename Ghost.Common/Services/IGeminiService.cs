using Ghost.Infrastructure.Models.Enum;

namespace Ghost.Common.Services;

public interface IGeminiService
{
    Task<List<string>> GetCommitMessages(PromptType promptType, string stagedChanges, string? code = null);
}
