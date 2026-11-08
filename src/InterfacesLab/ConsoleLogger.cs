using InterfacesLab.Logging;
public class ConsoleLogger : ILogger
{
    public void Log(string message)
        => Console.WriteLine($"[CONSOLE] {DateTime.Now:HH:mm:ss} — {message}");
}