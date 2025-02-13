using BadUSBPreventor.Enums;
using BadUSBPreventor.Models;

namespace BadUSBPreventor;

/// <summary>
/// Simple logger for console output.
/// </summary>
public static class Logger
{
    private static LoggingMode _logMode;
    
    public static void Init(LoggingMode mode)
    {
        _logMode = mode;
    }

    public static void Info(string message)
    {
        Console.WriteLine($"[INFO] {message}");
    }
    
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
    
    public static void LogDevice(UsbDeviceInfo device)
    {
        //Detecting and applying LogMode settings  
        Info(_logMode == LoggingMode.Detailed ? device.ToString() : device.ToStringBrief());
    }
}
