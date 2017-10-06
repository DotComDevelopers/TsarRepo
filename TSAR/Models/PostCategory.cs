using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
    public class PostCategory
    {
        [Key]
        [Column(Order = 0)]
        public string PostId { get; set; }

        [Column(Order = 1)]
        public string CategoryId { get; set; }

        public bool Checked { get; set; }

        public Post Post { get; set; }
        public Category Category { get; set; }
    }
}