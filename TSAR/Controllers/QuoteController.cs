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
    public class QuoteController : Controller
    {


      private IRepository<Product, int> _productRepository;

      public QuoteController(IRepository<Product, int> repo)
      {
        _productRepository = repo;
      }
    // GET: Quote
    public ViewResult Index()
        {
          var qvm = new QuoteViewModel
          {
            PreferredProducts = _productRepository.Get()
          };
          return View(qvm);
        }
    }
}