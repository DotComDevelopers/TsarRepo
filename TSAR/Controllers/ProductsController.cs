using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Rotativa;
using TSAR.Models;

namespace TSAR.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index()
        {
            //if (User.IsInRole("Admin, Consultant"))
            //{
                var products = db.Products.Include(p => p.Client);
                return View(products.ToList());
            //}
            //else
            //{
            //    var products 
            //}
            
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
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            //ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct([Bind(Include = "ProductId,ProductName,Description,Price,totalPrice,Selected,Id,ClientName,Email")] Product product)
        {
            if (!ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [Authorize(Roles = "Admin, Consultant, Client")]
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


        //pdf code

        //Generates View as PDF ----> might not populate dynamically 
        public ActionResult GenerateQuotationPDF()
        {
            return new Rotativa.ActionAsPdf("GetQuotation");
        }

        //ActionAsPDF - Useful, when we want to generate the PDF using another action method
        //public ActionResult ExportPDF()
        //{
        //    return new ActionAsPdf("GetQuotation")
        //    {
        //        FileName = Server.MapPath("~/Content/Relato.pdf"),
        //        PageOrientation = Rotativa.Options.Orientation.Landscape,
        //        PageSize = Rotativa.Options.Size.A4
        //    };
        //}

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
            var email = (from Client c in db.Clients
                             where c.ClientName == clientusername
                             select c.Email).FirstOrDefault();

            var tp = (from Product p in db.Products
                where p.Selected
                select p.totalPrice).FirstOrDefault();

            var price = (from Product p in db.Products
                where p.Selected
                select p.Price).FirstOrDefault();


            var selected = (from Product p in db.Products
                     where p.Selected
                     select p.Id).ToList();

            //Calculates total price of selected items
            foreach (var s in selected)
            {

                product.totalPrice = Convert.ToDouble((tp + price).ToString("R0.00"));
                //ViewBag.totalPrice = (tp + price).ToString("R0.00");
                //ViewBag.totalPrice = (product.totalPrice + price).ToString("R0.00");   
            }

            //Checks if Client is registered or not
            if (clientusername == null)
            {
                return RedirectToAction("Register", "Account", new { area = "" });
            }

            //Sends quotation as pdf to Client via email
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("dotcomdevelopers19@gmail.com");
            mail.To.Add(product.Email);
            mail.Subject = "Quotation";
            string htmlBody = "Please find your quotation attached as a PDF File!";
            mail.IsBodyHtml = true;
            mail.Body = htmlBody;
            mail.From = new MailAddress("dotcomdevelopers19@gmail.com", "Secretary");
            foreach (var e in email)
            {
                mail.To.Add(email);
            }
            System.Net.NetworkCredential cred = new System.Net.NetworkCredential("dotcomdevelopers19@gmail.com", "P@ssword0123");
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = cred;
            smtp.Send(mail);


            if (ModelState.IsValid)
            {
                product.ClientName = cn;
                product.Email = clientusername;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View((IEnumerable<Product>) product); 

        }



        public ActionResult MyQuotation()
        {

            var username = User.Identity.GetUserName();
            var cn = (from Client c in db.Clients
                      where c.ClientName == username
                      select c.Id).FirstOrDefault();
            ViewBag.ClientName = (from Client c in db.Clients
                            where c.ClientName == username
                            select c.ClientName).FirstOrDefault();


            return View(db.Products.Where(p => p.Id == cn).ToList());

        }

        [HttpPost]
        public ActionResult MyQuotation(string searchTerm)
        {
            var username = User.Identity.GetUserName();
            var cn = (from Client c in db.Clients
                      where c.ClientName == username
                      select c.Id).FirstOrDefault();
            return View(db.Products.Where(x => x.ClientName.StartsWith(searchTerm)).ToList());
        }

    }
}
