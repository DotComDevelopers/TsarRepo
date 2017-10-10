using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using TSAR.Models;

namespace TSAR.Repositories
{
  public class OrderRepository :IRepository<Order, int>
  {
    [Dependency]
    public ApplicationDbContext context { get; set; }

    public readonly ShoppingCart _ShoppingCart;
    public IEnumerable<Order> Get()
    {
      return context.Orders.ToList();
    }

    public Order Get(int id)
    {
      return context.Orders.Find(id);
    }

    public void Add(Order entity)
    {
      entity.OrderPlaced=DateTime.Now;
      context.Orders.Add(entity);
    
      var shoppingCartItems = _ShoppingCart.ShoppingCartItems;
      foreach (var item in shoppingCartItems)
      {
        var orderdetail = new OrderDetail()
        {
          Amount = item.Amount,
          ProductId = item.Product.ProductId,
          OrderId = entity.OrderId,
          ProductPrice = item.Product.ProductPrice
        };
        context.OrderDetails.Add(orderdetail);
      }
      context.SaveChanges();
    }

    public void Remove(Order entity)
    {
      var obj = context.Orders.Find(entity.OrderId);
      context.Orders.Remove(obj);
      context.SaveChanges();
    }
  }
}