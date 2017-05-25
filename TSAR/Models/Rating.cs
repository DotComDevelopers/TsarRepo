using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace TSAR.Models
{
    public class Rating
    {
        [Key]
        [Required]
        [Display(Name = "Rating id")]
        public int RatingId { get; set; }
        public virtual Consultant Consultant { get; set; }
        [Display(Name = "Consultant Name")]
        public string ConsultantName { get; set; }

       
        [Required]
        [Display(Name = "Rating")]
        public string Rate { get; set; }
        [Required]
        [Display(Name = "Comment")]
        public string Comment { get; set; }
      
        [Display(Name = "Client Username")]
        public string ClientUsername { get; set; }


    }
}