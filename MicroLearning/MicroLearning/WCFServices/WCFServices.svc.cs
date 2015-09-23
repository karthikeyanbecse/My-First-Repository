using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MicroLearning.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WCFServices" in code, svc and config file together.
    public class WCFServices : IWCFServices
    {
        public string SignUpUser(string username, string password)
        {
          
            //Sent Mail to user email id
            bool result = true;
            if (result)
            {
                return "Successful";
            }
            else
            {
                return "Fail";
            }
        }

    }
}
