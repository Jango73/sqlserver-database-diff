
using System;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SQLServerDatabaseDiff
{
    [Serializable]
    public class Row : Qualifiable
    {
        #region Fields

        private List<Value> m_Values;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Row"/> class.
        /// </summary>
        public Row()
        {
            m_Values = new List<Value>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        public List<Value> Values
        {
            get { return m_Values; }
            set { m_Values = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Makes a tree node from this object.
        /// </summary>
        /// <returns></returns>
        public override TreeNode toTreeNode()
        {
            TreeNode node = new TreeNode();
            String text = String.Empty;
            foreach (Value val in m_Values)
            {
                text += "[" + val.Data + "] ";
            }
            node.Text = text;
            node.Tag = this;
            return node;
        }

        #endregion
    }
}
