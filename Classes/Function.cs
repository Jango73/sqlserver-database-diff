
using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SQLServerDatabaseDiff
{
    [Serializable]
    public class Function : Qualifiable
    {
        #region Fields

        private String m_Name;

        [NonSerialized]
        private Base m_Parent;

        [NonSerialized]
        private String m_Text;

        [NonSerialized]
        private Int32 m_Checksum;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Function"/> class.
        /// </summary>
        public Function()
        {
            m_Parent = null;
            m_Name = String.Empty;
            m_Text = String.Empty;
            m_Checksum = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Function"/> class.
        /// </summary>
        /// <param name="Target">The target.</param>
        public Function(Function Target)
            : this()
        {
            GhostCopy(Target);

            m_Parent = Target.m_Parent;
            m_Name = Target.m_Name;
            m_Text = Target.m_Text;
            m_Checksum = Target.m_Checksum;
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

        #region Methods

        /// <summary>
        /// Computes a hash code for this object.
        /// </summary>
        /// <returns>
        /// Hash code.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = (result * 397) ^ ("Function").GetHashCode();
                result = (result * 397) ^ m_Name.GetHashCode();
                return result;
            }
        }

        /// <summary>
        /// Détermine si l'<see cref="T:System.Object"></see> spécifié est égal à l'<see cref="T:System.Object"></see> en cours.
        /// </summary>
        /// <param name="obj"><see cref="T:System.Object"></see> à comparer au <see cref="T:System.Object"></see> en cours.</param>
        /// <returns>
        /// true si l'<see cref="T:System.Object"></see> spécifié est égal à l'<see cref="T:System.Object"></see> en cours ; sinon, false.
        /// </returns>
        public override Boolean Equals(object obj)
        {
            return (GetHashCode() == obj.GetHashCode());
        }

        /// <summary>
        /// Makes a tree node from this object.
        /// </summary>
        /// <returns></returns>
        public override TreeNode toTreeNode()
        {
            TreeNode Node = new TreeNode();

            Node.Text = Name;

            Base.SetNodeImageFromQualifier(Node, Qualifier, IsGhost);

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
        public Modification qualifyVersus(Function target, Direction dir)
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

        /// <summary>
        /// Resolves the links.
        /// </summary>
        /// <param name="Twin">The twin.</param>
        public void ResolveLinks(Base NewParent)
        {
            m_Parent = NewParent;
        }

        #endregion
    }
}
