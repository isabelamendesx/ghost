namespace Ghost.Infrastructure.Factories;

public interface IPromptFactory
{
    string CreateCommitStartingWithCodePrompt(string gitdiff, string code);
    string CreateConventionalCommitPrompt(string gitdiff);
    string CreateCustomCommitPrompt(string gitdiff);
}
