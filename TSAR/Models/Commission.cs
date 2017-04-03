using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TSAR.Models
{
    public class Commission
    {


        [Key]
        [Required]
        public int CommissionId  { get; set; }


        [Required]
        [Display(Name = "Commission Name")]
        public string CommissionName { get; set; }

    }
}