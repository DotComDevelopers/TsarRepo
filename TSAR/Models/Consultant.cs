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

    [Display(Name = "Full Name")]
    public string FullName { get; set; }
        [Required]
        [Display(Name = "Consultant Gender")]
        public string Gender { get; set; }
        [Required]
    [Display(Name = "Contact Number")]
    public int ContactNumber { get; set; }
    [Required]
    [Display(Name = "Consultant Address")]
    public string ConsultantAddress { get; set; }

    [Required]
    [Display(Name = "Consultant Type")]
    public string ConsultantType { get; set; }

    public virtual Commission Commission { get; set; }
    [Display(Name = "Commission")]
    public int CommissionId { get; set; }

    [Display(Name = "Leave Balance")]
    public int LeaveBalance { get; set; } 



    [Required]
    [Display(Name = "User Name")]
    //[EmailAddress]
    public string ConsultantUserName { get; set; }
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    //[Required]
    //[EmailAddress]
    //[Display(Name = "Confirm Email")]
    //[Compare("Email")]
    //public string ConfirmEmail { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    //[DataType(DataType.Password)]
    //[Display(Name = "Confirm password")]
    //[Compare("Password")]
    //public string ConfirmPassword { get; set; }

  }
}