using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
    public class Tag
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string UrlSeo { get; set; }
        public bool Checked { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
    }
}