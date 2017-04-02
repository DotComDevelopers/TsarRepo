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
        [Display(Name = "Company Branch")]
        public string Branch { get; set; }
        [Required]
        [Display(Name = "Client Address")]
        public string ClientAddress { get; set; }
        [Required]
        [Display(Name = "Contact Number")]
        public long ContactNumber { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Client Type")]
        public string ClientType { get; set; }
        [Required]
        [Display(Name = "Project Leader")]
        public string ProjectLeader { get; set; }

        public virtual ManageTravel ManageTravel { get; set; }


        [Display(Name="Travel Code")]
        [Required]
        public string TravelCode { get; set; }
        


        [Required]
        [Display(Name = "Role Type")]
        public string RoleType { get; set; }
    }
}