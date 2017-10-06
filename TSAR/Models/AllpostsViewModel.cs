using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
    public class AllpostsViewModel
    {
        public IList<Category> PostCategories { get; set; }
        public IList<Tag> PostTags { get; set; }
        public string PostId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public Category Category { get; set; }
        public bool Checked { get; set; }
        public Tag Tag { get; set; }
        public string UrlSlug { get; set; }

    }
}