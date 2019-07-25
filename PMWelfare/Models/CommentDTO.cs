using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMWelfare.Models
{
    public class CommentDTO
    {
            public int ParentId { get; set; }
            public string CommentText { get; set; }
            public string UserName{ get; set; }
        }
        public class commentViewModel : CommentDTO
        {
            public int CommentId { get; set; }
            public DateTime?CommentDate { get; set; }
            public string strCommentDate { get {; return this.CommentDate.ToString(); } }
        }
    }
