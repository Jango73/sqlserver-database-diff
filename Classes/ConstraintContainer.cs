
using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SQLServerDatabaseDiff
{
    [Serializable]
    public class ConstraintContainer : Qualifiable
    {
        #region Fields

        private List<Constraint> m_Constraints;

        [NonSerialized]
        private Table m_Parent;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstraintContainer"/> class.
        /// </summary>
        public ConstraintContainer()
        {
            m_Parent = null;
            m_Constraints = new List<Constraint>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstraintContainer"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public ConstraintContainer(Table parent)
            : this()
        {
            m_Parent = parent;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstraintContainer"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="Target">The target.</param>
        public ConstraintContainer(Table parent, ConstraintContainer Target)
            : this(parent)
        {
            GhostCopy(Target);

            foreach (Constraint Const in Target.Constraints)
            {
                m_Constraints.Add(new Constraint(Const));
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the constraints.
        /// </summary>
        /// <value>The constraints.</value>
        public List<Constraint> Constraints
        {
            get { return m_Constraints; }
            set { m_Constraints = value; }
        }

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
                result = (result * 397) ^ ("ConstraintContainer").ToString().GetHashCode();
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
            node.Text = "Constraints";
            Base.SetNodeImageFromQualifier(node, Qualifier, IsGhost);

            foreach (Constraint c in Constraints)
            {
                node.Nodes.Add(c.toTreeNode());
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
        public Modification qualifyVersus(ConstraintContainer target, Direction dir)
        {
            Qualifier = Modification.None;

            foreach (Constraint src in m_Constraints)
            {
                bool found = false;

                foreach (Constraint tgt in target.Constraints)
                {
                    if (src.Name == tgt.Name && tgt.IsGhost == false)
                    {
                        Modification mod = src.qualifyVersus(tgt, dir);

                        if (Qualifier == Modification.None && mod != Modification.None)
                        {
                            Qualifier = Modification.Modified;
                        }

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

                    if (src.Twin == null) target.Constraints.Add(new Constraint(src));

                    Qualifier = Modification.Modified;
                }
            }

            return Qualifier;
        }

        /// <summary>
        /// Resolves the links.
        /// </summary>
        /// <param name="NewParent">The new parent.</param>
        public void ResolveLinks(Table NewParent)
        {
            m_Parent = NewParent;

            foreach (Constraint Const in m_Constraints)
            {
                Const.ResolveLinks(NewParent);
            }
        }

        #endregion
    }
}
