using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SQLServerDatabaseDiff
{
    public partial class ReportForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportForm"/> class.
        /// </summary>
        public ReportForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportForm"/> class.
        /// </summary>
        /// <param name="txt">The TXT.</param>
        public ReportForm(String txt)
            : this()
        {
            Output.Text = txt;
            Output.Select(0, 0);
        }
    }
}
