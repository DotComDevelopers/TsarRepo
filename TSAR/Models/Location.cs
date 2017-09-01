using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime CheckinTime { get; set; }

        public virtual Consultant Consultant { get; set; }
        public virtual Client Client { get; set; }


        public int Id { get; set; }


        public int ConsultantNum { get; set; }

    }
}