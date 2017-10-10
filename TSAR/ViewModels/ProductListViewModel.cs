using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSAR.Models;

namespace TSAR.ViewModels
{
  public class ProductListViewModel
  {
    public IEnumerable<Product> Products { get; set; }
  }
}