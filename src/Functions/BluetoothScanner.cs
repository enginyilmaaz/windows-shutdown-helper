using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Windows.Devices.Bluetooth.Advertisement;

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

        private static readonly ConcurrentDictionary<ulong, BleDeviceInfo> _discoveredDevices = new();
        private static readonly ConcurrentDictionary<ulong, DateTime> _monitorLastSeen = new();

        private static readonly object _discoveryLock = new();
        private static readonly object _monitorLock = new();

        private static bool _isDiscovering;
        private static bool _isMonitoring;

        public static event Action<BleDeviceInfo> DeviceDiscovered;

        // ---------- Discovery Scan (UI device list) ----------

        public static void StartDiscoveryScan()
        {
            lock (_discoveryLock)
            {
                if (_isDiscovering) return;

                _discoveredDevices.Clear();

                _discoveryWatcher = new BluetoothLEAdvertisementWatcher
                {
                    ScanningMode = BluetoothLEScanningMode.Active
                };

                _discoveryWatcher.Received += OnDiscoveryReceived;
                _discoveryWatcher.Start();
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

                _isDiscovering = false;
            }
        }

        public static List<BleDeviceInfo> GetDiscoveredDevices()
        {
            return _discoveredDevices.Values
                .Where(d => !string.IsNullOrEmpty(d.LocalName))
                .OrderByDescending(d => d.RssiDbm)
                .ToList();
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

            _discoveredDevices.AddOrUpdate(
                args.BluetoothAddress,
                info,
                (key, existing) =>
                {
                    existing.RssiDbm = info.RssiDbm;
                    existing.LastSeen = info.LastSeen;
                    if (!string.IsNullOrEmpty(name))
                    {
                        existing.LocalName = name;
                    }
                    return existing;
                });

            if (!string.IsNullOrEmpty(name))
            {
                DeviceDiscovered?.Invoke(info);
            }
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

            if (!_monitorLastSeen.TryGetValue(address, out DateTime lastSeen))
            {
                return false;
            }

            return (DateTime.Now - lastSeen).TotalSeconds < thresholdSeconds;
        }

        public static bool HasDeviceEverBeenSeen(string macAddress)
        {
            ulong address = MacStringToUlong(macAddress);
            return address != 0 && _monitorLastSeen.ContainsKey(address);
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
