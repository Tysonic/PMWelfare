using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMWelfare.Models;

namespace PMWelfare.Models
{

   

    public class CommentRepo
    {
        private welfare db  = new welfare();
        public IQueryable<ChatRoom> GetAll()
        {
            return db.ChatRoom.OrderBy(x => x.PostedAt);
        }
        public commentViewModel AddComment(CommentDTO comment)
        {
            var _comment = new ChatRoom()
            {
                ParentId = comment.ParentId,
                Message = comment.CommentText,
                UserName = comment.UserName,
                PostedAt = DateTime.Now
            };

            db.ChatRoom.Add(_comment);
            db.SaveChanges();
            return db.ChatRoom.Where(x => x.ChatId == _comment.ChatId)
                    .Select(x => new commentViewModel
                    {
                        CommentId = x.ChatId,
                        CommentText = x.Message,
                        ParentId = x.ParentId,
                        CommentDate = x.PostedAt,
                      UserName = x.UserName

                    }).FirstOrDefault();
        }
    }
}