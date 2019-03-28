
using System;
using System.Text;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SQLServerDatabaseDiff
{
    [Serializable]
    public class TableField : Field
    {
        [NonSerialized]
        private Table m_Parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableField"/> class.
        /// </summary>
        public TableField()
        {
            m_Parent = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableField"/> class.
        /// </summary>
        /// <param name="NewName">The new name.</param>
        /// <param name="NewType">The new type.</param>
        /// <param name="NewNullable">if set to <c>true</c> [new nullable].</param>
        /// <param name="NewIdentity">if set to <c>true</c> [new identity].</param>
        public TableField(String NewName, String NewType, Boolean NewNullable, Boolean NewIdentity)
            : base(NewName, NewType, NewNullable, NewIdentity)
        {
            m_Parent = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableField"/> class.
        /// </summary>
        /// <param name="Target">The target.</param>
        public TableField(Field Target)
            : base(Target)
        {
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
                int result = base.GetHashCode();

                Table ParentTable = m_Parent;
                if (IsGhost && Twin != null) ParentTable = ((TableField)Twin).Parent;
                if (ParentTable != null) result = (result * 397) ^ ParentTable.GetHashCode();

                return result;
            }
        }

        /// <summary>
        /// Resolves the links.
        /// </summary>
        /// <param name="Twin">The twin.</param>
        public void ResolveLinks(Table NewParent)
        {
            m_Parent = NewParent;
        }
    }
}
