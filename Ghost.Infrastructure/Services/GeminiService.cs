using Ghost.Infrastructure.Factories;
using Ghost.Infrastructure.Models.Enum;
using Library.Api.v1;

namespace Ghost.Infrastructure.Services;

public class GeminiService
{
    private readonly IGeminiClient _gemini;
    private readonly IPromptFactory _promptFactory;

    public GeminiService(IGeminiClient gemini, IPromptFactory promptFactory)
    {
        _gemini = gemini;
        _promptFactory = promptFactory;
    }

    public async Task<string> GetCommitMessage(PromptType promptType, string stagedChanges, string? code = null)
    {
        string prompt = promptType switch
        {
            PromptType.Conventional => _promptFactory.CreateConventionalCommitPrompt(stagedChanges),
            PromptType.StartingWithCode => _promptFactory.CreateCommitStartingWithCodePrompt(stagedChanges, code!),
            PromptType.Custom => _promptFactory.CreateCustomCommitPrompt(stagedChanges),
            _ => throw new ArgumentOutOfRangeException(nameof(promptType), promptType, null)
        };

        var response = await _gemini.SendTextPrompt(prompt);
        return response.Candidates![0].Content.Parts[0];
    }
}
