namespace WindowsAutoPowerManager.Config
{
    public static class SettingsINI
    {
        public static Settings DefaulSettingFile()
        {
            Settings setting = new Settings
            {
                LogsEnabled = true,
                StartWithWindows = false,
                RunInTaskbarWhenClosed = false,
                IsCountdownNotifierEnabled = true,
                CountdownNotifierSeconds = 5,
                Language = "auto",
                Theme = "system",
                BluetoothThresholdSeconds = 5,
                BluetoothRssiThreshold = 0
            };


            return setting;
        }
    }
}