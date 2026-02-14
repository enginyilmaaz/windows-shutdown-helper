namespace WindowsShutdownHelper
{
    partial class settingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(settingsForm));
            this.label_logs = new System.Windows.Forms.Label();
            this.label_startWithWindows = new System.Windows.Forms.Label();
            this.button_save = new System.Windows.Forms.Button();
            this.checkBox_logEnabled = new System.Windows.Forms.CheckBox();
            this.checkBox_startWithWindowsEnabled = new System.Windows.Forms.CheckBox();
            this.button_cancel = new System.Windows.Forms.Button();
            this.label_runInTaskbarWhenClosed = new System.Windows.Forms.Label();
            this.checkBox_runInTaskbarWhenClosed = new System.Windows.Forms.CheckBox();
            this.checkBox_isCountdownNotifierEnabled = new System.Windows.Forms.CheckBox();
            this.label_isCountdownNotifierEnabled = new System.Windows.Forms.Label();
            this.label_countdownNotifierSeconds = new System.Windows.Forms.Label();
            this.numericUpDown_countdownNotifierSeconds = new System.Windows.Forms.NumericUpDown();
            this.label_language = new System.Windows.Forms.Label();
            this.comboBox_lang = new System.Windows.Forms.ComboBox();
            this.label_seperator = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_countdownNotifierSeconds)).BeginInit();
            this.SuspendLayout();
            // 
            // label_logs
            // 
            this.label_logs.AutoSize = true;
            this.label_logs.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_logs.Location = new System.Drawing.Point(12, 75);
            this.label_logs.Name = "label_logs";
            this.label_logs.Size = new System.Drawing.Size(110, 22);
            this.label_logs.TabIndex = 0;
            this.label_logs.Text = "Record Logs:";
            // 
            // label_startWithWindows
            // 
            this.label_startWithWindows.AutoSize = true;
            this.label_startWithWindows.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_startWithWindows.Location = new System.Drawing.Point(12, 118);
            this.label_startWithWindows.Name = "label_startWithWindows";
            this.label_startWithWindows.Size = new System.Drawing.Size(148, 22);
            this.label_startWithWindows.TabIndex = 0;
            this.label_startWithWindows.Text = "StartwithWindows";
            // 
            // button_save
            // 
            this.button_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button_save.Location = new System.Drawing.Point(72, 311);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(133, 39);
            this.button_save.TabIndex = 7;
            this.button_save.Text = "save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // checkBox_logEnabled
            // 
            this.checkBox_logEnabled.AutoSize = true;
            this.checkBox_logEnabled.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.checkBox_logEnabled.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBox_logEnabled.Location = new System.Drawing.Point(310, 74);
            this.checkBox_logEnabled.Name = "checkBox_logEnabled";
            this.checkBox_logEnabled.Size = new System.Drawing.Size(85, 26);
            this.checkBox_logEnabled.TabIndex = 2;
            this.checkBox_logEnabled.Text = "Enabled";
            this.checkBox_logEnabled.UseVisualStyleBackColor = true;
            // 
            // checkBox_startWithWindowsEnabled
            // 
            this.checkBox_startWithWindowsEnabled.AutoSize = true;
            this.checkBox_startWithWindowsEnabled.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.checkBox_startWithWindowsEnabled.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBox_startWithWindowsEnabled.Location = new System.Drawing.Point(310, 117);
            this.checkBox_startWithWindowsEnabled.Name = "checkBox_startWithWindowsEnabled";
            this.checkBox_startWithWindowsEnabled.Size = new System.Drawing.Size(85, 26);
            this.checkBox_startWithWindowsEnabled.TabIndex = 3;
            this.checkBox_startWithWindowsEnabled.Text = "Enabled";
            this.checkBox_startWithWindowsEnabled.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button_cancel.Location = new System.Drawing.Point(239, 311);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(131, 39);
            this.button_cancel.TabIndex = 8;
            this.button_cancel.Text = "cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // label_runInTaskbarWhenClosed
            // 
            this.label_runInTaskbarWhenClosed.AutoSize = true;
            this.label_runInTaskbarWhenClosed.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_runInTaskbarWhenClosed.Location = new System.Drawing.Point(12, 162);
            this.label_runInTaskbarWhenClosed.Name = "label_runInTaskbarWhenClosed";
            this.label_runInTaskbarWhenClosed.Size = new System.Drawing.Size(196, 22);
            this.label_runInTaskbarWhenClosed.TabIndex = 0;
            this.label_runInTaskbarWhenClosed.Text = "runInTaskbarWhenClosed";
            // 
            // checkBox_runInTaskbarWhenClosed
            // 
            this.checkBox_runInTaskbarWhenClosed.AutoSize = true;
            this.checkBox_runInTaskbarWhenClosed.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.checkBox_runInTaskbarWhenClosed.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBox_runInTaskbarWhenClosed.Location = new System.Drawing.Point(310, 161);
            this.checkBox_runInTaskbarWhenClosed.Name = "checkBox_runInTaskbarWhenClosed";
            this.checkBox_runInTaskbarWhenClosed.Size = new System.Drawing.Size(85, 26);
            this.checkBox_runInTaskbarWhenClosed.TabIndex = 4;
            this.checkBox_runInTaskbarWhenClosed.Text = "Enabled";
            this.checkBox_runInTaskbarWhenClosed.UseVisualStyleBackColor = true;
            // 
            // checkBox_isCountdownNotifierEnabled
            // 
            this.checkBox_isCountdownNotifierEnabled.AutoSize = true;
            this.checkBox_isCountdownNotifierEnabled.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.checkBox_isCountdownNotifierEnabled.Location = new System.Drawing.Point(310, 249);
            this.checkBox_isCountdownNotifierEnabled.Name = "checkBox_isCountdownNotifierEnabled";
            this.checkBox_isCountdownNotifierEnabled.Size = new System.Drawing.Size(85, 26);
            this.checkBox_isCountdownNotifierEnabled.TabIndex = 6;
            this.checkBox_isCountdownNotifierEnabled.Text = "Enabled";
            this.checkBox_isCountdownNotifierEnabled.UseVisualStyleBackColor = true;
            // 
            // label_isCountdownNotifierEnabled
            // 
            this.label_isCountdownNotifierEnabled.AutoSize = true;
            this.label_isCountdownNotifierEnabled.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_isCountdownNotifierEnabled.Location = new System.Drawing.Point(12, 250);
            this.label_isCountdownNotifierEnabled.Name = "label_isCountdownNotifierEnabled";
            this.label_isCountdownNotifierEnabled.Size = new System.Drawing.Size(225, 22);
            this.label_isCountdownNotifierEnabled.TabIndex = 0;
            this.label_isCountdownNotifierEnabled.Text = "isCountdownNotifierEnabled";
            // 
            // label_countdownNotifierSeconds
            // 
            this.label_countdownNotifierSeconds.AutoSize = true;
            this.label_countdownNotifierSeconds.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_countdownNotifierSeconds.Location = new System.Drawing.Point(12, 207);
            this.label_countdownNotifierSeconds.Name = "label_countdownNotifierSeconds";
            this.label_countdownNotifierSeconds.Size = new System.Drawing.Size(215, 22);
            this.label_countdownNotifierSeconds.TabIndex = 0;
            this.label_countdownNotifierSeconds.Text = "countdownNotifierSeconds";
            // 
            // numericUpDown_countdownNotifierSeconds
            // 
            this.numericUpDown_countdownNotifierSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.numericUpDown_countdownNotifierSeconds.Location = new System.Drawing.Point(310, 207);
            this.numericUpDown_countdownNotifierSeconds.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown_countdownNotifierSeconds.Name = "numericUpDown_countdownNotifierSeconds";
            this.numericUpDown_countdownNotifierSeconds.Size = new System.Drawing.Size(79, 26);
            this.numericUpDown_countdownNotifierSeconds.TabIndex = 5;
            this.numericUpDown_countdownNotifierSeconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDown_countdownNotifierSeconds_KeyPress);
            // 
            // label_language
            // 
            this.label_language.AutoSize = true;
            this.label_language.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_language.Location = new System.Drawing.Point(12, 27);
            this.label_language.Name = "label_language";
            this.label_language.Size = new System.Drawing.Size(77, 22);
            this.label_language.TabIndex = 0;
            this.label_language.Text = "language";
            // 
            // comboBox_lang
            // 
            this.comboBox_lang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_lang.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboBox_lang.FormattingEnabled = true;
            this.comboBox_lang.Location = new System.Drawing.Point(261, 25);
            this.comboBox_lang.Name = "comboBox_lang";
            this.comboBox_lang.Size = new System.Drawing.Size(178, 28);
            this.comboBox_lang.TabIndex = 1;
            // 
            // label_seperator
            // 
            this.label_seperator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_seperator.Location = new System.Drawing.Point(0, 65);
            this.label_seperator.Name = "label_seperator";
            this.label_seperator.Size = new System.Drawing.Size(460, 2);
            this.label_seperator.TabIndex = 10;
            this.label_seperator.Text = "label1";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(0, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(460, 2);
            this.label1.TabIndex = 11;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(0, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(460, 2);
            this.label2.TabIndex = 12;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(0, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(460, 2);
            this.label3.TabIndex = 13;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(0, 241);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(460, 2);
            this.label4.TabIndex = 14;
            this.label4.Text = "label4";
            // 
            // settingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 375);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_seperator);
            this.Controls.Add(this.comboBox_lang);
            this.Controls.Add(this.label_language);
            this.Controls.Add(this.numericUpDown_countdownNotifierSeconds);
            this.Controls.Add(this.label_countdownNotifierSeconds);
            this.Controls.Add(this.checkBox_isCountdownNotifierEnabled);
            this.Controls.Add(this.label_isCountdownNotifierEnabled);
            this.Controls.Add(this.checkBox_runInTaskbarWhenClosed);
            this.Controls.Add(this.label_runInTaskbarWhenClosed);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.checkBox_startWithWindowsEnabled);
            this.Controls.Add(this.checkBox_logEnabled);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.label_startWithWindows);
            this.Controls.Add(this.label_logs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "settingsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.settingsForm_FormClosing);
            this.Load += new System.EventHandler(this.settingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_countdownNotifierSeconds)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_logs;
        private System.Windows.Forms.Label label_startWithWindows;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.CheckBox checkBox_logEnabled;
        private System.Windows.Forms.CheckBox checkBox_startWithWindowsEnabled;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Label label_runInTaskbarWhenClosed;
        private System.Windows.Forms.CheckBox checkBox_runInTaskbarWhenClosed;
        private System.Windows.Forms.CheckBox checkBox_isCountdownNotifierEnabled;
        private System.Windows.Forms.Label label_isCountdownNotifierEnabled;
        private System.Windows.Forms.Label label_countdownNotifierSeconds;
        private System.Windows.Forms.NumericUpDown numericUpDown_countdownNotifierSeconds;
        private System.Windows.Forms.Label label_language;
        private System.Windows.Forms.ComboBox comboBox_lang;
        private System.Windows.Forms.Label label_seperator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}