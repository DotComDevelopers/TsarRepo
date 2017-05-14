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
        public int ConsultantNum { get; set; }
        [Required]
        [Display(Name = "Rating")]
        public string Rate { get; set; }
        [Required]
        [Display(Name = "Reason")]
        public string Reason { get; set; }
        [Required]
        [Display(Name = "Client Name")]
        public string ClientName { get; set; }
    }
}