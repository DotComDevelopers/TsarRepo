using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Net;
using System.Web;

using System.Web.Mvc;
using Rotativa;
using TSAR.Models;


namespace TSAR.Controllers
{
    [Authorize(Roles = "Consultant")]
    public class PayslipController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Payslip
        public ActionResult Index()
        {


      Payroll payslip = new Payroll();
            {
                    string username = User.Identity.GetUserName();

                    var consultantnum = (from Consultant c in db.Consultants
                                      where c.ConsultantUserName == username
                                      select c.ConsultantNum).FirstOrDefault();

                    var totalValue = db.Timesheets.Where(p => p.ConsultantNum == consultantnum);
    

                    ViewBag.NoTimesheets = totalValue.Count();

                    var tottimesheet = totalValue.Sum(u => u.Total);

                    ViewBag.Comm = (payslip.Comm = tottimesheet * 0.2).ToString("R0.00");

                    ViewBag.Basic = (payslip.Basic = 7000).ToString("R0.00");
              payslip.Basic = (payslip.Basic = 7000);

                    ViewBag.Totpay = (payslip.totPay = payslip.Basic + payslip.Comm).ToString("R0.00");
                    ViewBag.Name = User.Identity.GetUserName();
                    var totpay = (payslip.totPay = payslip.Basic + payslip.Comm);

                
                    var tax = 0;
                    if (totpay >= 7500 && totpay <= 15500)
                    {
                        tax = 2800;
                    }
                    else if (totpay >= 15750 && totpay <= 24700)
                    {
                        tax = 4700;
                    }
                    else if (totpay >= 24750 && totpay <= 35000)
                    {
                        tax = 7500;
                    }

                    ViewBag.Tax = tax;

                    ViewBag.Net = (totpay - tax).ToString("R0.00");
                

            }

          return new ViewAsPdf("Index", payslip);
     
    }

    // GET: Payslip/Details/5
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

        // GET: Payslip/Create
        public ActionResult Create()
        {
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName");
            return View();
        }

        // POST: Payslip/Create
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
            }

            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName", payroll.ConsultantNum);
            return View(payroll);
        }

        // GET: Payslip/Edit/5
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

        // POST: Payslip/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PayrollId,Basic,Comm,totPay,tax,ConsultantNum,total")] Payroll payroll)
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

        // GET: Payslip/Delete/5
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

        // POST: Payslip/Delete/5
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
