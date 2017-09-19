using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TSAR.Models;

namespace TSAR.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Client);
            return View(products.ToList());
            //return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [Authorize(Roles = "Admin, Consultant")]
        // GET: Products/Create
        public ActionResult CreateProduct()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct([Bind(Include = "ProductId,ProductName,Description,Price,totalPrice,Selected,Id,ClientName,Email")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [Authorize(Roles = "Admin, Consultant")]
        // GET: Products/Edit/5
        public ActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct([Bind(Include = "ProductId,ProductName,Description,Price,totalPrice,Selected,Id,ClientName,Email")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [Authorize(Roles = "Admin, Consultant")]
        // GET: Products/Delete/5
        public ActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize(Roles = "Client")]
        // GET: Products/Create
        public ActionResult GetQuotation()
        {
            var clientusername = User.Identity.GetUserName();
            var cn = (from Client c in db.Clients
                      where c.ClientName == clientusername
                      select c.Id).FirstOrDefault();
            ViewBag.ClientName = (from Client c in db.Clients
                            where c.ClientName == clientusername
                            select c.Id).FirstOrDefault();
            ViewBag.Email = (from Client c in db.Clients
                             where c.ClientName == clientusername
                             select c.Email).FirstOrDefault();

            return View();

            //if (clientusername == null)
            //{
            //    return RedirectToAction( /*Register*/);
            //}
            //else
            //{
            //    return View(db.Products.Where(p => p.Id == cn).ToList()); //to change to Quotation View
            //}
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetQuotation([Bind(Include = "ProductId,ProductName,Description,Price,totalPrice,Selected,Id,ClientName,Email")] Product product)
        {

            var clientusername = User.Identity.GetUserName();
            var cn = (from Client c in db.Clients
                      where c.ClientName == clientusername
                      select c.Id).FirstOrDefault().ToString();
            ViewBag.ClientName = (from Client c in db.Clients
                                  where c.ClientName == clientusername
                                  select c.Id).FirstOrDefault();
            ViewBag.Email = (from Client c in db.Clients
                             where c.ClientName == clientusername
                             select c.Email).FirstOrDefault();
            //double total, cost;
            if (product.Selected)
            {
                //foreach (var product in Product)
                //{
                //    double total = product.Price++;
                //    //double total = cost++;
                //}
                product.totalPrice = product.Price++;
            }

             if (clientusername == null)
            {
                return RedirectToAction("Register", "Account", new {area = ""});
            }
            //else
            //{
            //    product.Email = clientusername;
            //}

            if (ModelState.IsValid)
            {
                product.ClientName = cn;
                product.Email = clientusername;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //return View(product); // To change to quotation view or pdf

            return View(product); //to change to Quotation View 

            //if (ModelState.IsValid)
            //{
            //    db.Products.Add(product);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //return View(product);
        }

    }
}
