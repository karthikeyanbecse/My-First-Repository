using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

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

    }
}
