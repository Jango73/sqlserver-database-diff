
using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SQLServerDatabaseDiff
{
    [Serializable]
    public class Table : Qualifiable
    {
        #region Fields

        private String m_Name;
        private TableFieldContainer m_FieldCont;
        private ConstraintContainer m_ConstraintCont;
        private DataContainer m_DataCont;

        [NonSerialized]
        private Base m_Parent;

        [NonSerialized]
        private List<Row> m_Rows;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class.
        /// </summary>
        public Table()
        {
            m_Parent = null;
            m_Name = String.Empty;
            m_Rows = new List<Row>();
            m_FieldCont = new TableFieldContainer(this);
            m_ConstraintCont = new ConstraintContainer(this);
            m_DataCont = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public Table(Base parent)
            : this()
        {
            m_Parent = parent;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="Target">The target.</param>
        public Table(Base parent, Table Target)
            : this(parent)
        {
            GhostCopy(Target);

            m_Parent = Target.Parent;
            m_Name = Target.Name;
            m_Rows = new List<Row>();
            m_FieldCont = new TableFieldContainer(this, Target.FieldCont);
            m_ConstraintCont = new ConstraintContainer(this, Target.ConstraintCont);

            if (Target.DataCont != null)
            {
                m_DataCont = new DataContainer(this, Target.DataCont);
            }
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
        /// Gets or sets the field cont.
        /// </summary>
        /// <value>The field cont.</value>
        public TableFieldContainer FieldCont
        {
            get { return m_FieldCont; }
            set { m_FieldCont = value; }
        }

        /// <summary>
        /// Gets or sets the constraint cont.
        /// </summary>
        /// <value>The constraint cont.</value>
        public ConstraintContainer ConstraintCont
        {
            get { return m_ConstraintCont; }
            set { m_ConstraintCont = value; }
        }

        /// <summary>
        /// Gets or sets the data cont.
        /// </summary>
        /// <value>The data cont.</value>
        [XmlIgnore]
        public DataContainer DataCont
        {
            get { return m_DataCont; }
            set { m_DataCont = value; }
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
                result = (result * 397) ^ m_Name.GetHashCode();
                // result = (result * 397) ^ m_Rows.Count;
                result = (result * 397) ^ ("Table").ToString().GetHashCode();
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
            Node.Nodes.Add(ConstraintCont.toTreeNode());

            if (m_DataCont != null)
            {
                Node.Nodes.Add(DataCont.toTreeNode());
            }

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
        public Modification qualifyVersus(Table target, Direction dir)
        {
            Qualifier = Modification.None;

            //----------------------------------------------------------------

            Modification mod = FieldCont.qualifyVersus(target.FieldCont, dir);

            if (Qualifier == Modification.None && mod != Modification.None)
            {
                Qualifier = mod;
            }

            //----------------------------------------------------------------

            mod = ConstraintCont.qualifyVersus(target.ConstraintCont, dir);

            if (Qualifier == Modification.None && mod != Modification.None)
            {
                Qualifier = mod;
            }

            //----------------------------------------------------------------

            if (m_DataCont != null && target.DataCont != null)
            {
                mod = DataCont.qualifyVersus(target.DataCont, dir);

                if (Qualifier == Modification.None && mod != Modification.None)
                {
                    Qualifier = mod;
                }
            }

            //----------------------------------------------------------------

            return Qualifier;
        }

        /// <summary>
        /// Resolves the links.
        /// </summary>
        /// <param name="Twin">The twin.</param>
        public void ResolveLinks(Base NewParent)
        {
            m_Parent = NewParent;
            m_FieldCont.ResolveLinks(this);
            m_ConstraintCont.ResolveLinks(this);
        }

        #endregion
    }
}
