namespace SQLServerDatabaseDiff
{
    partial class FilterForm
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
            this.BT_OK = new System.Windows.Forms.Button();
            this.BT_Cancel = new System.Windows.Forms.Button();
            this.TB_Filter = new System.Windows.Forms.TextBox();
            this.CHK_HideUnmodified = new System.Windows.Forms.CheckBox();
            this.CHK_HideCreated = new System.Windows.Forms.CheckBox();
            this.CHK_HideDropped = new System.Windows.Forms.CheckBox();
            this.CHK_HideModified = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // BT_OK
            // 
            this.BT_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BT_OK.Location = new System.Drawing.Point(85, 107);
            this.BT_OK.Name = "BT_OK";
            this.BT_OK.Size = new System.Drawing.Size(75, 23);
            this.BT_OK.TabIndex = 5;
            this.BT_OK.Text = "OK";
            this.BT_OK.UseVisualStyleBackColor = true;
            // 
            // BT_Cancel
            // 
            this.BT_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BT_Cancel.Location = new System.Drawing.Point(166, 107);
            this.BT_Cancel.Name = "BT_Cancel";
            this.BT_Cancel.Size = new System.Drawing.Size(75, 23);
            this.BT_Cancel.TabIndex = 6;
            this.BT_Cancel.Text = "Cancel";
            this.BT_Cancel.UseVisualStyleBackColor = true;
            // 
            // TB_Filter
            // 
            this.TB_Filter.Location = new System.Drawing.Point(12, 21);
            this.TB_Filter.Name = "TB_Filter";
            this.TB_Filter.Size = new System.Drawing.Size(307, 20);
            this.TB_Filter.TabIndex = 0;
            // 
            // CHK_HideUnmodified
            // 
            this.CHK_HideUnmodified.AutoSize = true;
            this.CHK_HideUnmodified.Location = new System.Drawing.Point(12, 47);
            this.CHK_HideUnmodified.Name = "CHK_HideUnmodified";
            this.CHK_HideUnmodified.Size = new System.Drawing.Size(102, 17);
            this.CHK_HideUnmodified.TabIndex = 1;
            this.CHK_HideUnmodified.Text = "Hide unmodified";
            this.CHK_HideUnmodified.UseVisualStyleBackColor = true;
            // 
            // CHK_HideCreated
            // 
            this.CHK_HideCreated.AutoSize = true;
            this.CHK_HideCreated.Location = new System.Drawing.Point(12, 70);
            this.CHK_HideCreated.Name = "CHK_HideCreated";
            this.CHK_HideCreated.Size = new System.Drawing.Size(87, 17);
            this.CHK_HideCreated.TabIndex = 3;
            this.CHK_HideCreated.Text = "Hide created";
            this.CHK_HideCreated.UseVisualStyleBackColor = true;
            // 
            // CHK_HideDropped
            // 
            this.CHK_HideDropped.AutoSize = true;
            this.CHK_HideDropped.Location = new System.Drawing.Point(193, 70);
            this.CHK_HideDropped.Name = "CHK_HideDropped";
            this.CHK_HideDropped.Size = new System.Drawing.Size(90, 17);
            this.CHK_HideDropped.TabIndex = 4;
            this.CHK_HideDropped.Text = "Hide dropped";
            this.CHK_HideDropped.UseVisualStyleBackColor = true;
            // 
            // CHK_HideModified
            // 
            this.CHK_HideModified.AutoSize = true;
            this.CHK_HideModified.Location = new System.Drawing.Point(193, 47);
            this.CHK_HideModified.Name = "CHK_HideModified";
            this.CHK_HideModified.Size = new System.Drawing.Size(90, 17);
            this.CHK_HideModified.TabIndex = 2;
            this.CHK_HideModified.Text = "Hide modified";
            this.CHK_HideModified.UseVisualStyleBackColor = true;
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 142);
            this.Controls.Add(this.CHK_HideModified);
            this.Controls.Add(this.CHK_HideDropped);
            this.Controls.Add(this.CHK_HideCreated);
            this.Controls.Add(this.CHK_HideUnmodified);
            this.Controls.Add(this.TB_Filter);
            this.Controls.Add(this.BT_Cancel);
            this.Controls.Add(this.BT_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FilterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FilterForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BT_OK;
        private System.Windows.Forms.Button BT_Cancel;
        private System.Windows.Forms.TextBox TB_Filter;
        private System.Windows.Forms.CheckBox CHK_HideUnmodified;
        private System.Windows.Forms.CheckBox CHK_HideCreated;
        private System.Windows.Forms.CheckBox CHK_HideDropped;
        private System.Windows.Forms.CheckBox CHK_HideModified;
    }
}