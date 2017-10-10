using System.Collections.Generic;
using TSAR.Models;
using Microsoft.Practices.Unity;
using System.Linq;

namespace TSAR.Repositories
{
  public class ProductRepository : IRepository<Product, int>
  {
    // IEnumerable<Product> Products { get; set; }

    //Product GetProductById(int productId);
    [Dependency]  
    public ApplicationDbContext context { get; set; }

    public IEnumerable<Product> Get()
    {
      return context.Products.ToList();
    }
      
    public Product Get(int id)
    {
      return context.Products.Find(id);
    }

    public void Add(Product entity)
    {
      context.Products.Add(entity);
      context.SaveChanges();
    }

    public void Remove(Product entity)
    {

      var obj = context.Products.Find(entity.ProductId);
      context.Products.Remove(obj);
      context.SaveChanges();
    }
  }




  
}