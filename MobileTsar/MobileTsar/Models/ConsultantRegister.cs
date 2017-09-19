using System;
using System.Collections.Generic;
using System.Linq;


namespace MobileTsar.Models
{
  public class ConsultantRegister
  {
    public int ConsultantRegisterId { get; set; }

    public DateTime DateTime { get; set; }

    public bool BarcodeScanned { get; set; }  

    //Foreign Keys
    public int ConsultantNum { get; set; }
  }
}