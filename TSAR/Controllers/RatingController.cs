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
    [Authorize(Roles = "Admin,Client")]
    public class RatingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rating
        public ActionResult Index()
        {
            var ratings = db.Ratings.Include(r => r.Consultant);
            return View(ratings.ToList());
        }

        // GET: Rating/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // GET: Rating/Create
        public ActionResult Create()
        {
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FullName");
            return View();
        }

        // POST: Rating/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RatingId,ConsultantNum,Rate,Comment,ClientName")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                rating.ClientName = User.Identity.GetUserName();
                db.Ratings.Add(rating);
                db.SaveChanges();
                return RedirectToAction("Done");
            }

            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FullName", rating.ConsultantNum);
            return View(rating);
        }

        // GET: Rating/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FullName", rating.ConsultantNum);
            return View(rating);
        }

        // POST: Rating/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RatingId,ConsultantNum,Rate,Comment,ClientName")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FullName", rating.ConsultantNum);
            return View(rating);
        }

        // GET: Rating/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // POST: Rating/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rating rating = db.Ratings.Find(id);
            db.Ratings.Remove(rating);
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

        public ViewResult Done()
        {

            return View();
        }
    }
}
