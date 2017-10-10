using System.Web.Mvc;
using Microsoft.Practices.Unity;
using TSAR.Models;
using TSAR.Repositories;
using Unity.Mvc5;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TSAR.Controllers;

namespace TSAR
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

      // register all your components with the container here
      // it is NOT necessary to register your controllers

      // e.g. container.RegisterType<ITestService, TestService>();
      container.RegisterType<IRepository<Product, int>, ProductRepository>();
      container.RegisterType<IRepository<Order, int>, OrderRepository>();
      container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();

          container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
          container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
          container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());
          container.RegisterType<AccountController>(new InjectionConstructor());
      DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}