using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroLearning.JsonObjects
{
    public class OnlineUsers 
    {
    }

    public class OnlineUser
    {
        public OnlineUser(string userId, string userName)
        {
            UserId = userId;
            UserName = userName;
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}