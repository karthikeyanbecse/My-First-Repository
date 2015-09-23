using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SQLPIPE
{
    public abstract class SQLExecuteBase
    {
        public static SQLExecuteBase CreateDatabaseConnection()
        {
            SQLExecuteBase SQLExecuteBaseInstance = null;
            return SQLExecuteBaseInstance;
        }

        public abstract SqlConnection CreateConnection(String connectionString);
        public abstract DataSet ExecuteQuery(SqlConnection sqlConnection, String sqlQuery);
    }

    public class SQLExecute : SQLExecuteBase
    {
        public SQLExecute()
        {
        }

        public override SqlConnection CreateConnection(String connectionString)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = connectionString;
            return sqlConnection;
        }

        public override DataSet ExecuteQuery(SqlConnection sqlConnection, String sqlQuery)
        {
            String connectionString;
            SqlDataAdapter sqlAdapater = new SqlDataAdapter();
            SqlCommand sqlCommand = new SqlCommand();
            sqlQuery = "Select * from Users where LUserName=" + "karthik";
            DataSet dataSet = null;
            bool openConnection = false;

            try
            {
                //connectionString = CreateConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                openConnection = OpenConnection(sqlConnection);
                sqlCommand.Connection = sqlConnection;
                sqlAdapater.SelectCommand = sqlCommand;
                sqlAdapater.Fill(dataSet);
                sqlConnection.Close();

                if (true == openConnection)
                {
                    CloseConnection(sqlConnection);
                    openConnection = false;
                }
            }
            finally
            {
                if (true == openConnection)
                {
                    CloseConnection(sqlConnection);
                }
            }

            return dataSet;
        }

        private bool OpenConnection(SqlConnection sqlConnection)
        {
            bool openedConnection = false;

            try
            {
                if (ConnectionState.Closed == sqlConnection.State ||
                    ConnectionState.Broken == sqlConnection.State)
                {
                    sqlConnection.Open();
                    openedConnection = true;
                }
            }
            catch
            {
                if (null != sqlConnection && sqlConnection is SqlConnection)
                {
                    SqlConnection.ClearPool(sqlConnection as SqlConnection);

                    if (ConnectionState.Closed == sqlConnection.State ||
                        ConnectionState.Broken == sqlConnection.State)
                    {
                        sqlConnection.Open();
                        openedConnection = true;
                    }
                }
            }

            return openedConnection;
        }

        private void CloseConnection(SqlConnection sqlConnection)
        {
            if (null != sqlConnection && ConnectionState.Closed != sqlConnection.State)
            {
                sqlConnection.Close();
            }
        }

    }
}
