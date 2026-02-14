using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using WindowsShutdownHelper.Enums;
using WindowsShutdownHelper.functions;
using WindowsShutdownHelper.Properties;

namespace WindowsShutdownHelper
{
    public partial class mainForm : Form
    {


        public static language language = languageSelector.languageFile();
        public static List<ActionModel> actionList = new List<ActionModel>();
        public static List<ActionModel> actionListTemp = new List<ActionModel>();
        public static settings settings = new settings();
        public static bool isDeletedFromNotifier;
        public static bool isSkippedCertainTimeAction;
        public static Timer timer = new Timer();
        public static int runInTaskbarCounter;

        public mainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();


            foreach (string arg in args)
            {
                if (arg == "-runInTaskBar" && runInTaskbarCounter <= 0)
                {
                    ++runInTaskbarCounter;
                    Hide();
                    ShowInTaskbar = false;
                }
            }

            base.OnLoad(e);
        }






        public void deleteExpriedAction()
        {
            foreach (ActionModel action in actionList.ToList())
            {
                if (action.triggerType == config.triggerTypes.fromNow)
                {
                    DateTime actionDate = DateTime.Parse(action.value);
                    if (DateTime.Now > actionDate)
                    {
                        actionList.Remove(action);
                        writeJsonToActionList();
                    }
                }
            }
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            Logger.doLog(config.actionTypes.appStarted);
            Text = language.main_FormName;
            toolStripStatusLabel_CurrentTime.Text = language.main_statusBar_currentTime + " : " + DateTime.Now + "  |  Build Id: " + BuildInfo.CommitId;
            notifyIcon_main.Text = language.main_FormName + " " + language.notifyIcon_main;
            numericUpDown_value.TextAlign = HorizontalAlignment.Center;


            detectScreen.manuelLockingActionLogger();
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\actionList.json"))
            {
                actionList =
                    JsonSerializer.Deserialize<List<ActionModel>>(
                        File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\actionList.json"));
                actionListTemp =
                    JsonSerializer.Deserialize<List<ActionModel>>(
                        File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\actionList.json"));
            }

            deleteExpriedAction();

            timer.Interval = 1000; // 1 sec
            timer.Tick += timerTick;
            timer.Start();


            comboBox_actionType.Items.Add(language.main_cbox_ActionType_Item_chooseAction);
            comboBox_actionType.Items.Add(language.main_cbox_ActionType_Item_shutdownComputer);
            comboBox_actionType.Items.Add(language.main_cbox_ActionType_Item_restartComputer);
            comboBox_actionType.Items.Add(language.main_cbox_ActionType_Item_logOffWindows);
            comboBox_actionType.Items.Add(language.main_cbox_ActionType_Item_sleepComputer);
            comboBox_actionType.Items.Add(language.main_cbox_ActionType_Item_lockComputer);
            comboBox_actionType.Items.Add(language.main_cbox_ActionType_Item_turnOffMonitor);
            comboBox_actionType.SelectedIndex = 0;

            comboBox_triggerType.Items.Add(language.main_cbox_TriggerType_Item_chooseTrigger);
            comboBox_triggerType.Items.Add(language.main_cbox_TriggerType_Item_systemIdle);
            comboBox_triggerType.Items.Add(language.main_cbox_TriggerType_Item_fromNow);
            comboBox_triggerType.Items.Add(language.main_cbox_TriggerType_Item_certainTime);
            comboBox_triggerType.SelectedIndex = 0;

            comboBox_timeUnit.Items.Add(language.main_timeUnit_seconds ?? "Seconds");
            comboBox_timeUnit.Items.Add(language.main_timeUnit_minutes ?? "Minutes");
            comboBox_timeUnit.Items.Add(language.main_timeUnit_hours ?? "Hours");
            comboBox_timeUnit.SelectedIndex = 1;

            label_trigger.Text = language.main_label_trigger + " : ";
            label_value.Text = language.main_label_value + " : ";
            label_firstly_choose_a_trigger.Text = language.label_firstly_choose_a_trigger;
            label_actionType.Text = language.main_label_actionType + " : ";
            button_AddAction.Text = language.main_button_addAction;
            groupBox_newAction.Text = language.main_groupbox_newAction;
            groupBox_actionList.Text = language.main_groupBox_actionList;


            contextMenuStrip_mainGrid.Items[(int)enum_cmStrip_mainGrid.DeleteAllAction].Text =
                language.contextMenuStrip_mainGrid_deleteAllAction;
            contextMenuStrip_mainGrid.Items[(int)enum_cmStrip_mainGrid.DeleteSelectedAction].Text =
                language.contextMenuStrip_mainGrid_deleteSelectedAction;




            contextMenuStrip_notifyIcon.Items[(int)enum_cmStrip_notifyIcon.AddNewAction].Text =
                language.contextMenuStrip_notifyIcon_addNewAction;

            contextMenuStrip_notifyIcon.Items[(int)enum_cmStrip_notifyIcon.ExitTheProgram].Text =
                language.contextMenuStrip_notifyIcon_exitProgram;

            contextMenuStrip_notifyIcon.Items[(int)enum_cmStrip_notifyIcon.Settings].Text =
                language.contextMenuStrip_notifyIcon_showSettings;

            contextMenuStrip_notifyIcon.Items[(int)enum_cmStrip_notifyIcon.ShowLogs].Text =
                language.contextMenuStrip_notifyIcon_showLogs;



            contextMenuStrip_MainForm.Items[(int)enum_cmStrip_MainForm.Settings].Text =
                language.contextMenuStrip_notifyIcon_showSettings;

            contextMenuStrip_MainForm.Items[(int)enum_cmStrip_MainForm.ShowLogs].Text =
                language.contextMenuStrip_notifyIcon_showLogs;

            contextMenuStrip_MainForm.Items[(int)enum_cmStrip_MainForm.ExitTheProgram].Text =
                language.contextMenuStrip_notifyIcon_exitProgram;


            toolTip.SetToolTip(pictureBox_logs, language.toolTip_showLogs);
            toolTip.SetToolTip(pictureBox_settings, language.toolTip_settings);


            dateTimePicker_time.MinDate = DateTime.Now.AddMinutes(1);
            refreshActionList();


            dataGridView_taskList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_taskList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }


        public void refreshActionList()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\actionList.json"))
            {
                actionListTemp =
                    JsonSerializer.Deserialize<List<ActionModel>>(
                        File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\actionList.json"));
            }

            foreach (ActionModel act in actionListTemp)
            {
                if (act.actionType == config.actionTypes.logOffWindows)
                {
                    act.actionType = language.main_cbox_ActionType_Item_logOffWindows;
                }

                if (act.actionType == config.actionTypes.lockComputer)
                {
                    act.actionType = language.main_cbox_ActionType_Item_lockComputer;
                }

                if (act.actionType == config.actionTypes.shutdownComputer)
                {
                    act.actionType = language.main_cbox_ActionType_Item_shutdownComputer;
                }

                if (act.actionType == config.actionTypes.restartComputer)
                {
                    act.actionType = language.main_cbox_ActionType_Item_restartComputer;
                }

                if (act.actionType == config.actionTypes.turnOffMonitor)
                {
                    act.actionType = language.main_cbox_ActionType_Item_turnOffMonitor;
                }

                if (act.actionType == config.actionTypes.sleepComputer)
                {
                    act.actionType = language.main_cbox_ActionType_Item_sleepComputer;
                }

                if (act.triggerType == config.triggerTypes.systemIdle)
                {
                    act.triggerType = language.main_cbox_TriggerType_Item_systemIdle;
                }

                if (act.triggerType == config.triggerTypes.certainTime)
                {
                    act.triggerType = language.main_cbox_TriggerType_Item_certainTime;
                }

                if (act.triggerType == config.triggerTypes.fromNow)
                {
                    act.triggerType = language.main_cbox_TriggerType_Item_fromNow;
                }

                if (act.valueUnit == "seconds")
                {
                    act.valueUnit = language.main_timeUnit_seconds ?? "Seconds";
                }
                else if (string.IsNullOrEmpty(act.valueUnit))
                {
                    act.valueUnit = language.main_timeUnit_minutes ?? "Minutes";
                }
            }

            dataGridView_taskList.DataSource = null;
            dataGridView_taskList.DataSource = actionListTemp;
            dataGridView_taskList.Columns["triggerType"].HeaderText = language.main_datagrid_main_triggerType;
            dataGridView_taskList.Columns["actionType"].HeaderText = language.main_datagrid_main_actionType;
            dataGridView_taskList.Columns["value"].HeaderText = language.main_datagrid_main_value;
            dataGridView_taskList.Columns["valueUnit"].HeaderText = language.main_datagrid_main_valueUnit ?? "Unit";
            dataGridView_taskList.Columns["createdDate"].HeaderText = language.main_datagrid_main_createdDate;
            dataGridView_taskList.Columns["triggerType"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dataGridView_taskList.Columns["actionType"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dataGridView_taskList.Columns["value"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dataGridView_taskList.Columns["valueUnit"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dataGridView_taskList.Columns["createdDate"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
        }


        public void writeJsonToActionList()
        {
            jsonWriter.WriteJson(AppDomain.CurrentDomain.BaseDirectory + "\\actionList.json", true,
                actionList.ToList());
            refreshActionList();
        }

        private void doAction(ActionModel action, uint idleTimeMin)
        {
            uint actionValueSeconds = string.IsNullOrEmpty(action.valueUnit)
                ? Convert.ToUInt32(action.value) * 60
                : Convert.ToUInt32(action.value);
            if (action.triggerType == config.triggerTypes.systemIdle && idleTimeMin == actionValueSeconds)
            {
                Actions.doActionByTypes(action);
            }

            if (action.triggerType == config.triggerTypes.certainTime && action.value == DateTime.Now.ToString("HH:mm:ss")
            )
            {
                if (isSkippedCertainTimeAction == false)
                {
                    Actions.doActionByTypes(action);
                }
                else
                {
                    isSkippedCertainTimeAction = false;
                }
            }

            if (action.triggerType == config.triggerTypes.fromNow && action.value == DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"))
            {
                Actions.doActionByTypes(action);
                actionList.Remove(action);
                writeJsonToActionList();
            }
        }


        private void timerTick(object sender, EventArgs e)
        {
            toolStripStatusLabel_CurrentTime.Text = language.main_statusBar_currentTime + " : " + DateTime.Now + "  |  Build Id: " + BuildInfo.CommitId;

            uint idleTimeMin = systemIdleDetector.GetLastInputTime();

            if (idleTimeMin == 0)
            {
                notifySystem.ResetIdleNotifications();
                timer.Stop();
                timer.Start();
            }

            if (isDeletedFromNotifier)
            {
                writeJsonToActionList();
                isDeletedFromNotifier = false;
            }

            foreach (ActionModel action in actionList.ToList())
            {
                doAction(action, idleTimeMin);
                notifySystem.showNotification(action, idleTimeMin);
            } //foreach
        }


        private void deleteAction()
        {
            if (Application.OpenForms.OfType<popUpViewer>().Any() == false)
            {
                if (dataGridView_taskList.Rows.Count > 0)
                {
                    if (dataGridView_taskList.CurrentRow != null)
                    {
                        popUpViewer popUpViewer = new popUpViewer(language.messageTitle_success,
                            language.messageContent_actionDeleted, 2,
                            Resources.success, Location.X, Location.Y, Width, Height);
                        popUpViewer.ShowDialog();
                        popUpViewer.Focus();
                        actionList.RemoveAt(dataGridView_taskList.CurrentCell.RowIndex);
                        writeJsonToActionList();
                    }
                    else
                    {
                        popUpViewer popUpViewer = new popUpViewer(language.messageTitle_warn,
                            language.messageContent_datagridMain_actionChoose, 2,
                            Resources.warn, Location.X, Location.Y, Width, Height);
                        popUpViewer.ShowDialog();
                        popUpViewer.Focus();
                    }
                }
            }
        }



        private void button_AddToList_Click(object sender, EventArgs e)
        {
            if (actionList.Count < 5)
            {
                if (comboBox_actionType.SelectedIndex > 0 && comboBox_triggerType.SelectedIndex > 0)
                {
                    ActionModel newAction = new ActionModel
                    {
                        createdDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")
                    };


                    if (comboBox_triggerType.SelectedIndex == (int)enum_combobox_triggerType.FromNow)
                    {
                        newAction.triggerType = config.triggerTypes.fromNow;
                        double inputValue = Convert.ToDouble(numericUpDown_value.Value);
                        DateTime targetTime;
                        if (comboBox_timeUnit.SelectedIndex == 0)
                            targetTime = DateTime.Now.AddSeconds(inputValue);
                        else if (comboBox_timeUnit.SelectedIndex == 2)
                            targetTime = DateTime.Now.AddHours(inputValue);
                        else
                            targetTime = DateTime.Now.AddMinutes(inputValue);
                        newAction.value = targetTime.ToString("dd.MM.yyyy HH:mm:ss");
                    }


                    else if (comboBox_triggerType.SelectedIndex == (int)enum_combobox_triggerType.SystemIdleTime)
                    {
                        newAction.triggerType = config.triggerTypes.systemIdle;
                        int inputValue = Convert.ToInt32(numericUpDown_value.Value);
                        int valueInSeconds;
                        if (comboBox_timeUnit.SelectedIndex == 0)
                            valueInSeconds = inputValue;
                        else if (comboBox_timeUnit.SelectedIndex == 2)
                            valueInSeconds = inputValue * 3600;
                        else
                            valueInSeconds = inputValue * 60;
                        newAction.value = valueInSeconds.ToString();
                        newAction.valueUnit = "seconds";
                    }


                    else if (comboBox_triggerType.SelectedIndex == (int)enum_combobox_triggerType.CertainTime)
                    {
                        newAction.triggerType = config.triggerTypes.certainTime;
                        newAction.value = dateTimePicker_time.Value.ToString("HH:mm:00");
                    }

                    if (comboBox_actionType.SelectedIndex == (int)enum_combobox_actionType.LockComputer)
                    {
                        newAction.actionType = config.actionTypes.lockComputer;
                    }

                    if (comboBox_actionType.SelectedIndex == (int)enum_combobox_actionType.LogOff)
                    {
                        newAction.actionType = config.actionTypes.logOffWindows;
                    }

                    if (comboBox_actionType.SelectedIndex == (int)enum_combobox_actionType.Restart)
                    {
                        newAction.actionType = config.actionTypes.restartComputer;
                    }

                    if (comboBox_actionType.SelectedIndex == (int)enum_combobox_actionType.Shutdown)
                    {
                        newAction.actionType = config.actionTypes.shutdownComputer;
                    }

                    if (comboBox_actionType.SelectedIndex == (int)enum_combobox_actionType.Sleep)
                    {
                        newAction.actionType = config.actionTypes.sleepComputer;
                    }

                    if (comboBox_actionType.SelectedIndex == (int)enum_combobox_actionType.TurnOffMonitor)
                    {
                        newAction.actionType = config.actionTypes.turnOffMonitor;
                    }

                    if (Application.OpenForms.OfType<popUpViewer>().Any() == false)
                    {
                        popUpViewer popUpViewer = new popUpViewer(language.messageTitle_success,
                            language.messageContent_actionCreated,
                            2, Resources.success, Location.X,
                            Location.Y, Width, Height);
                        popUpViewer.ShowDialog();
                        popUpViewer.Focus();
                        numericUpDown_value.Text = "1";

                        actionList.Add(newAction);

                        writeJsonToActionList();
                    }
                }

                else
                {
                    if (Application.OpenForms.OfType<popUpViewer>().Any() == false)
                    {
                        popUpViewer popUpViewer = new popUpViewer(language.messageTitle_warn,
                            language.messageContent_actionChoose,
                            2, Resources.warn, Location.X,
                            Location.Y, Width, Height);
                        popUpViewer.ShowDialog();
                        popUpViewer.Focus();
                    }
                }
            }

            else if (actionList.Count >= 5)
            {
                if (Application.OpenForms.OfType<popUpViewer>().Any() == false)
                {
                    popUpViewer popUpViewer = new popUpViewer(language.messageTitle_warn, language.messageContent_maxActionWarn,
                        2, Resources.warn, Location.X,
                        Location.Y, Width, Height);
                    popUpViewer.ShowDialog();
                    popUpViewer.Focus();
                }
            }
        }


        private void comboBox_trigger_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_triggerType.SelectedIndex == 0)
            {
                label_firstly_choose_a_trigger.Visible = true;
                numericUpDown_value.Visible = false;
                comboBox_timeUnit.Visible = false;
                dateTimePicker_time.Visible = false;
            }

            if (comboBox_triggerType.SelectedIndex == 1 || comboBox_triggerType.SelectedIndex == 2)
            {
                label_firstly_choose_a_trigger.Visible = false;
                numericUpDown_value.Visible = true;
                comboBox_timeUnit.Visible = true;
                dateTimePicker_time.Visible = false;
                label_value.Text = language.main_label_value_duration + " : ";
            }
            else if (comboBox_triggerType.SelectedIndex == 3)
            {
                label_firstly_choose_a_trigger.Visible = false;

                numericUpDown_value.Visible = false;
                comboBox_timeUnit.Visible = false;
                dateTimePicker_time.Visible = true;
                label_value.Text = language.main_label_value_time + " : ";
            }
        }

        private void deleteSelectedTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteAction();
        }

        private void dataGridView_taskList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView_taskList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dataGridView_taskList.Rows.Count > 0)
            {
                dataGridView_taskList.ContextMenuStrip = contextMenuStrip_mainGrid;
            }
        }

        private void dataGridView_taskList_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (dataGridView_taskList.Rows.Count == 0)
            {
                dataGridView_taskList.ContextMenuStrip = null;
            }
        }

        private void comboBox_actionType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void pictureBox_logs_Click(object sender, EventArgs e)
        {
            showLogs();
        }

        private void pictureBox_settings_Click(object sender, EventArgs e)
        {
            settingsFormOpen();
        }



        public void settingsFormOpen()
        {
            settingsForm settingsForm = new settingsForm(Location.X, Location.Y, Width, Height);
            if (Application.OpenForms.OfType<settingsForm>().Any() == false)
            {
                settingsForm.Show();
                settingsForm.Focus();
            }
            else if (Application.OpenForms.OfType<settingsForm>().Any())
            {
                popUpViewer popUpViewer = new popUpViewer(language.messageTitle_info, language.messageContent_another + " '" +
                                                                              language.settingsForm_Name + "' " +
                                                                              language.messageContent_windowAlredyOpen,
                    3,
                    Resources.info, Location.X, Location.Y, Width, Height);

                popUpViewer.ShowDialog();
                popUpViewer.Focus();
            }
        }

        public void showMain()
        {
            Show();
            Focus();
            ShowInTaskbar = true;
        }

        private void notifyIcon_main_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            showMain();
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\settings.json"))
            {
                settings = JsonSerializer.Deserialize<settings>(
                    File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\settings.json"));

                if (settings.runInTaskbarWhenClosed)
                {
                    e.Cancel = true;
                    Hide();
                }

            }
        }

        private void exitTheProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.doLog(config.actionTypes.appTerminated);
            Application.ExitThread();
        }

        private void addNewActionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showMain();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsFormOpen();
        }

        private void showTheLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showLogs();
        }

        private void clearAllActionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            actionList.Clear();
            writeJsonToActionList();

            if (Application.OpenForms.OfType<popUpViewer>().Any() == false)
            {
                popUpViewer popUpViewer = new popUpViewer(language.messageTitle_success,
                    language.messageContent_actionAllDeleted, 2, Resources.success,
                    Location.X,
                    Location.Y, Width, Height);
                popUpViewer.ShowDialog();
                popUpViewer.Focus();
            }
        }


        public void showLogs()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\logs.json"))
            {
                List<logSystem> logList = JsonSerializer
                    .Deserialize<List<logSystem>>(
                        File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\logs.json"))
                    .OrderByDescending(a => a.actionExecutedDate).Take(250).ToList();
                if (logList.Count > 0)
                {
                    logViewer logViewerForm = new logViewer(logList, Location.X, Location.Y, Width, Height);
                    if (Application.OpenForms.OfType<logViewer>().Any() == false)
                    {
                        logViewerForm.Show();
                    }
                    else if (Application.OpenForms.OfType<logViewer>().Any())
                    {
                        popUpViewer popUpViewer = new popUpViewer(language.messageTitle_info,
                            language.messageContent_another + " '" +
                            language.logViewerForm_Name + "' " + language.messageContent_windowAlredyOpen, 3,
                            Resources.info, Location.X, Location.Y, Width, Height);

                        popUpViewer.ShowDialog();
                        popUpViewer.Focus();
                    }
                }

                else
                {
                    popUpViewer popUpViewer = new popUpViewer(language.messageTitle_warn, language.messageContent_noLog, 3,
                        Resources.warn, Location.X,
                        Location.Y, Width, Height);
                    popUpViewer.ShowDialog();
                    popUpViewer.Focus();
                }
            }

            else
            {
                popUpViewer popUpViewer = new popUpViewer(language.messageTitle_warn, language.messageContent_noLog, 3,
                    Resources.warn, Location.X,
                    Location.Y, Width, Height);
                popUpViewer.ShowDialog();
                popUpViewer.Focus();
            }
        }

        private void contextMenuStrip_notifyIcon_Opening(object sender, CancelEventArgs e)
        {
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logger.doLog(config.actionTypes.appTerminated);
        }

        private void toolStripMenuItem_showSetting_Click(object sender, EventArgs e)
        {
            settingsFormOpen();
        }

        private void toolStripMenuItem_showLog_Click(object sender, EventArgs e)
        {
            showLogs();
        }

        private void toolStripMenuItem_exit_Click(object sender, EventArgs e)
        {
            Logger.doLog(config.actionTypes.appTerminated);
            Application.ExitThread();
        }


        ////
    }
}