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
    public class ManageTravelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string Search)

        {
            if (Search == null)
            {
                return View(db.ManageTravels.ToList());
            }
            else
                return View(db.ManageTravels.Where(p => p.travelCode == Search).ToList());

        }

        // GET: ManageTravel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManageTravel manageTravel = db.ManageTravels.Find(id);
            if (manageTravel == null)
            {
                return HttpNotFound();
            }
            return View(manageTravel);
        }

        // GET: ManageTravel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageTravel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tcode,travelCode,rate,distance")] ManageTravel manageTravel)
        {
            if (ModelState.IsValid)
            {
                db.ManageTravels.Add(manageTravel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manageTravel);
        }

        // GET: ManageTravel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManageTravel manageTravel = db.ManageTravels.Find(id);
            if (manageTravel == null)
            {
                return HttpNotFound();
            }
            return View(manageTravel);
        }

        // POST: ManageTravel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tcode,travelCode,rate,distance")] ManageTravel manageTravel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manageTravel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manageTravel);
        }

        // GET: ManageTravel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManageTravel manageTravel = db.ManageTravels.Find(id);
            if (manageTravel == null)
            {
                return HttpNotFound();
            }
            return View(manageTravel);
        }

        // POST: ManageTravel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ManageTravel manageTravel = db.ManageTravels.Find(id);
            db.ManageTravels.Remove(manageTravel);
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
