using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DATAPIPE;
using System.Data.SqlClient;
using SQLPIPE;

namespace MicroLearning.Services
{
    public class Content
    {

        public bool CreateNewContent(string contentName, string masterContent)
        {
            string contentType = "";
            if (masterContent.Trim() == "")
                contentType = "Ma";
            else
                contentType = "Mi";
            int testContentExists;
            GetContentId(contentName, out testContentExists);
            if (testContentExists == -1)
            {
                string sqlQuery1 = "INSERT INTO Content (ContentType, ContentName) VALUES" +
                    "('" + contentType + "','" + contentName + "')";

                SQLExecuteBase obj = DataAccessService.Instance.SQLExecuteBase;
                SqlConnection sqlConn = obj.CreateConnection(DataAccessService.Instance.SQLAccess.ConnectionString);

                int rowsAffected = (int)obj.ExecuteNonQuery(sqlConn, String.Format(sqlQuery1));

                if (rowsAffected != 0)
                {
                    int masterContentId = -1;
                    int contentId = -1;
                    GetContentId(contentName, out contentId);
                    string sqlQuery2;
                    if (masterContent != "")
                    {
                        GetContentId(masterContent, out masterContentId);
                        sqlQuery2 = "UPDATE Content  SET ReferenceContentId = " + masterContentId + " where ContentId = " + contentId;
                    }
                    else
                    {
                        sqlQuery2 = "UPDATE Content  SET ReferenceContentId = " + contentId + " where ContentId = " + contentId;
                    }

                    SQLExecuteBase obj2 = DataAccessService.Instance.SQLExecuteBase;
                    SqlConnection sqlConn2 = obj2.CreateConnection(DataAccessService.Instance.SQLAccess.ConnectionString);

                    int rowsAffected1 = (int)obj2.ExecuteNonQuery(sqlConn2, sqlQuery2);

                    return true;
                }
            }
            return false;
        }

        public bool GetContentId(string contentName, out Int32 ContentId)
        {
            string sqlQuery1 = "SELECT ContentId FROM Content WHERE ContentName = '" + contentName + "'";

            SQLExecuteBase obj = DataAccessService.Instance.SQLExecuteBase;
            SqlConnection sqlConn = obj.CreateConnection(DataAccessService.Instance.SQLAccess.ConnectionString);

            Object o = obj.ExecuteScalar(sqlConn, sqlQuery1);
            if (o != null)
            {
                int Id = (Int32)o;
                ContentId = Id;
                return true;
            }

            ContentId = -1;    
            return false;
        }

        public bool GetContentName(int contentId, out string ContentName)
        {
            string sqlQuery1 = "SELECT ContentName FROM Content WHERE ContentId = " + contentId + "";

            SQLExecuteBase obj = DataAccessService.Instance.SQLExecuteBase;
            SqlConnection sqlConn = obj.CreateConnection(DataAccessService.Instance.SQLAccess.ConnectionString);

            Object o = obj.ExecuteScalar(sqlConn, sqlQuery1);
            if (o != null)
            {
                string name = (string)o;
                ContentName = name;
                return true;
            }

            ContentName = "";
            return false;
        }

        public DataSet GetAllContent()
        {
            string sqlQuery1 = "SELECT * from Content  Order by ReferenceContentId, ContentId";

            SQLExecuteBase obj = DataAccessService.Instance.SQLExecuteBase;
            SqlConnection sqlConn = obj.CreateConnection(DataAccessService.Instance.SQLAccess.ConnectionString);

            DataSet ds = obj.ExecuteQuery(sqlConn, sqlQuery1);

            return ds;
        }

        public bool UpdateContentPosition(string contentName, string position)
        {

                     string   sqlQuery2 = "UPDATE Content  SET position = '" + position + "' where ContentName = '" + contentName +"'";
                 

                    SQLExecuteBase obj2 = DataAccessService.Instance.SQLExecuteBase;
                    SqlConnection sqlConn2 = obj2.CreateConnection(DataAccessService.Instance.SQLAccess.ConnectionString);

                    int rowsAffected2 = (int)obj2.ExecuteNonQuery(sqlConn2, sqlQuery2);

                    return true;
        }

        public DataSet GetContentPosition(string contentName)
        {

            string sqlQuery2 = "SELECT Position, Image FROM Content WHERE ContentName = '" + contentName + "'";


            SQLExecuteBase obj2 = DataAccessService.Instance.SQLExecuteBase;
            SqlConnection sqlConn2 = obj2.CreateConnection(DataAccessService.Instance.SQLAccess.ConnectionString);

            DataSet ds = obj2.ExecuteQuery(sqlConn2, sqlQuery2);

            return ds;
        }
    }
}