using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UrlSeo { get; set; }
        public string Description { get; set; }
        public bool Checked { get; set; }
        public ICollection<PostCategory> PostCategories { get; set; }
    }
}