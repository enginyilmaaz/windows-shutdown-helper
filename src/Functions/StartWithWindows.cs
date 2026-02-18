using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace WindowsAutoPowerManager.Functions
{
    public class StartWithWindows
    {
        private const string KeyName = @"Software\Microsoft\Windows\CurrentVersion\Run";

        private static string PathWithArguments
        {
            get { return "\"" + Application.ExecutablePath + "\" -runInTaskBar"; }
        }

        public static void AddStartup(string appTitle)
        {
            try
            {
                using (RegistryKey startupKey = OpenStartupKey())
                {
                    if (startupKey == null) return;

                    string currentValue = startupKey.GetValue(appTitle) as string;
                    if (!string.Equals(currentValue, PathWithArguments, StringComparison.Ordinal))
                    {
                        startupKey.SetValue(appTitle, PathWithArguments, RegistryValueKind.String);
                    }
                }
            }
            catch
            {
                // Keep startup toggle resilient against registry access issues.
            }
        }

        public static void DeleteStartup(string appTitle)
        {
            try
            {
                using (RegistryKey startupKey = OpenStartupKey())
                {
                    if (startupKey == null) return;

                    if (startupKey.GetValue(appTitle) != null)
                    {
                        startupKey.DeleteValue(appTitle, false);
                    }
                }
            }
            catch
            {
                // Keep startup toggle resilient against registry access issues.
            }
        }

        private static RegistryKey OpenStartupKey()
        {
            RegistryView view = Environment.Is64BitOperatingSystem
                ? RegistryView.Registry64
                : RegistryView.Registry32;

            return RegistryKey
                .OpenBaseKey(RegistryHive.CurrentUser, view)
                .OpenSubKey(KeyName, true);
        }
    }
}
