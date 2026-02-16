using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace WindowsShutdownHelper.functions
{
    public class ActionModel
    {
        public string triggerType { get; set; }
        public string actionType { get; set; }
        public string value { get; set; }
        public string valueUnit { get; set; }
        public string createdDate { get; set; }
    }

    public class Actions
    {
        public static void doActionByTypes(ActionModel action)
        {
            if (action.actionType == config.actionTypes.lockComputer)
            {
                Lock.Computer();
            }

            if (action.actionType == config.actionTypes.sleepComputer)
            {
                Sleep.Computer();
            }

            if (action.actionType == config.actionTypes.turnOffMonitor)
            {
                TurnOff.Monitor();
            }

            if (action.actionType == config.actionTypes.shutdownComputer)
            {
                ShutdownComputer();
            }

            if (action.actionType == config.actionTypes.restartComputer)
            {
                RestartComputer();
            }

            if (action.actionType == config.actionTypes.logOffWindows)
            {
                LogOff.Windows();
            }
        }

        public static void ShutdownComputer()
        {
            Logger.doLog(config.actionTypes.shutdownComputer);
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "shutdown",
                Arguments = "/s /t 0"
            };
            process.StartInfo = startInfo;
            process.Start();
        }


        public static void RestartComputer()
        {
            Logger.doLog(config.actionTypes.restartComputer);
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "shutdown",
                Arguments = "/r /t 0"
            };

            process.StartInfo = startInfo;
            process.Start();
        }


        public class Lock
        {
            public static bool manualLocked = true;

            [DllImport("user32.dll")]
            public static extern void LockWorkStation();

            public static void Computer()
            {
                if (detectScreen.isLockedWorkstation() == false)
                {
                    manualLocked = false;
                    Logger.doLog(config.actionTypes.lockComputer);
                    LockWorkStation();
                }
            }


            public static bool isLockedManually()
            {
                return manualLocked;
            }
        }


        public class LogOff
        {
            [DllImport("user32")]
            public static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

            public static void Windows()
            {
                Logger.doLog(config.actionTypes.logOffWindows);
                ExitWindowsEx(0, 0);
            }
        }

        public class Sleep
        {
            [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

            public static void Computer()
            {
                Logger.doLog(config.actionTypes.sleepComputer);
                SetSuspendState(false, true, true);
            }
        }


        public class TurnOff
        {
            public enum MonitorState
            {
                OFF = 2
            }

            [Flags]
            private enum SendMessageTimeoutFlags : uint
            {
                AbortIfHung = 0x0002
            }

            private const uint WM_SYSCOMMAND = 0x0112;
            private static readonly IntPtr SC_MONITORPOWER = new IntPtr(0xF170);
            private static readonly IntPtr HWND_BROADCAST = new IntPtr(0xFFFF);
            private const uint MonitorPowerCallTimeoutMs = 500;
            private const long MonitorOffCooldownMs = 4000;
            private static long _lastMonitorOffTick = -MonitorOffCooldownMs;

            [DllImport("user32.dll", SetLastError = true)]
            private static extern IntPtr SendMessageTimeout(
                IntPtr hWnd,
                uint msg,
                IntPtr wParam,
                IntPtr lParam,
                SendMessageTimeoutFlags fuFlags,
                uint uTimeout,
                out IntPtr lpdwResult);

            [DllImport("user32.dll", SetLastError = true)]
            private static extern bool PostMessage(
                IntPtr hWnd,
                uint msg,
                IntPtr wParam,
                IntPtr lParam);

            public static bool SetMonitorState(MonitorState state)
            {
                IntPtr lResult;
                IntPtr sendResult = SendMessageTimeout(
                    HWND_BROADCAST,
                    WM_SYSCOMMAND,
                    SC_MONITORPOWER,
                    new IntPtr((int)state),
                    SendMessageTimeoutFlags.AbortIfHung,
                    MonitorPowerCallTimeoutMs,
                    out lResult);

                if (sendResult != IntPtr.Zero)
                {
                    return true;
                }

                // Fallback for systems where broadcast send blocks or fails.
                return PostMessage(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, new IntPtr((int)state));
            }

            private static bool IsMonitorOffInCooldown()
            {
                long nowTick = Environment.TickCount64;
                long lastTick = Interlocked.Read(ref _lastMonitorOffTick);

                if (nowTick - lastTick < MonitorOffCooldownMs)
                {
                    return true;
                }

                Interlocked.Exchange(ref _lastMonitorOffTick, nowTick);
                return false;
            }

            public static void Monitor()
            {
                if (IsMonitorOffInCooldown())
                {
                    return;
                }

                Logger.doLog(config.actionTypes.turnOffMonitor);
                SetMonitorState(MonitorState.OFF);
            }
        }


        /////////////
    }
}
