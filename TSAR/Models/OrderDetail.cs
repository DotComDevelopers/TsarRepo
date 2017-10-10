using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
  public class OrderDetail
  {
    public int OrderDetailId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; }
    public decimal ProductPrice { get; set; } 
    public virtual  Product Product { get; set; }
    public virtual  Order Order { get; set; }

  }
}