namespace WindowsShutdownHelper
{
    partial class logViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(logViewer));
            this.button_clearLogs = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.dataGridView_logs = new System.Windows.Forms.DataGridView();
            this.comboBox_filtering = new System.Windows.Forms.ComboBox();
            this.comboBox_sorting = new System.Windows.Forms.ComboBox();
            this.label_filtering = new System.Windows.Forms.Label();
            this.label_sorting = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_logs)).BeginInit();
            this.SuspendLayout();
            // 
            // button_clearLogs
            // 
            this.button_clearLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button_clearLogs.Location = new System.Drawing.Point(109, 442);
            this.button_clearLogs.Name = "button_clearLogs";
            this.button_clearLogs.Size = new System.Drawing.Size(174, 30);
            this.button_clearLogs.TabIndex = 1;
            this.button_clearLogs.Text = "Clear Logs";
            this.button_clearLogs.UseVisualStyleBackColor = true;
            this.button_clearLogs.Click += new System.EventHandler(this.button_clearLogs_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button_cancel.Location = new System.Drawing.Point(300, 442);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(219, 30);
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // dataGridView_logs
            // 
            this.dataGridView_logs.AllowUserToAddRows = false;
            this.dataGridView_logs.AllowUserToDeleteRows = false;
            this.dataGridView_logs.AllowUserToResizeColumns = false;
            this.dataGridView_logs.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_logs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_logs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_logs.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_logs.Location = new System.Drawing.Point(-4, 47);
            this.dataGridView_logs.MultiSelect = false;
            this.dataGridView_logs.Name = "dataGridView_logs";
            this.dataGridView_logs.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_logs.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_logs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_logs.ShowCellToolTips = false;
            this.dataGridView_logs.ShowEditingIcon = false;
            this.dataGridView_logs.Size = new System.Drawing.Size(614, 379);
            this.dataGridView_logs.TabIndex = 0;
            // 
            // comboBox_filtering
            // 
            this.comboBox_filtering.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_filtering.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboBox_filtering.FormattingEnabled = true;
            this.comboBox_filtering.Location = new System.Drawing.Point(98, 12);
            this.comboBox_filtering.Name = "comboBox_filtering";
            this.comboBox_filtering.Size = new System.Drawing.Size(201, 24);
            this.comboBox_filtering.TabIndex = 3;
            this.comboBox_filtering.SelectedIndexChanged += new System.EventHandler(this.comboBox_filtering_SelectedIndexChanged);
            // 
            // comboBox_sorting
            // 
            this.comboBox_sorting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_sorting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboBox_sorting.FormattingEnabled = true;
            this.comboBox_sorting.Location = new System.Drawing.Point(427, 12);
            this.comboBox_sorting.Name = "comboBox_sorting";
            this.comboBox_sorting.Size = new System.Drawing.Size(167, 24);
            this.comboBox_sorting.TabIndex = 4;
            this.comboBox_sorting.SelectedIndexChanged += new System.EventHandler(this.comboBox_sorting_SelectedIndexChanged);
            // 
            // label_filtering
            // 
            this.label_filtering.AutoSize = true;
            this.label_filtering.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_filtering.Location = new System.Drawing.Point(12, 15);
            this.label_filtering.Name = "label_filtering";
            this.label_filtering.Size = new System.Drawing.Size(64, 16);
            this.label_filtering.TabIndex = 5;
            this.label_filtering.Text = "Filtering";
            // 
            // label_sorting
            // 
            this.label_sorting.AutoSize = true;
            this.label_sorting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_sorting.Location = new System.Drawing.Point(346, 15);
            this.label_sorting.Name = "label_sorting";
            this.label_sorting.Size = new System.Drawing.Size(57, 16);
            this.label_sorting.TabIndex = 6;
            this.label_sorting.Text = "Sorting";
            // 
            // logViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 484);
            this.Controls.Add(this.label_sorting);
            this.Controls.Add(this.label_filtering);
            this.Controls.Add(this.comboBox_sorting);
            this.Controls.Add(this.comboBox_filtering);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_clearLogs);
            this.Controls.Add(this.dataGridView_logs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "logViewer";
            this.Text = "Log Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.logViewer_FormClosing);
            this.Load += new System.EventHandler(this.logViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_logs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_clearLogs;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.DataGridView dataGridView_logs;
        private System.Windows.Forms.ComboBox comboBox_filtering;
        private System.Windows.Forms.ComboBox comboBox_sorting;
        private System.Windows.Forms.Label label_filtering;
        private System.Windows.Forms.Label label_sorting;
    }
}