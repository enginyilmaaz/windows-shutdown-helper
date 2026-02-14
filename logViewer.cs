using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using WindowsShutdownHelper.functions;
using WindowsShutdownHelper.Properties;

namespace WindowsShutdownHelper
{
    public partial class logViewer : Form
    {
        public static language language = languageSelector.languageFile();
        public static List<logSystem> logList = new List<logSystem>();
        public static int x;
        public static int y;
        public List<logSystem> logListLocal = new List<logSystem>();


        public logViewer(List<logSystem> _logList, int _x, int _y, int _width, int _height)
        {
            InitializeComponent();
            x = _x + (_width - Width) / 2;
            y = _y + (_height - Height) / 2;
            logList = _logList;
        }


        private void logViewer_Load(object sender, EventArgs e)
        {
            label_filtering.Text = language.logViewerForm_label_filtering + ": ";
            label_sorting.Text = language.logViewerForm_label_sorting + " :";
            comboBox_filtering.Items.Add(language.logViewerForm_filter_choose);
            comboBox_filtering.Items.Add(language.logViewerForm_filter_locks);
            comboBox_filtering.Items.Add(language.logViewerForm_filter_unlocks);
            comboBox_filtering.Items.Add(language.logViewerForm_filter_turnOffsMonitor);
            comboBox_filtering.Items.Add(language.logViewerForm_filter_sleeps);
            comboBox_filtering.Items.Add(language.logViewerForm_filter_logOffs);
            comboBox_filtering.Items.Add(language.logViewerForm_filter_shutdowns);
            comboBox_filtering.Items.Add(language.logViewerForm_filter_restarts);
            comboBox_filtering.Items.Add(language.logViewerForm_filter_appStarts);
            comboBox_filtering.Items.Add(language.logViewerForm_filter_appTerminates);
            comboBox_filtering.SelectedIndex = 0;

            comboBox_sorting.Items.Add(language.logViewerForm_sorting_choose);
            comboBox_sorting.Items.Add(language.logViewerForm_sorting_OldestToNewest);
            comboBox_sorting.Items.Add(language.logViewerForm_sorting_newestToOld);
            comboBox_sorting.SelectedIndex = 0;



            Location = new Point(x, y);

            Text = language.logViewerForm_Name;
            button_cancel.Text = language.logViewerForm_button_cancel;
            button_clearLogs.Text = language.logViewerForm_button_clearLogs;



            logRecordShowLocally();
            dataGridView_logs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_logs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_logs.Columns["actionExecutedDate"].HeaderText = language.logViewerForm_actionExecutionTime;
            dataGridView_logs.Columns["actionType"].HeaderText = language.logViewerForm_actionType;
            dataGridView_logs.Columns["actionExecutedDate"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dataGridView_logs.Columns["actionType"].DefaultCellStyle.Padding =
                new Padding(20, 0, 0, 0);




        }


        private void button_clearLogs_Click(object sender, EventArgs e)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\logs.json"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\logs.json");
            }

            logRecordShowLocally();

            popUpViewer popUpViewer = new popUpViewer(language.messageTitle_success,
                language.messageContent_clearedLogs + "\n" + language.messageContent_thisWillAutoClose,
                3, Resources.success, Location.X, Location.Y, Width, Height);
            popUpViewer.ShowDialog();

            GC.Collect();
            GC.SuppressFinalize(this);
            Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            GC.Collect();
            GC.SuppressFinalize(this);
            Close();
        }

        private void logViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }


        public void logRecordShowLocally()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\logs.json"))
            {
                logListLocal.Clear();
                logListLocal = JsonSerializer
                 .Deserialize<List<logSystem>>(
                     File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\logs.json"))
                 .OrderBy(a => a.actionExecutedDate).Take(250).ToList();
            }

            foreach (logSystem act in logListLocal)
            {
                if (act.actionType == config.actionTypes.logOffWindows)
                {
                    act.actionType = language.logViewerForm_logOffWindows;
                }

                if (act.actionType == config.actionTypes.lockComputer)
                {
                    act.actionType = language.logViewerForm_lockComputer;
                }

                if (act.actionType == config.actionTypes.shutdownComputer)
                {
                    act.actionType = language.logViewerForm_shutdownComputer;
                }

                if (act.actionType == config.actionTypes.restartComputer)
                {
                    act.actionType = language.logViewerForm_restartComputer;
                }

                if (act.actionType == config.actionTypes.turnOffMonitor)
                {
                    act.actionType = language.logViewerForm_turnOffMonitor;
                }

                if (act.actionType == config.actionTypes.sleepComputer)
                {
                    act.actionType = language.logViewerForm_sleepComputer;
                }

                if (act.actionType == config.actionTypes.lockComputerManually)
                {
                    act.actionType = language.logViewerForm_lockComputerManually;
                }

                if (act.actionType == config.actionTypes.unlockComputer)
                {
                    act.actionType = language.logViewerForm_unlockComputer;
                }

                if (act.actionType == config.actionTypes.appStarted)
                {
                    act.actionType = language.logViewerForm_appStarted;
                }

                if (act.actionType == config.actionTypes.appTerminated)
                {
                    act.actionType = language.logViewerForm_appTerminated;
                }
            }

            int counter = logListLocal.
                Where(s => s.actionType == language.logViewerForm_lockComputer).ToList().Count();
            if (counter <= 0)
            {
                comboBox_filtering.Items.Remove(language.logViewerForm_filter_locks);
            }

            counter = logListLocal.
                Where(s => s.actionType == language.logViewerForm_unlockComputer).ToList().Count();
            if (counter <= 0)
            {
                comboBox_filtering.Items.Remove(language.logViewerForm_filter_unlocks);
            }

            counter = logListLocal.
                Where(s => s.actionType == language.logViewerForm_turnOffMonitor).ToList().Count();
            if (counter <= 0)
            {
                comboBox_filtering.Items.Remove(language.logViewerForm_filter_turnOffsMonitor);
            }

            counter = logListLocal.
                Where(s => s.actionType == language.logViewerForm_sleepComputer).ToList().Count();
            if (counter <= 0)
            {
                comboBox_filtering.Items.Remove(language.logViewerForm_filter_sleeps);
            }

            counter = logListLocal.
                Where(s => s.actionType == language.logViewerForm_logOffWindows).ToList().Count();
            if (counter <= 0)
            {
                comboBox_filtering.Items.Remove(language.logViewerForm_filter_logOffs);
            }

            counter = logListLocal.
                Where(s => s.actionType == language.logViewerForm_shutdownComputer).ToList().Count();
            if (counter <= 0)
            {
                comboBox_filtering.Items.Remove(language.logViewerForm_filter_shutdowns);
            }

            counter = logListLocal.
                Where(s => s.actionType == language.logViewerForm_restartComputer).ToList().Count();
            if (counter <= 0)
            {
                comboBox_filtering.Items.Remove(language.logViewerForm_filter_restarts);
            }

            counter = logListLocal.
                Where(s => s.actionType == language.logViewerForm_appStarted).ToList().Count();
            if (counter <= 0)
            {
                comboBox_filtering.Items.Remove(language.logViewerForm_filter_appStarts);
            }

            counter = logListLocal.
                Where(s => s.actionType == language.logViewerForm_appTerminated).ToList().Count();
            if (counter <= 0)
            {
                comboBox_filtering.Items.Remove(language.logViewerForm_filter_appTerminates);
            }

            dataGridView_logs.DataSource = null;
            dataGridView_logs.DataSource = logListLocal;


            cellHeaderNumerator();
        }

        public void cellHeaderNumerator()
        {
            int rowNumber = 1;
            foreach (DataGridViewRow row in dataGridView_logs.Rows)
            {
                if (row.IsNewRow == false)
                {
                    row.HeaderCell.Value = "" + rowNumber;
                }

                rowNumber = rowNumber + 1;
            }

            dataGridView_logs.AutoResizeRowHeadersWidth(
                DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void comboBox_filtering_SelectedIndexChanged(object sender, EventArgs e)
        {
            filteringAndSorting();

        }

        private void comboBox_sorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            filteringAndSorting();
        }

        public void filteringAndSorting()
        {
            string selectedSorting = comboBox_sorting.Text;
            string selectedfiltering = comboBox_filtering.Text;

            if (selectedfiltering == language.logViewerForm_filter_choose)
            {

                if (selectedSorting == language.logViewerForm_sorting_OldestToNewest || selectedSorting == language.logViewerForm_sorting_choose)
                {
                    dataGridView_logs.DataSource = logListLocal.OrderBy(s => s.actionExecutedDate).ToList();
                }

                if (selectedSorting == language.logViewerForm_sorting_newestToOld)
                {
                    dataGridView_logs.DataSource = logListLocal.OrderByDescending(s => s.actionExecutedDate).ToList();
                }

            }

            if (selectedfiltering == language.logViewerForm_filter_locks)
            {
                if (selectedSorting == language.logViewerForm_sorting_OldestToNewest || selectedSorting == language.logViewerForm_sorting_choose)
                {
                    dataGridView_logs.DataSource = logListLocal
                        .Where(s => s.actionType == language.logViewerForm_lockComputer || s.actionType == language.logViewerForm_lockComputerManually).
                        OrderBy(s => s.actionExecutedDate).ToList();
                }

                if (selectedSorting == language.logViewerForm_sorting_newestToOld)
                {

                    dataGridView_logs.DataSource = logListLocal
                        .Where(s => s.actionType == language.logViewerForm_lockComputer || s.actionType == language.logViewerForm_lockComputerManually).
                        OrderByDescending(s => s.actionExecutedDate).ToList();

                }

            }

            if (selectedfiltering == language.logViewerForm_filter_unlocks)
            {
                if (selectedSorting == language.logViewerForm_sorting_OldestToNewest || selectedSorting == language.logViewerForm_sorting_choose)
                {
                    dataGridView_logs.DataSource = logListLocal
                        .Where(s => s.actionType == language.logViewerForm_unlockComputer).
                        OrderBy(s => s.actionExecutedDate).ToList();
                }

                if (selectedSorting == language.logViewerForm_sorting_newestToOld)
                {

                    dataGridView_logs.DataSource = logListLocal
                        .Where(s => s.actionType == language.logViewerForm_unlockComputer)
                        .OrderByDescending(s => s.actionExecutedDate).ToList();

                }

            }


            if (selectedfiltering == language.logViewerForm_filter_turnOffsMonitor)
            {
                if (selectedSorting == language.logViewerForm_sorting_OldestToNewest || selectedSorting == language.logViewerForm_sorting_choose)
                {
                    dataGridView_logs.DataSource = logListLocal
                        .Where(s => s.actionType == language.logViewerForm_turnOffMonitor).
                        OrderBy(s => s.actionExecutedDate).ToList();
                }

                if (selectedSorting == language.logViewerForm_sorting_newestToOld)
                {

                    dataGridView_logs.DataSource = logListLocal
                        .Where(s => s.actionType == language.logViewerForm_turnOffMonitor)
                        .OrderByDescending(s => s.actionExecutedDate).ToList();

                }



            }


            if (selectedfiltering == language.logViewerForm_filter_sleeps)
            {
                if (selectedSorting == language.logViewerForm_sorting_OldestToNewest || selectedSorting == language.logViewerForm_sorting_choose)
                {
                    dataGridView_logs.DataSource = logListLocal
                        .Where(s => s.actionType == language.logViewerForm_sleepComputer).
                        OrderBy(s => s.actionExecutedDate).ToList();
                }

                if (selectedSorting == language.logViewerForm_sorting_newestToOld)
                {

                    dataGridView_logs.DataSource = logListLocal
                        .Where(s => s.actionType == language.logViewerForm_sleepComputer)
                        .OrderByDescending(s => s.actionExecutedDate).ToList();

                }

            }


            if (selectedfiltering == language.logViewerForm_filter_logOffs)
            {
                if (selectedSorting == language.logViewerForm_sorting_OldestToNewest || selectedSorting == language.logViewerForm_sorting_choose)
                {
                    dataGridView_logs.DataSource = logListLocal
                        .Where(s => s.actionType == language.logViewerForm_logOffWindows).
                        OrderBy(s => s.actionExecutedDate).ToList();
                }

                if (selectedSorting == language.logViewerForm_sorting_newestToOld)
                {

                    dataGridView_logs.DataSource = logListLocal
                        .Where(s => s.actionType == language.logViewerForm_logOffWindows)
                        .OrderByDescending(s => s.actionExecutedDate).ToList();

                }

            }


            if (selectedfiltering == language.logViewerForm_filter_shutdowns)
            {
                if (selectedSorting == language.logViewerForm_sorting_OldestToNewest || selectedSorting == language.logViewerForm_sorting_choose)
                {
                    dataGridView_logs.DataSource = logListLocal
                        .Where(s => s.actionType == language.logViewerForm_shutdownComputer).
                        OrderBy(s => s.actionExecutedDate).ToList();
                }

                if (selectedSorting == language.logViewerForm_sorting_newestToOld)
                {

                    dataGridView_logs.DataSource = logListLocal
                        .Where(s => s.actionType == language.logViewerForm_shutdownComputer)
                        .OrderByDescending(s => s.actionExecutedDate).ToList();

                }

            }

            if (selectedfiltering == language.logViewerForm_filter_restarts)
            {
                if (selectedSorting == language.logViewerForm_sorting_OldestToNewest || selectedSorting == language.logViewerForm_sorting_choose)
                {
                    dataGridView_logs.DataSource = logListLocal
                        .Where(s => s.actionType == language.logViewerForm_restartComputer).
                        OrderBy(s => s.actionExecutedDate).ToList();
                }

                if (selectedSorting == language.logViewerForm_sorting_newestToOld)
                {

                    dataGridView_logs.DataSource = logListLocal
                        .Where(s => s.actionType == language.logViewerForm_restartComputer)
                        .OrderByDescending(s => s.actionExecutedDate).ToList();

                }


            }

            if (selectedfiltering == language.logViewerForm_filter_appStarts)
            {
                if (selectedSorting == language.logViewerForm_sorting_OldestToNewest || selectedSorting == language.logViewerForm_sorting_choose)
                {
                    dataGridView_logs.DataSource = logListLocal
                        .Where(s => s.actionType == language.logViewerForm_appStarted).
                        OrderBy(s => s.actionExecutedDate).ToList();
                }

                if (selectedSorting == language.logViewerForm_sorting_newestToOld)
                {

                    dataGridView_logs.DataSource = logListLocal
                        .Where(s => s.actionType == language.logViewerForm_appStarted)
                        .OrderByDescending(s => s.actionExecutedDate).ToList();

                }

            }

            if (selectedfiltering == language.logViewerForm_filter_appTerminates)
            {
                if (selectedSorting == language.logViewerForm_sorting_OldestToNewest || selectedSorting == language.logViewerForm_sorting_choose)
                {
                    dataGridView_logs.DataSource = logListLocal
                        .Where(s => s.actionType == language.logViewerForm_appTerminated).
                        OrderBy(s => s.actionExecutedDate).ToList();
                }

                if (selectedSorting == language.logViewerForm_sorting_newestToOld)
                {

                    dataGridView_logs.DataSource = logListLocal
                        .Where(s => s.actionType == language.logViewerForm_appTerminated)
                        .OrderByDescending(s => s.actionExecutedDate).ToList();

                }

            }

            cellHeaderNumerator();
        }
        /////////////////////////////////////////
    }
}