using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
  public class Product
  {
    public int ProductId { get; set; }
    public string ProductDescription { get; set; }
    public decimal ProductPrice { get; set; }
  }
}