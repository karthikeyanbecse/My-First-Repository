using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using System.IO;

namespace MicroLearning.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWCFServices" in both code and config file together.
    [ServiceContract]
    public interface IWCFServices
    {
        [OperationContract]
        [WebInvoke(Method = "*",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "SignUpUser")]
        string SignUpUser(string username, string password);

        [OperationContract]
        [WebInvoke(Method = "*",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "RegisterUser")]
        string RegisterUser(string username, string password, string email);

        [OperationContract]
        [WebInvoke(Method = "*",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "SendMessage")]
        string SendMessage(string fromUser, string toUser, string message);

        [OperationContract]
        [WebInvoke(Method = "*",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "GetAllOnlineUsers")]
        string GetAllOnlineUsers();

        [OperationContract]
        [WebInvoke(Method = "*",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "GetAllMessages")]
        string GetAllMessages(string username, string chatUser);

        [OperationContract]
        [WebInvoke(Method = "*",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "CreateNewContent")]
        string CreateNewContent(string contentName, string masterContentName);

        [OperationContract]
        [WebInvoke(Method = "*",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "GetAllContent")]
        string GetAllContent();

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    UriTemplate = "UploadFile")]
        //string UploadFile(Stream bytes);

        [OperationContract]
        [WebInvoke(Method = "*",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "Saveposition")]
        string Saveposition(string contentName, int x, int y, int width, int height);

        [OperationContract]
        [WebInvoke(Method = "*",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "GetContentPosition")]
        string GetContentPosition(string contentName);
    }
}
