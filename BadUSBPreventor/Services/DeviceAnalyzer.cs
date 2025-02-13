// Services/DeviceAnalyzer.cs
using System.Management;
using System.Text.RegularExpressions;
using BadUSBPreventor.Models;

namespace BadUSBPreventor.Services
{
    public static class DeviceAnalyzer
    {
        /// <summary>
        /// Analyzes a ManagementBaseObject and returns a UsbDeviceInfo.
        /// </summary>
        public static UsbDeviceInfo AnalyzeDevice(ManagementBaseObject instance)
        {
            // For safety, if instance["DeviceID"] is null, we do not want a crash
            var rawDeviceId = instance["DeviceID"]?.ToString() ?? "";
            var description = instance["Description"]?.ToString() ?? "";
            var deviceClass = instance["PNPClass"]?.ToString() ?? "";
            
            string vendorId = "Unknown";
            string productId = "Unknown";
            
            // Parse VendorID and ProductID from the deviceId
            var regex = new Regex(@"VID_([0-9A-F]{4})&PID_([0-9A-F]{4})", RegexOptions.IgnoreCase);
            var match = regex.Match(rawDeviceId);
            if (match.Success)
            {
                vendorId = match.Groups[1].Value;
                productId = match.Groups[2].Value;
            }
            
            var serialNumber = ExtractSerialNumber(rawDeviceId);
            
            return new UsbDeviceInfo(
                deviceId: rawDeviceId,
                description: description,
                vendorId: vendorId,
                productId: productId,
                deviceClass: deviceClass,
                serialNumber: serialNumber
            );
        }

        /// <summary>
        /// Extract from DeviceID what comes after the last "\".
        /// For example: "USB\VID_046D&amp;PID_C09D\2058355B4E57" → "2058355B4E57"
        /// If cannot extract, returns "Unknown".
        /// </summary>
        private static string ExtractSerialNumber(string deviceId)
        {
            int lastSlashIndex = deviceId.LastIndexOf('\\');
            if (lastSlashIndex < 0 || lastSlashIndex == deviceId.Length - 1)
                return "Unknown";

            var serial = deviceId.Substring(lastSlashIndex + 1).Trim();
            return string.IsNullOrWhiteSpace(serial) ? "Unknown" : serial;
        }

        /// <summary>
        /// Checks if a device is suspicious (e.g. HID without whitelist)
        /// </summary>
        public static bool IsSuspicious(UsbDeviceInfo device)
        {
            bool isHid = device.DeviceClass.Equals("HIDClass", System.StringComparison.OrdinalIgnoreCase) ||
                         device.Description.IndexOf("HID", System.StringComparison.OrdinalIgnoreCase) >= 0;
            // TODO: Implement whitelist check
            return isHid;
        }
    }
}
