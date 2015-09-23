using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SQLPIPE;
using DATAPIPE;
using System.Data.SqlClient;
using System.Data;

namespace MicroLearning.Services
{
    public class Users
    {
        public bool CheckUserExist(string username)
        {
            string sqlQuery1 = "SELECT UserId FROM Users WHERE UserName = '" + username + "'";

            SQLExecuteBase obj = DataAccessService.Instance.SQLExecuteBase;
            SqlConnection sqlConn = obj.CreateConnection(DataAccessService.Instance.SQLAccess.ConnectionString);

            Object o = obj.ExecuteScalar(sqlConn, sqlQuery1);

            if (o != null)
            {
                return true;
            }

            return false;
        }

        public bool CheckUserAndPasswordValid(string username, string password)
        {
            string sqlQuery1 = "SELECT UserId FROM Users WHERE UserName = '" + username + "'";
            string sqlQuery2 = "SELECT Count(*) FROM Membership WHERE UserId = '{0}' and Password = '" + password + "'";

            SQLExecuteBase obj = DataAccessService.Instance.SQLExecuteBase;
            SqlConnection sqlConn = obj.CreateConnection(DataAccessService.Instance.SQLAccess.ConnectionString);

            object guidobj = obj.ExecuteScalar(sqlConn, sqlQuery1);
            if (guidobj != null)
            {
                Guid guid;
                Guid.TryParse(guidobj.ToString(), out guid);
                if (guid != Guid.Empty)
                {
                    int count = (int)obj.ExecuteScalar(sqlConn, String.Format(sqlQuery2, guid));

                    if (count > 0)
                    {
                        Chat chat = new Chat();
                        chat.CreateOnlineUsers(guid);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CreateNewUser(string username, string password)
        {
            bool isUserExists = CheckUserExist(username);
            if (!isUserExists)
            {
                Guid guid = Guid.NewGuid();
                string sqlQuery1 = "INSERT INTO Users (UserId, UserName, LUserName, LastActivityDate) VALUES" +
                    "('{0}','" + username + "','" + username.ToLower() + "'" + ",GETDATE())";
                string sqlQuery2 = "INSERT INTO Membership (UserId, Password, CreateDate) VALUES" +
                    "('{0}','" + password + "', GETDATE())";

                SQLExecuteBase obj = DataAccessService.Instance.SQLExecuteBase;
                SqlConnection sqlConn = obj.CreateConnection(DataAccessService.Instance.SQLAccess.ConnectionString);

                int rowsAffected = (int)obj.ExecuteNonQuery(sqlConn, String.Format(sqlQuery1, guid));

                if (rowsAffected != 0)
                {
                    int passwordCreatedCount = (int)obj.ExecuteNonQuery(sqlConn, String.Format(sqlQuery2, guid));

                    if (passwordCreatedCount != 0)
                    {
                        Chat chat = new Chat();
                        chat.CreateOnlineUsers(guid);
                        return true;
                    }
                }

            }
            return false;
        }


        public bool UpdatePassword(string newPassword, Guid guid)
        {
            string sqlQuery1 = "UPDATE Membership SET Password = '" + newPassword + "'," +
                " LastPasswordChangedDate = GETDATE() " +
                "where UserId = '" + guid + "'";

            SQLExecuteBase obj = DataAccessService.Instance.SQLExecuteBase;
            SqlConnection sqlConn = obj.CreateConnection(DataAccessService.Instance.SQLAccess.ConnectionString);

            int rowsAffected = (int)obj.ExecuteNonQuery(sqlConn, sqlQuery1);

            if (rowsAffected != 0)
            {
                return true;
            }

            return false;
        }

        public bool UpdateUserEmail(string eMail, Guid guid)
        {
            string sqlQuery1 = "UPDATE Membership SET Email ='" + eMail +"', " +
                "LEmail ='" + eMail.ToLower() + "'" +
                "where UserId = '" + guid + "'";

            SQLExecuteBase obj = DataAccessService.Instance.SQLExecuteBase;
            SqlConnection sqlConn = obj.CreateConnection(DataAccessService.Instance.SQLAccess.ConnectionString);

            int rowsAffected = (int)obj.ExecuteNonQuery(sqlConn, sqlQuery1);

            if (rowsAffected != 0)
            {
                return true;
            }

            return false;
        }

        public bool GetUserGuid(string username, out Guid guid)
        {
            
            string sqlQuery1 = "SELECT UserId FROM Users WHERE UserName = '" + username + "'";

            SQLExecuteBase obj = DataAccessService.Instance.SQLExecuteBase;
            SqlConnection sqlConn = obj.CreateConnection(DataAccessService.Instance.SQLAccess.ConnectionString);

            Object o = obj.ExecuteScalar(sqlConn, sqlQuery1);
            if (o != null)
            {
                guid = (Guid)o;
                Chat chat = new Chat();
                chat.CreateOnlineUsers(guid);
                return true;
            }

            guid = new Guid();
            return false;
        }
    }
}