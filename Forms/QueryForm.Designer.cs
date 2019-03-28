namespace SQLServerDatabaseDiff
{
    partial class QueryForm
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
            this.Query = new SQLServerDatabaseDiff.QueryControl();
            this.SuspendLayout();
            // 
            // Query
            // 
            this.Query.Connection = null;
            this.Query.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Query.Location = new System.Drawing.Point(0, 0);
            this.Query.Name = "Query";
            this.Query.Size = new System.Drawing.Size(632, 446);
            this.Query.TabIndex = 0;
            // 
            // QueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 446);
            this.Controls.Add(this.Query);
            this.Name = "QueryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Query";
            this.ResumeLayout(false);

        }

        #endregion

        public QueryControl Query;

    }
}