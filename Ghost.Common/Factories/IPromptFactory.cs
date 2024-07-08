using Ghost.Infrastructure.Models.Enum;

namespace Ghost.Infrastructure.Factories;

public interface IPromptFactory
{
    string CreateCommitPrompt(PromptType promptType, string stagedChanges);
}
