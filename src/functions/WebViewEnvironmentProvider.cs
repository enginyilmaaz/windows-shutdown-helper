using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Core;

namespace WindowsShutdownHelper.functions
{
    internal static class WebViewEnvironmentProvider
    {
        private static readonly Lazy<Task<CoreWebView2Environment>> _sharedEnvironment =
            new Lazy<Task<CoreWebView2Environment>>(CreateEnvironmentAsync, LazyThreadSafetyMode.ExecutionAndPublication);

        public static Task<CoreWebView2Environment> GetAsync()
        {
            return _sharedEnvironment.Value;
        }

        public static void Prewarm()
        {
            _ = Task.Run(async () =>
            {
                try
                {
                    await _sharedEnvironment.Value.ConfigureAwait(false);
                }
                catch
                {
                    // Ignore prewarm failures; initialization will retry on first real use.
                }
            });
        }

        private static Task<CoreWebView2Environment> CreateEnvironmentAsync()
        {
            string userDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "WindowsShutdownHelper",
                "WebView2");

            Directory.CreateDirectory(userDataFolder);
            return CoreWebView2Environment.CreateAsync(null, userDataFolder);
        }
    }
}
