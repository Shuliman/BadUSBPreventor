namespace BadUSBPreventor;

/// <summary>
/// Module for parsing command-line arguments and application configuration.
/// </summary>
public class AppConfig
{
    public bool ShowHelp { get; private set; }
    public bool OnlySuspicious { get; private set; }

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
                // Additional arguments can be processed here.
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
        // Extend with additional options if needed.
    }
}