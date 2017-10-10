using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TSAR.Repositories;
using Microsoft.AspNetCore.Http;

namespace TSAR.Models
{
  public class ShoppingCart
  {
    private readonly ApplicationDbContext _applicationDbContext;

    public ShoppingCart(ApplicationDbContext applicationDbContext)
    {
      _applicationDbContext = applicationDbContext;
    }

    public string ShoppingCartId { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; }


    //public static ShoppingCart GetCart(IServiceProvider services)
    //{
    //  string sessionId = System.Web.HttpContext.Current.Session.SessionID;


    //  var context = new ApplicationDbContext();

    //  string cartId = sessionId.ToString() ?? Guid.NewGuid().ToString();

    //  return new ShoppingCart(context) {ShoppingCartId = cartId};
    //}
    public static ShoppingCart GetCart(string cartId)
    {
      string sessionId = System.Web.HttpContext.Current.Session.SessionID;


      var context = new ApplicationDbContext();

      string newcartId = sessionId.ToString() ?? Guid.NewGuid().ToString();

      return new ShoppingCart(context) { ShoppingCartId = cartId };
    }


    public void AddToCart(Product product, int amount)
    {
      var shoppingCartItem = _applicationDbContext.ShoppingCartItems
        .SingleOrDefault(s => s.Product.ProductId == product.ProductId &&
                              s.ShoppingCartId == ShoppingCartId);

      if (shoppingCartItem==null)
      {
        shoppingCartItem = new ShoppingCartItem
        {
          ShoppingCartId = Guid.NewGuid().ToString(),
          Product = product,
          Amount = 1
        };
        _applicationDbContext.ShoppingCartItems.Add(shoppingCartItem);
      }
      else
      {
        shoppingCartItem.Amount++;
      }
      _applicationDbContext.SaveChanges();
    }

    public int RemoveFromCart(Product product)
    {
      var shoppingCartItem = _applicationDbContext.ShoppingCartItems
        .SingleOrDefault(s => s.Product.ProductId == product.ProductId &&
                              s.ShoppingCartId == ShoppingCartId);

      var localAmount = 0;

      if (shoppingCartItem != null)
      {
        if (shoppingCartItem.Amount>1)
        {
          shoppingCartItem.Amount--;
          localAmount = shoppingCartItem.Amount;
        }
        else
        {
          _applicationDbContext.ShoppingCartItems.Remove(shoppingCartItem);
        }
      }
 
      _applicationDbContext.SaveChanges();
      return localAmount;
    }

    public List<ShoppingCartItem> GetShoppingCartItems()
    {
      return ShoppingCartItems ?? (ShoppingCartItems = _applicationDbContext.ShoppingCartItems
               .Where(c => c.ShoppingCartId == ShoppingCartId)
               .Include(s => s.Product)
               .ToList());
    }

    public void ClearCart()
    {
      var cartitems = _applicationDbContext.ShoppingCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId);
      _applicationDbContext.ShoppingCartItems.RemoveRange(cartitems);

      _applicationDbContext.SaveChanges();
    }

    public decimal GetShoppingCartTotal()
    {
      var total = _applicationDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
        .Select(c => c.Product.ProductPrice * c.Amount)
        .Sum();
      return total;
    }
  }
}