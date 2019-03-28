using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SQLServerDatabaseDiff
{
    public class BaseTreeData
    {
        public Base m_Base;
        public TreeView m_Twin;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTreeData"/> class.
        /// </summary>
        /// <param name="NewBase">The new base.</param>
        /// <param name="NewTwin">The new twin.</param>
        public BaseTreeData(Base NewBase, TreeView NewTwin)
        {
            m_Base = NewBase;
            m_Twin = NewTwin;
        }
    }
}
