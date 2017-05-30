using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSAR.Models;

namespace TSAR.ViewModels
{
    public class LeaveTypeViewModel
    {


        public IEnumerable<LeaveType> LeaveTypes { get; set; }
        public Leave Leave { get; set; }
        public LeaveType LeaveType { get; set; }


    }
}