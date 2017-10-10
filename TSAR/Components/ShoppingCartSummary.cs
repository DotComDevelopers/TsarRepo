using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using TSAR.Models;
using TSAR.ViewModels;

namespace TSAR.Components
{
  public class ShoppingCartSummary : ViewComponent
  {
    private readonly ShoppingCart _shoppingCart;
    public ShoppingCartSummary(ShoppingCart shoppingCart)
    {
      _shoppingCart = shoppingCart;
    }

    public IViewComponentResult InVoke()
    {
      var items = _shoppingCart.GetShoppingCartItems();
      _shoppingCart.ShoppingCartItems = items;

      var scvm = new ShoppingCartViewModel
      {
        ShoppingCart = _shoppingCart,
        ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
      };
      return View(scvm);
    }
  }
}