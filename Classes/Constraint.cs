
using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SQLServerDatabaseDiff
{
    [Serializable]
    public class Constraint : Qualifiable
    {
        public enum ConstraintType
        {
            Check = 0,
            Unique = 1,
            PrimaryKey = 2,
            ForeignKey = 3,
            Referential = 4
        }

        #region Fields

        private String m_Name;
        private String m_Text;
        private ConstraintType m_Type;
        private List<String> m_FieldNames;
        private String m_ReferencedTable;
        private List<String> m_ReferencedFieldNames;

        [NonSerialized]
        private Table m_Parent;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Constraint"/> class.
        /// </summary>
        public Constraint()
        {
            m_Name = String.Empty;
            m_Text = String.Empty;
            m_Type = ConstraintType.Check;
            m_FieldNames = new List<String>();
            m_ReferencedTable = String.Empty;
            m_ReferencedFieldNames = new List<String>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Constraint"/> class.
        /// </summary>
        /// <param name="Target">The target.</param>
        public Constraint(Constraint Target)
            : this()
        {
            GhostCopy(Target);

            m_Name = Target.m_Name;
            m_Text = Target.m_Text;
            m_Type = Target.m_Type;
            m_FieldNames = Target.m_FieldNames;
            m_ReferencedTable = Target.m_ReferencedTable;
            m_ReferencedFieldNames = Target.m_ReferencedFieldNames;
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
        public String Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public ConstraintType Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        /// <summary>
        /// Gets or sets the field names.
        /// </summary>
        /// <value>The field names.</value>
        public List<String> FieldNames
        {
            get { return m_FieldNames; }
            set { m_FieldNames = value; }
        }

        /// <summary>
        /// Gets or sets the referenced table.
        /// </summary>
        /// <value>The referenced table.</value>
        public String ReferencedTable
        {
            get { return m_ReferencedTable; }
            set { m_ReferencedTable = value; }
        }

        /// <summary>
        /// Gets or sets the referenced field names.
        /// </summary>
        /// <value>The referenced field names.</value>
        public List<String> ReferencedFieldNames
        {
            get { return m_ReferencedFieldNames; }
            set { m_ReferencedFieldNames = value; }
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

            Node.Text = String.Format("{0} {1} ({2})", Constraint.getFieldList(FieldNames), Name, ConstraintTypeToSQLConstraintType(Type));

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
        public Modification qualifyVersus(Constraint target, Direction dir)
        {
            Qualifier = Modification.None;

            if ((Type != target.Type || Text.ToLower() != target.Text.ToLower()) && dir == Direction.In)
            {
                Qualifier = Modification.Modified;
            }

            return Qualifier;
        }

        /// <summary>
        /// SQLs the type of the constraint type to constraint.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static ConstraintType SQLConstraintTypeToConstraintType(String text)
        {
            switch (text)
            {
                case "CHECK": return ConstraintType.Check;
                case "UNIQUE": return ConstraintType.Unique;
                case "PRIMARY KEY": return ConstraintType.PrimaryKey;
                case "FOREIGN KEY": return ConstraintType.ForeignKey;
                case "REFERENTIAL": return ConstraintType.Referential;
            }

            return ConstraintType.Check;
        }

        /// <summary>
        /// Constraints the type of the type to SQL constraint.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <returns></returns>
        public static String ConstraintTypeToSQLConstraintType(ConstraintType val)
        {
            switch (val)
            {
                case ConstraintType.Check: return "CHECK";
                case ConstraintType.Unique: return "UNIQUE";
                case ConstraintType.PrimaryKey: return "PRIMARY KEY";
                case ConstraintType.ForeignKey: return "FOREIGN KEY";
                case ConstraintType.Referential: return "REFERENTIAL";
            }

            return "CHECK";
        }

        /// <summary>
        /// Gets the field list.
        /// </summary>
        /// <param name="lst">The LST.</param>
        /// <returns></returns>
        public static String getFieldList(List<String> lst)
        {
            String ret = String.Empty;
            bool First = true;

            foreach (String item in lst)
            {
                if (First == false) ret += ", ";
                First = false;
                ret += "[" + item + "]";
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
                result = (result * 397) ^ m_Type.GetHashCode();

                Table ParentTable = m_Parent;
                if (IsGhost && Twin != null) ParentTable = ((Constraint)Twin).Parent;

                if (ParentTable != null)
                {
                    result = (result * 397) ^ ParentTable.GetHashCode();
                }

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
        /// Resolves the links.
        /// </summary>
        /// <param name="Twin">The twin.</param>
        public void ResolveLinks(Table NewParent)
        {
            m_Parent = NewParent;
        }

        #endregion
    }
}
