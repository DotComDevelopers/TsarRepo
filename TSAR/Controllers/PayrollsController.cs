using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TSAR.Models;

namespace TSAR.Controllers
{
    [Authorize(Roles = "Consultant")]
    public class PayrollsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Payrolls
        public ActionResult Index()
        {
            //int dateOfMonth = DateTime.Now.Day;
            Payroll payroll = new Payroll();
            //if (dateOfMonth >= 25)
            {
                {

                    string email = User.Identity.GetUserName();

                    var consultant = (from Consultant c in db.Consultants
                        where c.ConsultantUserName == email
                        select c.ConsultantNum).FirstOrDefault();

                    var totalValue = db.Timesheets.Where(p => p.ConsultantNum == consultant);
                    ViewBag.NoTimesheets = totalValue.Count();
                    var tottimesheet = totalValue.Sum(u => u.Total);

                    ViewBag.Comm = payroll.Comm = tottimesheet * 0.1;

                    ViewBag.Basic = payroll.Basic = 5000;

                    ViewBag.Totpay = payroll.totPay = payroll.Basic + payroll.Comm;

                    ViewBag.Tax = payroll.tax = payroll.totPay * 0.2;

                }
                return View(payroll);
            }
        }

        // GET: Payrolls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payroll payroll = db.Payrolls.Find(id);
            if (payroll == null)
            {
                return HttpNotFound();
            }
            return View(payroll);
        }

        // GET: Payrolls/Create
        public ActionResult Create()
        {
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName");
            return View();
        }

        // POST: Payrolls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PayrollId,Basic,Comm,totPay,tax,ConsultantNum,total")] Payroll payroll)
        {


            if (ModelState.IsValid)
            {
                db.Payrolls.Add(payroll);
                db.SaveChanges();
                return RedirectToAction("Index");

                ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName", payroll.ConsultantNum);
            }
            return View(payroll);


        }

        // GET: Payrolls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payroll payroll = db.Payrolls.Find(id);
            if (payroll == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName", payroll.ConsultantNum);
            return View(payroll);
        }

        // POST: Payrolls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PayrollId,Basic,Comm,totPay,tax,ConsultantNum,Hours,total")] Payroll payroll)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payroll).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName", payroll.ConsultantNum);
            return View(payroll);
        }

        // GET: Payrolls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payroll payroll = db.Payrolls.Find(id);
            if (payroll == null)
            {
                return HttpNotFound();
            }
            return View(payroll);
        }

        // POST: Payrolls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payroll payroll = db.Payrolls.Find(id);
            db.Payrolls.Remove(payroll);
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

