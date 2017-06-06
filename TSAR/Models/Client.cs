using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSAR.Models
{
    public class Client
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Client Name")]
        public string ClientName { get; set; }
        
        [Required]
        [Display(Name = "Client Address")]
        public string ClientAddress { get; set; }

        [Display(Name = "Client Address 2")]
        public string ClientAddress2 { get; set; }


        [Required]
        [Display(Name = "Contact Number")]
        public long ContactNumber { get; set; }

        [Display(Name = "Contact Number 2")]
        public string ContactNumber2 { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Email 2")]
        public string Email2 { get; set; }

        //[Required]
        //[Display(Name = "Project Leader")]
        //public string ProjectLeader { get; set; }

       



        [Display(Name = "Distance")]
  
        public string Distance  { get; set; }

        public string Distance2 { get; set; }

        //public virtual ManageTravel ManageTravel { get; set; }

        //[Display(Name = "Travel Code")]
        //[Required]
        //public string TravelCode { get; set; }
        //public double TravelFee { get; set; }
        //public string TravelCode2 { get; set; }
    }
}