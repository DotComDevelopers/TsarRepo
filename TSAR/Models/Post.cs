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
        
        public DateTime PostedOn { get; set; }
        
      
        public Consultant Consultant { get; set; }
        public int ConsultantNum { get; set; }
        public string FirstName { get; set; }
    }
}