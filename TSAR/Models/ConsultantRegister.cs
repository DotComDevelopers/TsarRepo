using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
  public class ConsultantRegister
  {
    [Key]
    public int ConsultantRegisterId { get; set; }

    //[DataType(DataType.Date)]
    //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime DateTime { get; set; }

    public bool BarcodeScanned { get; set; }  


    //Foreign Keys
    public Consultant Consultant { get; set; }
    public int ConsultantNum { get; set; }
  }
}