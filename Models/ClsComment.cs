using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkflowEvents.Models
{
    public class ClsComment
    {
        public int CommentID { get; set; }
        public string CommentDetails { get; set; }
        public Nullable<int> Task_ID_FK { get; set; }
    }
}