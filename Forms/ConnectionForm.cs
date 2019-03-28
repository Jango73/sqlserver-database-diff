using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SQLServerDatabaseDiff
{
    public partial class ConnectionForm : Form
    {
        private ConnectionAdapter m_Connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionForm"/> class.
        /// </summary>
        public ConnectionForm()
        {
            InitializeComponent();

            m_Connection = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionForm"/> class.
        /// </summary>
        /// <param name="Connection">The connection.</param>
        public ConnectionForm(ConnectionAdapter Connection)
            : this()
        {
            m_Connection = Connection;

            ConnectionCont.TB_HostName.Text = m_Connection.Host;
            ConnectionCont.TB_UserName.Text = m_Connection.User;
            ConnectionCont.TB_UserPass.Text = m_Connection.Pass;
            ConnectionCont.TB_Database.Text = m_Connection.Name;
            ConnectionCont.CHK_WindowsAuth.Checked = m_Connection.WindowsAuth;
        }

        /// <summary>
        /// Handles the Click event of the Button_OK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Button_OK_Click(object sender, EventArgs e)
        {
            m_Connection.Host = ConnectionCont.TB_HostName.Text;
            m_Connection.User = ConnectionCont.TB_UserName.Text;
            m_Connection.Pass = ConnectionCont.TB_UserPass.Text;
            m_Connection.Name = ConnectionCont.TB_Database.Text;
            m_Connection.WindowsAuth = ConnectionCont.CHK_WindowsAuth.Checked;

            Close();
        }

        /// <summary>
        /// Handles the Click event of the Button_Cancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
