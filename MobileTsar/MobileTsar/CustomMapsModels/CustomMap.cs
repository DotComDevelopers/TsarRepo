using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace MobileTsar.CustomMapsModels
{
   public class CustomMap:Map
    {
        public List<CustomPin> CustomPins { get; set; }

       
    }
}
