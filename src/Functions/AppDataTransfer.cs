using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace WindowsAutoPowerManager.Functions
{
    internal sealed class AppDataTransferPayload
    {
        public string AppName { get; set; }
        public string ExportedAt { get; set; }
        public int SchemaVersion { get; set; }
        public Settings Settings { get; set; }
        public List<ActionModel> Actions { get; set; }
        public List<LogSystem> Logs { get; set; }
    }

    internal static class AppDataTransfer
    {
        private const int CurrentSchemaVersion = 1;

        private static readonly JsonSerializerOptions ReadOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };

        public static string BuildDefaultFileName()
        {
            string safeAppName = MakeSafeFileName(Config.Constants.AppName);
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", CultureInfo.CurrentCulture);
            return $"{safeAppName}_{timestamp}.settings.conf";
        }

        public static void ExportToFile(
            string filePath,
            Settings settings,
            IEnumerable<ActionModel> actions,
            IEnumerable<LogSystem> logs)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path is required.", nameof(filePath));
            }

            var payload = new AppDataTransferPayload
            {
                AppName = Config.Constants.AppName,
                ExportedAt = DateTime.Now.ToString("O", CultureInfo.InvariantCulture),
                SchemaVersion = CurrentSchemaVersion,
                Settings = settings ?? Config.SettingsINI.DefaulSettingFile(),
                Actions = CloneActions(actions),
                Logs = CloneLogs(logs)
            };

            JsonWriter.WriteJson(filePath, true, payload);
        }

        public static bool TryImportFromFile(
            string filePath,
            out Settings settings,
            out List<ActionModel> actions,
            out List<LogSystem> logs,
            out string errorMessage)
        {
            settings = Config.SettingsINI.DefaulSettingFile();
            actions = new List<ActionModel>();
            logs = new List<LogSystem>();
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(filePath))
            {
                errorMessage = "File path is empty.";
                return false;
            }

            try
            {
                string json = File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    errorMessage = "The selected file is empty.";
                    return false;
                }

                var payload = JsonSerializer.Deserialize<AppDataTransferPayload>(json, ReadOptions);
                if (payload == null)
                {
                    errorMessage = "The selected file is not a valid configuration package.";
                    return false;
                }

                settings = payload.Settings ?? Config.SettingsINI.DefaulSettingFile();
                actions = CloneActions(payload.Actions);
                logs = CloneLogs(payload.Logs);
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        private static List<ActionModel> CloneActions(IEnumerable<ActionModel> actions)
        {
            if (actions == null)
            {
                return new List<ActionModel>();
            }

            return actions
                .Where(a => a != null)
                .Select(a => new ActionModel
                {
                    TriggerType = a.TriggerType,
                    ActionType = a.ActionType,
                    Value = a.Value,
                    ValueUnit = a.ValueUnit,
                    CreatedDate = a.CreatedDate
                })
                .ToList();
        }

        private static List<LogSystem> CloneLogs(IEnumerable<LogSystem> logs)
        {
            if (logs == null)
            {
                return new List<LogSystem>();
            }

            return logs
                .Where(l => l != null)
                .Select(l => new LogSystem
                {
                    ActionType = l.ActionType,
                    ActionExecutedDate = l.ActionExecutedDate
                })
                .ToList();
        }

        private static string MakeSafeFileName(string name)
        {
            string value = string.IsNullOrWhiteSpace(name) ? "app" : name.Trim();
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                value = value.Replace(invalidChar, '_');
            }

            return value.Replace(' ', '_');
        }
    }
}
