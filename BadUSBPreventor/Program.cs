using System;
using System.Runtime.InteropServices;
using BadUSBPreventor.Services;

namespace BadUSBPreventor
{
    /// <summary>
    /// Main program entry point.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Validate platform compatibility (Windows only)
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WriteLine("This application is designed to run on Windows.");
                return;
            }

            // Parse command-line arguments
            var config = AppConfig.ParseArguments(args);
            if (config.ShowHelp)
            {
                AppConfig.PrintUsage();
                return;
            }

            Logger.LogInfo("Starting USB device monitoring...");

            using var monitor = new UsbMonitor();
            // Subscribe to the device insertion event.
            monitor.DeviceInserted += (sender, device) =>
            {
                // If the "--only-suspicious" flag is set, filter out non-suspicious devices.
                if (config.OnlySuspicious && !DeviceAnalyzer.IsSuspicious(device))
                {
                    return;
                }

                Logger.LogInfo("New USB device detected:");
                Logger.LogInfo(device.ToString());

                // Additional processing can be added here (e.g., compare against a whitelist).
                if (DeviceAnalyzer.IsSuspicious(device))
                {
                    Logger.LogWarning("Suspicious device detected!");
                }
            };

            monitor.Start();

            Logger.LogInfo("Monitoring USB devices. Press any key to exit...");
            Console.ReadKey();

            monitor.Stop();
        }
    }
}
