using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SQLPIPE;
using System.Data.SqlClient;
using DATAPIPE;

namespace MicroLearning.Services
{
    public class Chat
    {
        public DataSet GetAllOnlineUsers()
        {
            string sqlQuery1 = "SELECT a.OnlineUserId, b.UserName FROM OnlineUsers a Join Users b on a.OnlineUserId = b.UserId where a.Status = 1 and a.OnlineUserEndTime < DATEADD(minute,5,GETDATE())";

            SQLExecuteBase obj = DataAccessService.Instance.SQLExecuteBase;
            SqlConnection sqlConn = obj.CreateConnection(DataAccessService.Instance.SQLAccess.ConnectionString);

            DataSet ds = obj.ExecuteQuery(sqlConn, sqlQuery1);

            return ds;
        }


        public bool SendMessage(Guid fromUserId, Guid toUserId, String comment)
        {
            if (!(fromUserId.ToString() == "00000000-0000-0000-0000-000000000000") || !(toUserId.ToString() == "00000000-0000-0000-0000-000000000000"))
            {
                Guid guid = Guid.NewGuid();
                string sqlQuery1 = "INSERT INTO Chat (ChatId, FromUserId, ToUserId, Comment, ChatingTime) VALUES" +
                    "('{0}','" + fromUserId + "','" + toUserId + "','" + comment + "'" + ", GETDATE())";

                SQLExecuteBase obj = DataAccessService.Instance.SQLExecuteBase;
                SqlConnection sqlConn = obj.CreateConnection(DataAccessService.Instance.SQLAccess.ConnectionString);

                int rowsAffected = (int)obj.ExecuteNonQuery(sqlConn, String.Format(sqlQuery1, guid));

                if (rowsAffected != 0)
                {
                    return true;
                }
            }

            return false;
        }

        public DataSet GetAllMessages(Guid userId, Guid chatUserGuid)
        {
            //string sqlQuery1 = "SELECT Comment FROM Chat where (FromUserId = '{0}' or ToUserId = '{1}') and ChatingTime > DATEADD(minute,-10,GETDATE())";
            string sqlQuery1 = "Select CH2.FromUser As FromUser, US1.UserName As ToUser, CH2.Comment, CH2.ChatingTime As ChatingTime"+
                " FROM Users US1 JOIN" +
                "(Select US2.UserName As FromUser, CH.ToUserId As ToUserId, CH.Comment As Comment, CH.ChatingTime As ChatingTime"+
                " FROM Users US2 JOIN"+
                "(SELECT FromUserId, ToUserId, Comment, ChatingTime FROM Chat WHERE ((FromUserId = '" +
                    userId + "' and ToUserId = '" + chatUserGuid + "') or (FromUserId = '" 
                    + chatUserGuid + "' and ToUserId = '" + userId + "' )) and ChatingTime > DATEADD(minute,-100,GETDATE())" +
                ") CH" +
                 " ON US2.UserId = CH.FromUserId) CH2"+
                " ON US1.UserId = CH2.ToUserId order by CH2.ChatingTime";

            SQLExecuteBase obj = DataAccessService.Instance.SQLExecuteBase;
            SqlConnection sqlConn = obj.CreateConnection(DataAccessService.Instance.SQLAccess.ConnectionString);

            DataSet ds = obj.ExecuteQuery(sqlConn, String.Format(sqlQuery1, userId, userId));

            return ds;
        }


        public bool CreateOnlineUsers(Guid onlineUserId)
        {
            Guid guid = Guid.NewGuid();


            string sqlQuery1 = " Update OnlineUsers Set OnlineUserEndTime = DATEADD(minute,5,GETDATE()) " +
                "where OnlineUserId = '" + onlineUserId + "' AND Status = 1 "+
                "AND OnlineUserEndTime > DATEADD(minute,-5,GETDATE())";
            string sqlQuery2 = " Update OnlineUsers Set Status = 0 " +
                    "where OnlineUserId = '" + onlineUserId + "'";
            string sqlQuery3 = "INSERT INTO OnlineUsers (OnlineUserId, OnlineUserEndTime, Status) VALUES" +
                "('" + onlineUserId + "', DATEADD(minute,5,GETDATE()) ,'1')";

            SQLExecuteBase obj = DataAccessService.Instance.SQLExecuteBase;
            SqlConnection sqlConn = obj.CreateConnection(DataAccessService.Instance.SQLAccess.ConnectionString);

            int rowsAffected = (int)obj.ExecuteNonQuery(sqlConn, sqlQuery1);

            if (rowsAffected == 0)
            {
                obj.ExecuteNonQuery(sqlConn, sqlQuery2);
                obj.ExecuteNonQuery(sqlConn, sqlQuery3);
            }
            
            return true;
        }


    }
}