using System;

namespace MobileTsar.Models
{
    public class Timesheet
    {

        public int TimesheetId { get; set; }

        public DateTime CaptureDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }


        public string ActivityDescription { get; set; }

        // Calculated stuff:
        // [Required]

        public double Total { get; set; }

        // [Required]

        public double Hours { get; set; }

        ///Relationships:
        public virtual Consultant Consultant { get; set; }
        public virtual Client Client { get; set; }


        public int Id { get; set; }


        public int ConsultantNum { get; set; }


        public string TicketReference { get; set; }


        public bool SignOff { get; set; }

        public virtual Travel Travel { get; set; }
        public string MClientAddress { get; set; }

       


    }
}