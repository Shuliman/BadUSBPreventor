using BadUSBPreventor.Enums;

namespace BadUSBPreventor;

/// <summary>
/// Module for parsing command-line arguments and application configuration.
/// </summary>
public class AppConfig
{
    public bool ShowHelp { get; private set; }
    public bool OnlySuspicious { get; private set; }
    public LoggingMode LogMode { get; private set; } = LoggingMode.Brief;

    public static AppConfig ParseArguments(string[] args)
    {
        var config = new AppConfig();

        foreach (var arg in args)
        {
            switch (arg.ToLower())
            {
                case "--help":
                case "-h":
                    config.ShowHelp = true;
                    break;
                case "--only-suspicious":
                    config.OnlySuspicious = true;
                    break;
                case "--verbose":
                case "--full":
                    config.LogMode = LoggingMode.Detailed;
                    break;
                case "--brief": 
                    config.LogMode = LoggingMode.Brief;
                    break;
            }
        }
        return config;
    }

    public static void PrintUsage()
    {
        Console.WriteLine("Usage: BadUSBPreventor [options]");
        Console.WriteLine("Options:");
        Console.WriteLine("  --help, -h           Show this help message and exit");
        Console.WriteLine("  --only-suspicious    Display only suspicious USB devices");
        Console.WriteLine("  --verbose, --full    Use detailed logging mode (full WMI data)");
        Console.WriteLine("  --brief              Use brief logging mode (default)");
        Console.WriteLine();
        Console.WriteLine("Note: If no logging option is provided, brief mode is used by default.");
    }
}