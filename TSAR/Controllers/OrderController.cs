using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using TSAR.Models;
using TSAR.Repositories;
using ActionResult = System.Web.Mvc.ActionResult;
using Controller = System.Web.Mvc.Controller;

namespace TSAR.Controllers
{
    public class OrderController : Controller
    {

      private readonly IRepository<Order, int> _orderRepository;

      private readonly ShoppingCart _shoppingCart;

      public OrderController(IRepository<Order, int> repo,ShoppingCart shoppingCart)
      {
        _orderRepository = repo;
        _shoppingCart = shoppingCart;
      }
    // GET: Order
    public ActionResult Checkout()
        {
            return View();
        }

    [System.Web.Mvc.HttpPost]
      public ActionResult Checkout(Order order)
    {
      var items = _shoppingCart.GetShoppingCartItems();
      _shoppingCart.ShoppingCartItems = items;

      if (_shoppingCart.ShoppingCartItems.Count==0)
      {
        ModelState.AddModelError("","Your cart is empty please add something first");
      }
      if (ModelState.IsValid)
      {
        _orderRepository.Add(order);
        _shoppingCart.ClearCart();
        return RedirectToAction("CheckoutComplete");
      }
        return View(order);
      }

      public ActionResult CheckoutComplete()
      {
        ViewBag.Complete = "Thank You, Quote complete";
        return View();
      }
    }
}