
using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SQLServerDatabaseDiff
{
    #region Enums

    public enum Direction
    {
        None = 0,
        In = 1,
        Out = 2
    }

    #endregion

    [Serializable]
    public class Base : Qualifiable
    {
        #region Fields

        private ConnectionAdapter m_Connection;
        private TableContainer m_TableCont;
        private ViewContainer m_ViewCont;
        private ProcedureContainer m_ProcedureCont;
        private FunctionContainer m_FunctionCont;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Base"/> class.
        /// </summary>
        public Base()
        {
            m_Connection = new ConnectionAdapter();
            m_TableCont = new TableContainer(this);
            m_ViewCont = new ViewContainer(this);
            m_ProcedureCont = new ProcedureContainer(this);
            m_FunctionCont = new FunctionContainer(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public ConnectionAdapter Connection
        {
            get { return m_Connection; }
            set { m_Connection = value; }
        }

        /// <summary>
        /// Gets or sets the table cont.
        /// </summary>
        /// <value>The table cont.</value>
        public TableContainer TableCont
        {
            get { return m_TableCont; }
            set { m_TableCont = value; }
        }

        /// <summary>
        /// Gets or sets the view cont.
        /// </summary>
        /// <value>The view cont.</value>
        public ViewContainer ViewCont
        {
            get { return m_ViewCont; }
            set { m_ViewCont = value; }
        }

        /// <summary>
        /// Gets or sets the procedure cont.
        /// </summary>
        /// <value>The procedure cont.</value>
        public ProcedureContainer ProcedureCont
        {
            get { return m_ProcedureCont; }
            set { m_ProcedureCont = value; }
        }

        /// <summary>
        /// Gets or sets the function cont.
        /// </summary>
        /// <value>The function cont.</value>
        public FunctionContainer FunctionCont
        {
            get { return m_FunctionCont; }
            set { m_FunctionCont = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fetches the data.
        /// </summary>
        /// <param name="Analyzer">The analyzer.</param>
        /// <param name="config">The config.</param>
        public bool fetchData(BaseAnalyzer Analyzer, Configuration config)
        {
            SqlConnection myConnection = Connection.connect();

            if (myConnection == null) return false;

            //----------------------------------------------------------------
            // Handle tables

            if (config.AnalyzeTables)
            {
                int NumTables = 0;

                Table TableCountTable = ConnectionAdapter.executeSelect(myConnection, "SELECT COUNT(name) FROM sys.objects WHERE type_desc='USER_TABLE'");

                if (TableCountTable.Rows.Count > 0)
                {
                    Int32.TryParse(TableCountTable.Rows[0].Values[0].Data, out NumTables);
                }

                Analyzer.Progress_Max = NumTables;
                Analyzer.Progress_Current = 0;

                Table TableList = ConnectionAdapter.executeSelect(myConnection, "SELECT name FROM sys.objects WHERE type_desc='USER_TABLE'");

                foreach (Row table_new_row in TableList.Rows)
                {
                    Analyzer.Progress_Current++;

                    Table NewTable = new Table(this);
                    NewTable.Name = table_new_row.Values[0].Data;

                    Analyzer.Status = m_Connection.Name + " : " + NewTable.Name + "...";

                    GetTableData(NewTable, config);

                    TableCont.Tables.Add(NewTable);
                }
            }

            //----------------------------------------------------------------
            // Handle views

            if (config.AnalyzeViews)
            {
                int NumViews = 0;

                Table ViewCountTable = ConnectionAdapter.executeSelect(myConnection, "SELECT COUNT(name) FROM sys.objects WHERE type_desc='VIEW'");

                if (ViewCountTable.Rows.Count > 0)
                {
                    Int32.TryParse(ViewCountTable.Rows[0].Values[0].Data, out NumViews);
                }

                Analyzer.Progress_Max = NumViews;
                Analyzer.Progress_Current = 0;

                Table TableList = ConnectionAdapter.executeSelect(myConnection, "SELECT name FROM sys.objects WHERE type_desc='VIEW'");

                foreach (Row table_new_row in TableList.Rows)
                {
                    Analyzer.Progress_Current++;

                    View NewView = new View(this);
                    NewView.Name = table_new_row.Values[0].Data;

                    Analyzer.Status = m_Connection.Name + " : " + NewView.Name + "...";

                    GetViewData(NewView, config);

                    ViewCont.Views.Add(NewView);
                }
            }

            //----------------------------------------------------------------
            // Handle procedures

            if (config.AnalyzeProcedures)
            {
                Analyzer.Status = "Analyzing procedures...";

                String query = "SELECT name FROM sys.objects WHERE type_desc='SQL_STORED_PROCEDURE'";

                Table proc_list = ConnectionAdapter.executeSelect(myConnection, query);

                foreach (Row proc_new_row in proc_list.Rows)
                {
                    Procedure proc = new Procedure();

                    proc.Name = proc_new_row.Values[0].Data;
                    proc.Parent = this;

                    ProcedureCont.Procedures.Add(proc);
                }
            }

            //----------------------------------------------------------------
            // Handle functions

            if (config.AnalyzeFunctions)
            {
                Analyzer.Status = "Analyzing functions...";

                String query = "SELECT name FROM sys.objects WHERE type_desc='SQL_SCALAR_FUNCTION' OR type_desc='SQL_TABLE_VALUED_FUNCTION'";

                Table func_list = ConnectionAdapter.executeSelect(myConnection, query);

                foreach (Row func_new_row in func_list.Rows)
                {
                    Function func = new Function();

                    func.Name = func_new_row.Values[0].Data;
                    func.Parent = this;

                    FunctionCont.Functions.Add(func);
                }
            }

            //----------------------------------------------------------------

            Analyzer.Status = "Analyze done...";
            Analyzer.Progress_Current = 0;

            myConnection.Close();

            return true;
        }

        /// <summary>
        /// Gets the table data.
        /// </summary>
        /// <param name="Target">The target.</param>
        public void GetTableData(Table Target, Configuration config)
        {
            SqlConnection myConnection = Connection.connect();

            if (myConnection == null) return;

            Target.FieldCont = new TableFieldContainer(Target);
            Target.DataCont = new DataContainer(Target);
            Target.ConstraintCont = new ConstraintContainer(Target);

            Table TableList = ConnectionAdapter.executeSelect
            (
                myConnection,
                String.Format
                (
                    "SELECT name, object_id, parent_object_id FROM sys.objects WHERE type_desc='USER_TABLE' and name='{0}'",
                    Target.Name
                )
            );

            String table_name = TableList.Rows[0].Values[0].Data;
            String object_id = TableList.Rows[0].Values[1].Data;
            String parent_object_id = TableList.Rows[0].Values[2].Data;

            String query = String.Format
            (
                "SELECT sys.columns.name, sys.types.name, sys.columns.max_length, sys.columns.is_nullable, sys.columns.is_identity " +
                "FROM sys.columns " +
                "INNER JOIN sys.types " +
                "ON sys.columns.system_type_id=sys.types.system_type_id " +
                "AND sys.columns.user_type_id=sys.types.user_type_id " +
                "WHERE sys.columns.object_id='{0}'",
                object_id
            );

            Table ColumnList = ConnectionAdapter.executeSelect(myConnection, query);

            foreach (Row col in ColumnList.Rows)
            {
                String type = col.Values[1].Data;
                String num = col.Values[2].Data;
                String nullable = col.Values[3].Data;
                String identity = col.Values[4].Data;

                if (IsTextType(type) == true && num != String.Empty && num != "NULL") type += "(" + num + ")";

                TableField f = new TableField(col.Values[0].Data, type, nullable.ToLower() == "true" ? true : false, identity.ToLower() == "true" ? true : false);
                f.Parent = Target;

                Target.FieldCont.Fields.Add(f);
            }

            //----------------------------------------------------------------
            // Handle constraints

            query = String.Format("SELECT CONSTRAINT_NAME, CONSTRAINT_TYPE FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE TABLE_NAME='{0}'", table_name);

            Table ConstraintList = ConnectionAdapter.executeSelect(myConnection, query);

            foreach (Row current_row in ConstraintList.Rows)
            {
                Constraint NewConstraint = new Constraint();

                NewConstraint.Name = current_row.Values[0].Data;
                NewConstraint.Type = Constraint.SQLConstraintTypeToConstraintType(current_row.Values[1].Data);
                NewConstraint.Parent = Target;

                if (NewConstraint.Type == Constraint.ConstraintType.Check)
                {
                    query = String.Format("SELECT CHECK_CLAUSE FROM INFORMATION_SCHEMA.CHECK_CONSTRAINTS WHERE CONSTRAINT_NAME='{0}'", NewConstraint.Name);

                    Table ConstraintText = ConnectionAdapter.executeSelect(myConnection, query);

                    if (ConstraintText.Rows.Count > 0)
                    {
                        NewConstraint.Text = ConstraintText.Rows[0].Values[0].Data;
                    }
                }
                else if (NewConstraint.Type == Constraint.ConstraintType.ForeignKey)
                {
                    query = String.Format
                    (
                        "SELECT " +
                        "CONSTRAINT_NAME = REF_CONST.CONSTRAINT_NAME, " +
                        "TABLE_NAME = FK.TABLE_NAME, " +
                        "COLUMN_NAME = FK_COLS.COLUMN_NAME, " +
                        "REFERENCED_TABLE_NAME = PK.TABLE_NAME, " +
                        "REFERENCED_COLUMN_NAME = PK_COLS.COLUMN_NAME " +
                        "FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS REF_CONST " +
                        "INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK " +
                        "ON REF_CONST.CONSTRAINT_CATALOG = FK.CONSTRAINT_CATALOG " +
                        "AND REF_CONST.CONSTRAINT_SCHEMA = FK.CONSTRAINT_SCHEMA " +
                        "AND REF_CONST.CONSTRAINT_NAME = FK.CONSTRAINT_NAME " +
                        "AND FK.CONSTRAINT_TYPE = 'FOREIGN KEY' " +
                        "INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK ON REF_CONST.UNIQUE_CONSTRAINT_CATALOG = PK.CONSTRAINT_CATALOG " +
                        "AND REF_CONST.UNIQUE_CONSTRAINT_SCHEMA = PK.CONSTRAINT_SCHEMA " +
                        "AND REF_CONST.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME " +
                        "AND PK.CONSTRAINT_TYPE = 'PRIMARY KEY' " +
                        "INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE FK_COLS ON REF_CONST.CONSTRAINT_NAME = FK_COLS.CONSTRAINT_NAME " +
                        "INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE PK_COLS ON PK.CONSTRAINT_NAME = PK_COLS.CONSTRAINT_NAME " +
                        "WHERE REF_CONST.CONSTRAINT_NAME = '{0}'",
                        NewConstraint.Name
                    );

                    Table ConstraintText = ConnectionAdapter.executeSelect(myConnection, query);

                    if (ConstraintText != null && ConstraintText.Rows.Count > 0)
                    {
                        NewConstraint.ReferencedTable = ConstraintText.Rows[0].Values[3].Data;
                        NewConstraint.ReferencedFieldNames.Add(ConstraintText.Rows[0].Values[4].Data);
                    }
                }

                query = String.Format
                (
                    "SELECT COLUMN_NAME " +
                    "FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE " +
                    "WHERE TABLE_NAME='{0}' AND CONSTRAINT_NAME='{1}'",
                    table_name,
                    NewConstraint.Name
                );

                Table ConstraintRows = ConnectionAdapter.executeSelect(myConnection, query);

                foreach (Row rw in ConstraintRows.Rows)
                {
                    NewConstraint.FieldNames.Add(rw.Values[0].Data);
                }

                Target.ConstraintCont.Constraints.Add(NewConstraint);
            }

            // Handle data

            if (config.AnalyzeData)
            {
                Target.DataCont = new DataContainer(Target);
            }

            myConnection.Close();
        }

        /// <summary>
        /// Gets the view data.
        /// </summary>
        /// <param name="Target">The target.</param>
        /// <param name="config">The config.</param>
        public void GetViewData(View Target, Configuration config)
        {
            SqlConnection myConnection = Connection.connect();

            if (myConnection == null) return;

            Target.FieldCont = new ViewFieldContainer(Target);

            Table TableList = ConnectionAdapter.executeSelect
            (
                myConnection,
                String.Format
                (
                    "SELECT name, object_id, parent_object_id FROM sys.objects WHERE type_desc='VIEW' and name='{0}'",
                    Target.Name
                )
            );

            String table_name = TableList.Rows[0].Values[0].Data;
            String object_id = TableList.Rows[0].Values[1].Data;
            String parent_object_id = TableList.Rows[0].Values[2].Data;

            String query = String.Format
            (
                "SELECT sys.columns.name, sys.types.name, sys.columns.max_length, sys.columns.is_nullable, sys.columns.is_identity " +
                "FROM sys.columns " +
                // "INNER JOIN sys.types ON sys.columns.system_type_id=sys.types.system_type_id " +
                "INNER JOIN sys.types " +
                "ON sys.columns.system_type_id=sys.types.system_type_id " +
                "AND sys.columns.user_type_id=sys.types.user_type_id " +
                "WHERE sys.columns.object_id='{0}'",
                object_id
            );

            Table ColumnList = ConnectionAdapter.executeSelect(myConnection, query);

            foreach (Row col in ColumnList.Rows)
            {
                String type = col.Values[1].Data;
                String num = col.Values[2].Data;
                String nullable = col.Values[3].Data;
                String identity = col.Values[4].Data;

                if
                (
                    IsTextType(type) == true &&
                    num != String.Empty &&
                    num != "NULL"
                )
                {
                    type += "(" + num + ")";
                }

                ViewField f = new ViewField(col.Values[0].Data, type, nullable.ToLower() == "true" ? true : false, identity.ToLower() == "true" ? true : false);
                f.Parent = Target;

                Target.FieldCont.Fields.Add(f);
            }

            myConnection.Close();
        }

        /// <summary>
        /// Makes a tree node from this object.
        /// </summary>
        /// <returns></returns>
        public override TreeNode toTreeNode()
        {
            TreeNode Node = new TreeNode();

            Node.Text = Connection.Name;

            Base.SetNodeImageFromQualifier(Node, Qualifier, IsGhost);

            //----------------------------------------------------------------

            Node.Nodes.Add(TableCont.toTreeNode());

            Node.Nodes.Add(ViewCont.toTreeNode());

            Node.Nodes.Add(ProcedureCont.toTreeNode());

            Node.Nodes.Add(FunctionCont.toTreeNode());

            //----------------------------------------------------------------

            Node.Tag = this;
            if (IsGhost) Node.ForeColor = Color.Gray;

            return Node;
        }

        /// <summary>
        /// Qualifies this object against another one.
        /// </summary>
        /// <param name="Analyzer">The analyzer.</param>
        /// <param name="target">The target.</param>
        /// <param name="dir">The direction.</param>
        /// <returns></returns>
        public Modification qualifyVersus(BaseAnalyzer Analyzer, Base target, Direction dir)
        {
            Qualifier = Modification.None;

            Analyzer.Progress_Max = 4;
            Analyzer.Progress_Current = 0;

            // Qualify tables

            Analyzer.Progress_Current++;
            Analyzer.Status = "Qualifying tables...";

            if (TableCont.qualifyVersus(target.TableCont, dir) != Modification.None)
            {
                Qualifier = Modification.Modified;
            }

            // Qualify views

            Analyzer.Progress_Current++;
            Analyzer.Status = "Qualifying views...";

            if (ViewCont.qualifyVersus(target.ViewCont, dir) != Modification.None)
            {
                Qualifier = Modification.Modified;
            }

            // Qualify procedures

            Analyzer.Progress_Current++;
            Analyzer.Status = "Qualifying procedures...";

            if (ProcedureCont.qualifyVersus(target.ProcedureCont, dir) != Modification.None)
            {
                Qualifier = Modification.Modified;
            }

            // Qualify functions

            Analyzer.Progress_Current++;
            Analyzer.Status = "Qualifying functions...";

            if (FunctionCont.qualifyVersus(target.FunctionCont, dir) != Modification.None)
            {
                Qualifier = Modification.Modified;
            }

            Analyzer.Status = "Qualifying done...";
            Analyzer.Progress_Current = 0;

            return Qualifier;
        }

        /// <summary>
        /// Mods the string.
        /// </summary>
        /// <param name="mod">The mod.</param>
        /// <returns></returns>
        public static String modString(Modification mod)
        {
            switch (mod)
            {
                case Modification.Created: return "CREATE";
                case Modification.Deleted: return "DROP";
                case Modification.Modified: return "ALTER";
            }

            return String.Empty;
        }

        /// <summary>
        /// Sets the node image from qualifier.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="mod">The mod.</param>
        /// <param name="IsGhost">if set to <c>true</c> [is ghost].</param>
        public static void SetNodeImageFromQualifier(TreeNode node, Modification mod, Boolean IsGhost)
        {
            if (IsGhost)
            {
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
            }
            else
            {
                if (mod == Modification.None)
                {
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 1;
                }
                else if (mod == Modification.Created)
                {
                    node.ImageIndex = 2;
                    node.SelectedImageIndex = 2;
                }
                else if (mod == Modification.Deleted)
                {
                    node.ImageIndex = 4;
                    node.SelectedImageIndex = 4;
                }
                else
                {
                    node.ImageIndex = 3;
                    node.SelectedImageIndex = 3;
                }
            }
        }

        /// <summary>
        /// Determines whether [is text type] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// 	<c>true</c> if [is text type] [the specified type]; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsTextType(String type)
        {
            if (type == "char" || type == "nchar" || type == "varchar" || type == "nvarchar" || type == "text") return true;
            return false;
        }

        /// <summary>
        /// Resolves the links.
        /// </summary>
        /// <param name="Twin">The twin.</param>
        public void ResolveLinks()
        {
            m_TableCont.ResolveLinks(this);
            m_ViewCont.ResolveLinks(this);
            m_ProcedureCont.ResolveLinks(this);
            m_FunctionCont.ResolveLinks(this);
        }

        #endregion
    }
}
