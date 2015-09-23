using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SQLPIPE
{

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
    
            SqlDataAdapter sqlAdapater = new SqlDataAdapter();
            SqlCommand sqlCommand = new SqlCommand();
            //sqlQuery = "Select * from Users where LUserName=" + "karthik";
            DataSet dataSet = null;
            bool openConnection = false;

            try
            {
                //connectionString = CreateConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                openConnection = OpenConnection(sqlConnection);
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = sqlQuery;
                sqlCommand.CommandType = CommandType.Text;
                sqlAdapater.SelectCommand = sqlCommand;

                dataSet = new DataSet();
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

        public override int ExecuteNonQuery(SqlConnection sqlConnection, String sqlQuery)
        {
            SqlCommand sqlCommand = new SqlCommand();

            bool openConnection = false;
            int rowsAffected;

            try
            {
                openConnection = OpenConnection(sqlConnection);
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = sqlQuery;
                sqlCommand.CommandType = CommandType.Text;

                rowsAffected = sqlCommand.ExecuteNonQuery();

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

            return rowsAffected;
        }

        public override object ExecuteScalar(SqlConnection sqlConnection, String sqlQuery)
        {
            SqlCommand sqlCommand = new SqlCommand();

            bool openConnection = false;
            object singleValue;
            try
            {
                openConnection = OpenConnection(sqlConnection);
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = sqlQuery;
                sqlCommand.CommandType = CommandType.Text;

                singleValue = sqlCommand.ExecuteScalar();

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

            return singleValue;
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
