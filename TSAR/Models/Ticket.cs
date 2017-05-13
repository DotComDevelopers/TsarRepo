using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
    public class Ticket
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Client Name")]
        public string ClientName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Fault Description")]
        public string FaultDescription { get; set; }

        [Display(Name = "Priority")]
        public string Priority { get; set; }


        [Required]
        [Display(Name = "Date & Time")]
        public DateTime Date { get; set; }
    }
}