using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLPIPE;

namespace DATAPIPE
{
    public abstract class DataAccessServiceBase
    {
        private static DataAccessServiceBase instance = null;

        public static DataAccessServiceBase Instance
        {
            get
            {
                return instance;
            }
        }

        protected static void SetInstance(DataAccessServiceBase obj)
        {
            instance = obj;
        }

        public abstract void Init(string connString);
        public abstract SQLExecuteBase SQLExecuteBase{get;}
        public abstract SQLAccess SQLAccess { get; }
    }
}
