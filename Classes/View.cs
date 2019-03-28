
using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SQLServerDatabaseDiff
{
    public class View : Qualifiable
    {
        #region Fields

        private String m_Name;
        private ViewFieldContainer m_FieldCont;

        [NonSerialized]
        private String m_Text;

        [NonSerialized]
        private Base m_Parent;

        [NonSerialized]
        private List<Row> m_Rows;

        [NonSerialized]
        private Int32 m_Checksum;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="View"/> class.
        /// </summary>
        public View()
        {
            m_Parent = null;
            m_Name = String.Empty;
            m_FieldCont = new ViewFieldContainer(this);
            m_Rows = new List<Row>();
            m_Text = String.Empty;
            m_Checksum = 0;
        }

        public View(Base parent)
            : this()
        {
            m_Parent = parent;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        [XmlIgnore]
        public Base Parent
        {
            get { return m_Parent; }
            set { m_Parent = value; }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public String Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        /// <summary>
        /// Gets or sets the field cont.
        /// </summary>
        /// <value>The field cont.</value>
        public ViewFieldContainer FieldCont
        {
            get { return m_FieldCont; }
            set { m_FieldCont = value; }
        }

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>The rows.</value>
        [XmlIgnore]
        public List<Row> Rows
        {
            get { return m_Rows; }
            set { m_Rows = value; }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [XmlIgnore]
        public String Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }

        /// <summary>
        /// Gets or sets the checksum.
        /// </summary>
        /// <value>The checksum.</value>
        [XmlIgnore]
        public Int32 Checksum
        {
            get
            {
                if (m_Checksum == 0)
                {
                    String text = getDefinition();

                    if (text != String.Empty)
                    {
                        m_Checksum = text.GetHashCode();
                    }
                    else
                    {
                        m_Checksum = -1;
                    }
                }

                return m_Checksum;
            }

            set { m_Checksum = value; }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Retourne un <see cref="T:System.String"></see> qui représente l'<see cref="T:System.Object"></see> en cours.
        /// </summary>
        /// <returns>
        /// 	<see cref="T:System.String"></see> qui représente le <see cref="T:System.Object"></see> en cours.
        /// </returns>
        public override string ToString()
        {
            String ret = String.Empty;

            foreach (Row row in Rows)
            {
                foreach (Value val in row.Values)
                {
                    ret += val.Data + " ";
                }

                ret += "\r\n";
            }

            return ret;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Makes a tree node from this object.
        /// </summary>
        /// <returns></returns>
        public override TreeNode toTreeNode()
        {
            TreeNode Node = new TreeNode();

            Node.Text = Name;

            Base.SetNodeImageFromQualifier(Node, Qualifier, IsGhost);

            Node.Nodes.Add(FieldCont.toTreeNode());

            Node.Tag = this;
            if (IsGhost) Node.ForeColor = Color.Gray;

            return Node;
        }

        /// <summary>
        /// Qualifies this object against another one.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="dir">The direction.</param>
        /// <returns></returns>
        public Modification qualifyVersus(View target, Direction dir)
        {
            Qualifier = Modification.None;

            Modification mod = FieldCont.qualifyVersus(target.FieldCont, dir);

            if (Qualifier == Modification.None && mod != Modification.None)
            {
                Qualifier = mod;
            }

            return Qualifier;
        }

        /// <summary>
        /// Gets the definition.
        /// </summary>
        /// <returns></returns>
        public String getDefinition()
        {
            String query = String.Format("exec sp_helptext @objname = '{0}'", Name);

            SqlConnection myConnection = m_Parent.Connection.connect();

            if (myConnection == null) return String.Empty;

            Table text_table = ConnectionAdapter.executeSelect(myConnection, query);

            myConnection.Close();

            if (text_table.Rows.Count > 0)
            {
                String ret = String.Empty;

                foreach (Row row in text_table.Rows)
                {
                    ret += row.Values[0].Data;
                }

                return ret;
            }

            return String.Empty;
        }

        #endregion
    }
}
