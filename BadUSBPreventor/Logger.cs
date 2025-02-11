using BadUSBPreventor.Enums;

namespace BadUSBPreventor;

/// <summary>
/// Simple logger for console output.
/// </summary>
public static class Logger
{
    private static LoggingMode _logMode = LoggingMode.Brief;
    
    public static void Init(LoggingMode mode)
    {
        _logMode = mode;
    }

    public static void Info(string message)
    {
        Console.WriteLine($"[INFO] {message}");
    }

    // Метод для вывода подробных логов – срабатывает только в Detailed режиме.
    public static void Detailed(string message)
    {
        if (_logMode == LoggingMode.Detailed)
        {
            Console.WriteLine($"[DETAIL] {message}");
        }
    }

    public static void Warning(string message)
    {
        Console.WriteLine($"[WARNING] {message}");
    }
}
