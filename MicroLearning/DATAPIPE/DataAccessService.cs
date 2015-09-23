using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLPIPE;

namespace DATAPIPE
{
    public class DataAccessService : DataAccessServiceBase
    {
        String connectionString;
        private SQLPIPE.SQLAccess sqlAccess = null;
        private SQLPIPE.SQLExecuteBase sqlExecuteBase;

        public static DataAccessServiceBase Instance
        {
            get
            {
                if (null == DataAccessServiceBase.Instance)
                {
                    DataAccessServiceBase.SetInstance(new DataAccessService());
                    DataAccessServiceBase.Instance.Init("Data Source=KARTHIK-VAIO\\SQLEXPRESS;Initial Catalog=MicroLearning;Integrated Security=True");
                }
                return DataAccessServiceBase.Instance as DataAccessService;
            }
        }

        public override SQLExecuteBase SQLExecuteBase
        {
            get
            {
                if (null == sqlExecuteBase)
                {
                    this.sqlExecuteBase = SQLExecuteBase.GetSQLExecuteBaseStaticInstance();
                }
                return sqlExecuteBase;
            }
        }

        public override SQLAccess SQLAccess
        {
            get
            {
                if (null == sqlAccess)
                {
                    //connectionString = sqlExecute.CreateConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                    //String dataManagerXMLConfig = GetDataManagerConfigurationXml();
                    sqlAccess = this.SQLExecuteBase.GetSQLAccessStaticInstance(this.connectionString);
                    //dataAccess.UpdateCacheEvent += new EventHandler<ExecuteSPEventArgs>(dataManager_UpdateCacheEvent);
                    //dataAccess.Init(dataManagerXMLConfig);
                }
                return sqlAccess;
            }
        }

        public override void Init(string connString)
        {
            this.connectionString = connString;
        }
    }
}
