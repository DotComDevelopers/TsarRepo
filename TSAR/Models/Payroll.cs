using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSAR.Models
{
    public class Payroll
    {
        [Key]

        public int PayrollId { get; set; }

        public double Basic { get; set; }

        public double Comm { get; set; }

        public double totPay { get; set; }

        public double tax { get; set; }

        public virtual Consultant Consultant { get; set; }

        public int ConsultantNum { get; set; }


        public virtual Timesheet Timesheet { get; set; }

        public double total { get; set; }


    }
}

