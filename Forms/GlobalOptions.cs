using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SQLServerDatabaseDiff
{
    public partial class GlobalOptions : Form
    {
        private Configuration m_Configuration;

        public GlobalOptions()
        {
            InitializeComponent();
        }

        public GlobalOptions(Configuration conf)
            : this()
        {
            m_Configuration = conf;

            ShowReportOnAnalyze.Checked = m_Configuration.ShowReportOnAnalyze;
            HideUnmodifiedByDefault.Checked = m_Configuration.HideUnmodifiedByDefault;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            if (DialogResult == DialogResult.OK)
            {
                m_Configuration.ShowReportOnAnalyze = ShowReportOnAnalyze.Checked;
                m_Configuration.HideUnmodifiedByDefault = HideUnmodifiedByDefault.Checked;
            }
        }
    }
}
