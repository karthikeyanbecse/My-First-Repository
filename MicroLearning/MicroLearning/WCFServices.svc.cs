using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DATAPIPE;
using SQLPIPE;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using MicroLearning.JsonObjects;
using System.Web;
using System.IO;
using System.ServiceModel.Web;

namespace MicroLearning.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WCFServices" in code, svc and config file together.
    public class WCFServices : IWCFServices
    {
        public string SignUpUser(string username, string password)
        {

            //CreateNewContent("MacroContent1", "");
            //CreateNewContent("MicroContent1", "MacroContent1");
            //CreateNewContent("MicroContent2", "MacroContent1");
            //CreateNewContent("MicroContent3", "MacroContent1");
            //CreateNewContent("MacroContent2", "");
            //CreateNewContent("MicroContent1", "MacroContent2");
            //CreateNewContent("MicroContent2", "MacroContent2");
            //CreateNewContent("MicroContent3", "MacroContent2");

            //GetAllContent();
            
            JSonResult result = new JSonResult();
            result.Status = false;

            Users obj = new Users();

            bool isUserValid = obj.CheckUserAndPasswordValid(username, password);

            //bool userCreated = obj.CreateNewUser("KARTHIK", "KARTHIK");
            //bool userCreated1 = obj.CreateNewUser("DIVI", "DIVI");
            //bool userCreated2 = obj.CreateNewUser("BALA", "BALA");
            //bool userCreated3 = obj.CreateNewUser("SEETHA", "SEETHA");
            //bool userCreated4 = obj.CreateNewUser("RAJNIKANTH", "RAJNIKANTH");
            //bool userCreated5 = obj.CreateNewUser("MICHAEL,SCHUMACKER", "MICHAEL,SCHUMACKER");
            //bool userCreated6 = obj.CreateNewUser("KAMALAHASAN", "KAMALAHASAN");
            //bool userCreated7 = obj.CreateNewUser("AJITH", "AJITH");
            //bool userCreated8 = obj.CreateNewUser("VIKRAM", "VIKRAM");
            //bool userCreated9 = obj.CreateNewUser("SURYA", "SURYA");
            //bool userCreated10 = obj.CreateNewUser("ARYA", "ARYA");
            //bool userCreated11 = obj.CreateNewUser("KOUNDAMANI", "KOUNDAMANI");
            //bool userCreated12 = obj.CreateNewUser("SENTHIL", "SENTHIL");
            //bool userCreated13 = obj.CreateNewUser("VADIVELU", "VADIVELU");
            //bool userCreated14 = obj.CreateNewUser("SANTHANAM", "SANTHANAM");

            //Guid karthikUserId,fromUserId1, fromUserId2;
            //obj.GetUserGuid("Karthik", out karthikUserId);
            //obj.GetUserGuid("BALA", out fromUserId1);
            //obj.GetUserGuid("DIVI", out fromUserId2);

            //Chat chat = new Chat();
            //chat.SendMessage(fromUserId1, karthikUserId, "Hi, Karthik");
            //chat.SendMessage(fromUserId1, karthikUserId, "How r u?");
            //chat.SendMessage(karthikUserId, fromUserId2, "Hi DIVI");
            //chat.SendMessage(fromUserId2, karthikUserId, "I am fine, dear.......");

            //DataSet ds1 = chat.GetAllOnlineUsers();
            //DataSet ds2 = chat.GetAllMessages(karthikUserId);

            Guid guid;
            obj.GetUserGuid(username, out guid);

            SignedInUser user = new SignedInUser();
            if (isUserValid)
            {
                user.SignedInUserId = guid.ToString();
                user.SignedInUserName = username;
                user.Status = "true";
            }
            else { user.Status = "false"; }

            result.Status = true;
            bool st = true;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonresult = serializer.Serialize(user);

            return jsonresult;
        }

        public string RegisterUser(string username, string password, string email)
        {
            JSonResult result = new JSonResult();
            result.Status = false;

            Users obj = new Users();
            bool userCreated = obj.CreateNewUser(username, password);

            Guid guid;
            obj.GetUserGuid(username, out guid);

            bool isUserValid = obj.CheckUserAndPasswordValid(username, password);

            SignedInUser user = new SignedInUser();
            if (isUserValid)
            {
                user.SignedInUserId = guid.ToString();
                user.SignedInUserName = username;
                user.Status = "true";
            }
            else { user.Status = "false"; }

            result.Status = true;
            bool st = true;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonresult = serializer.Serialize(user);

            return jsonresult;
        }

        public string SendMessage(string fromUser, string toUser, string message)
        {
            JSonResult result = new JSonResult();
            result.Status = false;

            Users obj = new Users();
            Guid fromUserId, toUserId;
            obj.GetUserGuid(toUser, out toUserId);
            obj.GetUserGuid(fromUser, out fromUserId);

            Chat chat = new Chat();
            result.Status = chat.SendMessage(fromUserId, toUserId, message);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonresult = serializer.Serialize(result.Status);

            return jsonresult;

        }

        public string GetAllOnlineUsers()
        {
            JSonResult result = new JSonResult();
            result.Status = false;

            List<OnlineUser> onlineUsers = new List<OnlineUser>();
            Chat chat = new Chat();

            List<OnlineUser> ou = new List<OnlineUser>();

            DataSet allUsersDataset = chat.GetAllOnlineUsers();
            foreach (DataRow dr in allUsersDataset.Tables[0].Rows)
            {
                onlineUsers.Add (new OnlineUser(dr[0].ToString(), dr[1].ToString()));
                ou.Add(new OnlineUser(dr[0].ToString(), dr[1].ToString()));
            }

            result.Status = true;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonresult = serializer.Serialize(ou);
            
            return jsonresult;
        }

        public string GetAllMessages(string username, string chatUser)
        {
            JSonResult result = new JSonResult();
            result.Status = false;

            
            List<GetMessages> allMessages = new List<GetMessages>();
            Chat chat = new Chat();
            Users user = new Users();

            Guid guid;
            user.GetUserGuid(username, out guid);
            Guid chatUserGuid;
            user.GetUserGuid(chatUser, out chatUserGuid);
            DataSet ds = chat.GetAllMessages(guid, chatUserGuid);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                GetMessages message = new GetMessages();
                message.FromUser = dr["FromUser"].ToString();
                message.Message = dr["Comment"].ToString();
                message.Time = dr["ChatingTime"].ToString();

                allMessages.Add(message);
            }

             
            result.Status = true;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonresult = serializer.Serialize(allMessages);

            return jsonresult;
        }

        public string CreateNewContent(string contentName, string masterContentName)
        {
            JSonResult result = new JSonResult();
            result.Status = false;

            Content content = new Content();
            result.Status = content.CreateNewContent(contentName, masterContentName);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonresult = serializer.Serialize(result.Status);

            return jsonresult;
        }
        public string GetAllContent()
        {
            JSonResult result = new JSonResult();
            result.Status = false;

            List<GetContent> allContent = new List<GetContent>();
            Content content = new Content();
            DataSet ds = content.GetAllContent();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                GetContent eachContent = new GetContent();
                eachContent.ContentId = dr["ContentId"].ToString();
                eachContent.ContentName = dr["ContentName"].ToString();
                eachContent.ReferenceContentId = dr["ReferenceContentId"].ToString();
                string masterContentName;
                content.GetContentName(Convert.ToInt32(dr["ReferenceContentId"]), out masterContentName);
                eachContent.MasterContentName = masterContentName;
                eachContent.ContentType = dr["ContentType"].ToString();

                allContent.Add(eachContent);
            }


            result.Status = true;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonresult = serializer.Serialize(allContent);

            return jsonresult;
        }

        public string Saveposition(string contentName, int x, int y, int width, int height)
        {
            JSonResult result = new JSonResult();
            result.Status = false;


            Content cont = new Content();
            cont.UpdateContentPosition(contentName, x + "_" + y + "_" + width + "_" + height);


            result.Status = true;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonresult = serializer.Serialize(result.Status);

            return jsonresult;
        
        }

        public string GetContentPosition(string contentName)
        {
            JSonResult result = new JSonResult();
            result.Status = false;


            Content cont = new Content();
            DataSet ds = cont.GetContentPosition(contentName);
            string position = "";
            List<GetContent> allContent = new List<GetContent>();
            foreach (DataRow dr in ds.Tables[0].Rows){
             GetContent eachContent = new GetContent();
                eachContent.Position = dr["Position"].ToString();
                eachContent.Image = dr["Image"].ToString();
                allContent.Add(eachContent);
            
            }
            result.Status = true;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonresult = serializer.Serialize(allContent);

            return jsonresult;
        }

    }
}
