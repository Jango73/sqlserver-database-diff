
using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SQLServerDatabaseDiff
{
    [Serializable]
    public class TableFieldContainer : Qualifiable
    {
        #region Fields

        private List<TableField> m_Fields;

        [NonSerialized]
        private Table m_Parent;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TableFieldContainer"/> class.
        /// </summary>
        public TableFieldContainer()
        {
            m_Parent = null;
            m_Fields = new List<TableField>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableFieldContainer"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public TableFieldContainer(Table parent)
            : this()
        {
            m_Parent = parent;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableFieldContainer"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="Target">The target.</param>
        public TableFieldContainer(Table parent, TableFieldContainer Target)
            : this(parent)
        {
            GhostCopy(Target);

            foreach (TableField Field in Target.Fields)
            {
                m_Fields.Add(new TableField(Field));
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        [XmlIgnore]
        public Table Parent
        {
            get { return m_Parent; }
            set { m_Parent = value; }
        }

        /// <summary>
        /// Gets or sets the fields.
        /// </summary>
        /// <value>The fields.</value>
        public List<TableField> Fields
        {
            get { return m_Fields; }
            set { m_Fields = value; }
        }

        /// <summary>
        /// Gets the different fields count.
        /// </summary>
        /// <value>The different fields count.</value>
        [XmlIgnore]
        public int DifferentFieldsCount
        {
            get { return DifferentFieds.Count; }
        }

        /// <summary>
        /// Gets the different fieds.
        /// </summary>
        /// <value>The different fieds.</value>
        [XmlIgnore]
        public List<Field> DifferentFieds
        {
            get
            {
                List<Field> ret = new List<Field>();

                foreach (Field f in m_Fields)
                {
                    if (f.Twin != null && (f.Qualifier == Modification.Modified || f.Twin.Qualifier == Modification.Modified))
                    {
                        ret.Add(f);
                    }
                }

                return ret;
            }
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
                result = (result * 397) ^ ("TableFieldContainer").ToString().GetHashCode();
                result = (result * 397) ^ m_Parent.Name.GetHashCode();
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
            TreeNode node = new TreeNode();
            node.Text = "Columns";
            Base.SetNodeImageFromQualifier(node, Qualifier, IsGhost);

            foreach (Field f in Fields)
            {
                node.Nodes.Add(f.toTreeNode());
            }

            node.Tag = this;
            if (IsGhost) node.ForeColor = Color.Gray;
            return node;
        }

        /// <summary>
        /// Qualifies this object against another one.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="dir">The direction.</param>
        /// <returns></returns>
        public Modification qualifyVersus(TableFieldContainer target, Direction dir)
        {
            Qualifier = Modification.None;

            foreach (Field src in m_Fields)
            {
                bool found = false;

                foreach (Field tgt in target.m_Fields)
                {
                    if (src.Name == tgt.Name && tgt.IsGhost == false)
                    {
                        Modification mod = src.qualifyVersus(tgt, dir);

                        if (Qualifier == Modification.None && mod != Modification.None)
                        {
                            Qualifier = Modification.Modified;
                        }

                        src.Twin = tgt;

                        found = true;
                        break;
                    }
                }

                if (found == false)
                {
                    if (dir == Direction.Out)
                    {
                        src.Qualifier = Modification.Deleted;
                    }
                    else
                    {
                        src.Qualifier = Modification.Created;
                    }

                    if (src.Twin == null) target.Fields.Add(new TableField(src));

                    Qualifier = Modification.Modified;
                }
            }

            return Qualifier;
        }

        /// <summary>
        /// Resolves the links.
        /// </summary>
        /// <param name="Twin">The twin.</param>
        public void ResolveLinks(Table NewParent)
        {
            m_Parent = NewParent;

            foreach (TableField Fld in m_Fields)
            {
                Fld.ResolveLinks(NewParent);
            }
        }

        #endregion
    }
}
