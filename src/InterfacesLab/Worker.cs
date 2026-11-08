using InterfacesLab.Logging;

public static class Worker
{
    public static void DoWork(ILogger logger)
    {
        logger.Log("Начало работы");
        // ... какая-то логика ...
        logger.Log("Работа завершена");
    }
}