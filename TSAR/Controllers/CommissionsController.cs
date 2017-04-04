using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TSAR.Models;

namespace TSAR.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CommissionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Commissions
        public ActionResult Index()
        {

            return View(db.Commissions.ToList());
        }

        // GET: Commissions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commission commission = db.Commissions.Find(id);
            if (commission == null)
            {
                return HttpNotFound();
            }
            return View(commission);
        }

        // GET: Commissions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Commissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommissionId,CommissionName")] Commission commission)
        {
            if (ModelState.IsValid)
            {
                db.Commissions.Add(commission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(commission);
        }

        // GET: Commissions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commission commission = db.Commissions.Find(id);
            if (commission == null)
            {
                return HttpNotFound();
            }
            return View(commission);
        }

        // POST: Commissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommissionId,CommissionName")] Commission commission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(commission);
        }

        // GET: Commissions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commission commission = db.Commissions.Find(id);
            if (commission == null)
            {
                return HttpNotFound();
            }
            return View(commission);
        }

        // POST: Commissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Commission commission = db.Commissions.Find(id);
            db.Commissions.Remove(commission);
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
