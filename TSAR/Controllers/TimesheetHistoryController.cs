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
    public class TimesheetHistoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TimesheetHistory
        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var consultants = from s in db.Consultants
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                consultants = consultants.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    consultants = consultants.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    consultants = consultants.OrderBy(s => s.ConsultantNum);
                    break;
                case "date_desc":
                    consultants = consultants.OrderByDescending(s => s.ComissionCode);
                    break;
                default:
                    consultants = consultants.OrderBy(s => s.LastName);
                    break;
            }

            return View(consultants.ToList());
        }

        // GET: TimesheetHistory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.Timesheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }

        // GET: TimesheetHistory/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName");
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName");
            return View();
        }

        // POST: TimesheetHistory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimesheetId,CaptureDate,StartTime,EndTime,ActivityDescription,Total,Hours,Id,ConsultantNum")] Timesheet timesheet)
        {
            if (ModelState.IsValid)
            {
                db.Timesheets.Add(timesheet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName", timesheet.Id);
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName", timesheet.ConsultantNum);
            return View(timesheet);
        }

        // GET: TimesheetHistory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.Timesheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName", timesheet.Id);
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName", timesheet.ConsultantNum);
            return View(timesheet);
        }

        // POST: TimesheetHistory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TimesheetId,CaptureDate,StartTime,EndTime,ActivityDescription,Total,Hours,Id,ConsultantNum")] Timesheet timesheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timesheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName", timesheet.Id);
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName", timesheet.ConsultantNum);
            return View(timesheet);
        }

        // GET: TimesheetHistory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.Timesheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }

        // POST: TimesheetHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Timesheet timesheet = db.Timesheets.Find(id);
            db.Timesheets.Remove(timesheet);
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
