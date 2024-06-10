namespace Ghost.Infrastructure.Models;

public class Prompt
{
    public string Introduction { get; set; } = string.Empty;
    public string Format { get; set; } = string.Empty;
    public List<string>? Types { get; set; } = new List<string>();
    public List<string> Specification { get; set; } = new List<string>();
    public string? Footer { get; set; } = string.Empty;
}
