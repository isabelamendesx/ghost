namespace Ghost.Common.Services;

public interface IMessageFormatter
{
    List<string> Format(string messages, string? code = null);
}
