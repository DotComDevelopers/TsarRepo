using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
    public class Budget
    {
        [Key]
        [Required]
        [Display(Name = "Budget Code")]
        public int BudgetCode { get; set; }
        public virtual Client Client { get; set; }
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Balance")]
        public float Balance { get; set; }
     
    }
}