using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TSAR.Models;
using TSAR.SmsHelper;


namespace TSAR.Controllers
{
    [Authorize(Roles = "Admin, Consultant")]
    public class LeavesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Leaves
        public ActionResult Index()
        {
            var leaves = db.Leaves.Include(l => l.Consultant).Include(l => l.LeaveType);
            return View(leaves.ToList());
        }

        // GET: Leaves/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leave leave = db.Leaves.Find(id);
            if (leave == null)
            {
                return HttpNotFound();
            }
            return View(leave);
        }

        //public int count;
        // GET: Leaves/Create
        public ActionResult Create()
        {
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName");
            ViewBag.LeaveTypeId = new SelectList(db.LeaveTypes, "LeaveTypeId", "LeaveTypeName");

            //count++;
            return View();
        }
        public int count;
        // POST: Leaves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LeaveId,FirstName,ApprovedBy,IsConfirmed,LeaveDecsription,AccumulatedLeave,AllocatedLeave,LeaveCount,LeaveDate,ReturnDate,LeaveTypeId,ConsultantNum,Gender")] Leave leave)
        {
            if (ModelState.IsValid)
            {
                //var c = new Consultant();
                //if (c.Gender.Equals("Male"))

                //}
                leave.IsConfirmed = false;
                count++;
                leave.LeaveCount = count;
                leave.AllocatedLeave = 24;
                leave.AccumulatedLeave = leave.AllocatedLeave - leave.LeaveCount;
                db.Leaves.Add(leave);
                db.SaveChanges();

                var twilioSmsClient = new TwilioSmsRestClient();
                var smsStatusResult = twilioSmsClient.SendMessage($"Leave Application created Successfully. Leave Application Reference {leave.LeaveId}");

                if (smsStatusResult.IsCompleted)
                {
                    return RedirectToAction("Done");
                }
                else
                {//an appropriate message stating sms failed error, either try again it
                    return View(leave);
                }

                return RedirectToAction("Index"); //>>>>>>>>>>Was created automatically with controller >>>> Edit also has this
            }

            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName", leave.ConsultantNum);
            ViewBag.LeaveTypeId = new SelectList(db.LeaveTypes, "LeaveTypeId", "LeaveTypeName", leave.LeaveTypeId);
            return View(leave);
        }

        // GET: Leaves/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leave leave = db.Leaves.Find(id);
            if (leave == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName", leave.ConsultantNum);
            ViewBag.LeaveTypeId = new SelectList(db.LeaveTypes, "LeaveTypeId", "LeaveTypeName", leave.LeaveTypeId);
            return View(leave);
        }

        // POST: Leaves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LeaveId,FirstName,ApprovedBy,IsConfirmed,LeaveDecsription,AccumulatedLeave,AllocatedLeave,LeaveCount,LeaveDate,ReturnDate,LeaveTypeId,ConsultantNum,Gender")] Leave leave)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leave).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName", leave.ConsultantNum);
            ViewBag.LeaveTypeId = new SelectList(db.LeaveTypes, "LeaveTypeId", "LeaveTypeName", leave.LeaveTypeId);
            return View(leave);
        }

        // GET: Leaves/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leave leave = db.Leaves.Find(id);
            if (leave == null)
            {
                return HttpNotFound();
            }
            return View(leave);
        }

        // POST: Leaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Leave leave = db.Leaves.Find(id);
            db.Leaves.Remove(leave);
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
