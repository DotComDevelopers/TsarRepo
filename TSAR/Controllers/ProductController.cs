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
    public class ProductController : Controller
    {
      private IRepository<Product, int> _productRepository;
    public ProductController(IRepository<Product, int> repo)
    {
      _productRepository = repo;
    }


      public ActionResult List()
      {
        //var products = _productRepository.Get();

      var vm = new ProductListViewModel();
        vm.Products = _productRepository.Get();
        return View(vm);
      }
    }
}