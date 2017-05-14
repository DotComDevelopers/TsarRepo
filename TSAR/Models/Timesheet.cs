﻿using System;
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

        public int Id { get; set; }
        public int ConsultantNum { get; set; }





    }
}