using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
    public class CommentLike
    {
        [Key]
        public string CommentId { get; set; }
        public string UserName { get; set; }
        public bool Like { get; set; }
        public bool Dislike { get; set; }

        public Comment Comment { get; set; }
    }
}