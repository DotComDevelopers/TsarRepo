using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
    public class Reply
    {
        [Key]
        public string Id { get; set; }

        public string PostId { get; set; }
        public string CommentId { get; set; }
        public string ParentReplyId { get; set; }
        public DateTime DateTime { get; set; }
        public string UserName { get; set; }
        public string Body { get; set; }
        public bool Deleted { get; set; }
        public Post Post { get; set; }
        public Comment Comment { get; set; }
        public ICollection<ReplyLike> ReplyLikes { get; set; }
    }
}