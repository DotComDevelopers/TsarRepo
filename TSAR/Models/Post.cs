using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
    public class Post
    {
        [Key]
        public string Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Body { get; set; }

        public string Meta { get; set; }

        public string UrlSeo { get; set; }

        public bool Published { get; set; }
        [DefaultValue(0)]

        public int NetLikeCount { get; set; }

        public DateTime PostedOn { get; set; }

        public DateTime? Modified { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Reply> Replies { get; set; }
        public ICollection<PostCategory> PostCategories { get; set; }
        public ICollection<PostTag> PostTags { get; set; }

        public ICollection<PostLike> PostLikes { get; set; }
    }
}