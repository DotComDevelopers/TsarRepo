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
        public virtual Projects Project { get; set; }
        public int ProjectId { get; set; }
        [Display(Name = "Reference Code")]
        public string Code { get; set; }



        [Required]
        [Display(Name = "Amount")]
        public double Balance { get; set; }

    }
}