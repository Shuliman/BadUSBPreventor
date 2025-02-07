namespace BadUSBPreventor.Models;

/// <summary>
/// Data class to store USB device information.
/// </summary>
public class UsbDeviceInfo
{
    // Non-nullable properties with initialization via constructor
    public string DeviceId { get; init; }
    public string Description { get; init; }
    public string VendorId { get; init; }
    public string ProductId { get; init; }
    public string DeviceClass { get; init; }
    
    public UsbDeviceInfo(
        string? deviceId,
        string? description,
        string? vendorId,
        string? productId,
        string? deviceClass)
    {
        // If there is no information - substitute with default values
        DeviceId = string.IsNullOrWhiteSpace(deviceId) ? "No data" : deviceId!;
        Description = string.IsNullOrWhiteSpace(description) ? "No description" : description!;
        VendorId = string.IsNullOrWhiteSpace(vendorId) ? "Unknown" : vendorId!;
        ProductId = string.IsNullOrWhiteSpace(productId) ? "Unknown" : productId!;
        DeviceClass = string.IsNullOrWhiteSpace(deviceClass) ? "Unknown" : deviceClass!;
    }

    public override string ToString() =>
        $"DeviceID: {DeviceId}\n" +
        $"Description: {Description}\n" +
        $"VendorID: {VendorId}\n" +
        $"ProductID: {ProductId}\n" +
        $"DeviceClass: {DeviceClass}";
}