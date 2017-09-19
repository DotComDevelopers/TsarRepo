using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TSAR.Models;
using ZXing;

namespace TSAR.Controllers
{
   
    public class NewsletterController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Newsletter
        [Authorize(Roles="Admin")]
        public ActionResult Index(string search)
        {
            if (search == null)
            {
                return View(db.Subscriptions.Where(p => p.Email == search));

            }
            else
                return View(db.Subscriptions.Where(p => p.Email == search));
        }

        // GET: Newsletter/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription subscription = db.Subscriptions.Find(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            return View(subscription);
        }

        // GET: Newsletter/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Newsletter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email")] Subscription subscription)
        {
      //BARCODE SCANNER CODE 
      //Main();
      if (ModelState.IsValid)
            {
                db.Subscriptions.Add(subscription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subscription);
        }

    //BARCODE SCANNER CODE
      //static void Main()
      //{
      //  // instantiate a writer object
      //  var barcodeWriter = new BarcodeWriter();

      //  // set the barcode format
      //  barcodeWriter.Format = BarcodeFormat.QR_CODE;

      //  // write text and generate a 2-D barcode as a bitmap
      //  barcodeWriter
      //    .Write("https://jeremylindsayni.wordpress.com/")
      //    .Save(@"C:\Users\Devashen\Desktop\Random\generated.bmp");
      //}

    // GET: Newsletter/Edit/5
    public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription subscription = db.Subscriptions.Find(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            return View(subscription);
        }

        // POST: Newsletter/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email")] Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subscription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subscription);
        }

        // GET: Newsletter/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription subscription = db.Subscriptions.Find(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            return View(subscription);
        }

        // POST: Newsletter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subscription subscription = db.Subscriptions.Find(id);
            if (subscription == null) throw new ArgumentNullException(nameof(subscription));
            db.Subscriptions.Remove(subscription);
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
    }
}
