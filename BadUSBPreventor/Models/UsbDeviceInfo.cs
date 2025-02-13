namespace BadUSBPreventor.Models;

/// <summary>
/// Data class to store USB device information.
/// </summary>
public class UsbDeviceInfo(
    string? deviceId,
    string? description,
    string? vendorId,
    string? productId,
    string? deviceClass,
    string? serialNumber 
)
{
    public string DeviceId { get; } = string.IsNullOrWhiteSpace(deviceId) ? "No data" : deviceId!;
    public string Description { get; } = string.IsNullOrWhiteSpace(description) ? "No description" : description!;
    public string VendorId { get; } = string.IsNullOrWhiteSpace(vendorId) ? "Unknown" : vendorId!;
    public string ProductId { get; } = string.IsNullOrWhiteSpace(productId) ? "Unknown" : productId!;
    public string DeviceClass { get; } = string.IsNullOrWhiteSpace(deviceClass) ? "Unknown" : deviceClass!;
    public string SerialNumber { get; } = string.IsNullOrWhiteSpace(serialNumber) ? "Unknown" : serialNumber!;

    public override string ToString() =>
        $"DeviceID: {DeviceId}\n" +
        $"Description: {Description}\n" +
        $"VendorID: {VendorId}\n" +
        $"ProductID: {ProductId}\n" +
        $"DeviceClass: {DeviceClass}\n" +
        $"SerialNumber: {SerialNumber}";

    public string ToStringBrief()
    {
        // Format example: "HID-compliant mouse [VID=046D, PID=C09D, S/N=2058355B4E57, Class=HIDClass]"
        // If SerialNumber is not found, output "S/N=?" or skip it altogether.
        var serialPart = string.IsNullOrEmpty(SerialNumber) ? "?" : SerialNumber;
        return $"{Description} [VID={VendorId}, PID={ProductId}, S/N={serialPart}, Class={DeviceClass}]";
    }
}