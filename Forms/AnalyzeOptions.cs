using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SQLServerDatabaseDiff
{
    public partial class AnalyzeOptions : Form
    {
        private Configuration m_Configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyzeOptions"/> class.
        /// </summary>
        public AnalyzeOptions()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyzeOptions"/> class.
        /// </summary>
        /// <param name="conf">The conf.</param>
        public AnalyzeOptions(Configuration conf)
            : this()
        {
            m_Configuration = conf;

            CHK_AnalyzeTables.Checked = m_Configuration.AnalyzeTables;
            CHK_AnalyzeViews.Checked = m_Configuration.AnalyzeViews;
            CHK_AnalyzeProcedures.Checked = m_Configuration.AnalyzeProcedures;
            CHK_AnalyzeFunctions.Checked = m_Configuration.AnalyzeFunctions;
            CHK_AnalyzeData.Checked = m_Configuration.AnalyzeData;
        }

        /// <summary>
        /// Déclenche l'événement <see cref="E:System.Windows.Forms.Form.FormClosed"></see>.
        /// </summary>
        /// <param name="e"><see cref="T:System.Windows.Forms.FormClosedEventArgs"></see> qui contient les données d'événement.</param>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            if (DialogResult == DialogResult.OK)
            {
                m_Configuration.AnalyzeTables = CHK_AnalyzeTables.Checked;
                m_Configuration.AnalyzeViews = CHK_AnalyzeViews.Checked;
                m_Configuration.AnalyzeProcedures = CHK_AnalyzeProcedures.Checked;
                m_Configuration.AnalyzeFunctions = CHK_AnalyzeFunctions.Checked;
                m_Configuration.AnalyzeData = CHK_AnalyzeData.Checked;
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the AnalyzeViews control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AnalyzeViews_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}
