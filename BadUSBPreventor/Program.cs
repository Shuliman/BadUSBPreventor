namespace BadUSBPreventor;

using System;
using System.Management;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting USB device monitoring...");

// Create a query to track new USB devices.
// You can change the filter in the query to more accurately select the desired class of devices.
        WqlEventQuery query = new WqlEventQuery(
            "__InstanceCreationEvent",
            new TimeSpan(0, 0, 1),
            "TargetInstance ISA 'Win32_USBHub'");

        using (ManagementEventWatcher watcher = new ManagementEventWatcher(query))
        {
            watcher.EventArrived += DeviceInsertedEvent;
            watcher.Start();

            Console.WriteLine("Waiting for devices to connect. Press any key to exit...");
            Console.ReadKey();

            watcher.Stop();
        }
    }

    private static void DeviceInsertedEvent(object sender, EventArrivedEventArgs e)
    {
// Get an object describing the device
        ManagementBaseObject instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
        string deviceId = instance["DeviceID"]?.ToString() ?? "No data";
        string description = instance["Description"]?.ToString() ?? "No description";

        Console.WriteLine("New USB device detected:");
        Console.WriteLine($"DeviceID: {deviceId}");
        Console.WriteLine($"Description: {description}");

// TODO - analyse
// - Determine device class (e.g. if description or other fields contain HID hint).
// - Extract VendorID and ProductID from deviceId string.
// - Compare with whitelist.
    }
}