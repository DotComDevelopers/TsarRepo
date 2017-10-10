using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSAR.Models;
using TSAR.Repositories;
using TSAR.ViewModels;

namespace TSAR.Controllers
{
    public class ShoppingCartController : Controller
    {
      private IRepository<Product, int> _productRepository;
      private readonly ShoppingCart _shoppingCart;

    public ShoppingCartController(IRepository<Product, int> repo, ShoppingCart shoppingCart)
    {
      _productRepository = repo;
      _shoppingCart = shoppingCart;
    }
        // GET: ShoppingCart
        public ViewResult Index()
        {
          var items = _shoppingCart.GetShoppingCartItems();
          _shoppingCart.ShoppingCartItems = items;

          var sCVm = new ShoppingCartViewModel
          {
            ShoppingCart = _shoppingCart,
            ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
          };
          return View(sCVm);
        }

      public ActionResult AddToShoppingCart(int productId)
      {
        var selectedProduct = _productRepository.Get(productId);
        if (selectedProduct!=null)
        {
          _shoppingCart.AddToCart(selectedProduct,1);
        }
        return Index();
      }

      public ActionResult RemoveFromShoppingCart(int productId)
      {
        var selectedProduct = _productRepository.Get(productId);
        if (selectedProduct != null)  
        {
          _shoppingCart.RemoveFromCart(selectedProduct);
        }
        return Index();
      }
  }
}