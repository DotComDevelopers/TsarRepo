using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
    public class ClientPassword
    {
        [Key]
        public int ClientPasswordId { get; set; }

        public virtual Client Client { get; set; }
        public int id { get; set; }

        public string Operator { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Operator Password")]
        public string OperatorPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Company Password")]
        public string CompanyPassword { get; set; }
    }
}