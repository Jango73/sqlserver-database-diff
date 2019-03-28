namespace SQLServerDatabaseDiff
{
    partial class AnalyzeOptions
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
            this.CHK_AnalyzeTables = new System.Windows.Forms.CheckBox();
            this.CHK_AnalyzeProcedures = new System.Windows.Forms.CheckBox();
            this.CHK_AnalyzeFunctions = new System.Windows.Forms.CheckBox();
            this.CHK_AnalyzeData = new System.Windows.Forms.CheckBox();
            this.BT_OK = new System.Windows.Forms.Button();
            this.BT_Cancel = new System.Windows.Forms.Button();
            this.CHK_AnalyzeViews = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // CHK_AnalyzeTables
            // 
            this.CHK_AnalyzeTables.AutoSize = true;
            this.CHK_AnalyzeTables.Location = new System.Drawing.Point(12, 12);
            this.CHK_AnalyzeTables.Name = "CHK_AnalyzeTables";
            this.CHK_AnalyzeTables.Size = new System.Drawing.Size(99, 17);
            this.CHK_AnalyzeTables.TabIndex = 0;
            this.CHK_AnalyzeTables.Text = "Compare tables";
            this.CHK_AnalyzeTables.UseVisualStyleBackColor = true;
            // 
            // CHK_AnalyzeProcedures
            // 
            this.CHK_AnalyzeProcedures.AutoSize = true;
            this.CHK_AnalyzeProcedures.Location = new System.Drawing.Point(12, 58);
            this.CHK_AnalyzeProcedures.Name = "CHK_AnalyzeProcedures";
            this.CHK_AnalyzeProcedures.Size = new System.Drawing.Size(124, 17);
            this.CHK_AnalyzeProcedures.TabIndex = 2;
            this.CHK_AnalyzeProcedures.Text = "Compare procedures";
            this.CHK_AnalyzeProcedures.UseVisualStyleBackColor = true;
            // 
            // CHK_AnalyzeFunctions
            // 
            this.CHK_AnalyzeFunctions.AutoSize = true;
            this.CHK_AnalyzeFunctions.Location = new System.Drawing.Point(12, 81);
            this.CHK_AnalyzeFunctions.Name = "CHK_AnalyzeFunctions";
            this.CHK_AnalyzeFunctions.Size = new System.Drawing.Size(114, 17);
            this.CHK_AnalyzeFunctions.TabIndex = 3;
            this.CHK_AnalyzeFunctions.Text = "Compare functions";
            this.CHK_AnalyzeFunctions.UseVisualStyleBackColor = true;
            // 
            // CHK_AnalyzeData
            // 
            this.CHK_AnalyzeData.AutoSize = true;
            this.CHK_AnalyzeData.Location = new System.Drawing.Point(12, 104);
            this.CHK_AnalyzeData.Name = "CHK_AnalyzeData";
            this.CHK_AnalyzeData.Size = new System.Drawing.Size(92, 17);
            this.CHK_AnalyzeData.TabIndex = 4;
            this.CHK_AnalyzeData.Text = "Compare data";
            this.CHK_AnalyzeData.UseVisualStyleBackColor = true;
            // 
            // BT_OK
            // 
            this.BT_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BT_OK.Location = new System.Drawing.Point(67, 132);
            this.BT_OK.Name = "BT_OK";
            this.BT_OK.Size = new System.Drawing.Size(75, 23);
            this.BT_OK.TabIndex = 5;
            this.BT_OK.Text = "OK";
            this.BT_OK.UseVisualStyleBackColor = true;
            // 
            // BT_Cancel
            // 
            this.BT_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BT_Cancel.Location = new System.Drawing.Point(148, 132);
            this.BT_Cancel.Name = "BT_Cancel";
            this.BT_Cancel.Size = new System.Drawing.Size(75, 23);
            this.BT_Cancel.TabIndex = 6;
            this.BT_Cancel.Text = "Cancel";
            this.BT_Cancel.UseVisualStyleBackColor = true;
            // 
            // CHK_AnalyzeViews
            // 
            this.CHK_AnalyzeViews.AutoSize = true;
            this.CHK_AnalyzeViews.Location = new System.Drawing.Point(12, 35);
            this.CHK_AnalyzeViews.Name = "CHK_AnalyzeViews";
            this.CHK_AnalyzeViews.Size = new System.Drawing.Size(98, 17);
            this.CHK_AnalyzeViews.TabIndex = 1;
            this.CHK_AnalyzeViews.Text = "Compare views";
            this.CHK_AnalyzeViews.UseVisualStyleBackColor = true;
            this.CHK_AnalyzeViews.CheckedChanged += new System.EventHandler(this.AnalyzeViews_CheckedChanged);
            // 
            // AnalyzeOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 167);
            this.Controls.Add(this.CHK_AnalyzeViews);
            this.Controls.Add(this.BT_Cancel);
            this.Controls.Add(this.BT_OK);
            this.Controls.Add(this.CHK_AnalyzeData);
            this.Controls.Add(this.CHK_AnalyzeFunctions);
            this.Controls.Add(this.CHK_AnalyzeProcedures);
            this.Controls.Add(this.CHK_AnalyzeTables);
            this.Name = "AnalyzeOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compare options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CHK_AnalyzeTables;
        private System.Windows.Forms.CheckBox CHK_AnalyzeProcedures;
        private System.Windows.Forms.CheckBox CHK_AnalyzeFunctions;
        private System.Windows.Forms.CheckBox CHK_AnalyzeData;
        private System.Windows.Forms.Button BT_OK;
        private System.Windows.Forms.Button BT_Cancel;
        private System.Windows.Forms.CheckBox CHK_AnalyzeViews;
    }
}