using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using WindowsShutdownHelper.functions;
using WindowsShutdownHelper.Properties;

namespace WindowsShutdownHelper
{
    public partial class settingsForm : Form
    {
        public static language language = languageSelector.languageFile();
        public static int x;
        public static int y;
        public static string firstLangValue;
        public static settings settings = new settings();

        public settingsForm(int _x, int _y, int _width, int _height)
        {
            InitializeComponent();
            x = _x + (_width - Width) / 2;
            y = _y + (_height - Height) / 2;
        }


        private void settingsForm_Load(object sender, EventArgs e)
        {
            Location = new Point(x, y);
            numericUpDown_countdownNotifierSeconds.TextAlign = HorizontalAlignment.Center;
            Text = language.settingsForm_Name;
            label_language.Text = language.settingsForm_label_language + " : ";
            label_logs.Text = language.settingsForm_label_logs + " : ";
            label_startWithWindows.Text = language.settingsForm_label_startWithWindows + " : ";
            label_runInTaskbarWhenClosed.Text = language.settingsForm_label_runInTaskbarWhenClosed + " : ";
            label_isCountdownNotifierEnabled.Text = language.settingsForm_label_isCountdownNotifierEnabled + " : ";
            label_countdownNotifierSeconds.Text = language.settingsForm_label_countdownNotifierSeconds + " : ";
            button_cancel.Text = language.settingsForm_button_cancel;
            button_save.Text = language.settingsForm_button_save;

            checkBox_isCountdownNotifierEnabled.Text = language.settingsForm_checkbox_enabled;
            checkBox_logEnabled.Text = language.settingsForm_checkbox_enabled;
            checkBox_runInTaskbarWhenClosed.Text = language.settingsForm_checkbox_enabled;
            checkBox_startWithWindowsEnabled.Text = language.settingsForm_checkbox_enabled;

            comboboxLangDataLoader();
            refrehSettings();

            if (settings.language == "auto")
            {
                comboBox_lang.SelectedIndex = 0;
            }
            else
            {
                string selectedLang = CultureInfo.GetCultureInfo(settings.language).DisplayName;
                int selectedIndex = comboBox_lang.FindStringExact(selectedLang);

                comboBox_lang.SelectedIndex = selectedIndex;
            }
        }

        public void comboboxLangDataLoader()
        {
            List<string> existLanguages = Directory
                .GetFiles(AppDomain.CurrentDomain.BaseDirectory + "lang\\", "lang_??.json")
                .Select(Path.GetFileNameWithoutExtension)
                .ToList();
            List<languageNames> langs = new List<languageNames>();
            languageNames autoLang = new languageNames();
            string currentlangName = CultureInfo.GetCultureInfo(CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
                .DisplayName;
            autoLang.langCode = "auto";
            autoLang.LangName = language.settingsForm_combobox_auto_lang + "(" + currentlangName + ")";
            langs.Add(autoLang);


            foreach (string lng in existLanguages)
            {
                //if (lng.Length <= 2)
                //{

                string language = lng.Substring(lng.Length - 2);
                languageNames lang = new languageNames
                {
                    langCode = language,
                    LangName = CultureInfo.GetCultureInfo(language).DisplayName
                };
                langs.Add(lang);
                //}
            } //foreach


            comboBox_lang.DataSource = langs;
            comboBox_lang.DisplayMember = "LangName";
            comboBox_lang.ValueMember = "langCode";
        }

        public void refrehSettings()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\settings.json"))
            {
                settings = JsonSerializer.Deserialize<settings>(
                    File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\settings.json"));
                firstLangValue = settings.language;
                if (settings.logsEnabled)
                {
                    checkBox_logEnabled.Checked = true;
                }
                else
                {
                    checkBox_logEnabled.Checked = false;
                }

                if (settings.startWithWindows)
                {
                    checkBox_startWithWindowsEnabled.Checked = true;
                }
                else
                {
                    checkBox_startWithWindowsEnabled.Checked = false;
                }

                if (settings.runInTaskbarWhenClosed)
                {
                    checkBox_runInTaskbarWhenClosed.Checked = true;
                }
                else
                {
                    checkBox_runInTaskbarWhenClosed.Checked = false;
                }

                if (settings.isCountdownNotifierEnabled)
                {
                    checkBox_isCountdownNotifierEnabled.Checked = true;
                }
                else
                {
                    checkBox_isCountdownNotifierEnabled.Checked = false;
                }

                numericUpDown_countdownNotifierSeconds.Value = settings.countdownNotifierSeconds;
            }


            else
            {
                checkBox_logEnabled.Checked = true;
                checkBox_startWithWindowsEnabled.Checked = false;
                checkBox_runInTaskbarWhenClosed.Checked = false;
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
            GC.Collect();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkBox_logEnabled.Checked)
                {
                    settings.logsEnabled = true;
                }
                else
                {
                    settings.logsEnabled = false;
                }

                if (checkBox_runInTaskbarWhenClosed.Checked)
                {
                    settings.runInTaskbarWhenClosed = true;
                }
                else
                {
                    settings.runInTaskbarWhenClosed = false;
                }

                if (checkBox_startWithWindowsEnabled.Checked)
                {
                    settings.startWithWindows = true;
                    startWithWindows.AddStartup(language.settingsForm_addStartupAppName);
                }

                else
                {
                    settings.startWithWindows = false;
                    startWithWindows.DeleteStartup(language.settingsForm_addStartupAppName);
                }

                if (checkBox_isCountdownNotifierEnabled.Checked)
                {
                    settings.isCountdownNotifierEnabled = true;
                }
                else
                {
                    settings.isCountdownNotifierEnabled = false;
                }

                settings.countdownNotifierSeconds = Convert.ToInt16(numericUpDown_countdownNotifierSeconds.Value);

                settings.language = comboBox_lang.SelectedValue.ToString();
                jsonWriter.WriteJson(AppDomain.CurrentDomain.BaseDirectory + "\\settings.json", true, settings);


                if (firstLangValue == settings.language)
                {
                    popUpViewer popUpViewer = new popUpViewer(language.messageTitle_success,
                        language.messageContent_settingsSaved,
                        2, Resources.success, Location.X, Location.Y, Width, Height);
                    popUpViewer.ShowDialog();
                }
                else
                {
                    {
                        popUpViewer popUpViewer = new popUpViewer(language.messageTitle_success,
                            language.messageContent_settingSavedWithLangChanged,
                            4, Resources.success, Location.X, Location.Y, Width, Height);
                        popUpViewer.ShowDialog();
                    }
                }
            }


            catch
            {
            }
        }

        private void numericUpDown_countdownNotifierSeconds_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void settingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }

        ///////////////////////////////////////////////////////////////////////////
    }
}