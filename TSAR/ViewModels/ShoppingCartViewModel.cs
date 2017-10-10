using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSAR.Models;

namespace TSAR.ViewModels
{
  public class ShoppingCartViewModel
  {
    public ShoppingCart ShoppingCart { get; set; }
    public decimal ShoppingCartTotal { get; set; }  
  }
}