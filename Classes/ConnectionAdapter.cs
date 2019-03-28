using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SQLServerDatabaseDiff
{
    [Serializable]
    public class ConnectionAdapter
    {
        private String m_Host;
        private String m_User;
        private String m_Pass;
        private String m_Name;
        private Boolean m_WindowsAuth;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionAdapter"/> class.
        /// </summary>
        public ConnectionAdapter()
        {
            m_Host = String.Empty;
            m_User = String.Empty;
            m_Pass = String.Empty;
            m_Name = String.Empty;
            m_WindowsAuth = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionAdapter"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        public ConnectionAdapter(ConnectionAdapter target)
            : this()
        {
            m_Host = target.m_Host;
            m_User = target.m_User;
            m_Pass = target.m_Pass;
            m_Name = target.m_Name;
            m_WindowsAuth = target.m_WindowsAuth;
        }

        public String Host
        {
            get { return m_Host; }
            set { m_Host = value; }
        }

        public String User
        {
            get { return m_User; }
            set { m_User = value; }
        }

        public String Pass
        {
            get { return m_Pass; }
            set { m_Pass = value; }
        }

        public String Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public Boolean WindowsAuth
        {
            get { return m_WindowsAuth; }
            set { m_WindowsAuth = value; }
        }

        /// <summary>
        /// Connects this instance.
        /// </summary>
        /// <returns></returns>
        public SqlConnection connect()
        {
            String ConnectionText = String.Empty;

            if (Host == String.Empty) return null;

            ConnectionText += "server=" + Host + ";";
            ConnectionText += "user id=" + User + ";";
            ConnectionText += "password=" + Pass + ";";
            ConnectionText += "database=" + Name + ";";

            if (WindowsAuth)
            {
                ConnectionText += "Trusted_Connection=yes;connection timeout=2";
            }
            else
            {
                ConnectionText += "Trusted_Connection=no;connection timeout=2";
            }

            SqlConnection myConnection = new SqlConnection(ConnectionText);

            try
            {
                myConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

            return myConnection;
        }

        /// <summary>
        /// Connects the no base.
        /// </summary>
        /// <returns></returns>
        public SqlConnection connectWithoutDataBaseName()
        {
            return connectWithoutDataBaseName(2);
        }

        /// <summary>
        /// Connects without specifying a database name.
        /// </summary>
        /// <param name="TimeOut">The time out.</param>
        /// <returns></returns>
        public SqlConnection connectWithoutDataBaseName(int TimeOut)
        {
            String ConnectionText = String.Empty;

            if (Host == String.Empty) return null;

            ConnectionText += "server=" + Host + ";";
            ConnectionText += "user id=" + User + ";";
            ConnectionText += "password=" + Pass + ";";

            if (WindowsAuth)
            {
                ConnectionText += "Trusted_Connection=yes;connection timeout=" + TimeOut.ToString();
            }
            else
            {
                ConnectionText += "Trusted_Connection=no;connection timeout=" + TimeOut.ToString();
            }

            SqlConnection myConnection = new SqlConnection(ConnectionText);

            try
            {
                myConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

            return myConnection;
        }

        /// <summary>
        /// Executes the select.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static Table executeSelect(SqlConnection connection, String text)
        {
            SqlCommand myCommand = new SqlCommand(text, connection);

            Table table = new Table();

            SqlDataReader myReader = null;

            try
            {
                myReader = myCommand.ExecuteReader();

                Boolean fieldsDone = false;

                while (myReader.Read())
                {
                    Row row = new Row();

                    for (int i = 0; i < myReader.FieldCount; i++)
                    {
                        if (fieldsDone == false)
                        {
                            TableField f = new TableField();
                            f.Name = myReader.GetName(i);
                            table.FieldCont.Fields.Add(f);
                        }

                        Value val = new Value();

                        val.Type = myReader.GetDataTypeName(i);

                        if (myReader.IsDBNull(i))
                        {
                            val.Data = "NULL";
                        }
                        else if (val.Type == "bit")
                        {
                            /*
                            Byte [] buf = new Byte [2];
                            myReader.GetBytes(i, 0, buf, 0, 1);
                            val.Data = buf.ToString();
                            */
                            val.Data = myReader.GetBoolean(i).ToString();
                        }
                        else if (val.Type == "tinyint")
                        {
                            val.Data = myReader.GetByte(i).ToString();
                        }
                        else if (val.Type == "smallint")
                        {
                            val.Data = myReader.GetInt16(i).ToString();
                        }
                        else if (val.Type == "int")
                        {
                            val.Data = myReader.GetInt32(i).ToString();
                        }
                        else if (val.Type == "bigint")
                        {
                            val.Data = myReader.GetInt64(i).ToString();
                        }
                        else if (val.Type == "float")
                        {
                            val.Data = myReader.GetDouble(i).ToString();
                        }
                        else if (val.Type == "decimal")
                        {
                            val.Data = myReader.GetDecimal(i).ToString();
                        }
                        else if (val.Type == "numeric")
                        {
                            val.Data = myReader.GetDouble(i).ToString();
                        }
                        else if (val.Type == "text" || val.Type == "char" || val.Type == "varchar")
                        {
                            val.Data = myReader.GetString(i);
                        }
                        else if (val.Type == "ntext" || val.Type == "nchar" || val.Type == "nvarchar")
                        {
                            val.Data = myReader.GetString(i);
                        }
                        else if (val.Type == "datetime")
                        {
                            val.Data = myReader.GetDateTime(i).ToString();
                        }
                        else
                        {
                            val.Data = String.Empty;
                        }

                        row.Values.Add(val);
                    }

                    table.Rows.Add(row);

                    fieldsDone = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (myReader != null) myReader.Close();
            }

            return table;
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static String executeScalar(SqlConnection connection, String text)
        {
            SqlCommand myCommand = new SqlCommand(text, connection);

            object res = myCommand.ExecuteScalar();

            if (res != null) return res.ToString();

            return String.Empty;
        }

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="text">The text.</param>
        public static void executeNonQuery(SqlConnection connection, String text)
        {
            SqlCommand myCommand = new SqlCommand(text, connection);

            myCommand.ExecuteNonQuery();
        }
    }
}
