using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
    public class AccomodationFilesEmail
    {
        public Guid File_Name { get; set; }
        public string Consultant_Name { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
        [Required]
        public HttpPostedFileBase file { get; set; }
    }
}