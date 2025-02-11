using System.Runtime.InteropServices;
using BadUSBPreventor.Models;
using BadUSBPreventor.Services;

namespace BadUSBPreventor
{
    /// <summary>
    /// Main program entry point.
    /// </summary>
    static class Program
    {
        private static void Main(string[] args)
        {
            if (!IsWindows())
                return;

            var config = SetupConfig(args);
            if (config.ShowHelp)
                return;

            Logger.Init(config.LogMode);
            Logger.Info("Starting USB device monitoring...");

            using var monitor = CreateUsbMonitor(config);
            monitor.Start();

            Logger.Info("Monitoring USB devices. Press any key to exit...");
            Console.ReadKey();

            monitor.Stop();
        }

        private static bool IsWindows()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WriteLine("This application is designed to run on Windows.");
                return false;
            }
            return true;
        }

        private static AppConfig SetupConfig(string[] args)
        {
            var config = AppConfig.ParseArguments(args);
            if (config.ShowHelp)
            {
                AppConfig.PrintUsage();
            }
            return config;
        }

        private static UsbMonitor CreateUsbMonitor(AppConfig config)
        {
            var monitor = new UsbMonitor();
            monitor.DeviceInserted += (sender, device) => HandleDeviceInserted(device, config);
            return monitor;
        }

        private static void HandleDeviceInserted(UsbDeviceInfo device, AppConfig config)

        {
            if (config.OnlySuspicious && !DeviceAnalyzer.IsSuspicious(device))
                return;

            Logger.Info("New USB device detected:");
            Logger.Info(device.ToString());

            if (DeviceAnalyzer.IsSuspicious(device))
                Logger.Warning("Suspicious device detected!");
        }

    }
}
