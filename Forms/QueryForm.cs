using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SQLServerDatabaseDiff
{
    public partial class QueryForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryForm"/> class.
        /// </summary>
        public QueryForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryForm"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="text">The text.</param>
        public QueryForm(ConnectionAdapter target, String text)
            : this()
        {
            Query.Connection = new ConnectionAdapter(target);
            Query.Query.Text = text;
            Query.Query.Select(0, 0);
        }
    }
}
