namespace Ghost.Common.Services;

public interface IGitService
{
    Task CommitChangesAsync(string message);
    Task EnsureGitIsInstalledAsync();
    Task EnsureIsGitRepositoryAsync();
    Task<string> GetStagedChangesAsync();
}