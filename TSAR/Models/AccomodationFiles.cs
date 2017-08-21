using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSAR.Models
{
    public class AccomodationFiles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid accomodationfile_id { get; set; }
        public string file_name { get; set; }
        public string file_type { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        public byte[] file_bytes { get; set; }
    }
}