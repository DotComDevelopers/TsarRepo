using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSAR.Models
{
  public class ShoppingCartItem
  {
    public int ShoppingCartItemId { get; set; }
    public Product Product { get; set; }
    public int Amount { get; set; }
    public string ShoppingCartId { get; set; }  
  }
}