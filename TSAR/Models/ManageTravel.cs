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
        //works
      
        
        [Display(Name = "Travel code")]
        public string TravelCode { get; set; }

        [Required]
        [Display(Name = "Rate")]
        public double Rate { get; set; }


        [Required]
        [Display(Name = "Distance")]

        public string Distance { get; set; }

        [Required]
        [Display(Name = "TravelFee")]
        public double TravelFee { get; set; }
        public virtual ICollection<Client> Clients { get; set; }

      
    }
}