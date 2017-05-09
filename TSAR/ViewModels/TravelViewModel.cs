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
       
        public string ClientName { get; set; }
        [Required]
       
        public string Branch { get; set; }
        [Required]
        public string ClientAddress { get; set; }
        [Required]
        public long ContactNumber { get; set; }
        [Required]
   
        
        public string Email { get; set; }

        [Required]
 
        public string ProjectLeader { get; set; }

        public virtual ManageTravel ManageTravel { get; set; }


 
       
        public string TravelCode { get; set; }
       
        

        [Required]
        
        public double Rate { get; set; }


        [Required]
        

        public string Distance { get; set; }

        [Required]
        
        public double TravelFee { get; set; }
        public virtual ICollection<Client> Clients { get; set; }

    }
}