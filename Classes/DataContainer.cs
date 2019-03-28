
using System;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SQLServerDatabaseDiff
{
    [Serializable]
    public class DataContainer : Qualifiable
    {
        #region Fields

        [NonSerialized]
        private Table m_Parent;

        [NonSerialized]
        private Int32 m_Checksum;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContainer"/> class.
        /// </summary>
        public DataContainer()
        {
            m_Parent = null;
            m_Checksum = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContainer"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public DataContainer(Table parent)
            : this()
        {
            m_Parent = parent;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContainer"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="Target">The target.</param>
        public DataContainer(Table parent, DataContainer Target)
            : this(parent)
        {
            if (Target == null) return;

            GhostCopy(Target);

            m_Checksum = Target.m_Checksum;
        }

        #endregion

        #region Properties

        [XmlIgnore]
        public Table Parent
        {
            get { return m_Parent; }
            set { m_Parent = value; }
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

        #region Methods

        /// <summary>
        /// Makes a tree node from this object.
        /// </summary>
        /// <returns></returns>
        public override TreeNode toTreeNode()
        {
            TreeNode node = new TreeNode();

            node.Text = "Data";

            Base.SetNodeImageFromQualifier(node, Qualifier, IsGhost);

            return node;
        }

        /// <summary>
        /// Qualifies this object against another one.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="dir">The direction.</param>
        /// <returns></returns>
        public Modification qualifyVersus(DataContainer target, Direction dir)
        {
            Qualifier = Modification.None;

            if (Checksum != target.Checksum && dir == Direction.In)
            {
                Qualifier = Modification.Modified;
            }

            return Qualifier;
        }

        /// <summary>
        /// Gets the definition.
        /// </summary>
        /// <returns></returns>
        public String getDefinition()
        {
            String query = String.Format("SELECT * FROM [{0}]", m_Parent.Name);

            SqlConnection myConnection = m_Parent.Parent.Connection.connect();

            if (myConnection == null) return String.Empty;

            Table text_table = ConnectionAdapter.executeSelect(myConnection, query);

            myConnection.Close();

            if (text_table.Rows.Count > 0)
            {
                String ret = String.Empty;

                foreach (Row row in text_table.Rows)
                {
                    foreach (Value val in row.Values)
                    {
                        ret += val.Data;
                    }
                }

                return ret;
            }

            return String.Empty;
        }

        #endregion
    }
}
