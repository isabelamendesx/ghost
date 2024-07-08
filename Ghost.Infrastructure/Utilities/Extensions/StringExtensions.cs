namespace Ghost.Infrastructure.Utilities.Extensions;

public static class StringExtensions
{
    public static List<string> Separate(this string messages)
    {
        return messages.Split('|')
                       .Select(message => message.Trim().ToLower())
                       .ToList();
    }
    public static List<string> AddPrefix(this IEnumerable<string> messages, string prefix)
    {
        return messages.Select(message => $"{prefix} - {message}")
                       .ToList();
    }
}
