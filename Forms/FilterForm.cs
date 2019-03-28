using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SQLServerDatabaseDiff
{
    public partial class FilterForm : Form
    {
        private Filter m_Filter;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterForm"/> class.
        /// </summary>
        public FilterForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterForm"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        public FilterForm(Filter target) : this()
        {
            m_Filter = target;

            TB_Filter.Text = m_Filter.Text;
            CHK_HideUnmodified.Checked = m_Filter.HideUnmodified;
            CHK_HideModified.Checked = m_Filter.HideModified;
            CHK_HideCreated.Checked = m_Filter.HideCreated;
            CHK_HideDropped.Checked = m_Filter.HideDropped;
        }

        /// <summary>
        /// Déclenche l'événement <see cref="E:System.Windows.Forms.Form.FormClosed"></see>.
        /// </summary>
        /// <param name="e"><see cref="T:System.Windows.Forms.FormClosedEventArgs"></see> qui contient les données d'événement.</param>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            m_Filter.Text = TB_Filter.Text;
            m_Filter.HideUnmodified = CHK_HideUnmodified.Checked;
            m_Filter.HideModified = CHK_HideModified.Checked;
            m_Filter.HideCreated = CHK_HideCreated.Checked;
            m_Filter.HideDropped = CHK_HideDropped.Checked;
        }
    }
}
