namespace BadUSBPreventor.Models;

/// <summary>
/// Data class to store USB device information.
/// </summary>
public class UsbDeviceInfo(
    string? deviceId,
    string? description,
    string? vendorId,
    string? productId,
    string? deviceClass)
{
    public string DeviceId { get; } = string.IsNullOrWhiteSpace(deviceId) ? "No data" : deviceId!;
    public string Description { get; } = string.IsNullOrWhiteSpace(description) ? "No description" : description!;
    public string VendorId { get; } = string.IsNullOrWhiteSpace(vendorId) ? "Unknown" : vendorId!;
    public string ProductId { get; } = string.IsNullOrWhiteSpace(productId) ? "Unknown" : productId!;
    public string DeviceClass { get; } = string.IsNullOrWhiteSpace(deviceClass) ? "Unknown" : deviceClass!;

    public override string ToString() =>
        $"DeviceID: {DeviceId}\n" +
        $"Description: {Description}\n" +
        $"VendorID: {VendorId}\n" +
        $"ProductID: {ProductId}\n" +
        $"DeviceClass: {DeviceClass}";
}