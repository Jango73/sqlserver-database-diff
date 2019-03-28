using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SQLServerDatabaseDiff
{
    public partial class UpgradeOptions : Form
    {
        private Configuration m_Configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeOptions"/> class.
        /// </summary>
        public UpgradeOptions()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeOptions"/> class.
        /// </summary>
        /// <param name="conf">The conf.</param>
        public UpgradeOptions(Configuration conf) : this()
        {
            m_Configuration = conf;

            AllowTableCreate.Checked = m_Configuration.UpgradeAllowTableCreate;
            AllowTableDrop.Checked = m_Configuration.UpgradeAllowTableDrop;
            AllowTableAlter.Checked = m_Configuration.UpgradeAllowTableAlter;

            AllowViewCreate.Checked = m_Configuration.UpgradeAllowViewCreate;
            AllowViewDrop.Checked = m_Configuration.UpgradeAllowViewDrop;
            AllowViewAlter.Checked = m_Configuration.UpgradeAllowViewAlter;

            AllowFieldCreate.Checked = m_Configuration.UpgradeAllowFieldCreate;
            AllowFieldDrop.Checked = m_Configuration.UpgradeAllowFieldDrop;
            AllowFieldAlter.Checked = m_Configuration.UpgradeAllowFieldAlter;

            AllowConstraintCreate.Checked = m_Configuration.UpgradeAllowConstraintCreate;
            AllowConstraintDrop.Checked = m_Configuration.UpgradeAllowConstraintDrop;
            AllowConstraintAlter.Checked = m_Configuration.UpgradeAllowConstraintAlter;

            AllowProcedureCreate.Checked = m_Configuration.UpgradeAllowProcedureCreate;
            AllowProcedureDrop.Checked = m_Configuration.UpgradeAllowProcedureDrop;
            AllowProcedureAlter.Checked = m_Configuration.UpgradeAllowProcedureAlter;

            AllowFunctionCreate.Checked = m_Configuration.UpgradeAllowFunctionCreate;
            AllowFunctionDrop.Checked = m_Configuration.UpgradeAllowFunctionDrop;
            AllowFunctionAlter.Checked = m_Configuration.UpgradeAllowFunctionAlter;
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
                m_Configuration.UpgradeAllowTableCreate = AllowTableCreate.Checked;
                m_Configuration.UpgradeAllowTableDrop = AllowTableDrop.Checked;
                m_Configuration.UpgradeAllowTableAlter = AllowTableAlter.Checked;

                m_Configuration.UpgradeAllowViewCreate = AllowViewCreate.Checked;
                m_Configuration.UpgradeAllowViewDrop = AllowViewDrop.Checked;
                m_Configuration.UpgradeAllowViewAlter = AllowViewAlter.Checked;

                m_Configuration.UpgradeAllowFieldCreate = AllowFieldCreate.Checked;
                m_Configuration.UpgradeAllowFieldDrop = AllowFieldDrop.Checked;
                m_Configuration.UpgradeAllowFieldAlter = AllowFieldAlter.Checked;

                m_Configuration.UpgradeAllowConstraintCreate = AllowConstraintCreate.Checked;
                m_Configuration.UpgradeAllowConstraintDrop = AllowConstraintDrop.Checked;
                m_Configuration.UpgradeAllowConstraintAlter = AllowConstraintAlter.Checked;

                m_Configuration.UpgradeAllowProcedureCreate = AllowProcedureCreate.Checked;
                m_Configuration.UpgradeAllowProcedureDrop = AllowProcedureDrop.Checked;
                m_Configuration.UpgradeAllowProcedureAlter = AllowProcedureAlter.Checked;

                m_Configuration.UpgradeAllowFunctionCreate = AllowFunctionCreate.Checked;
                m_Configuration.UpgradeAllowFunctionDrop = AllowFunctionDrop.Checked;
                m_Configuration.UpgradeAllowFunctionAlter = AllowFunctionAlter.Checked;
            }
        }
    }
}
