using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string PostId { get; set; }

        public DateTime DateTime { get; set; }

        public string UserName { get; set; }

        public string Body { get; set; }
        [DefaultValue(0)]

        public int NetLike { get; set; }
        public bool Deleted { get; set; }

        public Post Post { get; set; }
        public ICollection<Reply> Replies { get; set; }
        public ICollection<CommentLike> CommentLikes { get; set; }
    }
}