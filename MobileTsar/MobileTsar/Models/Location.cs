using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTsar.Models
{
   public class Location
    {

       
        public int LocationId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime CheckinTime { get; set; }

        public virtual Consultant Consultant { get; set; }
        public virtual Client Client { get; set; }


        public int? Id { get; set; }


        public int ConsultantNum { get; set; }
    }
}
