using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SQLServerDatabaseDiff
{
    public partial class QueryControl : UserControl
    {
        private ConnectionAdapter m_Connection;

        #region Constructors

        public QueryControl()
        {
            InitializeComponent();

            m_Connection = new ConnectionAdapter();

            this.Query.KeyDown += new KeyEventHandler(QueryControl_KeyDown);
        }

        #endregion

        public ConnectionAdapter Connection
        {
            get { return m_Connection; }
            set
            {
                m_Connection = value;
                GetDatabases();
            }
        }

        #region Events

        void QueryControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.Button_Exec_Click(null, null);
            }
        }

        private void Button_Exec_Click(object sender, EventArgs e)
        {
            SqlConnection conn = m_Connection.connect();

            if (conn != null)
            {
                try
                {
                    String query = String.Empty;

                    if (Query.SelectionLength == 0)
                    {
                        query = Query.Text;
                    }
                    else
                    {
                        query = Query.SelectedText;
                    }

                    Table tbl = ConnectionAdapter.executeSelect(conn, query);

                    if (tbl != null)
                    {
                        DataGrid.Rows.Clear();
                        DataGrid.ColumnHeadersHeight = 40;
                        DataGrid.ColumnCount = tbl.FieldCont.Fields.Count;
                        DataGrid.RowCount = tbl.Rows.Count + 1;
                        // DataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

                        for (int i = 0; i < tbl.FieldCont.Fields.Count; i++)
                        {
                            DataGrid.Columns[i].HeaderCell.Value = tbl.FieldCont.Fields[i].Name;
                        }

                        if (tbl.Rows.Count > 0)
                        {
                            for (int i = 0; i < tbl.Rows.Count; i++)
                            {
                                for (int j = 0; j < tbl.Rows[i].Values.Count; j++)
                                {
                                    DataGrid.Rows[i].Cells[j].Value = tbl.Rows[i].Values[j].Data;
                                }
                            }
                        }
                        else
                        {
                            DataGrid.Rows[0].Cells[0].Value = "No result";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
            }
        }

        private void Button_Connect_Click(object sender, EventArgs e)
        {
            ConnectionForm frm = new ConnectionForm(m_Connection);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                GetDatabases();
            }
        }

        private void Button_Load_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofn = new OpenFileDialog();

            ofn.Filter = "SQL File (*.sql)|*.sql";

            if (ofn.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = File.OpenText(ofn.FileName);
                Query.Text = reader.ReadToEnd();
                reader.Close();
            }
        }

        private void Button_Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog ofn = new SaveFileDialog();

            ofn.Filter = "SQL File (*.sql)|*.sql";

            if (ofn.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = File.CreateText(ofn.FileName);
                writer.Write(Query.Text);
                writer.Close();
            }
        }

        private void DatabaseList_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Connection.Name = ((ComboBox)sender).SelectedItem.ToString();
        }

        #endregion

        #region Methods

        private void GetDatabases()
        {
            if (m_Connection == null) return;

            SqlConnection connection = m_Connection.connectWithoutDataBaseName();

            if (connection != null)
            {
                try
                {
                    Table tbl = ConnectionAdapter.executeSelect(connection, "SELECT NAME FROM sys.databases");

                    if (tbl != null)
                    {
                        DatabaseList.Items.Clear();

                        foreach (Row row in tbl.Rows)
                        {
                            DatabaseList.Items.Add(row.Values[0].Data);
                        }

                        if (DatabaseList.Items.Contains(m_Connection.Name))
                        {
                            DatabaseList.SelectedItem = m_Connection.Name;
                        }
                        else
                        {
                            if (DatabaseList.SelectedItem != null)
                            {
                                m_Connection.Name = DatabaseList.SelectedItem.ToString();
                            }
                            else if (DatabaseList.Items.Count > 0)
                            {
                                DatabaseList.SelectedItem = DatabaseList.Items[0];
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        #endregion
    }
}
