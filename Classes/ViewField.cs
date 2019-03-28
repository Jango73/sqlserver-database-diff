
using System;
using System.Text;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SQLServerDatabaseDiff
{
    [Serializable]
    public class ViewField : Field
    {
        [NonSerialized]
        private View m_Parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewField"/> class.
        /// </summary>
        public ViewField()
        {
            m_Parent = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewField"/> class.
        /// </summary>
        /// <param name="NewName">The new name.</param>
        /// <param name="NewType">The new type.</param>
        /// <param name="NewNullable">if set to <c>true</c> [new nullable].</param>
        /// <param name="NewIdentity">if set to <c>true</c> [new identity].</param>
        public ViewField(String NewName, String NewType, Boolean NewNullable, Boolean NewIdentity)
            : base(NewName, NewType, NewNullable, NewIdentity)
        {
            m_Parent = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewField"/> class.
        /// </summary>
        /// <param name="Target">The target.</param>
        public ViewField(ViewField Target)
            : base(Target)
        {
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        [XmlIgnore]
        public View Parent
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
                View ParentView = m_Parent;
                if (IsGhost && Twin != null) ParentView = ((ViewField)Twin).Parent;
                if (ParentView != null) result = (result * 397) ^ ParentView.GetHashCode();
                return result;
            }
        }
    }
}
