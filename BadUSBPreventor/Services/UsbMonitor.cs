using System.Management;
using BadUSBPreventor.Models;

namespace BadUSBPreventor.Services;

/// <summary>
/// Module for monitoring USB device events using WMI.
/// </summary>
public class UsbMonitor : IDisposable
{
    private ManagementEventWatcher _watcher;

    /// <summary>
    /// Event triggered when a new USB device is inserted.
    /// </summary>
    public event EventHandler<UsbDeviceInfo> DeviceInserted;

    public UsbMonitor()
    {
        // WMI query to monitor new USB devices.
        // We are using Win32_USBHub as a filter; adjust the query to cover other device types if needed.
        WqlEventQuery query = new WqlEventQuery(
            "__InstanceCreationEvent",
            new TimeSpan(0, 0, 1),
            "TargetInstance ISA 'Win32_PnPEntity'");

        _watcher = new ManagementEventWatcher(query);
        _watcher.EventArrived += OnEventArrived;
    }

    /// <summary>
    /// Starts the USB monitoring.
    /// </summary>
    public void Start()
    {
        _watcher.Start();
    }

    /// <summary>
    /// Stops the USB monitoring.
    /// </summary>
    public void Stop()
    {
        _watcher.Stop();
    }

    private void OnEventArrived(object sender, EventArrivedEventArgs e)
    {
        try
        {
            ManagementBaseObject instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
            var deviceInfo = DeviceAnalyzer.AnalyzeDevice(instance);

            // Raise the event for the new device insertion.
            DeviceInserted?.Invoke(this, deviceInfo);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing device event: {ex.Message}");
        }
    }

    public void Dispose()
    {
        if (_watcher != null)
        {
            _watcher.EventArrived -= OnEventArrived;
            _watcher.Dispose();
        }
    }
}