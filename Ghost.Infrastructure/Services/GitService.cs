using CliWrap.Exceptions;
using CliWrap;
using Ghost.Infrastructure.Services;
using CliWrap.Buffered;

namespace Ghost.Infrastructure.Services;

public class GitService : IGitService
{
    public async Task EnsureGitIsInstalledAsync()
    {
        try
        {
            var result = await Cli.Wrap("git")
                .WithArguments("--version")
                .ExecuteBufferedAsync();
        }
        catch (CommandExecutionException ex)
        {
            throw new InvalidOperationException("Git is not installed. Please install Git to proceed.", ex);
        }
    }

    public async Task EnsureIsGitRepositoryAsync()
    {
        try
        {
            var result = await Cli.Wrap("git")
                .WithArguments("rev-parse --is-inside-work-tree")
                .ExecuteBufferedAsync();
        }
        catch (CommandExecutionException ex)
        {
            throw new InvalidOperationException("The current directory is not a Git repository.", ex);
        }
    }

    public async Task<string> GetStagedChangesAsync()
    {
        try
        {
            var result = await Cli.Wrap("git")
                .WithArguments("diff --cached")
                .ExecuteBufferedAsync();

            if (string.IsNullOrWhiteSpace(result.StandardOutput))
                throw new InvalidOperationException("No staged changes found.");

            return result.StandardOutput;
        }
        catch (CommandExecutionException ex)
        {
            throw new InvalidOperationException("Failed to retrieve staged changes from Git, please check your staged changes and try again.", ex);
        }
    }

    public async Task CommitChangesAsync(string message)
    {
        try
        {
            var result = await Cli.Wrap("git")
                .WithArguments($"commit -m \"{message}\"")
                .ExecuteBufferedAsync();

            Console.WriteLine(result.StandardOutput);
        }
        catch (CommandExecutionException ex)
        {
            throw new InvalidOperationException("Git commit failed, please try again", ex);
        }
    }
}
