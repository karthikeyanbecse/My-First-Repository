using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroLearning.JsonObjects
{
    public class SignedInUser 
    {
        public string Status { get; set; }
        public string SignedInUserId { get; set; }
        public string SignedInUserName { get; set; }
    }
}