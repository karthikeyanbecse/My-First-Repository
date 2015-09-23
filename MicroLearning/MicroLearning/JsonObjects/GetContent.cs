using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroLearning.JsonObjects
{
    public class GetContent
    {
        public string ContentId { get; set; }
        public string ContentName { get; set; }
        public string ReferenceContentId { get; set; }
        public string MasterContentName { get; set; }
        public string ContentType { get; set; }
        public string Position { get; set; }
        public string Image { get; set; }
    }
}