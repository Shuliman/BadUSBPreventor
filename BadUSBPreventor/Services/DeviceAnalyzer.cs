using System.Text.RegularExpressions;
using System.Management;
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
            string? deviceId = instance["DeviceID"]?.ToString();
            string? description = instance["Description"]?.ToString();
            string? deviceClass = instance["PNPClass"]?.ToString();

            // Default values for VendorId and ProductId
            string vendorId = "Unknown";
            string productId = "Unknown";

            // Regex for fetching VendorID and ProductID from DeviceId string
            // Regex looking for substring: "VID_XXXX&PID_XXXX"
            var regex = new Regex(@"VID_([0-9A-F]{4})&PID_([0-9A-F]{4})", RegexOptions.IgnoreCase);
            var match = regex.Match(deviceId ?? "");
            if (match.Success)
            {
                vendorId = match.Groups[1].Value;
                productId = match.Groups[2].Value;
            }

            return new UsbDeviceInfo(deviceId, description, vendorId, productId, deviceClass);
        }

        /// <summary>
        /// Checks if a device is suspicious (e.g. HID without whitelist)
        /// </summary>
        public static bool IsSuspicious(UsbDeviceInfo device)
        {
            bool isHid = device.DeviceClass.Equals("HIDClass", System.StringComparison.OrdinalIgnoreCase) ||
                         device.Description.IndexOf("HID", System.StringComparison.OrdinalIgnoreCase) >= 0;
            // TODO whitelist check
            return isHid;
        }
    }
}