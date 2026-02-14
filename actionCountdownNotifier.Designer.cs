namespace WindowsShutdownHelper
{
    partial class actionCountdownNotifier
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
            this.panel_main = new System.Windows.Forms.Panel();
            this.label_contentYouCanThat = new System.Windows.Forms.Label();
            this.label_contentActionType = new System.Windows.Forms.Label();
            this.button_skip = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_Ignore = new System.Windows.Forms.Button();
            this.label_title = new System.Windows.Forms.Label();
            this.pictureBox_main = new System.Windows.Forms.PictureBox();
            this.label_contentCountdownNotify = new System.Windows.Forms.Label();
            this.panel_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_main)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.BackColor = System.Drawing.Color.Ivory;
            this.panel_main.Controls.Add(this.label_contentYouCanThat);
            this.panel_main.Controls.Add(this.label_contentActionType);
            this.panel_main.Controls.Add(this.button_skip);
            this.panel_main.Controls.Add(this.button_delete);
            this.panel_main.Controls.Add(this.button_Ignore);
            this.panel_main.Controls.Add(this.label_title);
            this.panel_main.Controls.Add(this.pictureBox_main);
            this.panel_main.Controls.Add(this.label_contentCountdownNotify);
            this.panel_main.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel_main.Location = new System.Drawing.Point(0, 0);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(358, 172);
            this.panel_main.TabIndex = 0;
            this.panel_main.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_main_borderPaint);
            this.panel_main.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_main_MouseDown);
            this.panel_main.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_main_MouseMove);
            this.panel_main.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_main_MouseUp);
            // 
            // label_contentYouCanThat
            // 
            this.label_contentYouCanThat.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_contentYouCanThat.Location = new System.Drawing.Point(1, 103);
            this.label_contentYouCanThat.Name = "label_contentYouCanThat";
            this.label_contentYouCanThat.Size = new System.Drawing.Size(356, 28);
            this.label_contentYouCanThat.TabIndex = 0;
            this.label_contentYouCanThat.Text = "_contentYouCanThat";
            this.label_contentYouCanThat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_contentYouCanThat.Click += new System.EventHandler(this.label_contentIfYouDontWant_Click);
            this.label_contentYouCanThat.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_main_MouseDown);
            this.label_contentYouCanThat.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_main_MouseMove);
            this.label_contentYouCanThat.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_main_MouseUp);
            // 
            // label_contentActionType
            // 
            this.label_contentActionType.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_contentActionType.ForeColor = System.Drawing.Color.Red;
            this.label_contentActionType.Location = new System.Drawing.Point(1, 77);
            this.label_contentActionType.Name = "label_contentActionType";
            this.label_contentActionType.Size = new System.Drawing.Size(356, 28);
            this.label_contentActionType.TabIndex = 0;
            this.label_contentActionType.Text = "label_contentActionType";
            this.label_contentActionType.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label_contentActionType.Click += new System.EventHandler(this.label_contentActionType_Click);
            this.label_contentActionType.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_main_MouseDown);
            this.label_contentActionType.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_main_MouseMove);
            this.label_contentActionType.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_main_MouseUp);
            // 
            // button_skip
            // 
            this.button_skip.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button_skip.Enabled = false;
            this.button_skip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button_skip.Location = new System.Drawing.Point(243, 138);
            this.button_skip.Name = "button_skip";
            this.button_skip.Size = new System.Drawing.Size(100, 25);
            this.button_skip.TabIndex = 3;
            this.button_skip.Text = "Skip";
            this.button_skip.UseVisualStyleBackColor = false;
            this.button_skip.Click += new System.EventHandler(this.button_skip_Click);
            // 
            // button_delete
            // 
            this.button_delete.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button_delete.Enabled = false;
            this.button_delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button_delete.Location = new System.Drawing.Point(15, 138);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(100, 25);
            this.button_delete.TabIndex = 1;
            this.button_delete.Text = "Delete";
            this.button_delete.UseVisualStyleBackColor = false;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // button_Ignore
            // 
            this.button_Ignore.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button_Ignore.Enabled = false;
            this.button_Ignore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button_Ignore.Location = new System.Drawing.Point(129, 138);
            this.button_Ignore.Name = "button_Ignore";
            this.button_Ignore.Size = new System.Drawing.Size(100, 25);
            this.button_Ignore.TabIndex = 2;
            this.button_Ignore.Text = "Ignore";
            this.button_Ignore.UseVisualStyleBackColor = false;
            this.button_Ignore.Click += new System.EventHandler(this.button_Skip_Click);
            // 
            // label_title
            // 
            this.label_title.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_title.Location = new System.Drawing.Point(173, 11);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(138, 31);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "label_title";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_main_MouseDown);
            this.label_title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_main_MouseMove);
            this.label_title.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_main_MouseUp);
            // 
            // pictureBox_main
            // 
            this.pictureBox_main.Image = global::WindowsShutdownHelper.Properties.Resources.success;
            this.pictureBox_main.Location = new System.Drawing.Point(129, 11);
            this.pictureBox_main.Name = "pictureBox_main";
            this.pictureBox_main.Size = new System.Drawing.Size(42, 31);
            this.pictureBox_main.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_main.TabIndex = 1;
            this.pictureBox_main.TabStop = false;
            this.pictureBox_main.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_main_MouseDown);
            this.pictureBox_main.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_main_MouseMove);
            this.pictureBox_main.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_main_MouseUp);
            // 
            // label_contentCountdownNotify
            // 
            this.label_contentCountdownNotify.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_contentCountdownNotify.Location = new System.Drawing.Point(1, 50);
            this.label_contentCountdownNotify.Name = "label_contentCountdownNotify";
            this.label_contentCountdownNotify.Size = new System.Drawing.Size(356, 28);
            this.label_contentCountdownNotify.TabIndex = 0;
            this.label_contentCountdownNotify.Text = "label_contentCountdownNotify";
            this.label_contentCountdownNotify.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label_contentCountdownNotify.Click += new System.EventHandler(this.label_contentCountdownNotify_Click);
            this.label_contentCountdownNotify.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_main_MouseDown);
            this.label_contentCountdownNotify.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_main_MouseMove);
            this.label_contentCountdownNotify.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_main_MouseUp);
            // 
            // actionCountdownNotifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(358, 172);
            this.Controls.Add(this.panel_main);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "actionCountdownNotifier";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "popUpViewer";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.actionCountdownNotifier_Load);
            this.panel_main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_main)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_main;
        private System.Windows.Forms.PictureBox pictureBox_main;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Button button_Ignore;
        private System.Windows.Forms.Button button_skip;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Label label_contentActionType;
        private System.Windows.Forms.Label label_contentYouCanThat;
        private System.Windows.Forms.Label label_contentCountdownNotify;
    }
}