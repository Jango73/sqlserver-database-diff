
using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SQLServerDatabaseDiff
{
    [Serializable]
    public class Field : Qualifiable
    {
        #region Fields

        private String m_Name;
        private String m_Type;
        private Boolean m_Nullable;
        private Boolean m_Identity;
        private List<Constraint> m_Constraints;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Field"/> class.
        /// </summary>
        public Field()
        {
            m_Name = String.Empty;
            m_Type = String.Empty;
            m_Nullable = true;
            m_Identity = false;
            m_Constraints = new List<Constraint>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Field"/> class.
        /// </summary>
        /// <param name="NewName">The new name.</param>
        /// <param name="NewType">The new type.</param>
        /// <param name="NewNullable">if set to <c>true</c> [new nullable].</param>
        /// <param name="NewIdentity">if set to <c>true</c> [new identity].</param>
        public Field(String NewName, String NewType, Boolean NewNullable, Boolean NewIdentity)
            : this()
        {
            m_Name = NewName;
            m_Type = NewType;
            m_Nullable = NewNullable;
            m_Identity = NewIdentity;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Field"/> class.
        /// </summary>
        /// <param name="Target">The target.</param>
        public Field(Field Target)
            : this()
        {
            GhostCopy(Target);

            m_Name = Target.Name;
            m_Type = Target.Type;
            m_Nullable = Target.Nullable;
            m_Identity = Target.Identity;
        }

        #endregion

        #region Properties

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
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public String Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Field"/> is nullable.
        /// </summary>
        /// <value><c>true</c> if nullable; otherwise, <c>false</c>.</value>
        public Boolean Nullable
        {
            get { return m_Nullable; }
            set { m_Nullable = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Field"/> is identity.
        /// </summary>
        /// <value><c>true</c> if identity; otherwise, <c>false</c>.</value>
        public Boolean Identity
        {
            get { return m_Identity; }
            set { m_Identity = value; }
        }

        /// <summary>
        /// Gets or sets the constraints.
        /// </summary>
        /// <value>The constraints.</value>
        public List<Constraint> Constraints
        {
            get { return m_Constraints; }
            set { m_Constraints = value; }
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
            TreeNode node = new TreeNode();

            node.Text = Name + " " + Type;
            if (Nullable == false) node.Text += " NOT NULL";
            if (Identity == true) node.Text += " IDENTITY";

            Base.SetNodeImageFromQualifier(node, Qualifier, IsGhost);

            //----------------------------------------------------------------

            foreach (Constraint cnst in m_Constraints)
            {
                node.Nodes.Add(cnst.toTreeNode());
            }

            //----------------------------------------------------------------

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
        public Modification qualifyVersus(Field target, Direction dir)
        {
            Qualifier = Modification.None;

            if (Type != target.Type && dir == Direction.In)
            {
                Qualifier = Modification.Modified;
            }

            if (Nullable != target.Nullable && dir == Direction.In)
            {
                Qualifier = Modification.Modified;
            }

            if (Identity != target.Identity && dir == Direction.In)
            {
                Qualifier = Modification.Modified;
            }

            return Qualifier;
        }

        #endregion
    }
}
