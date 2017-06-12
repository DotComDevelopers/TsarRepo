using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TSAR.Models;

namespace TSAR.Models
{
    public class Timesheet
    {
        [Key]
        public int TimesheetId { get; set; }

        [Required]
        [Display(Name = "Date Captured")]
        public DateTime CaptureDate { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        public TimeSpan EndTime { get; set; }

        [Required]
        [Display(Name = "Activity Description")]
        public string ActivityDescription { get; set; }

        // Calculated stuff:
        // [Required]
        [Display(Name = "Total")]
        public double Total { get; set; }

        // [Required]
        [Display(Name = "Hours")]
        public double Hours { get; set; }

        ///Relationships:
        public virtual Consultant Consultant { get; set; }
        public virtual Client Client { get; set; }

        [Display(Name = "Client Name")]
        public int Id { get; set; }

        [Display(Name = "Consultant Name")]
        public int ConsultantNum { get; set; }

        [Display(Name = "Ticket Reference")]
        public string TicketReference { get; set; }

        [Display(Name = "Sign Off")]
        public bool SignOff { get; set; }

        public virtual Travel Travel { get; set; }
        public string MClientAddress { get; set; }

       


    }
}