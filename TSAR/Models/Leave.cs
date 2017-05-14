using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
    public class Leave
    {

        [Key]
        [Required]
        [Display(Name = "Leave Id")]
        public int LeaveId { get; set; }


        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }

        [Required]
        [Display(Name = "Is Confirmed")]
        public bool IsConfirmed { get; set; }

        [Required]
        [Display(Name = "Leave Description")]
        public string LeaveDecsription { get; set; }

        [Required]
        [Display(Name = "Leave Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime LeaveDate { get; set; }

        [Required]
        [Display(Name = "Return Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ReturnDate { get; set; }


    }
}