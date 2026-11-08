using InterfacesLab.Logging;
public class FileLogger : ILogger
{
    private readonly string _path;

    public FileLogger(string path) => _path = path;

    public void Log(string message)
    {
        var line = $"[FILE] {DateTime.Now:yyyy-MM-dd HH:mm:ss} — {message}";
        File.AppendAllText(_path, line + Environment.NewLine);
        Console.WriteLine($"Записано в {_path}: {message}");
    }
}