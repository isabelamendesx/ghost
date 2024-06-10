namespace Ghost.Infrastructure.Utilities;

public static class FileManager
{
    public static string Read(string filePath)
    {
        return File.ReadAllText(filePath);
    }

    public static void Write(string filePath, string content)
    {
        File.WriteAllText(filePath, content);
    }

    public static bool Exists(string filePath, string fileName)
    {
        if (File.Exists(filePath)) return true;

        throw new FileNotFoundException($"File {fileName} not found: {filePath}");
    }
}
