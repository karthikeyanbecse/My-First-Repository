using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SQLPIPE;
using System.Data;
using System.Data.SqlClient;

namespace SQLPIPE
{

    public class SQLAccess
    {

        //private SQLExecuteBase sqlExecuteBase;
        private String connectionString;


        internal SQLAccess(SQLExecuteBase sqlExecuteBase, String connectionString)
        {
            //this.sqlExecuteBase = sqlExecuteBase;
            this.connectionString = connectionString;
        }

        public String ConnectionString
        {
            get
            {
                return this.connectionString;
            }
        }

        //public SQLExecuteBase SQLExecuteBase
        //{
        //    get
        //    {
        //        return this.sqlExecuteBase;
        //    }
        //}

        //internal DataSet ExecuteQuery(String sqlQuery)
        //{

        //    SqlConnection sqlConnection = this.SQLExecuteBase.CreateConnection(this.ConnectionString);
        //    return this.sqlExecuteBase.ExecuteQuery(sqlConnection, sqlQuery);
        //}



    }
}
