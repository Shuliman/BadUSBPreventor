namespace BadUSBPreventor;

/// <summary>
/// Simple logger for console output.
/// </summary>
public static class Logger
{
    public static void LogInfo(string message)
    {
        Console.WriteLine("[INFO] " + message);
    }

    public static void LogWarning(string message)
    {
        Console.WriteLine("[WARNING] " + message);
    }

    public static void LogError(string message)
    {
        Console.WriteLine("[ERROR] " + message);
    }
}