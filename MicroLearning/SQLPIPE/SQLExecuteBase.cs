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
        public static SQLExecuteBase GetSQLExecuteBaseStaticInstance()
        {
            SQLExecuteBase SQLExecuteBaseInstance = new SQLExecute();
            return SQLExecuteBaseInstance;
        }

        public SQLAccess GetSQLAccessStaticInstance(string connectionString)
        {
            return new SQLAccess(this, connectionString);
        }

        public abstract SqlConnection CreateConnection(String connectionString);
        public abstract DataSet ExecuteQuery(SqlConnection sqlConnection, String sqlQuery);
        public abstract int ExecuteNonQuery(SqlConnection sqlConnection, String sqlQuery);
        public abstract object ExecuteScalar(SqlConnection sqlConnection, String sqlQuery);
    }
}
