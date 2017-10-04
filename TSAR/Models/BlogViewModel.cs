using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
   
    public class BlogViewModel
    {
        public DateTime PostedOn { get; set; }

        public DateTime? Modified { get; set; }

        public IList<Tag> Tag { get; set; }

        public int PostDislike { get; set; }

        public int PostLike { get; set; }

        public int TotalPosts { get; set; }

        public List<string> Category { get; set; }

        public Post Post { get; set; }

        public string ID { get; set; }

        public string ShortDescription { get; set; }

        public string Title { get; set; }

        public IList<Category> PostCategory { get; set; }
        public IList<Tag> PostTag { get; set; }

        public string UrlSlug { get; set; }

    }

    
}