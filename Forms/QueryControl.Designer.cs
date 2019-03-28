namespace SQLServerDatabaseDiff
{
    partial class QueryControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.DatabaseList = new System.Windows.Forms.ComboBox();
            this.Button_Exec = new System.Windows.Forms.Button();
            this.Button_Connect = new System.Windows.Forms.Button();
            this.Button_Load = new System.Windows.Forms.Button();
            this.Button_Save = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Query = new System.Windows.Forms.TextBox();
            this.DataGrid = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(600, 400);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.DatabaseList);
            this.flowLayoutPanel1.Controls.Add(this.Button_Exec);
            this.flowLayoutPanel1.Controls.Add(this.Button_Connect);
            this.flowLayoutPanel1.Controls.Add(this.Button_Load);
            this.flowLayoutPanel1.Controls.Add(this.Button_Save);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(594, 29);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // DatabaseList
            // 
            this.DatabaseList.FormattingEnabled = true;
            this.DatabaseList.Location = new System.Drawing.Point(3, 3);
            this.DatabaseList.Name = "DatabaseList";
            this.DatabaseList.Size = new System.Drawing.Size(166, 21);
            this.DatabaseList.TabIndex = 0;
            this.DatabaseList.SelectedIndexChanged += new System.EventHandler(this.DatabaseList_SelectedIndexChanged);
            // 
            // Button_Exec
            // 
            this.Button_Exec.Location = new System.Drawing.Point(175, 3);
            this.Button_Exec.Name = "Button_Exec";
            this.Button_Exec.Size = new System.Drawing.Size(75, 23);
            this.Button_Exec.TabIndex = 1;
            this.Button_Exec.Text = "Exec";
            this.Button_Exec.UseVisualStyleBackColor = true;
            this.Button_Exec.Click += new System.EventHandler(this.Button_Exec_Click);
            // 
            // Button_Connect
            // 
            this.Button_Connect.Location = new System.Drawing.Point(256, 3);
            this.Button_Connect.Name = "Button_Connect";
            this.Button_Connect.Size = new System.Drawing.Size(75, 23);
            this.Button_Connect.TabIndex = 2;
            this.Button_Connect.Text = "Connection";
            this.Button_Connect.UseVisualStyleBackColor = true;
            this.Button_Connect.Click += new System.EventHandler(this.Button_Connect_Click);
            // 
            // Button_Load
            // 
            this.Button_Load.Location = new System.Drawing.Point(337, 3);
            this.Button_Load.Name = "Button_Load";
            this.Button_Load.Size = new System.Drawing.Size(75, 23);
            this.Button_Load.TabIndex = 3;
            this.Button_Load.Text = "Load...";
            this.Button_Load.UseVisualStyleBackColor = true;
            this.Button_Load.Click += new System.EventHandler(this.Button_Load_Click);
            // 
            // Button_Save
            // 
            this.Button_Save.Location = new System.Drawing.Point(418, 3);
            this.Button_Save.Name = "Button_Save";
            this.Button_Save.Size = new System.Drawing.Size(75, 23);
            this.Button_Save.TabIndex = 4;
            this.Button_Save.Text = "Save...";
            this.Button_Save.UseVisualStyleBackColor = true;
            this.Button_Save.Click += new System.EventHandler(this.Button_Save_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 38);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Query);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.DataGrid);
            this.splitContainer1.Size = new System.Drawing.Size(594, 359);
            this.splitContainer1.SplitterDistance = 192;
            this.splitContainer1.TabIndex = 4;
            // 
            // Query
            // 
            this.Query.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Query.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Query.Location = new System.Drawing.Point(0, 0);
            this.Query.Multiline = true;
            this.Query.Name = "Query";
            this.Query.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Query.Size = new System.Drawing.Size(594, 192);
            this.Query.TabIndex = 6;
            // 
            // DataGrid
            // 
            this.DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGrid.Location = new System.Drawing.Point(0, 0);
            this.DataGrid.Name = "DataGrid";
            this.DataGrid.Size = new System.Drawing.Size(594, 163);
            this.DataGrid.TabIndex = 0;
            // 
            // QueryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "QueryControl";
            this.Size = new System.Drawing.Size(600, 400);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox DatabaseList;
        private System.Windows.Forms.Button Button_Exec;
        private System.Windows.Forms.Button Button_Connect;
        private System.Windows.Forms.Button Button_Load;
        private System.Windows.Forms.Button Button_Save;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.TextBox Query;
        private System.Windows.Forms.DataGridView DataGrid;
    }
}
