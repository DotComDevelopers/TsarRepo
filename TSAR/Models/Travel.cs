using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
    public class Travel
    {
        [Key]

        public int TravelId { get; set; }
        public virtual Client Client { get; set; }
        public int Id { get; set; }
        [Display(Name = "Client Address")]
        public string MClientAddress { get; set; }
        public double TravelRate { get; set; }
        public string Distance { get; set; }
        public double TravelFee { get; set; }
    }
}