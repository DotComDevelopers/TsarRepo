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
        

        [Display(Name = "Client Name")]
        public string ClientName { get; set; }

        
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Fault Description")]
        public string FaultDescription { get; set; }

        [Display(Name = "Priority")]
        public string Priority { get; set; }


       
        [Display(Name = "Date & Time")]
        public DateTime Date { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Assigned Consultant")]
        public string ConsultantName { get; set; }

        public int ConsultantId { get; set; }


        public string TicketReference { get; set; }


        public virtual Client Clients { get; set; }
        
        public virtual Timesheet Timesheets { get; set; }

        public virtual Consultant Consultants { get; set; }
    }
}