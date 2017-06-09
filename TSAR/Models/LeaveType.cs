using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
    public class LeaveType
    {


        [Key]
        [Required]
        [Display(Name = "Leave Type Id")]
        public int LeaveTypeId { get; set; }


        [Required]
        [Display(Name = "Leave Type")]
        public string LeaveTypeName { get; set; }


    }
}