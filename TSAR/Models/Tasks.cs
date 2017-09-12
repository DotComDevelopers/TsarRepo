using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TSAR.Models;

namespace TSAR.Models
{ 
    public enum Status
    {
        ToDo,Doing,Done
    }
    public class Tasks
    {
        [Key]
        [Required]

        public int TaskID { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Consultant Consultant { get; set; }

        public int ConsultantNum { get; set; }

        public Status Status { get; set; }




    }
}
