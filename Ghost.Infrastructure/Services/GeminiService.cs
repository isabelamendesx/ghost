﻿using Ghost.Common.Services;
using Ghost.Infrastructure.Factories;
using Ghost.Infrastructure.Models.Enum;
using Library.Api.v1;
using Library.Builders;

namespace Ghost.Infrastructure.Services;

public class GeminiService : IGeminiService
{
    private readonly IGeminiClient _gemini;
    private readonly IPromptFactory _promptFactory;

    public GeminiService(IGeminiClient gemini, IPromptFactory promptFactory)
    {
        _gemini = gemini;
        _promptFactory = promptFactory;
    }

    public async Task<List<string>> GetCommitMessages(PromptType promptType, string stagedChanges, string? code = null)
    {
        string prompt = promptType switch
        {
            PromptType.Conventional => _promptFactory.CreateConventionalCommitPrompt(stagedChanges),
            PromptType.StartingWithCode => _promptFactory.CreateCommitStartingWithCodePrompt(stagedChanges, code!),
            PromptType.Custom => _promptFactory.CreateCustomCommitPrompt(stagedChanges),
            _ => throw new ArgumentOutOfRangeException(nameof(promptType), promptType, null)
        };

        var generationConfig = new GenerationConfigBuilder()
                            .WithTemperature((float)0.5)
                            .Build();

        var response = await _gemini.SendTextPrompt(prompt, generationConfig);

        return SplitCommitMessages(response.Candidates![0].Content.Parts[0]);
    }

    private List<string> SplitCommitMessages(string messages)
    {
        return messages.Split(',').ToList();
    }
}
