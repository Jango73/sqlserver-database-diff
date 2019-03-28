
using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SQLServerDatabaseDiff
{
    public partial class MainForm : Form
    {
        #region Fields

        private Base[] m_Bases;
        private Configuration m_Configuration;
        private String AppPath = String.Empty;
        private static String m_Version = "1.1";
        private ConnectionAdapter m_BackupConnection;
        private BaseAnalyzer m_Analyzer;
        private Timer m_Timer;

        public const int Base1 = 0;
        public const int Base2 = 1;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            AppPath = Path.GetDirectoryName(Application.ExecutablePath);

            //----------------------------------------------------------------
            // Read config or create default one

            try
            {
                m_Configuration = Configuration.Deserialize(AppPath + "\\config.xml");
            }
            catch (Exception)
            {
                m_Configuration = new Configuration();
                m_Configuration.BackupPath = AppPath + "\\Backups";
            }

            initUIInfo();

            //----------------------------------------------------------------
            // Create databases

            m_Bases = new Base[2];

            m_Bases[Base1] = new Base();
            m_Bases[Base2] = new Base();

            m_Bases[Base1].Twin = m_Bases[Base2];
            m_Bases[Base2].Twin = m_Bases[Base1];

            m_BackupConnection = new ConnectionAdapter(m_Configuration.Connection3);

            grabUIInfo();

            // Init images for trees

            ImageList img = new ImageList();

            img.Images.Add(MainResources.Icon_None, Color.White);
            img.Images.Add(MainResources.Icon_Same, Color.White);
            img.Images.Add(MainResources.Icon_Create, Color.White);
            img.Images.Add(MainResources.Icon_Modified, Color.White);
            img.Images.Add(MainResources.Icon_Deleted, Color.White);

            TV_Tree1.ParentForm = this;
            TV_Tree2.ParentForm = this;
            TV_Tree1.ImageList = img;
            TV_Tree2.ImageList = img;

            //----------------------------------------------------------------
            // Init backup data

            // refreshBackupData();
            createBackupContextMenu(TV_Backup);

            //----------------------------------------------------------------
            // Add events to select node on item right click

            TV_Tree1.MouseDown += new MouseEventHandler(TreeView_MouseDown);
            TV_Tree2.MouseDown += new MouseEventHandler(TreeView_MouseDown);
            TV_Backup.MouseDown += new MouseEventHandler(TreeViewBackup_MouseDown);

            m_Timer = new Timer();
            m_Timer.Interval = 500;
            m_Timer.Tick += new EventHandler(m_Timer_Tick);
            m_Timer.Start();
        }

        #endregion

        #region Properties

        public Base[] Bases
        {
            get { return m_Bases; }
            set { m_Bases = value; }
        }

        public Configuration Configuration
        {
            get { return m_Configuration; }
            set { m_Configuration = value; }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Déclenche l'événement <see cref="E:System.Windows.Forms.Form.FormClosed"></see>.
        /// </summary>
        /// <param name="e"><see cref="T:System.Windows.Forms.FormClosedEventArgs"></see> qui contient les données d'événement.</param>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (m_Analyzer != null) m_Analyzer.Abort();

            grabUIInfo();

            Configuration.Serialize(AppPath + "\\config.xml", m_Configuration);

            base.OnFormClosed(e);
        }

        #endregion

        #region MenuEvents

        /// <summary>
        /// Handles the Click event of the loadToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDialog = new OpenFileDialog();

            OpenDialog.Filter = "XML file (*.xml)|*.xml";

            if (OpenDialog.ShowDialog() == DialogResult.OK)
            {
                OpenFile(OpenDialog.FileName);
            }
        }

        /// <summary>
        /// Handles the Click event of the saveToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveDialog = new SaveFileDialog();

            SaveDialog.Filter = "XML file (*.xml)|*.xml";

            if (SaveDialog.ShowDialog() == DialogResult.OK)
            {
                SaveFile(SaveDialog.FileName);
            }
        }

        /// <summary>
        /// Handles the MouseDown event of the TreeView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void TreeView_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeView tree = (TreeView)sender;
                tree.SelectedNode = tree.GetNodeAt(e.X, e.Y);
            }
        }

        /// <summary>
        /// Handles the AfterExpand event of the TreeView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewEventArgs"/> instance containing the event data.</param>
        private void TreeView_AfterExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            TreeView Tree1 = (TreeView)sender;
            TreeView Tree2 = ((BaseTreeData)Tree1.Tag).m_Twin;

            if (Tree1 != null && Tree2 != null)
            {
                TreeNode Node2 = FindTwinNode(Tree2.Nodes, e.Node);
                if (Node2 != null) Node2.Expand();
            }
        }

        /// <summary>
        /// Handles the AfterCollapse event of the TreeView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewEventArgs"/> instance containing the event data.</param>
        private void TreeView_AfterCollapse(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            TreeView Tree1 = (TreeView)sender;
            TreeView Tree2 = ((BaseTreeData)Tree1.Tag).m_Twin;
            if (Tree1 != null && Tree2 != null)
            {
                TreeNode Node1 = e.Node;
                TreeNode Node2 = FindTwinNode(Tree2.Nodes, e.Node);
                if (Node2 != null) Node2.Collapse();
            }
        }

        /// <summary>
        /// Scrolleds the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        public void Scrolled(object sender)
        {
            TreeView Tree1 = (TreeView)sender;
            if (Tree1.Tag == null) return;

            TreeView Tree2 = ((BaseTreeData)Tree1.Tag).m_Twin;
            if (Tree2 == null) return;

            TreeNode TopNode1 = Tree1.TopNode;
            if (TopNode1 != null)
            {
                if (TopNode1 == Tree1.Nodes[0])
                {
                    Tree2.TopNode = Tree2.Nodes[0];
                    return;
                }
                TreeNode TopNode2 = FindTwinNode(Tree2.Nodes, TopNode1);
                if (TopNode2 != null)
                {
                    Tree2.TopNode = TopNode2;
                }
            }
        }

        /// <summary>
        /// Finds the twin node.
        /// </summary>
        /// <param name="Col">The col.</param>
        /// <param name="Node">The node.</param>
        /// <returns></returns>
        private TreeNode FindTwinNode(TreeNodeCollection TestCollection, TreeNode Node)
        {
            foreach (TreeNode TestNode in TestCollection)
            {
                if (AreNodesTwins(TestNode, Node) == true) return TestNode;
                TreeNode Ret = FindTwinNode(TestNode.Nodes, Node);
                if (Ret != null) return Ret;
            }
            return null;
        }

        /// <summary>
        /// Ares the nodes twins.
        /// </summary>
        /// <param name="Node1">The node1.</param>
        /// <param name="Node2">The node2.</param>
        /// <returns></returns>
        private Boolean AreNodesTwins(TreeNode Node1, TreeNode Node2)
        {
            if (Node1.Tag == null || Node2.Tag == null) return false;

            Type Type1 = Node1.Tag.GetType();
            Type Type2 = Node2.Tag.GetType();

            if (Type1 != Type2) return false;

            return Node1.Tag.Equals(Node2.Tag);
        }

        /// <summary>
        /// Handles the MouseDown event of the TreeViewBackup control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void TreeViewBackup_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeView tree = (TreeView)sender;
                tree.SelectedNode = tree.GetNodeAt(e.X, e.Y);
            }
        }

        /// <summary>
        /// Handles the Click event of the Analyze control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Analyze_Click(object sender, EventArgs e)
        {
            if (new AnalyzeOptions(m_Configuration).ShowDialog() == DialogResult.OK)
            {
                m_Bases[Base1] = new Base();
                m_Bases[Base2] = new Base();

                m_Bases[Base1].Twin = m_Bases[Base2];
                m_Bases[Base2].Twin = m_Bases[Base1];

                TV_Tree1.Nodes.Clear();
                TV_Tree2.Nodes.Clear();

                grabUIInfo();

                if (m_Analyzer != null) m_Analyzer.Abort();

                m_Analyzer = new BaseAnalyzer(this);
                m_Analyzer.Run();
            }
        }

        /// <summary>
        /// Handles the Tick event of the m_Timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void m_Timer_Tick(object sender, EventArgs e)
        {
            if (m_Analyzer != null)
            {
                m_Analyzer.ComputeTiming();

                MainStatusText.Text = m_Analyzer.Status;
                MainProgress.Maximum = m_Analyzer.Progress_Max;
                MainProgress.Value = m_Analyzer.Progress_Current;

                TimeStatusText.Text = m_Analyzer.RemainTime.ToLongTimeString();

                if (m_Analyzer.Finished)
                {
                    m_Analyzer = null;
                    BuildTrees();
                }
            }
        }

        /// <summary>
        /// Create the tree view context menu
        /// </summary>
        private void createBaseContextMenu(Base bse, TreeView tree)
        {
            ContextMenu ContextMenu = new System.Windows.Forms.ContextMenu();
            MenuItem ContextMenuItem1 = new System.Windows.Forms.MenuItem();
            MenuItem ContextMenuItem2 = new System.Windows.Forms.MenuItem();
            MenuItem ContextMenuItem3 = new System.Windows.Forms.MenuItem();
            MenuItem ContextMenuItem4 = new System.Windows.Forms.MenuItem();

            ContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { ContextMenuItem1, ContextMenuItem2, ContextMenuItem3, ContextMenuItem4 });

            ContextMenuItem1.Index = 0;
            ContextMenuItem1.Text = "Filter...";
            ContextMenuItem1.Click += new System.EventHandler(this.TreeView_Filter_Click);

            ContextMenuItem2.Index = 1;
            ContextMenuItem2.Text = "View...";
            ContextMenuItem2.Click += new System.EventHandler(this.TreeView_View_Click);

            ContextMenuItem3.Index = 2;
            ContextMenuItem3.Text = "Export data...";
            ContextMenuItem3.Click += new System.EventHandler(this.TreeView_ExportData_Click);

            ContextMenuItem4.Index = 3;
            ContextMenuItem4.Text = "Recompare...";
            ContextMenuItem4.Click += new System.EventHandler(this.TreeView_Recompare_Click);

            ContextMenu.Popup += new EventHandler(TreeContextMenu_Popup);
            ContextMenu.Tag = tree;
            tree.ContextMenu = ContextMenu;
        }

        /// <summary>
        /// Handles the Click event of the TreeView_Recompare control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void TreeView_Recompare_Click(object sender, EventArgs e)
        {
            MenuItem menu = (MenuItem)sender;

            TreeView Tree = (TreeView)((MenuItem)sender).Parent.Tag;

            Base TargetBase = ((BaseTreeData)Tree.Tag).m_Base;

            if (Tree.SelectedNode != null)
            {
                object target = (object)Tree.SelectedNode.Tag;

                if (target is ContainerBase)
                {
                }
                else if
                (
                    target is Procedure ||
                    target is Function ||
                    target is View ||
                    target is Table
                )
                {
                    RecompareNodes(Tree, TargetBase, (Qualifiable)target);
                }
            }
        }

        /// <summary>
        /// Recompares the nodes.
        /// </summary>
        /// <param name="Tree">The tree.</param>
        /// <param name="Obj">The obj.</param>
        private void RecompareNodes(TreeView Tree, Base TargetBase, Qualifiable Obj)
        {
            if (Obj.IsGhost == false && Obj.Twin != null && Obj.Twin.IsGhost == false)
            {
                if (Obj is Table)
                {
                    TargetBase.GetTableData((Table)Obj, m_Configuration);
                    ((Base)TargetBase.Twin).GetTableData((Table)Obj.Twin, m_Configuration);
                    ((Table)Obj).qualifyVersus((Table)Obj.Twin, Direction.Out);
                    ((Table)Obj.Twin).qualifyVersus((Table)Obj, Direction.In);
                }
                else if (Obj is View)
                {
                    TargetBase.GetViewData((View)Obj, m_Configuration);
                    ((Base)TargetBase.Twin).GetViewData((View)Obj.Twin, m_Configuration);
                    ((View)Obj).qualifyVersus((View)Obj.Twin, Direction.Out);
                    ((View)Obj.Twin).qualifyVersus((View)Obj, Direction.In);
                }
                else if (Obj is Procedure)
                {
                    ((Procedure)Obj).Checksum = 0;
                    ((Procedure)Obj.Twin).Checksum = 0;
                    ((Procedure)Obj).qualifyVersus((Procedure)Obj.Twin, Direction.Out);
                    ((Procedure)Obj.Twin).qualifyVersus((Procedure)Obj, Direction.In);
                }
                else if (Obj is Function)
                {
                    ((Function)Obj).Checksum = 0;
                    ((Function)Obj.Twin).Checksum = 0;
                    ((Function)Obj).qualifyVersus((Function)Obj.Twin, Direction.Out);
                    ((Function)Obj.Twin).qualifyVersus((Function)Obj, Direction.In);
                }

                TreeView Tree2 = ((BaseTreeData)Tree.Tag).m_Twin;

                if (Tree2 != null)
                {
                    TreeNode Node2 = FindTwinNode(Tree2.Nodes, Tree.SelectedNode);

                    if (Node2 != null)
                    {
                        ReplaceTreeNode(Tree2, Node2, Obj.Twin.toTreeNode());
                    }
                }

                ReplaceTreeNode(Tree, Tree.SelectedNode, Obj.toTreeNode());
            }
        }

        /// <summary>
        /// Replaces the tree node.
        /// </summary>
        /// <param name="Tree">The tree.</param>
        /// <param name="OriginalNode">The original node.</param>
        /// <param name="NewNode">The new node.</param>
        private void ReplaceTreeNode(TreeView Tree, TreeNode OriginalNode, TreeNode NewNode)
        {
            TreeNode ParentNode = OriginalNode.Parent;
            TreeNode PreviousNode = OriginalNode.PrevNode;

            ParentNode.Nodes.Remove(OriginalNode);

            if (PreviousNode != null)
            {
                ParentNode.Nodes.Add(NewNode);
            }

            TreeView Tree2 = ((BaseTreeData)Tree.Tag).m_Twin;

            if (Tree2 != null)
            {
                TreeNode Node2 = FindTwinNode(Tree2.Nodes, NewNode);

                if (Node2 != null)
                {
                    Node2.Collapse();
                }
            }
        }

        /// <summary>
        /// Handles the Popup event of the TreeContextMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void TreeContextMenu_Popup(object sender, EventArgs e)
        {
            ContextMenu menu = (ContextMenu)sender;

            if (menu.Tag is TreeView)
            {
                TreeView tree = (TreeView)menu.Tag;

                if (tree.SelectedNode != null)
                {
                    object target = (object)tree.SelectedNode.Tag;

                    menu.MenuItems[0].Enabled = false;
                    menu.MenuItems[1].Enabled = false;
                    menu.MenuItems[2].Enabled = false;
                    menu.MenuItems[3].Enabled = false;

                    if (target is ContainerBase)
                    {
                        menu.MenuItems[0].Enabled = true;
                    }

                    if (target is Procedure || target is Function)
                    {
                        menu.MenuItems[1].Enabled = true;
                        menu.MenuItems[3].Enabled = true;
                    }

                    if (target is Table)
                    {
                        menu.MenuItems[2].Enabled = true;
                        menu.MenuItems[3].Enabled = true;
                    }

                    if (target is View)
                    {
                        menu.MenuItems[3].Enabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// Edits the filter for the given node
        /// </summary>
        void TreeView_Filter_Click(object sender, EventArgs e)
        {
            TreeView tree = (TreeView)((MenuItem)sender).Parent.Tag;
            Base b = ((BaseTreeData)tree.Tag).m_Base;

            if (tree.SelectedNode != null)
            {
                object target = (object)tree.SelectedNode.Tag;

                if (target is ContainerBase)
                {
                    ContainerBase cont = (ContainerBase)target;

                    Filter res = cont.Filter;

                    if (new FilterForm(res).ShowDialog() == DialogResult.OK)
                    {
                        TreeNode topNode = tree.TopNode;
                        Boolean isExpanded = tree.SelectedNode.IsExpanded;
                        TreeNode new_node = cont.toTreeNode();
                        TreeNode parent = tree.SelectedNode.Parent;

                        parent.Nodes.Remove(tree.SelectedNode);
                        parent.Nodes.Add(new_node);

                        if (isExpanded)
                        {
                            new_node.Expand();
                        }
                        tree.TopNode = topNode;
                    }
                }
            }
        }

        /// <summary>
        /// Opens a text editor if item is viewable
        /// </summary>
        void TreeView_View_Click(object sender, EventArgs e)
        {
            TreeView tree = (TreeView)((MenuItem)sender).Parent.Tag;
            Base b = ((BaseTreeData)tree.Tag).m_Base;

            if (tree.SelectedNode != null)
            {
                object target = (object)tree.SelectedNode.Tag;

                if (target is Procedure)
                {
                    QueryForm frm = new QueryForm(b.Connection, ((Procedure)target).getDefinition());
                    frm.Show();
                }
                else if (target is Function)
                {
                    QueryForm frm = new QueryForm(b.Connection, ((Function)target).getDefinition());
                    frm.Show();
                }
                else if (target is View)
                {
                    QueryForm frm = new QueryForm(b.Connection, ((View)target).getDefinition());
                    frm.Show();
                }
            }
        }

        /// <summary>
        /// Opens a text editor if item is viewable
        /// </summary>
        void TreeView_ExportData_Click(object sender, EventArgs e)
        {
            TreeView tree = (TreeView)((MenuItem)sender).Parent.Tag;
            Base b = ((BaseTreeData)tree.Tag).m_Base;

            if (tree.SelectedNode != null)
            {
                object target = (object)tree.SelectedNode.Tag;

                if (target is Table)
                {
                    exportData(b, (Table)target);
                }
            }
        }

        /// <summary>
        /// Create the backup tree view context menu
        /// </summary>
        private void createBackupContextMenu(TreeView tree)
        {
            ContextMenu ContextMenu = new System.Windows.Forms.ContextMenu();
            MenuItem ContextMenuItem1 = new System.Windows.Forms.MenuItem();
            MenuItem ContextMenuItem2 = new System.Windows.Forms.MenuItem();
            MenuItem ContextMenuItem3 = new System.Windows.Forms.MenuItem();

            ContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { ContextMenuItem1, ContextMenuItem2, ContextMenuItem3 });

            ContextMenuItem1.Index = 0;
            ContextMenuItem1.Text = "Backup...";
            ContextMenuItem1.Click += new System.EventHandler(this.BackupTreeView_Backup_Click);

            ContextMenuItem2.Index = 1;
            ContextMenuItem2.Text = "Restore...";
            ContextMenuItem2.Click += new System.EventHandler(this.BackupTreeView_Restore_Click);

            ContextMenuItem3.Index = 2;
            ContextMenuItem3.Text = "Delete...";
            ContextMenuItem3.Click += new System.EventHandler(this.BackupTreeView_Delete_Click);

            ContextMenu.Popup += new EventHandler(BackupTreeContextMenu_Popup);
            ContextMenu.Tag = tree;
            tree.ContextMenu = ContextMenu;
        }

        /// <summary>
        /// Handles the Popup event of the BackupTreeContextMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void BackupTreeContextMenu_Popup(object sender, EventArgs e)
        {
            ContextMenu menu = (ContextMenu)sender;
            if (menu.Tag is TreeView)
            {
                TreeView tree = (TreeView)menu.Tag;

                if (tree.SelectedNode != null)
                {
                    object target = (object)tree.SelectedNode.Tag;

                    menu.MenuItems[0].Enabled = false;
                    menu.MenuItems[1].Enabled = false;
                    menu.MenuItems[2].Enabled = false;

                    if (target is FileInfo)
                    {
                        menu.MenuItems[1].Enabled = true;
                        menu.MenuItems[2].Enabled = true;
                    }
                    else
                    {
                        menu.MenuItems[0].Enabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// Backup the selected database
        /// </summary>
        void BackupTreeView_Backup_Click(object sender, EventArgs e)
        {
            TreeView tree = (TreeView)((MenuItem)sender).Parent.Tag;

            if (tree.SelectedNode != null)
            {
                object target = (object)tree.SelectedNode.Tag;

                if (target is FileInfo)
                {
                }
                else
                {
                    // Backup the database

                    String Path = TB_BackupPath.Text + "\\" + tree.SelectedNode.Text;
                    String FileName = findUniqueFileName(Path, tree.SelectedNode.Text, ".bak");

                    doBackup(Path + "\\" + FileName, tree.SelectedNode.Text);
                    refreshBackupData();
                }
            }
        }

        /// <summary>
        /// Restore the selected backup file
        /// </summary>
        void BackupTreeView_Restore_Click(object sender, EventArgs e)
        {
            TreeView tree = (TreeView)((MenuItem)sender).Parent.Tag;

            if (tree.SelectedNode != null)
            {
                object target = (object)tree.SelectedNode.Tag;

                if (target is FileInfo)
                {
                    FileInfo file = (FileInfo)target;
                    String Path = TB_BackupPath.Text + "\\" + tree.SelectedNode.Text;
                    doRestore(file.FullName, tree.SelectedNode.Parent.Text);
                }
            }
        }

        /// <summary>
        /// Delete a node
        /// </summary>
        void BackupTreeView_Delete_Click(object sender, EventArgs e)
        {
            TreeView tree = (TreeView)((MenuItem)sender).Parent.Tag;

            if (tree.SelectedNode != null)
            {
                object target = (object)tree.SelectedNode.Tag;

                if (target is FileInfo)
                {
                    FileInfo f = (FileInfo)target;

                    if (MessageBox.Show("Delete " + f.Name + "?", String.Empty, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        f.Delete();
                        tree.Nodes.Remove(tree.SelectedNode);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the BT_BackupRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BT_BackupRefresh_Click(object sender, EventArgs e)
        {
            refreshBackupData();
        }

        /// <summary>
        /// Handles the Click event of the quitToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the Click event of the GenerateScripts_AB control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void GenerateScripts_AB_Click(object sender, EventArgs e)
        {
            if (m_Bases[Base1].Qualifier != Qualifiable.Modification.None || m_Bases[Base2].Qualifier != Qualifiable.Modification.None)
            {
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.Filter = "SQL script (*.sql)|*.sql";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    UpgradeOptions opt = new UpgradeOptions(m_Configuration);

                    if (opt.ShowDialog() == DialogResult.OK)
                    {
                        m_Bases[Base1].qualifyVersus(new BaseAnalyzer(this), m_Bases[Base2], Direction.Out);
                        m_Bases[Base2].qualifyVersus(new BaseAnalyzer(this), m_Bases[Base1], Direction.In);

                        TextWriter writer = new StreamWriter(sfd.FileName);

                        Report Rep = new Report(m_Bases, m_Configuration);
                        Rep.writeUpgradeScript(writer, m_Bases[Base1].Connection.Name);

                        writer.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Databases are identical...");
            }
        }

        /// <summary>
        /// Handles the Click event of the GenerateScripts_BA control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void GenerateScripts_BA_Click(object sender, EventArgs e)
        {
            if (m_Bases[Base1].Qualifier != Qualifiable.Modification.None || m_Bases[Base2].Qualifier != Qualifiable.Modification.None)
            {
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.Filter = "SQL script (*.sql)|*.sql";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    UpgradeOptions opt = new UpgradeOptions(m_Configuration);

                    if (opt.ShowDialog() == DialogResult.OK)
                    {
                        m_Bases[Base2].qualifyVersus(new BaseAnalyzer(this), m_Bases[Base1], Direction.Out);
                        m_Bases[Base1].qualifyVersus(new BaseAnalyzer(this), m_Bases[Base2], Direction.In);

                        TextWriter writer = new StreamWriter(sfd.FileName);

                        Report Rep = new Report(m_Bases, m_Configuration);
                        Rep.writeUpgradeScript(writer, m_Bases[Base2].Connection.Name);

                        writer.Close();

                        m_Bases[Base1].qualifyVersus(new BaseAnalyzer(this), m_Bases[Base2], Direction.Out);
                        m_Bases[Base2].qualifyVersus(new BaseAnalyzer(this), m_Bases[Base1], Direction.In);
                    }
                }
            }
            else
            {
                MessageBox.Show("Databases are identical...");
            }
        }

        /// <summary>
        /// Handles the Click event of the GenReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void GenReport_Click(object sender, EventArgs e)
        {
            if (m_Bases[Base1].Qualifier != Qualifiable.Modification.None || m_Bases[Base2].Qualifier != Qualifiable.Modification.None)
            {
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.Filter = "Text file (*.txt)|*.txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    m_Bases[Base1].qualifyVersus(new BaseAnalyzer(this), m_Bases[Base2], Direction.Out);
                    m_Bases[Base2].qualifyVersus(new BaseAnalyzer(this), m_Bases[Base1], Direction.In);

                    TextWriter writer = new StreamWriter(sfd.FileName);

                    Report Rep = new Report(m_Bases, m_Configuration);
                    Rep.writeReport(writer);

                    writer.Close();
                }
            }
            else
            {
                MessageBox.Show("Databases are identical...");
            }
        }

        /// <summary>
        /// Handles the Click event of the RunScripts_AB control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RunScripts_AB_Click(object sender, EventArgs e)
        {
            if (m_Bases[Base1].Qualifier != Qualifiable.Modification.None || m_Bases[Base2].Qualifier != Qualifiable.Modification.None)
            {
                UpgradeOptions opt = new UpgradeOptions(m_Configuration);

                if (opt.ShowDialog() == DialogResult.OK)
                {
                    m_Bases[Base1].qualifyVersus(new BaseAnalyzer(this), m_Bases[Base2], Direction.Out);
                    m_Bases[Base2].qualifyVersus(new BaseAnalyzer(this), m_Bases[Base1], Direction.In);

                    StringWriter writer = new StringWriter();

                    Report Rep = new Report(m_Bases, m_Configuration);
                    Rep.writeUpgradeScript(writer, m_Bases[Base1].Connection.Name);

                    SqlConnection myConnection = m_Bases[Base1].Connection.connect();

                    if (myConnection == null) return;

                    try
                    {
                        ConnectionAdapter.executeNonQuery(myConnection, writer.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        myConnection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Databases are identical...");
            }
        }

        /// <summary>
        /// Handles the Click event of the RunScripts_BA control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RunScripts_BA_Click(object sender, EventArgs e)
        {
            if (m_Bases[Base1].Qualifier != Qualifiable.Modification.None || m_Bases[Base2].Qualifier != Qualifiable.Modification.None)
            {
                UpgradeOptions opt = new UpgradeOptions(m_Configuration);

                if (opt.ShowDialog() == DialogResult.OK)
                {
                    m_Bases[Base2].qualifyVersus(new BaseAnalyzer(this), m_Bases[Base1], Direction.Out);
                    m_Bases[Base1].qualifyVersus(new BaseAnalyzer(this), m_Bases[Base2], Direction.In);

                    StringWriter writer = new StringWriter();

                    Report Rep = new Report(m_Bases, m_Configuration);
                    Rep.writeUpgradeScript(writer, m_Bases[Base2].Connection.Name);

                    SqlConnection myConnection = m_Bases[Base2].Connection.connect();

                    if (myConnection == null) return;

                    try
                    {
                        ConnectionAdapter.executeNonQuery(myConnection, writer.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        myConnection.Close();
                    }

                    m_Bases[Base1].qualifyVersus(new BaseAnalyzer(this), m_Bases[Base2], Direction.Out);
                    m_Bases[Base2].qualifyVersus(new BaseAnalyzer(this), m_Bases[Base1], Direction.In);
                }
            }
            else
            {
                MessageBox.Show("Databases are identical...");
            }
        }

        /// <summary>
        /// Handles the Click event of the NewQuery control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void NewQuery_Click(object sender, EventArgs e)
        {
            QueryControl ctrl = new QueryControl();

            ctrl.Connection = new ConnectionAdapter(m_Bases[Base1].Connection);
            ctrl.Dock = DockStyle.Fill;

            TabPage page = new TabPage("Query");

            page.Controls.Add(ctrl);
            page.Tag = "Query";

            MainTab.TabPages.Add(page);
        }

        /// <summary>
        /// Handles the Click event of the BT_BrowseBackupPath control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BT_BrowseBackupPath_Click(object sender, EventArgs e)
        {
            DialogResult res = this.folderBrowserDialog.ShowDialog();

            if (res == DialogResult.OK)
            {
                TB_BackupPath.Text = this.folderBrowserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// Handles the Click event of the BT_BackupConnection control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BT_BackupConnection_Click(object sender, EventArgs e)
        {
            ConnectionForm frm = new ConnectionForm(m_BackupConnection);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                refreshBackupData();
            }
        }

        /// <summary>
        /// Handles the Click event of the Options control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Options_Click(object sender, EventArgs e)
        {
            GlobalOptions opt = new GlobalOptions(m_Configuration);
            opt.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the About control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void About_Click(object sender, EventArgs e)
        {
            String txt =
                "SQL Server Database Diff " + m_Version + "\r\n" +
                "Copyright (c) 2008-2013 Guillaume Darier" + "\r\n" +
                "\r\n" +
                "This software is licensed under the GNU GPL V3." + "\r\n" +
                "For questions and/or remarks, contact guillaumed00@gmail.com";

            MessageBox.Show(txt);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Opens the file.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        private void OpenFile(String FileName)
        {
            Base[] newBases;
            XmlSerializer Serializer = new XmlSerializer(typeof(Base[]));
            TextReader Reader = new StreamReader(FileName);
            newBases = (Base[])Serializer.Deserialize(Reader);
            Reader.Close();

            if (newBases.Length == 2)
            {
                m_Bases[Base1] = newBases[Base1];
                m_Bases[Base2] = newBases[Base2];

                m_Bases[Base1].ResolveLinks();
                m_Bases[Base2].ResolveLinks();

                m_Bases[Base1].qualifyVersus(m_Bases[Base2], Direction.Out);
                m_Bases[Base2].qualifyVersus(m_Bases[Base1], Direction.In);

                Connection1.TB_HostName.Text = m_Bases[Base1].Connection.Host;
                Connection1.TB_Database.Text = m_Bases[Base1].Connection.Name;
                Connection1.TB_UserName.Text = m_Bases[Base1].Connection.User;
                Connection1.TB_UserPass.Text = m_Bases[Base1].Connection.Pass;

                Connection2.TB_HostName.Text = m_Bases[Base2].Connection.Host;
                Connection2.TB_Database.Text = m_Bases[Base2].Connection.Name;
                Connection2.TB_UserName.Text = m_Bases[Base2].Connection.User;
                Connection2.TB_UserPass.Text = m_Bases[Base2].Connection.Pass;

                BuildTrees();
            }
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        private void SaveFile(String FileName)
        {
            XmlSerializer Serializer = new XmlSerializer(typeof(Base[]));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(FileName, Encoding.UTF8);
            xmlTextWriter.Formatting = Formatting.Indented;
            Serializer.Serialize(xmlTextWriter, m_Bases);
            xmlTextWriter.Close();
        }

        /// <summary>
        /// Builds the database trees
        /// </summary>
        private void BuildTrees()
        {
            m_Bases[Base1].TableCont.Filter.HideUnmodified = m_Configuration.HideUnmodifiedByDefault;
            m_Bases[Base2].TableCont.Filter.HideUnmodified = m_Configuration.HideUnmodifiedByDefault;
            m_Bases[Base1].ViewCont.Filter.HideUnmodified = m_Configuration.HideUnmodifiedByDefault;
            m_Bases[Base2].ViewCont.Filter.HideUnmodified = m_Configuration.HideUnmodifiedByDefault;
            m_Bases[Base1].ProcedureCont.Filter.HideUnmodified = m_Configuration.HideUnmodifiedByDefault;
            m_Bases[Base2].ProcedureCont.Filter.HideUnmodified = m_Configuration.HideUnmodifiedByDefault;
            m_Bases[Base1].FunctionCont.Filter.HideUnmodified = m_Configuration.HideUnmodifiedByDefault;
            m_Bases[Base2].FunctionCont.Filter.HideUnmodified = m_Configuration.HideUnmodifiedByDefault;

            TV_Tree1.Nodes.Clear();
            TV_Tree2.Nodes.Clear();

            MainStatusText.Text = "Building tree " + m_Bases[Base1].Connection.Name + "...";
            TV_Tree1.Nodes.Add(m_Bases[Base1].toTreeNode());
            TV_Tree1.Tag = new BaseTreeData(m_Bases[Base1], TV_Tree2);

            MainStatusText.Text = "Building tree " + m_Bases[Base2].Connection.Name + "...";
            TV_Tree2.Nodes.Add(m_Bases[Base2].toTreeNode());
            TV_Tree2.Tag = new BaseTreeData(m_Bases[Base2], TV_Tree1);

            TV_Tree1.Sort();
            TV_Tree2.Sort();

            TV_Tree1.Nodes[0].Expand();
            TV_Tree2.Nodes[0].Expand();

            MainStatusText.Text = String.Empty;

            createBaseContextMenu(m_Bases[Base1], TV_Tree1);
            createBaseContextMenu(m_Bases[Base2], TV_Tree2);

            if (m_Configuration.ShowReportOnAnalyze)
            {
                StringWriter Writer = new StringWriter();
                Report Rep = new Report(m_Bases, m_Configuration);
                Rep.writeReport(Writer);
                ReportForm RepForm = new ReportForm(Writer.ToString());
                RepForm.ShowDialog();
            }
        }

        /// <summary>
        /// Refreshes the backup data.
        /// </summary>
        private void refreshBackupData()
        {
            if (m_BackupConnection == null) return;

            SqlConnection connection = m_BackupConnection.connectWithoutDataBaseName();

            if (connection != null)
            {
                try
                {
                    if (TB_BackupPath.Text != m_Configuration.BackupPath)
                    {
                        m_Configuration.BackupPath = TB_BackupPath.Text;
                        TV_Backup.Nodes.Clear();
                    }

                    Table tbl = ConnectionAdapter.executeSelect(connection, "SELECT NAME FROM sys.databases");

                    if (tbl != null)
                    {
                        List<String> names = new List<String>();

                        foreach (Row row in tbl.Rows)
                        {
                            names.Add(row.Values[0].Data);
                        }

                        {
                            if (Directory.Exists(TB_BackupPath.Text))
                            {
                                DirectoryInfo master = new DirectoryInfo(TB_BackupPath.Text);
                                DirectoryInfo[] dirs = master.GetDirectories();

                                foreach (DirectoryInfo dir in dirs)
                                {
                                    if (names.Contains(dir.Name) == false)
                                    {
                                        names.Add(dir.Name);
                                    }
                                }
                            }
                        }

                        foreach (String name in names)
                        {
                            TreeNode node = new TreeNode();
                            node.Text = name;

                            bool Exists = false;

                            foreach (TreeNode test in TV_Backup.Nodes)
                            {
                                if (test.Text == node.Text)
                                {
                                    Exists = true;
                                    break;
                                }
                            }

                            if (Exists == false) TV_Backup.Nodes.Add(node);
                        }

                        foreach (TreeNode node in TV_Backup.Nodes)
                        {
                            String Path = TB_BackupPath.Text + "\\" + node.Text;

                            if (Directory.Exists(Path))
                            {
                                DirectoryInfo dir = new DirectoryInfo(Path);
                                FileInfo[] files = dir.GetFiles("*.bak");

                                foreach (FileInfo file in files)
                                {
                                    TreeNode filenode = new TreeNode();
                                    String Text = file.Name;
                                    Text += " [" + file.CreationTime.ToShortDateString() + "]";
                                    Text += " [" + file.Length.ToString() + "]";
                                    filenode.Text = Text;
                                    filenode.Tag = file;

                                    bool Exists = false;

                                    foreach (TreeNode test in node.Nodes)
                                    {
                                        if (test.Text == filenode.Text)
                                        {
                                            Exists = true;
                                            break;
                                        }
                                    }

                                    if (Exists == false) node.Nodes.Add(filenode);
                                }
                            }
                        }

                        // Remove non-existant files from list

                        foreach (TreeNode node in TV_Backup.Nodes)
                        {
                            foreach (TreeNode filenode in node.Nodes)
                            {
                                FileInfo file = (FileInfo)filenode.Tag;

                                if (File.Exists(file.FullName) == false)
                                {
                                    node.Nodes.Remove(filenode);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Does the backup.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="BaseName">Name of the base.</param>
        private void doBackup(String FileName, String BaseName)
        {
            if (m_BackupConnection == null) return;

            if (Directory.Exists(Path.GetDirectoryName(FileName)) == false)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FileName));
            }

            SqlConnection connection = m_BackupConnection.connectWithoutDataBaseName(10000);

            if (connection != null)
            {
                try
                {
                    String query = String.Format("BACKUP DATABASE [{0}] TO DISK='{1}'", BaseName, FileName);

                    ConnectionAdapter.executeScalar(connection, query);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Does the restore.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="BaseName">Name of the base.</param>
        private void doRestore(String FileName, String BaseName)
        {
            if (m_BackupConnection == null) return;

            if (Directory.Exists(Path.GetDirectoryName(FileName)) == false)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FileName));
            }

            SqlConnection connection = m_BackupConnection.connectWithoutDataBaseName(10000);

            if (connection != null)
            {
                try
                {
                    String query = String.Format
                    (
                        "DECLARE @DatabaseName nvarchar(50) SET @DatabaseName = N'{0}' " +
                        "DECLARE @SQL varchar(max) " +
                        "SELECT @SQL = COALESCE(@SQL,'') + 'Kill ' + Convert(varchar, SPId) + ';' " +
                        "FROM MASTER..SysProcesses " +
                        "WHERE DBId = DB_ID(@DatabaseName) AND SPId <> @@SPId EXEC(@SQL)",
                        BaseName
                    );

                    ConnectionAdapter.executeScalar(connection, query);

                    try
                    {
                        query = String.Format("DROP DATABASE [{0}]", BaseName);
                        ConnectionAdapter.executeScalar(connection, query);
                    }
                    catch (Exception)
                    {
                    }

                    query = String.Format
                    (
                        "RESTORE DATABASE [{0}] FROM DISK = '{1}' WITH FILE = 1",
                        BaseName, FileName
                    );

                    ConnectionAdapter.executeScalar(connection, query);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Finds a free name for a backup using three digits for sequence
        /// </summary>
        private String findUniqueFileName(String Path, String Name, String Extension)
        {
            String RetVal = String.Empty;
            int Index = 0;
            while (true)
            {
                String IndexStr = Index.ToString(); while (IndexStr.Length < 3) IndexStr = "0" + IndexStr;
                RetVal = Name + "_" + DateTime.Now.ToShortDateString() + "_" + IndexStr + Extension;
                RetVal = RetVal.Replace("/", "_");
                if (File.Exists(Path + "\\" + RetVal) == false) break;
                Index++;
            }
            return RetVal;
        }

        /// <summary>
        /// Creates a script that inserts table data
        /// </summary>
        private void exportData(Base database, Table table)
        {
            String FileName = table.Name + "_DataInsert.sql";

            SaveFileDialog sfd = new SaveFileDialog();

            sfd.FileName = FileName;
            sfd.Filter = "SQL Files (*.sql)|*.sql";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                SqlConnection connection = database.Connection.connect();

                if (connection != null)
                {
                    try
                    {
                        String query = String.Format("SELECT * FROM {0}", table.Name);

                        Table data = ConnectionAdapter.executeSelect(connection, query);

                        if (data != null && data.Rows.Count > 0)
                        {
                            TextWriter writer = new StreamWriter(sfd.FileName);

                            if (writer != null)
                            {
                                bool HasIdentity = false;
                                foreach (Field f in table.FieldCont.Fields)
                                {
                                    if (f.Identity)
                                    {
                                        HasIdentity = true;
                                        break;
                                    }
                                }
                                if (HasIdentity)
                                {
                                    writer.WriteLine(String.Format("SET IDENTITY_INSERT [{0}] ON", table.Name));
                                }
                                writer.WriteLine(String.Format("DELETE FROM [{0}]", table.Name));
                                foreach (Row row in data.Rows)
                                {
                                    writer.WriteLine(String.Format("INSERT INTO [{0}]", table.Name));
                                    writer.Write("(");
                                    bool firstfield = true;
                                    foreach (Field f in data.FieldCont.Fields)
                                    {
                                        if (firstfield == false) writer.Write(", ");
                                        writer.Write(f.Name);
                                        firstfield = false;
                                    }
                                    writer.WriteLine(")");
                                    writer.WriteLine("values");
                                    writer.Write("(");
                                    bool firstvalue = true;
                                    foreach (Value v in row.Values)
                                    {
                                        if (firstvalue == false) writer.Write(", ");
                                        writer.Write("'" + v.Data.Replace("'", "''") + "'");
                                        firstvalue = false;
                                    }
                                    writer.WriteLine(")");
                                }
                                if (HasIdentity)
                                {
                                    writer.WriteLine(String.Format("SET IDENTITY_INSERT [{0}] OFF", table.Name));
                                }
                                writer.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Sets configuration information to the UI
        /// </summary>
        private void initUIInfo()
        {
            Connection1.TB_HostName.Text = m_Configuration.Connection1.Host;
            Connection1.TB_UserName.Text = m_Configuration.Connection1.User;
            Connection1.TB_UserPass.Text = m_Configuration.Connection1.Pass;
            Connection1.TB_Database.Text = m_Configuration.Connection1.Name;
            Connection1.CHK_WindowsAuth.Checked = m_Configuration.Connection1.WindowsAuth;

            Connection2.TB_HostName.Text = m_Configuration.Connection2.Host;
            Connection2.TB_UserName.Text = m_Configuration.Connection2.User;
            Connection2.TB_UserPass.Text = m_Configuration.Connection2.Pass;
            Connection2.TB_Database.Text = m_Configuration.Connection2.Name;
            Connection2.CHK_WindowsAuth.Checked = m_Configuration.Connection2.WindowsAuth;

            TB_BackupPath.Text = m_Configuration.BackupPath;
        }

        /// <summary>
        /// Gets configuration information from the UI
        /// </summary>
        private void grabUIInfo()
        {
            m_Configuration.Connection1.Host = m_Bases[Base1].Connection.Host = Connection1.TB_HostName.Text;
            m_Configuration.Connection1.User = m_Bases[Base1].Connection.User = Connection1.TB_UserName.Text;
            m_Configuration.Connection1.Pass = m_Bases[Base1].Connection.Pass = Connection1.TB_UserPass.Text;
            m_Configuration.Connection1.Name = m_Bases[Base1].Connection.Name = Connection1.TB_Database.Text;
            m_Configuration.Connection1.WindowsAuth = m_Bases[Base1].Connection.WindowsAuth = Connection1.CHK_WindowsAuth.Checked;

            m_Configuration.Connection2.Host = m_Bases[Base2].Connection.Host = Connection2.TB_HostName.Text;
            m_Configuration.Connection2.User = m_Bases[Base2].Connection.User = Connection2.TB_UserName.Text;
            m_Configuration.Connection2.Pass = m_Bases[Base2].Connection.Pass = Connection2.TB_UserPass.Text;
            m_Configuration.Connection2.Name = m_Bases[Base2].Connection.Name = Connection2.TB_Database.Text;
            m_Configuration.Connection2.WindowsAuth = m_Bases[Base2].Connection.WindowsAuth = Connection2.CHK_WindowsAuth.Checked;

            m_Configuration.Connection3.Host = m_BackupConnection.Host;
            m_Configuration.Connection3.User = m_BackupConnection.User;
            m_Configuration.Connection3.Pass = m_BackupConnection.Pass;
            m_Configuration.Connection3.Name = m_BackupConnection.Name;
            m_Configuration.Connection3.WindowsAuth = m_BackupConnection.WindowsAuth;

            m_Configuration.BackupPath = TB_BackupPath.Text;
        }

        /// <summary>
        /// Shows a message to the user
        /// </summary>
        private void AddOutput(String Text)
        {
            MessageBox.Show(Text);
        }

        #endregion
    }
}
