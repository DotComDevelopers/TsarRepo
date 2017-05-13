using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
    public class RolesView
    {
        [Required]
        public string RoleName { get; set; }
    }
}