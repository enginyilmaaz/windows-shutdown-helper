using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Devices.Enumeration;

namespace WindowsShutdownHelper.Functions
{
    public class BleDeviceInfo
    {
        public ulong BluetoothAddress { get; set; }
        public string MacAddress { get; set; }
        public string LocalName { get; set; }
        public short RssiDbm { get; set; }
        public DateTime LastSeen { get; set; }
    }

    internal static class BluetoothScanner
    {
        private static BluetoothLEAdvertisementWatcher _discoveryWatcher;
        private static BluetoothLEAdvertisementWatcher _monitorWatcher;
        private static DeviceWatcher _classicBtWatcher;

        private static readonly ConcurrentDictionary<ulong, BleDeviceInfo> _discoveredDevices = new();
        private static readonly ConcurrentDictionary<ulong, DateTime> _monitorLastSeen = new();

        private static readonly object _discoveryLock = new();
        private static readonly object _monitorLock = new();

        private static bool _isDiscovering;
        private static bool _isMonitoring;
        private static int _discoveryVersion;

        public static event Action<BleDeviceInfo> DeviceDiscovered;

        // Bluetooth Classic protocol ID for DeviceWatcher
        private const string ClassicBtProtocolId = "{e0cbf06c-cd8b-4647-bb8a-263b43f0f974}";

        // ---------- Discovery Scan (UI device list) ----------

        public static void StartDiscoveryScan()
        {
            lock (_discoveryLock)
            {
                if (_isDiscovering) return;

                _discoveredDevices.Clear();
                Interlocked.Increment(ref _discoveryVersion);

                // BLE advertisement watcher
                _discoveryWatcher = new BluetoothLEAdvertisementWatcher
                {
                    ScanningMode = BluetoothLEScanningMode.Active
                };

                _discoveryWatcher.Received += OnDiscoveryReceived;
                _discoveryWatcher.Start();

                // Classic Bluetooth device watcher
                try
                {
                    string selector = "System.Devices.Aep.ProtocolId:=\"" + ClassicBtProtocolId + "\"";
                    var requestedProps = new string[] { "System.Devices.Aep.DeviceAddress" };
                    _classicBtWatcher = DeviceInformation.CreateWatcher(
                        selector, requestedProps, DeviceInformationKind.AssociationEndpoint);

                    _classicBtWatcher.Added += OnClassicDeviceAdded;
                    _classicBtWatcher.Updated += OnClassicDeviceUpdated;
                    _classicBtWatcher.Start();
                }
                catch
                {
                    // Classic BT not available, continue with BLE only
                    _classicBtWatcher = null;
                }

                _isDiscovering = true;
            }
        }

        public static void StopDiscoveryScan()
        {
            lock (_discoveryLock)
            {
                if (!_isDiscovering) return;

                if (_discoveryWatcher != null)
                {
                    _discoveryWatcher.Received -= OnDiscoveryReceived;
                    _discoveryWatcher.Stop();
                    _discoveryWatcher = null;
                }

                if (_classicBtWatcher != null)
                {
                    try
                    {
                        _classicBtWatcher.Added -= OnClassicDeviceAdded;
                        _classicBtWatcher.Updated -= OnClassicDeviceUpdated;
                        _classicBtWatcher.Stop();
                    }
                    catch { }
                    _classicBtWatcher = null;
                }

                _isDiscovering = false;
                Interlocked.Increment(ref _discoveryVersion);
            }
        }

        public static List<BleDeviceInfo> GetDiscoveredDevices()
        {
            return _discoveredDevices.Values
                .Where(d => !string.IsNullOrEmpty(d.LocalName))
                .OrderByDescending(d => d.RssiDbm)
                .ToList();
        }

        public static bool TryGetDiscoveredDevicesIfChanged(ref int knownVersion, out List<BleDeviceInfo> devices)
        {
            int latestVersion = Volatile.Read(ref _discoveryVersion);
            if (latestVersion == knownVersion)
            {
                devices = null;
                return false;
            }

            knownVersion = latestVersion;
            devices = GetDiscoveredDevices();
            return true;
        }

        private static void OnDiscoveryReceived(
            BluetoothLEAdvertisementWatcher sender,
            BluetoothLEAdvertisementReceivedEventArgs args)
        {
            string name = args.Advertisement.LocalName;

            var info = new BleDeviceInfo
            {
                BluetoothAddress = args.BluetoothAddress,
                MacAddress = FormatMacAddress(args.BluetoothAddress),
                LocalName = name,
                RssiDbm = args.RawSignalStrengthInDBm,
                LastSeen = DateTime.Now
            };

            bool changed = false;
            _discoveredDevices.AddOrUpdate(
                args.BluetoothAddress,
                _ =>
                {
                    changed = true;
                    return info;
                },
                (key, existing) =>
                {
                    if (existing.RssiDbm != info.RssiDbm)
                    {
                        existing.RssiDbm = info.RssiDbm;
                        changed = true;
                    }

                    existing.LastSeen = info.LastSeen;
                    if (!string.IsNullOrEmpty(name))
                    {
                        if (!string.Equals(existing.LocalName, name, StringComparison.Ordinal))
                        {
                            changed = true;
                        }

                        existing.LocalName = name;
                    }
                    return existing;
                });

            if (changed)
            {
                Interlocked.Increment(ref _discoveryVersion);
            }

            if (!string.IsNullOrEmpty(name))
            {
                DeviceDiscovered?.Invoke(info);
            }
        }

        private static void OnClassicDeviceAdded(DeviceWatcher sender, DeviceInformation info)
        {
            AddClassicDevice(info);
        }

        private static void OnClassicDeviceUpdated(DeviceWatcher sender, DeviceInformationUpdate update)
        {
            // Update is mainly for connection state changes, ignore for discovery
        }

        private static void AddClassicDevice(DeviceInformation info)
        {
            string name = info.Name;
            if (string.IsNullOrEmpty(name)) return;

            string deviceAddress = "";
            if (info.Properties.TryGetValue("System.Devices.Aep.DeviceAddress", out object addr))
            {
                deviceAddress = addr?.ToString() ?? "";
            }

            if (string.IsNullOrEmpty(deviceAddress)) return;

            ulong macLong = MacStringToUlong(deviceAddress);
            if (macLong == 0) return;

            var deviceInfo = new BleDeviceInfo
            {
                BluetoothAddress = macLong,
                MacAddress = FormatMacAddress(macLong),
                LocalName = name,
                RssiDbm = 0,
                LastSeen = DateTime.Now
            };

            bool changed = false;
            _discoveredDevices.AddOrUpdate(
                macLong,
                _ =>
                {
                    changed = true;
                    return deviceInfo;
                },
                (key, existing) =>
                {
                    if (!string.IsNullOrEmpty(name))
                    {
                        if (!string.Equals(existing.LocalName, name, StringComparison.Ordinal))
                        {
                            changed = true;
                        }

                        existing.LocalName = name;
                    }
                    existing.LastSeen = DateTime.Now;
                    return existing;
                });

            if (changed)
            {
                Interlocked.Increment(ref _discoveryVersion);
            }

            DeviceDiscovered?.Invoke(deviceInfo);
        }

        // ---------- Background Monitoring ----------

        public static void StartMonitoring()
        {
            lock (_monitorLock)
            {
                if (_isMonitoring) return;

                _monitorWatcher = new BluetoothLEAdvertisementWatcher
                {
                    ScanningMode = BluetoothLEScanningMode.Passive
                };

                _monitorWatcher.Received += OnMonitorReceived;
                _monitorWatcher.Start();
                _isMonitoring = true;
            }
        }

        public static void StopMonitoring()
        {
            lock (_monitorLock)
            {
                if (!_isMonitoring) return;

                if (_monitorWatcher != null)
                {
                    _monitorWatcher.Received -= OnMonitorReceived;
                    _monitorWatcher.Stop();
                    _monitorWatcher = null;
                }

                _monitorLastSeen.Clear();
                _isMonitoring = false;
            }
        }

        public static bool IsMonitoring => _isMonitoring;

        public static bool IsDeviceReachable(string macAddress, int thresholdSeconds)
        {
            ulong address = MacStringToUlong(macAddress);
            if (address == 0) return false;
            return IsDeviceReachable(address, thresholdSeconds, DateTime.Now);
        }

        public static bool IsDeviceReachable(ulong bluetoothAddress, int thresholdSeconds, DateTime now)
        {
            if (bluetoothAddress == 0) return false;
            if (!_monitorLastSeen.TryGetValue(bluetoothAddress, out DateTime lastSeen))
            {
                return false;
            }

            return (now - lastSeen).TotalSeconds < thresholdSeconds;
        }

        public static bool HasDeviceEverBeenSeen(string macAddress)
        {
            ulong address = MacStringToUlong(macAddress);
            return HasDeviceEverBeenSeen(address);
        }

        public static bool HasDeviceEverBeenSeen(ulong bluetoothAddress)
        {
            return bluetoothAddress != 0 && _monitorLastSeen.ContainsKey(bluetoothAddress);
        }

        private static void OnMonitorReceived(
            BluetoothLEAdvertisementWatcher sender,
            BluetoothLEAdvertisementReceivedEventArgs args)
        {
            _monitorLastSeen[args.BluetoothAddress] = DateTime.Now;
        }

        // ---------- Helpers ----------

        public static string FormatMacAddress(ulong bluetoothAddress)
        {
            var bytes = BitConverter.GetBytes(bluetoothAddress);
            return $"{bytes[5]:X2}:{bytes[4]:X2}:{bytes[3]:X2}:{bytes[2]:X2}:{bytes[1]:X2}:{bytes[0]:X2}";
        }

        public static ulong MacStringToUlong(string mac)
        {
            if (string.IsNullOrWhiteSpace(mac)) return 0;

            try
            {
                string hex = mac.Replace(":", "").Replace("-", "");
                return Convert.ToUInt64(hex, 16);
            }
            catch
            {
                return 0;
            }
        }
    }
}
