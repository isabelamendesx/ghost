using Ghost.Common.Services;
using Ghost.Infrastructure.Utilities.Extensions;

namespace Ghost.Infrastructure.Services;

public class StartingWithCodeFormatter : IMessageFormatter
{
    public List<string> Format(string messages, string? code = null) 
        => messages.Separate().AddPrefix(code!);
}

public class CustomFormatter : IMessageFormatter
{
    public List<string> Format(string messages, string? code = null) 
        => messages.Separate();
}

public class ConventionalFormatter : IMessageFormatter
{
    public List<string> Format(string messages, string? code = null) 
        => messages.Separate();
}

