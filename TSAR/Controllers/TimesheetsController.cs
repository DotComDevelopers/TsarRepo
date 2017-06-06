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
    [Authorize(Roles = "Admin,Consultant,Client")]
    public class TimesheetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Timesheets
        public ActionResult Index()
        {
            string username = User.Identity.GetUserName();
            int consultantnum = (from Consultant c in db.Consultants
                where c.ConsultantUserName == username
                select c.ConsultantNum).FirstOrDefault();
            var timesheets = db.Timesheets.Include(t => t.Client).Include(t => t.Consultant);
            List<Timesheet> index=null;
            if(User.IsInRole("Admin"))
            {
              index =  timesheets.ToList();
            }
            else if (User.IsInRole("Consultant"))
            {

                index = db.Timesheets.Where(p => p.ConsultantNum==consultantnum).ToList();
            }

            return View(index);
        }

        public ActionResult ViewTimeheets()
        {
            var timesheets = db.Timesheets.Include(t => t.Client).Include(t => t.Consultant);
            string username = User.Identity.GetUserName();
            int clientid = (from Client c in db.Clients
                            where c.Email == username
                            select c.Id).FirstOrDefault();
            return View(db.Timesheets.Where(p => p.Id==clientid).ToList());
        }
        // GET: Timesheets/Details/5
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


        

        // GET: Timesheets/Create
        public ActionResult Create()
        {
            
            ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName");
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FullName");
            return View();
        }

        // POST: Timesheets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimesheetId,CaptureDate,StartTime,EndTime,ActivityDescription,Total,Hours,Id,ConsultantNum,TicketReference,SignOff")] Timesheet timesheet,string tickRef)
        {           

            if (ModelState.IsValid)
            {
                timesheet.TicketReference = tickRef;
                string clientname = (from Ticket t in db.Tickets
                                     where t.TicketReference == tickRef
                                     select t.ClientName).FirstOrDefault();

                timesheet.Id = (from Client c in db.Clients
                                where c.ClientName == clientname
                                select c.Id).FirstOrDefault();

                timesheet.ConsultantNum = (from Ticket t in db.Tickets
                                             where t.TicketReference == tickRef
                                             select t.ConsultantId).FirstOrDefault();
                timesheet.CaptureDate = System.DateTime.Now;
                timesheet.Hours = (timesheet.EndTime - timesheet.StartTime).TotalHours;
                timesheet.Total = (700 * (timesheet.EndTime - timesheet.StartTime).TotalHours);
                db.Timesheets.Add(timesheet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName", timesheet.Id);
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FullName", timesheet.ConsultantNum);
            return View(timesheet);
        }

        // GET: Timesheets/Edit/5
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
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FullName", timesheet.ConsultantNum);
            return View(timesheet);
        }

        // POST: Timesheets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TimesheetId,CaptureDate,StartTime,EndTime,ActivityDescription,Total,Hours,Id,ConsultantNum,TicketReference,SignOff")] Timesheet timesheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timesheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName", timesheet.Id);
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FullName", timesheet.ConsultantNum);
            return View(timesheet);
        }

        // GET: Timesheets/Delete/5
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

        // POST: Timesheets/Delete/5
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


        public ActionResult SignOff(int? id)
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
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FullName", timesheet.ConsultantNum);
            return View(timesheet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignOff([Bind(Include = "TimesheetId,CaptureDate,StartTime,EndTime,ActivityDescription,Total,Hours,Id,ConsultantNum,TicketReference,SignOff")] Timesheet timesheet)
        {
            if (ModelState.IsValid)
            {
                if (timesheet.SignOff == true)
                {
                    Ticket tick = db.Tickets.Where(p => p.TicketReference == timesheet.TicketReference).SingleOrDefault();
                    tick.Status = "Closed-Ticket";
                    db.Entry(tick).State = EntityState.Modified;
                    db.SaveChanges();
                }
                db.Entry(timesheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Create","Rating");

            }
            ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName", timesheet.Id);
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FullName", timesheet.ConsultantNum);
            return View(timesheet);
        }
    }
}
                                                                                                