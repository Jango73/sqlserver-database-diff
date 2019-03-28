namespace SQLServerDatabaseDiff
{
    partial class GlobalOptions
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
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.ShowReportOnAnalyze = new System.Windows.Forms.CheckBox();
            this.HideUnmodifiedByDefault = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Button_OK
            // 
            this.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Button_OK.Location = new System.Drawing.Point(92, 272);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 0;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Cancel.Location = new System.Drawing.Point(173, 272);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 1;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            // 
            // ShowReportOnAnalyze
            // 
            this.ShowReportOnAnalyze.AutoSize = true;
            this.ShowReportOnAnalyze.Location = new System.Drawing.Point(12, 12);
            this.ShowReportOnAnalyze.Name = "ShowReportOnAnalyze";
            this.ShowReportOnAnalyze.Size = new System.Drawing.Size(137, 17);
            this.ShowReportOnAnalyze.TabIndex = 2;
            this.ShowReportOnAnalyze.Text = "Show report on analyze";
            this.ShowReportOnAnalyze.UseVisualStyleBackColor = true;
            // 
            // HideUnmodifiedByDefault
            // 
            this.HideUnmodifiedByDefault.AutoSize = true;
            this.HideUnmodifiedByDefault.Location = new System.Drawing.Point(12, 35);
            this.HideUnmodifiedByDefault.Name = "HideUnmodifiedByDefault";
            this.HideUnmodifiedByDefault.Size = new System.Drawing.Size(212, 17);
            this.HideUnmodifiedByDefault.TabIndex = 3;
            this.HideUnmodifiedByDefault.Text = "Hide unmodified components by default";
            this.HideUnmodifiedByDefault.UseVisualStyleBackColor = true;
            // 
            // GlobalOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 307);
            this.Controls.Add(this.HideUnmodifiedByDefault);
            this.Controls.Add(this.ShowReportOnAnalyze);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.Name = "GlobalOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Global options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.CheckBox ShowReportOnAnalyze;
        private System.Windows.Forms.CheckBox HideUnmodifiedByDefault;
    }
}