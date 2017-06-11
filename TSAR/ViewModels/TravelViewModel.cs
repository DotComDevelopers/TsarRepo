using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TSAR.Models;

namespace TSAR.ViewModels
{
    public class TravelViewModel
    {

        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Client Name")]
        public string ClientName { get; set; }
        [Display(Name = "Client Address")]
        public string MClientAddress { get; set; }
        [Required]
        [Display(Name = "Client Address")]
        public string ClientAddress { get; set; }
        [Display(Name = "Client Address *Optional*")]
        public string ClientAddress2 { get; set; }
        [Required]
        [Display(Name = "Contact Number")]
        public long ContactNumber { get; set; }
        [Display(Name = "Contact Number 2 *Optional*")]
        public string ContactNumber2 { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Email 2 *Optional*")]
        public string Email2 { get; set; }
    
       // [Required]
        //[Display(Name = "Project Leader")]

        //public string ProjectLeader { get; set; }
     





        public double Rate { get; set; }





        
        public int TravelId { get; set; }
        public virtual Client Client { get; set; }
       
        public double TravelRate { get; set; }
        public string Distance { get; set; }
        public double TravelFee { get; set; }
        public string Distance2 { get; set; }
    }
}