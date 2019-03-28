using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SQLServerDatabaseDiff
{
    [Serializable]
    public class ContainerBase : Qualifiable
    {
        #region Fields;

        protected Filter m_Filter;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerBase"/> class.
        /// </summary>
        public ContainerBase()
        {
            m_Filter = new Filter();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>The filter.</value>
        public Filter Filter
        {
            get { return m_Filter; }
            set { m_Filter = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Makes a tree node from this object.
        /// </summary>
        /// <returns></returns>
        public override TreeNode toTreeNode()
        {
            return null;
        }

        /// <summary>
        /// Qualifies this object against another one.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="dir">The direction.</param>
        /// <returns></returns>
        public Modification qualifyVersus(ContainerBase target, Direction dir)
        {
            Qualifier = Modification.None;

            return Qualifier;
        }

        #endregion
    }
}
