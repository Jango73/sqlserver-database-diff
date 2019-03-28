namespace SQLServerDatabaseDiff
{
    partial class UpgradeOptions
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AllowTableAlter = new System.Windows.Forms.CheckBox();
            this.AllowTableDrop = new System.Windows.Forms.CheckBox();
            this.AllowTableCreate = new System.Windows.Forms.CheckBox();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.AllowFieldAlter = new System.Windows.Forms.CheckBox();
            this.AllowFieldDrop = new System.Windows.Forms.CheckBox();
            this.AllowFieldCreate = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.AllowConstraintAlter = new System.Windows.Forms.CheckBox();
            this.AllowConstraintDrop = new System.Windows.Forms.CheckBox();
            this.AllowConstraintCreate = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.AllowProcedureAlter = new System.Windows.Forms.CheckBox();
            this.AllowProcedureDrop = new System.Windows.Forms.CheckBox();
            this.AllowProcedureCreate = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.AllowViewAlter = new System.Windows.Forms.CheckBox();
            this.AllowViewDrop = new System.Windows.Forms.CheckBox();
            this.AllowViewCreate = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.AllowFunctionAlter = new System.Windows.Forms.CheckBox();
            this.AllowFunctionDrop = new System.Windows.Forms.CheckBox();
            this.AllowFunctionCreate = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AllowTableAlter);
            this.groupBox1.Controls.Add(this.AllowTableDrop);
            this.groupBox1.Controls.Add(this.AllowTableCreate);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(208, 95);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tables";
            // 
            // AllowTableAlter
            // 
            this.AllowTableAlter.AutoSize = true;
            this.AllowTableAlter.Location = new System.Drawing.Point(6, 65);
            this.AllowTableAlter.Name = "AllowTableAlter";
            this.AllowTableAlter.Size = new System.Drawing.Size(74, 17);
            this.AllowTableAlter.TabIndex = 3;
            this.AllowTableAlter.Text = "Allow alter";
            this.AllowTableAlter.UseVisualStyleBackColor = true;
            // 
            // AllowTableDrop
            // 
            this.AllowTableDrop.AutoSize = true;
            this.AllowTableDrop.Location = new System.Drawing.Point(6, 42);
            this.AllowTableDrop.Name = "AllowTableDrop";
            this.AllowTableDrop.Size = new System.Drawing.Size(75, 17);
            this.AllowTableDrop.TabIndex = 2;
            this.AllowTableDrop.Text = "Allow drop";
            this.AllowTableDrop.UseVisualStyleBackColor = true;
            // 
            // AllowTableCreate
            // 
            this.AllowTableCreate.AutoSize = true;
            this.AllowTableCreate.Location = new System.Drawing.Point(6, 19);
            this.AllowTableCreate.Name = "AllowTableCreate";
            this.AllowTableCreate.Size = new System.Drawing.Size(84, 17);
            this.AllowTableCreate.TabIndex = 1;
            this.AllowTableCreate.Text = "Allow create";
            this.AllowTableCreate.UseVisualStyleBackColor = true;
            // 
            // Button_OK
            // 
            this.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Button_OK.Location = new System.Drawing.Point(145, 315);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 24;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Cancel.Location = new System.Drawing.Point(226, 315);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 25;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.AllowFieldAlter);
            this.groupBox2.Controls.Add(this.AllowFieldDrop);
            this.groupBox2.Controls.Add(this.AllowFieldCreate);
            this.groupBox2.Location = new System.Drawing.Point(11, 113);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(208, 95);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Columns";
            // 
            // AllowFieldAlter
            // 
            this.AllowFieldAlter.AutoSize = true;
            this.AllowFieldAlter.Location = new System.Drawing.Point(6, 65);
            this.AllowFieldAlter.Name = "AllowFieldAlter";
            this.AllowFieldAlter.Size = new System.Drawing.Size(74, 17);
            this.AllowFieldAlter.TabIndex = 11;
            this.AllowFieldAlter.Text = "Allow alter";
            this.AllowFieldAlter.UseVisualStyleBackColor = true;
            // 
            // AllowFieldDrop
            // 
            this.AllowFieldDrop.AutoSize = true;
            this.AllowFieldDrop.Location = new System.Drawing.Point(6, 42);
            this.AllowFieldDrop.Name = "AllowFieldDrop";
            this.AllowFieldDrop.Size = new System.Drawing.Size(75, 17);
            this.AllowFieldDrop.TabIndex = 10;
            this.AllowFieldDrop.Text = "Allow drop";
            this.AllowFieldDrop.UseVisualStyleBackColor = true;
            // 
            // AllowFieldCreate
            // 
            this.AllowFieldCreate.AutoSize = true;
            this.AllowFieldCreate.Location = new System.Drawing.Point(6, 19);
            this.AllowFieldCreate.Name = "AllowFieldCreate";
            this.AllowFieldCreate.Size = new System.Drawing.Size(84, 17);
            this.AllowFieldCreate.TabIndex = 9;
            this.AllowFieldCreate.Text = "Allow create";
            this.AllowFieldCreate.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.AllowConstraintAlter);
            this.groupBox3.Controls.Add(this.AllowConstraintDrop);
            this.groupBox3.Controls.Add(this.AllowConstraintCreate);
            this.groupBox3.Location = new System.Drawing.Point(225, 113);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(208, 95);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Constraints";
            // 
            // AllowConstraintAlter
            // 
            this.AllowConstraintAlter.AutoSize = true;
            this.AllowConstraintAlter.Location = new System.Drawing.Point(6, 65);
            this.AllowConstraintAlter.Name = "AllowConstraintAlter";
            this.AllowConstraintAlter.Size = new System.Drawing.Size(74, 17);
            this.AllowConstraintAlter.TabIndex = 15;
            this.AllowConstraintAlter.Text = "Allow alter";
            this.AllowConstraintAlter.UseVisualStyleBackColor = true;
            // 
            // AllowConstraintDrop
            // 
            this.AllowConstraintDrop.AutoSize = true;
            this.AllowConstraintDrop.Location = new System.Drawing.Point(6, 42);
            this.AllowConstraintDrop.Name = "AllowConstraintDrop";
            this.AllowConstraintDrop.Size = new System.Drawing.Size(75, 17);
            this.AllowConstraintDrop.TabIndex = 14;
            this.AllowConstraintDrop.Text = "Allow drop";
            this.AllowConstraintDrop.UseVisualStyleBackColor = true;
            // 
            // AllowConstraintCreate
            // 
            this.AllowConstraintCreate.AutoSize = true;
            this.AllowConstraintCreate.Location = new System.Drawing.Point(6, 19);
            this.AllowConstraintCreate.Name = "AllowConstraintCreate";
            this.AllowConstraintCreate.Size = new System.Drawing.Size(84, 17);
            this.AllowConstraintCreate.TabIndex = 13;
            this.AllowConstraintCreate.Text = "Allow create";
            this.AllowConstraintCreate.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.AllowProcedureAlter);
            this.groupBox4.Controls.Add(this.AllowProcedureDrop);
            this.groupBox4.Controls.Add(this.AllowProcedureCreate);
            this.groupBox4.Location = new System.Drawing.Point(12, 214);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(208, 95);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Procedures";
            // 
            // AllowProcedureAlter
            // 
            this.AllowProcedureAlter.AutoSize = true;
            this.AllowProcedureAlter.Location = new System.Drawing.Point(6, 65);
            this.AllowProcedureAlter.Name = "AllowProcedureAlter";
            this.AllowProcedureAlter.Size = new System.Drawing.Size(74, 17);
            this.AllowProcedureAlter.TabIndex = 19;
            this.AllowProcedureAlter.Text = "Allow alter";
            this.AllowProcedureAlter.UseVisualStyleBackColor = true;
            // 
            // AllowProcedureDrop
            // 
            this.AllowProcedureDrop.AutoSize = true;
            this.AllowProcedureDrop.Location = new System.Drawing.Point(6, 42);
            this.AllowProcedureDrop.Name = "AllowProcedureDrop";
            this.AllowProcedureDrop.Size = new System.Drawing.Size(75, 17);
            this.AllowProcedureDrop.TabIndex = 18;
            this.AllowProcedureDrop.Text = "Allow drop";
            this.AllowProcedureDrop.UseVisualStyleBackColor = true;
            // 
            // AllowProcedureCreate
            // 
            this.AllowProcedureCreate.AutoSize = true;
            this.AllowProcedureCreate.Location = new System.Drawing.Point(6, 19);
            this.AllowProcedureCreate.Name = "AllowProcedureCreate";
            this.AllowProcedureCreate.Size = new System.Drawing.Size(84, 17);
            this.AllowProcedureCreate.TabIndex = 17;
            this.AllowProcedureCreate.Text = "Allow create";
            this.AllowProcedureCreate.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.AllowViewAlter);
            this.groupBox5.Controls.Add(this.AllowViewDrop);
            this.groupBox5.Controls.Add(this.AllowViewCreate);
            this.groupBox5.Location = new System.Drawing.Point(226, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(208, 95);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Views";
            // 
            // AllowViewAlter
            // 
            this.AllowViewAlter.AutoSize = true;
            this.AllowViewAlter.Location = new System.Drawing.Point(6, 65);
            this.AllowViewAlter.Name = "AllowViewAlter";
            this.AllowViewAlter.Size = new System.Drawing.Size(74, 17);
            this.AllowViewAlter.TabIndex = 7;
            this.AllowViewAlter.Text = "Allow alter";
            this.AllowViewAlter.UseVisualStyleBackColor = true;
            // 
            // AllowViewDrop
            // 
            this.AllowViewDrop.AutoSize = true;
            this.AllowViewDrop.Location = new System.Drawing.Point(6, 42);
            this.AllowViewDrop.Name = "AllowViewDrop";
            this.AllowViewDrop.Size = new System.Drawing.Size(75, 17);
            this.AllowViewDrop.TabIndex = 6;
            this.AllowViewDrop.Text = "Allow drop";
            this.AllowViewDrop.UseVisualStyleBackColor = true;
            // 
            // AllowViewCreate
            // 
            this.AllowViewCreate.AutoSize = true;
            this.AllowViewCreate.Location = new System.Drawing.Point(6, 19);
            this.AllowViewCreate.Name = "AllowViewCreate";
            this.AllowViewCreate.Size = new System.Drawing.Size(84, 17);
            this.AllowViewCreate.TabIndex = 5;
            this.AllowViewCreate.Text = "Allow create";
            this.AllowViewCreate.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.AllowFunctionAlter);
            this.groupBox6.Controls.Add(this.AllowFunctionDrop);
            this.groupBox6.Controls.Add(this.AllowFunctionCreate);
            this.groupBox6.Location = new System.Drawing.Point(226, 214);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(208, 95);
            this.groupBox6.TabIndex = 20;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Functions";
            // 
            // AllowFunctionAlter
            // 
            this.AllowFunctionAlter.AutoSize = true;
            this.AllowFunctionAlter.Location = new System.Drawing.Point(6, 65);
            this.AllowFunctionAlter.Name = "AllowFunctionAlter";
            this.AllowFunctionAlter.Size = new System.Drawing.Size(74, 17);
            this.AllowFunctionAlter.TabIndex = 23;
            this.AllowFunctionAlter.Text = "Allow alter";
            this.AllowFunctionAlter.UseVisualStyleBackColor = true;
            // 
            // AllowFunctionDrop
            // 
            this.AllowFunctionDrop.AutoSize = true;
            this.AllowFunctionDrop.Location = new System.Drawing.Point(6, 42);
            this.AllowFunctionDrop.Name = "AllowFunctionDrop";
            this.AllowFunctionDrop.Size = new System.Drawing.Size(75, 17);
            this.AllowFunctionDrop.TabIndex = 22;
            this.AllowFunctionDrop.Text = "Allow drop";
            this.AllowFunctionDrop.UseVisualStyleBackColor = true;
            // 
            // AllowFunctionCreate
            // 
            this.AllowFunctionCreate.AutoSize = true;
            this.AllowFunctionCreate.Location = new System.Drawing.Point(6, 19);
            this.AllowFunctionCreate.Name = "AllowFunctionCreate";
            this.AllowFunctionCreate.Size = new System.Drawing.Size(84, 17);
            this.AllowFunctionCreate.TabIndex = 21;
            this.AllowFunctionCreate.Text = "Allow create";
            this.AllowFunctionCreate.UseVisualStyleBackColor = true;
            // 
            // UpgradeOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 347);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "UpgradeOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upgrade options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox AllowTableAlter;
        private System.Windows.Forms.CheckBox AllowTableDrop;
        private System.Windows.Forms.CheckBox AllowTableCreate;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox AllowFieldAlter;
        private System.Windows.Forms.CheckBox AllowFieldDrop;
        private System.Windows.Forms.CheckBox AllowFieldCreate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox AllowConstraintAlter;
        private System.Windows.Forms.CheckBox AllowConstraintDrop;
        private System.Windows.Forms.CheckBox AllowConstraintCreate;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox AllowProcedureAlter;
        private System.Windows.Forms.CheckBox AllowProcedureDrop;
        private System.Windows.Forms.CheckBox AllowProcedureCreate;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox AllowViewAlter;
        private System.Windows.Forms.CheckBox AllowViewDrop;
        private System.Windows.Forms.CheckBox AllowViewCreate;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox AllowFunctionAlter;
        private System.Windows.Forms.CheckBox AllowFunctionDrop;
        private System.Windows.Forms.CheckBox AllowFunctionCreate;
    }
}