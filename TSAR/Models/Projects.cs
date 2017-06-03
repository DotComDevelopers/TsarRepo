using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
    public class Projects
    {
        [Key]
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }
        public virtual Client Client { get; set; }
        public int Id { get; set; }

       
    }
}