using System;


using System.Web;

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.UI.WebControls;

namespace TSAR.Models
{
    public class ManageTravel
    {
        [Key]

        public int tcode { get; set; }

        [Display(Name = "Travel code")]
        public string travelCode { get; set; }

        [Required]
        [Display(Name = "Rate")]
        public double rate { get; set; }


        [Required]
        [Display(Name = "Distance")]
        public double distance { get; set; }
    }
}