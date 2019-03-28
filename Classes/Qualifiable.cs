
using System;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SQLServerDatabaseDiff
{
    [Serializable]
    public class Qualifiable
    {
        public enum Modification
        {
            None = 0,
            Created = 1,
            Deleted = 2,
            Modified = 3
        }

        #region Fields

        private Modification m_Qualifier;

        private Boolean m_IsGhost;

        [NonSerialized]
        private Qualifiable m_Twin;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Qualifiable"/> class.
        /// </summary>
        public Qualifiable()
        {
            m_Qualifier = Modification.None;
            m_Twin = null;
            m_IsGhost = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the qualifier.
        /// </summary>
        /// <value>The qualifier.</value>
        public Modification Qualifier
        {
            get { return m_Qualifier; }
            set { m_Qualifier = value; }
        }

        /// <summary>
        /// Gets or sets the twin.
        /// </summary>
        /// <value>The twin.</value>
        [XmlIgnore]
        public Qualifiable Twin
        {
            get { return m_Twin; }
            set { m_Twin = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is ghost.
        /// </summary>
        /// <value><c>true</c> if this instance is ghost; otherwise, <c>false</c>.</value>
        public Boolean IsGhost
        {
            get { return m_IsGhost; }
            set { m_IsGhost = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Ghosts the copy.
        /// </summary>
        /// <param name="Target">The target.</param>
        protected void GhostCopy(Qualifiable Target)
        {
            Twin = Target;
            Target.Twin = this;
            IsGhost = true;
        }

        /// <summary>
        /// Qualifies this object against another one.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="dir">The direction.</param>
        /// <returns></returns>
        public virtual Modification qualifyVersus(Qualifiable target, Direction dir)
        {
            return Modification.None;
        }

        /// <summary>
        /// Makes a tree node from this object.
        /// </summary>
        /// <returns></returns>
        public virtual TreeNode toTreeNode()
        {
            return new TreeNode();
        }

        #endregion
    }
}
