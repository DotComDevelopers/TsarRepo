using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSAR.Models
{
    public class Consultant
    {
        [Key]
        public int ConsultantNum { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Contact Number")]
        public int ContactNumber { get; set; }
        [Required]
        [Display(Name = "Consultant Address")]
        public string ConsultantAddress { get; set; }
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Consultant Type")]
        public string ConsultantType { get; set; }
        [Required]
        [Display(Name = "Commission Code")]
        public string ComissionCode { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Role Type")]
        public string RoleType { get; set; }
    }
}