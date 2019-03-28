namespace SQLServerDatabaseDiff
{
    partial class MainForm
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
            this.MainStatus = new System.Windows.Forms.StatusStrip();
            this.MainProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.MainStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.TimeStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateUpgradeScriptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateUpgradeScriptsForBAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runUpgradeFromABToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runUpgradeFromBAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newQueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainTab = new System.Windows.Forms.TabControl();
            this.MainTabPage1 = new System.Windows.Forms.TabPage();
            this.MainSplit = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label9 = new System.Windows.Forms.Label();
            this.HalfSplit1 = new System.Windows.Forms.SplitContainer();
            this.Connection1 = new SQLServerDatabaseDiff.ConnectionControl();
            this.TV_Tree1 = new SQLServerDatabaseDiff.SubTreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label10 = new System.Windows.Forms.Label();
            this.HalfSplit2 = new System.Windows.Forms.SplitContainer();
            this.Connection2 = new SQLServerDatabaseDiff.ConnectionControl();
            this.TV_Tree2 = new SQLServerDatabaseDiff.SubTreeView();
            this.MainTabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_BackupPath = new System.Windows.Forms.TextBox();
            this.BT_BrowseBackupPath = new System.Windows.Forms.Button();
            this.BT_BackupConnection = new System.Windows.Forms.Button();
            this.BT_BackupRefresh = new System.Windows.Forms.Button();
            this.TV_Backup = new System.Windows.Forms.TreeView();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.MainStatus.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.MainTab.SuspendLayout();
            this.MainTabPage1.SuspendLayout();
            this.MainSplit.Panel1.SuspendLayout();
            this.MainSplit.Panel2.SuspendLayout();
            this.MainSplit.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.HalfSplit1.Panel1.SuspendLayout();
            this.HalfSplit1.Panel2.SuspendLayout();
            this.HalfSplit1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.HalfSplit2.Panel1.SuspendLayout();
            this.HalfSplit2.Panel2.SuspendLayout();
            this.HalfSplit2.SuspendLayout();
            this.MainTabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainStatus
            // 
            this.MainStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainProgress,
            this.MainStatusText,
            this.TimeStatusText});
            this.MainStatus.Location = new System.Drawing.Point(0, 544);
            this.MainStatus.Name = "MainStatus";
            this.MainStatus.Size = new System.Drawing.Size(792, 22);
            this.MainStatus.TabIndex = 0;
            this.MainStatus.Text = "MainStatus";
            // 
            // MainProgress
            // 
            this.MainProgress.Name = "MainProgress";
            this.MainProgress.Size = new System.Drawing.Size(100, 16);
            // 
            // MainStatusText
            // 
            this.MainStatusText.AutoSize = false;
            this.MainStatusText.Name = "MainStatusText";
            this.MainStatusText.Size = new System.Drawing.Size(300, 17);
            this.MainStatusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TimeStatusText
            // 
            this.TimeStatusText.AutoSize = false;
            this.TimeStatusText.Name = "TimeStatusText";
            this.TimeStatusText.Size = new System.Drawing.Size(150, 17);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mergeToolStripMenuItem,
            this.queriesToolStripMenuItem,
            this.toolStripMenuItem1});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(792, 24);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.optionsToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.loadToolStripMenuItem.Text = "Load...";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.saveToolStripMenuItem.Text = "Save...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(122, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.optionsToolStripMenuItem.Text = "Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.Options_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // mergeToolStripMenuItem
            // 
            this.mergeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.analyzeToolStripMenuItem,
            this.generateReportToolStripMenuItem,
            this.generateUpgradeScriptsToolStripMenuItem,
            this.generateUpgradeScriptsForBAToolStripMenuItem,
            this.runUpgradeFromABToolStripMenuItem,
            this.runUpgradeFromBAToolStripMenuItem});
            this.mergeToolStripMenuItem.Name = "mergeToolStripMenuItem";
            this.mergeToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.mergeToolStripMenuItem.Text = "Database";
            // 
            // analyzeToolStripMenuItem
            // 
            this.analyzeToolStripMenuItem.Name = "analyzeToolStripMenuItem";
            this.analyzeToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.analyzeToolStripMenuItem.Text = "Compare...";
            this.analyzeToolStripMenuItem.Click += new System.EventHandler(this.Analyze_Click);
            // 
            // generateReportToolStripMenuItem
            // 
            this.generateReportToolStripMenuItem.Name = "generateReportToolStripMenuItem";
            this.generateReportToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.generateReportToolStripMenuItem.Text = "Generate report...";
            this.generateReportToolStripMenuItem.Click += new System.EventHandler(this.GenReport_Click);
            // 
            // generateUpgradeScriptsToolStripMenuItem
            // 
            this.generateUpgradeScriptsToolStripMenuItem.Name = "generateUpgradeScriptsToolStripMenuItem";
            this.generateUpgradeScriptsToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.generateUpgradeScriptsToolStripMenuItem.Text = "Generate upgrade scripts for A -> B...";
            this.generateUpgradeScriptsToolStripMenuItem.Click += new System.EventHandler(this.GenerateScripts_AB_Click);
            // 
            // generateUpgradeScriptsForBAToolStripMenuItem
            // 
            this.generateUpgradeScriptsForBAToolStripMenuItem.Name = "generateUpgradeScriptsForBAToolStripMenuItem";
            this.generateUpgradeScriptsForBAToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.generateUpgradeScriptsForBAToolStripMenuItem.Text = "Generate upgrade scripts for B -> A...";
            this.generateUpgradeScriptsForBAToolStripMenuItem.Click += new System.EventHandler(this.GenerateScripts_BA_Click);
            // 
            // runUpgradeFromABToolStripMenuItem
            // 
            this.runUpgradeFromABToolStripMenuItem.Name = "runUpgradeFromABToolStripMenuItem";
            this.runUpgradeFromABToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.runUpgradeFromABToolStripMenuItem.Text = "Run upgrade from A -> B";
            this.runUpgradeFromABToolStripMenuItem.Click += new System.EventHandler(this.RunScripts_AB_Click);
            // 
            // runUpgradeFromBAToolStripMenuItem
            // 
            this.runUpgradeFromBAToolStripMenuItem.Name = "runUpgradeFromBAToolStripMenuItem";
            this.runUpgradeFromBAToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.runUpgradeFromBAToolStripMenuItem.Text = "Run upgrade from B -> A";
            this.runUpgradeFromBAToolStripMenuItem.Click += new System.EventHandler(this.RunScripts_BA_Click);
            // 
            // queriesToolStripMenuItem
            // 
            this.queriesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newQueryToolStripMenuItem});
            this.queriesToolStripMenuItem.Name = "queriesToolStripMenuItem";
            this.queriesToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.queriesToolStripMenuItem.Text = "Queries";
            // 
            // newQueryToolStripMenuItem
            // 
            this.newQueryToolStripMenuItem.Name = "newQueryToolStripMenuItem";
            this.newQueryToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.newQueryToolStripMenuItem.Text = "New query";
            this.newQueryToolStripMenuItem.Click += new System.EventHandler(this.NewQuery_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItem1.Text = "?";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.About_Click);
            // 
            // MainTab
            // 
            this.MainTab.Controls.Add(this.MainTabPage1);
            this.MainTab.Controls.Add(this.MainTabPage2);
            this.MainTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTab.Location = new System.Drawing.Point(0, 24);
            this.MainTab.Name = "MainTab";
            this.MainTab.SelectedIndex = 0;
            this.MainTab.Size = new System.Drawing.Size(792, 520);
            this.MainTab.TabIndex = 2;
            // 
            // MainTabPage1
            // 
            this.MainTabPage1.Controls.Add(this.MainSplit);
            this.MainTabPage1.Location = new System.Drawing.Point(4, 22);
            this.MainTabPage1.Name = "MainTabPage1";
            this.MainTabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.MainTabPage1.Size = new System.Drawing.Size(784, 494);
            this.MainTabPage1.TabIndex = 0;
            this.MainTabPage1.Text = "Compare";
            this.MainTabPage1.UseVisualStyleBackColor = true;
            // 
            // MainSplit
            // 
            this.MainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplit.Location = new System.Drawing.Point(3, 3);
            this.MainSplit.Name = "MainSplit";
            // 
            // MainSplit.Panel1
            // 
            this.MainSplit.Panel1.Controls.Add(this.splitContainer1);
            // 
            // MainSplit.Panel2
            // 
            this.MainSplit.Panel2.Controls.Add(this.splitContainer2);
            this.MainSplit.Size = new System.Drawing.Size(778, 488);
            this.MainSplit.SplitterDistance = 388;
            this.MainSplit.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.HalfSplit1);
            this.splitContainer1.Size = new System.Drawing.Size(388, 488);
            this.splitContainer1.SplitterDistance = 31;
            this.splitContainer1.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 16);
            this.label9.TabIndex = 0;
            this.label9.Text = "Database A";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HalfSplit1
            // 
            this.HalfSplit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HalfSplit1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.HalfSplit1.IsSplitterFixed = true;
            this.HalfSplit1.Location = new System.Drawing.Point(0, 0);
            this.HalfSplit1.Name = "HalfSplit1";
            this.HalfSplit1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // HalfSplit1.Panel1
            // 
            this.HalfSplit1.Panel1.Controls.Add(this.Connection1);
            // 
            // HalfSplit1.Panel2
            // 
            this.HalfSplit1.Panel2.Controls.Add(this.TV_Tree1);
            this.HalfSplit1.Size = new System.Drawing.Size(388, 453);
            this.HalfSplit1.SplitterDistance = 120;
            this.HalfSplit1.TabIndex = 1;
            // 
            // Connection1
            // 
            this.Connection1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Connection1.Location = new System.Drawing.Point(0, 0);
            this.Connection1.Name = "Connection1";
            this.Connection1.Size = new System.Drawing.Size(388, 120);
            this.Connection1.TabIndex = 0;
            // 
            // TV_Tree1
            // 
            this.TV_Tree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TV_Tree1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TV_Tree1.Location = new System.Drawing.Point(0, 0);
            this.TV_Tree1.Name = "TV_Tree1";
            this.TV_Tree1.Size = new System.Drawing.Size(388, 329);
            this.TV_Tree1.TabIndex = 0;
            this.TV_Tree1.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterCollapse);
            this.TV_Tree1.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterExpand);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label10);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.HalfSplit2);
            this.splitContainer2.Size = new System.Drawing.Size(386, 488);
            this.splitContainer2.SplitterDistance = 31;
            this.splitContainer2.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 16);
            this.label10.TabIndex = 1;
            this.label10.Text = "Database B";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HalfSplit2
            // 
            this.HalfSplit2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HalfSplit2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.HalfSplit2.IsSplitterFixed = true;
            this.HalfSplit2.Location = new System.Drawing.Point(0, 0);
            this.HalfSplit2.Name = "HalfSplit2";
            this.HalfSplit2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // HalfSplit2.Panel1
            // 
            this.HalfSplit2.Panel1.Controls.Add(this.Connection2);
            // 
            // HalfSplit2.Panel2
            // 
            this.HalfSplit2.Panel2.Controls.Add(this.TV_Tree2);
            this.HalfSplit2.Size = new System.Drawing.Size(386, 453);
            this.HalfSplit2.SplitterDistance = 120;
            this.HalfSplit2.TabIndex = 2;
            // 
            // Connection2
            // 
            this.Connection2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Connection2.Location = new System.Drawing.Point(0, 0);
            this.Connection2.Name = "Connection2";
            this.Connection2.Size = new System.Drawing.Size(386, 120);
            this.Connection2.TabIndex = 0;
            // 
            // TV_Tree2
            // 
            this.TV_Tree2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TV_Tree2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TV_Tree2.Location = new System.Drawing.Point(0, 0);
            this.TV_Tree2.Name = "TV_Tree2";
            this.TV_Tree2.Size = new System.Drawing.Size(386, 329);
            this.TV_Tree2.TabIndex = 0;
            this.TV_Tree2.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterCollapse);
            this.TV_Tree2.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterExpand);
            // 
            // MainTabPage2
            // 
            this.MainTabPage2.Controls.Add(this.tableLayoutPanel1);
            this.MainTabPage2.Location = new System.Drawing.Point(4, 22);
            this.MainTabPage2.Name = "MainTabPage2";
            this.MainTabPage2.Size = new System.Drawing.Size(784, 494);
            this.MainTabPage2.TabIndex = 1;
            this.MainTabPage2.Text = "Backups";
            this.MainTabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.TV_Backup, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(192, 74);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.TB_BackupPath);
            this.flowLayoutPanel1.Controls.Add(this.BT_BrowseBackupPath);
            this.flowLayoutPanel1.Controls.Add(this.BT_BackupConnection);
            this.flowLayoutPanel1.Controls.Add(this.BT_BackupRefresh);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(186, 29);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Backup path";
            // 
            // TB_BackupPath
            // 
            this.TB_BackupPath.Location = new System.Drawing.Point(3, 16);
            this.TB_BackupPath.Name = "TB_BackupPath";
            this.TB_BackupPath.Size = new System.Drawing.Size(300, 20);
            this.TB_BackupPath.TabIndex = 2;
            // 
            // BT_BrowseBackupPath
            // 
            this.BT_BrowseBackupPath.Location = new System.Drawing.Point(3, 42);
            this.BT_BrowseBackupPath.Name = "BT_BrowseBackupPath";
            this.BT_BrowseBackupPath.Size = new System.Drawing.Size(75, 23);
            this.BT_BrowseBackupPath.TabIndex = 3;
            this.BT_BrowseBackupPath.Text = "Browse...";
            this.BT_BrowseBackupPath.UseVisualStyleBackColor = true;
            this.BT_BrowseBackupPath.Click += new System.EventHandler(this.BT_BrowseBackupPath_Click);
            // 
            // BT_BackupConnection
            // 
            this.BT_BackupConnection.Location = new System.Drawing.Point(84, 42);
            this.BT_BackupConnection.Name = "BT_BackupConnection";
            this.BT_BackupConnection.Size = new System.Drawing.Size(75, 23);
            this.BT_BackupConnection.TabIndex = 4;
            this.BT_BackupConnection.Text = "Connection...";
            this.BT_BackupConnection.UseVisualStyleBackColor = true;
            this.BT_BackupConnection.Click += new System.EventHandler(this.BT_BackupConnection_Click);
            // 
            // BT_BackupRefresh
            // 
            this.BT_BackupRefresh.Location = new System.Drawing.Point(3, 71);
            this.BT_BackupRefresh.Name = "BT_BackupRefresh";
            this.BT_BackupRefresh.Size = new System.Drawing.Size(75, 23);
            this.BT_BackupRefresh.TabIndex = 5;
            this.BT_BackupRefresh.Text = "Refresh";
            this.BT_BackupRefresh.UseVisualStyleBackColor = true;
            this.BT_BackupRefresh.Click += new System.EventHandler(this.BT_BackupRefresh_Click);
            // 
            // TV_Backup
            // 
            this.TV_Backup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TV_Backup.Location = new System.Drawing.Point(3, 38);
            this.TV_Backup.Name = "TV_Backup";
            this.TV_Backup.Size = new System.Drawing.Size(186, 33);
            this.TV_Backup.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.MainTab);
            this.Controls.Add(this.MainStatus);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL Server Database Diff";
            this.MainStatus.ResumeLayout(false);
            this.MainStatus.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.MainTab.ResumeLayout(false);
            this.MainTabPage1.ResumeLayout(false);
            this.MainSplit.Panel1.ResumeLayout(false);
            this.MainSplit.Panel2.ResumeLayout(false);
            this.MainSplit.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.HalfSplit1.Panel1.ResumeLayout(false);
            this.HalfSplit1.Panel2.ResumeLayout(false);
            this.HalfSplit1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.HalfSplit2.Panel1.ResumeLayout(false);
            this.HalfSplit2.Panel2.ResumeLayout(false);
            this.HalfSplit2.ResumeLayout(false);
            this.MainTabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip MainStatus;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analyzeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateUpgradeScriptsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateUpgradeScriptsForBAToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar MainProgress;
        private System.Windows.Forms.ToolStripStatusLabel MainStatusText;
        private System.Windows.Forms.ToolStripMenuItem generateReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runUpgradeFromABToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runUpgradeFromBAToolStripMenuItem;
        private System.Windows.Forms.TabControl MainTab;
        private System.Windows.Forms.TabPage MainTabPage1;
        private System.Windows.Forms.SplitContainer MainSplit;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.SplitContainer HalfSplit1;
        private SubTreeView TV_Tree1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.SplitContainer HalfSplit2;
        private SubTreeView TV_Tree2;
        private System.Windows.Forms.ToolStripMenuItem queriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newQueryToolStripMenuItem;
        private ConnectionControl Connection1;
        private ConnectionControl Connection2;
        private System.Windows.Forms.TabPage MainTabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_BackupPath;
        private System.Windows.Forms.Button BT_BrowseBackupPath;
        private System.Windows.Forms.Button BT_BackupConnection;
        private System.Windows.Forms.TreeView TV_Backup;
        private System.Windows.Forms.Button BT_BackupRefresh;
        private System.Windows.Forms.ToolStripStatusLabel TimeStatusText;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

