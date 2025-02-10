# BadUSBPreventor

## Overview

**BadUSBPreventor** is a console application designed to monitor USB device connections on Windows. It listens to WMI events and logs detailed information about newly connected Plug and Play devices.

## Features

- **Real-time monitoring:** Listens for new device connection events using WMI.
- **Detailed logging:** Logs all properties of the connected device as reported by Windows.
- **Device analysis:** Extracts information like DeviceID, VendorID, ProductID, and Device Class using regex.
- **Suspicious device detection:** (Planned) Flag devices that match certain criteria (e.g., HID devices not on a whitelist).
- **Admin privileges:** The application requires administrative rights (set via the application manifest).


## Usage
1. **Build the application** using your preferred .NET build tool.
2. **Run the executable**.
    - The application requires administrative privileges.
3. **Monitor device events:** The console will display detailed information for each new device connected.

## License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for more details.
