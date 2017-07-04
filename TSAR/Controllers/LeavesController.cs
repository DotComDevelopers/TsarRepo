using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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
            var leaves = db.Leaves.Include(l => l.Consultant);
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
            var conUserName = User.Identity.GetUserName();
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName");
            ViewBag.LeaveTypeId = new SelectList(db.LeaveTypes, "LeaveTypeId", "LeaveTypeName");
            ViewBag.Gender = (from Consultant c in db.Consultants
                              where c.ConsultantUserName == conUserName
                              select c.Gender).FirstOrDefault();
            ViewBag.FirstName = (from Consultant c in db.Consultants
                                 where c.ConsultantUserName == conUserName
                                 select c.FirstName).FirstOrDefault();
            ViewBag.LeaveBal = (from Consultant c in db.Consultants
                                where c.ConsultantUserName == conUserName
                                select c.LeaveBalance).FirstOrDefault();

            //13/06/17
            ViewBag.AnnualLeaveBal = (from Consultant c in db.Consultants
                                      where c.ConsultantUserName == conUserName
                                      select c.AnnualLeaveBalance).FirstOrDefault();

            ViewBag.MaternityLeaveBal = (from Consultant c in db.Consultants
                                         where c.ConsultantUserName == conUserName
                                         select c.MaternityLeaveBalance).FirstOrDefault();

            ViewBag.PaternityLeaveBal = (from Consultant c in db.Consultants
                                         where c.ConsultantUserName == conUserName
                                         select c.PaternityLeaveBalance).FirstOrDefault();

            ViewBag.SickLeaveBal = (from Consultant c in db.Consultants
                                    where c.ConsultantUserName == conUserName
                                    select c.SickLeaveBalance).FirstOrDefault();

            ViewBag.FamilyLeaveBal = (from Consultant c in db.Consultants
                                      where c.ConsultantUserName == conUserName
                                      select c.FamilyResponsibilityBalance).FirstOrDefault();

            //count++;
            return View();
        }
        //public int count;

        // POST: Leaves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LeaveId,FirstName,ApprovedBy,IsConfirmed,LeaveDecsription,AccumulatedLeave,AllocatedLeave,LeaveCount,LeaveDate,ReturnDate,LeaveTypeId,ConsultantNum,LeaveTypeName")] Leave leave)
        {
            var conUserName = User.Identity.GetUserName();
            if (ModelState.IsValid)
            {
                //var c = new Consultant();
                //if (c.Gender.Equals("Male"))
                // {

                //}
                if (User.IsInRole("Consultant"))
                {
                    leave.IsConfirmed = false;
                    leave.ApprovedBy = "Sent for Approval";
                }
                //if (ViewBag.Gender == "Female")
                //{
                //    leave.MaternityBalance
                //}
                var conleavebal = (from Consultant c in db.Consultants
                                   where c.ConsultantUserName == conUserName
                                   select c.LeaveBalance).FirstOrDefault();

                //13/06/17
                var annleavebal = (from Consultant c in db.Consultants
                                   where c.ConsultantUserName == conUserName
                                   select c.AnnualLeaveBalance).FirstOrDefault();

                var matleavebal = (from Consultant c in db.Consultants
                                   where c.ConsultantUserName == conUserName
                                   select c.MaternityLeaveBalance).FirstOrDefault();

                var patleavebal = (from Consultant c in db.Consultants
                                   where c.ConsultantUserName == conUserName
                                   select c.PaternityLeaveBalance).FirstOrDefault();

                var sickleavebal = (from Consultant c in db.Consultants
                                    where c.ConsultantUserName == conUserName
                                    select c.SickLeaveBalance).FirstOrDefault();

                var famleavebal = (from Consultant c in db.Consultants
                                   where c.ConsultantUserName == conUserName
                                   select c.FamilyResponsibilityBalance).FirstOrDefault();

                //count++;
                leave.FirstName = (from Consultant c in db.Consultants where c.ConsultantUserName == conUserName select c.FirstName).FirstOrDefault();
                //leave.LeaveCount = count;
                leave.ConsultantNum = (from Consultant c in db.Consultants where c.ConsultantUserName == conUserName select c.ConsultantNum).FirstOrDefault();
                leave.AllocatedLeave = conleavebal;

                //13/06/17
                leave.AnnualBalance = annleavebal;
                leave.MaternityBalance = matleavebal;
                leave.PaternityBalance = patleavebal;
                leave.SickBalance = sickleavebal;
                leave.FamilyResponsibilityBalance = famleavebal;

                //if (leave.AllocatedLeave <= 0)
                //{
                //  leave.AccumulatedLeave = 0;
                //}
                leave.LeaveCount = (leave.ReturnDate - leave.LeaveDate).Days;

                leave.AccumulatedLeave = conleavebal;

                //var consultant = (from Consultant c in db.Consultants where c.ConsultantUserName == conUserName select c).FirstOrDefault();
                //{
                //  if (consultant != null) consultant.LeaveBalance = leave.AccumulatedLeave;
                //};         
                //db.Entry(consultant).State = EntityState.Modified;
                db.Leaves.Add(leave);
                db.SaveChanges();

                //var twilioSmsClient = new TwilioSmsRestClient();
                //var smsStatusResult = twilioSmsClient.SendMessage($"Leave Application created Successfully. Leave Application Reference {leave.LeaveId}");

                //if (smsStatusResult.IsCompleted)
                //{
                //    return RedirectToAction("Done");
                //}
                //else
                //{//an appropriate message stating sms failed error, either try again it
                return View(leave);
                //}

                //   return RedirectToAction("Index"); 
            }

            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName", leave.ConsultantNum);
            //ViewBag.LeaveTypeId = new SelectList(db.LeaveTypes, "LeaveTypeId", "LeaveTypeName", leave.LeaveTypeId);
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

            ViewBag.FirstName = (from Leave l
              in db.Leaves
                                 where l.LeaveId == leave.LeaveId
                                 select l.FirstName).FirstOrDefault();

            ViewBag.LeaveDecsription = (from Leave l
              in db.Leaves
                                        where l.LeaveId == leave.LeaveId
                                        select l.LeaveDecsription).FirstOrDefault();

            //ViewBag.AccumulatedLeaveString = (from Leave l
            //  in db.Leaves
            //  where l.LeaveId == leave.LeaveId
            //  select l.AccumulatedLeave).FirstOrDefault().ToString();

            var conleaveid = (from Leave l
              in db.Leaves
                              where l.LeaveId == leave.LeaveId
                              select l.ConsultantNum).FirstOrDefault();

            ViewBag.AccumulatedLeaveString = (from Consultant c in db.Consultants
                                              where c.ConsultantNum == conleaveid
                                              select c.LeaveBalance).FirstOrDefault();

            ViewBag.AccumulatedLeaveInt = (from Leave l
              in db.Leaves
                                           where l.LeaveId == leave.LeaveId
                                           select l.AccumulatedLeave).FirstOrDefault();

            ViewBag.AllocatedLeave = (from Leave l
              in db.Leaves
                                      where l.LeaveId == leave.LeaveId
                                      select l.AllocatedLeave).FirstOrDefault();

            ViewBag.LeaveCount = (from Leave l
              in db.Leaves
                                  where l.LeaveId == leave.LeaveId
                                  select l.LeaveCount).FirstOrDefault();

            ViewBag.LeaveDate = (from Leave l
              in db.Leaves
                                 where l.LeaveId == leave.LeaveId
                                 select l.LeaveDate).FirstOrDefault().ToShortDateString();

            ViewBag.ReturnDate = (from Leave l
              in db.Leaves
                                  where l.LeaveId == leave.LeaveId
                                  select l.ReturnDate).FirstOrDefault().ToShortDateString();

            ViewBag.LeaveTypeName = (from Leave l
              in db.Leaves
                                     where l.LeaveId == leave.LeaveId
                                     select l.LeaveTypeName).FirstOrDefault();

            ViewBag.ApprovedBy = (from Leave l
              in db.Leaves
                                  where l.LeaveId == leave.LeaveId
                                  select l.ApprovedBy).FirstOrDefault();

            //13/06/17
            ViewBag.AnnualLeaveBal = (from Leave l in db.Leaves
                                  where l.LeaveId == leave.LeaveId
                                  select l.AnnualBalance).FirstOrDefault();

            ViewBag.MaternityLeaveBal = (from Leave l in db.Leaves
                                      where l.LeaveId == leave.LeaveId
                                      select l.MaternityBalance).FirstOrDefault();

            ViewBag.PaternityLeaveBal = (from Leave l in db.Leaves
                                      where l.LeaveId == leave.LeaveId
                                      select l.PaternityBalance).FirstOrDefault();

            ViewBag.SickLeaveBal = (from Leave l in db.Leaves
                                      where l.LeaveId == leave.LeaveId
                                      select l.SickBalance).FirstOrDefault();

            ViewBag.FamilyLeaveBal = (from Leave l in db.Leaves
                                      where l.LeaveId == leave.LeaveId
                                      select l.FamilyResponsibilityBalance).FirstOrDefault();


            //ViewBag.LeaveTypeId = new SelectList(db.LeaveTypes, "LeaveTypeId", "LeaveTypeName", leave.LeaveTypeId);
            return View(leave);
        }

        // POST: Leaves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LeaveId,FirstName,ApprovedBy,IsConfirmed,LeaveDecsription,AccumulatedLeave,AllocatedLeave,LeaveCount,LeaveDate,ReturnDate,ConsultantNum,LeaveTypeName")] Leave leave)
        {
            var conUserName = User.Identity.GetUserName();

            var conleaveid = (from Leave l in db.Leaves
                              where l.LeaveId == leave.LeaveId
                              select l.Consultant.ConsultantUserName).FirstOrDefault();

            if (ModelState.IsValid)
            {
                leave.ConsultantNum = (from Leave l
                                       in db.Leaves
                                       where l.LeaveId == leave.LeaveId
                                       select l.ConsultantNum).FirstOrDefault();

                leave.FirstName = (from Leave l
                  in db.Leaves
                                   where l.LeaveId == leave.LeaveId
                                   select l.FirstName).FirstOrDefault();

                //leave.IsConfirmed = ViewBag.IsConfirmed;

                leave.LeaveDecsription = (from Leave l
                  in db.Leaves
                                          where l.LeaveId == leave.LeaveId
                                          select l.LeaveDecsription).FirstOrDefault();

                var conleavebal = (from Consultant c in db.Consultants
                                   where c.ConsultantUserName == conUserName
                                   select c.LeaveBalance).FirstOrDefault();

                //13/06/17
                var aleavebal = (from Consultant c in db.Consultants
                                   where c.ConsultantUserName == conUserName
                                   select c.AnnualLeaveBalance).FirstOrDefault();

                var mleavebal = (from Consultant c in db.Consultants
                                   where c.ConsultantUserName == conUserName
                                   select c.MaternityLeaveBalance).FirstOrDefault();

                var pleavebal = (from Consultant c in db.Consultants
                                   where c.ConsultantUserName == conUserName
                                   select c.PaternityLeaveBalance).FirstOrDefault();

                var sleavebal = (from Consultant c in db.Consultants
                                   where c.ConsultantUserName == conUserName
                                   select c.SickLeaveBalance).FirstOrDefault();

                var fleavebal = (from Consultant c in db.Consultants
                                   where c.ConsultantUserName == conUserName
                                   select c.FamilyResponsibilityBalance).FirstOrDefault();

                leave.AccumulatedLeave = conleavebal - leave.LeaveCount + leave.AllocatedLeave;

                //13/06/17
                //leave.AnnualBalance = aleavebal - leave.LeaveCount + leave.AnnualBalance;
                //leave.MaternityBalance = mleavebal - leave.LeaveCount + leave.MaternityBalance;
               // leave.PaternityBalance = pleavebal - leave.LeaveCount + leave.PaternityBalance;
                //leave.SickBalance = sleavebal - leave.LeaveCount + leave.SickBalance;
                //leave.FamilyResponsibilityBalance = fleavebal - leave.LeaveCount + leave.FamilyResponsibilityBalance;

                leave.AllocatedLeave = (from Leave l
                  in db.Leaves
                                        where l.LeaveId == leave.LeaveId
                                        select l.AllocatedLeave).FirstOrDefault();

                leave.LeaveCount = (from Leave l
                  in db.Leaves
                                    where l.LeaveId == leave.LeaveId
                                    select l.LeaveCount).FirstOrDefault();

                leave.LeaveDate = (from Leave l
                  in db.Leaves
                                   where l.LeaveId == leave.LeaveId
                                   select l.LeaveDate).FirstOrDefault();

                leave.ReturnDate = (from Leave l
                  in db.Leaves
                                    where l.LeaveId == leave.LeaveId
                                    select l.ReturnDate).FirstOrDefault();

                leave.LeaveTypeName = (from Leave l
                  in db.Leaves
                                       where l.LeaveId == leave.LeaveId
                                       select l.LeaveTypeName).FirstOrDefault();

                //13/06/17
                leave.AnnualBalance = (from Leave l in db.Leaves
                                       where l.LeaveId == leave.LeaveId
                                       select l.AnnualBalance).FirstOrDefault();


                leave.MaternityBalance = (from Leave l in db.Leaves
                                       where l.LeaveId == leave.LeaveId
                                       select l.MaternityBalance).FirstOrDefault();

                leave.PaternityBalance = (from Leave l in db.Leaves
                                       where l.LeaveId == leave.LeaveId
                                       select l.PaternityBalance).FirstOrDefault();

                leave.SickBalance = (from Leave l in db.Leaves
                                       where l.LeaveId == leave.LeaveId
                                       select l.SickBalance).FirstOrDefault();

                leave.FamilyResponsibilityBalance = (from Leave l in db.Leaves
                                       where l.LeaveId == leave.LeaveId
                                       select l.FamilyResponsibilityBalance).FirstOrDefault();

                if (leave.LeaveTypeName == "Annual")
                {
                    leave.AnnualBalance = aleavebal - leave.LeaveCount + leave.AnnualBalance;
                }
                else
                {
                    if ((leave.LeaveTypeName == "Maternity") && (ViewBag.Gender == "Female"))
                    {
                        leave.MaternityBalance = mleavebal - leave.LeaveCount + leave.MaternityBalance;
                    }
                    else
                    {
                        if ((leave.LeaveTypeName == "Paternity")&&(ViewBag.Gender == "Male"))
                        {
                            leave.PaternityBalance = pleavebal - leave.LeaveCount + leave.PaternityBalance;
                        }
                        else
                        {
                            if (leave.LeaveTypeName == "Sick")
                            {
                                leave.SickBalance = sleavebal - leave.LeaveCount + leave.SickBalance;
                            }
                            else
                            {
                                if (leave.LeaveTypeName == "Family Resonsibilty")
                                {
                                    leave.FamilyResponsibilityBalance = fleavebal - leave.LeaveCount + leave.FamilyResponsibilityBalance;
                                }
                            }
                        }
                    }
                }

                if (leave.IsConfirmed == true)
                {
                    leave.ApprovedBy = "Approved by " + User.Identity.Name;
                }
                else
                {
                    leave.ApprovedBy = "Not Approved";
                }

                var consultant = (from Consultant c in db.Consultants
                                  where c.ConsultantUserName == conleaveid
                                  select c).FirstOrDefault();

                {
                    if (consultant != null) consultant.LeaveBalance = leave.AccumulatedLeave;
                };

                //13/06/17
                {
                    if (consultant != null) consultant.AnnualLeaveBalance = leave.AnnualBalance;
                };
                {
                    if (consultant != null) consultant.MaternityLeaveBalance = leave.MaternityBalance;
                };
                {
                    if (consultant != null) consultant.PaternityLeaveBalance = leave.PaternityBalance;
                };
                {
                    if (consultant != null) consultant.SickLeaveBalance = leave.SickBalance;
                };
                {
                    if (consultant != null) consultant.FamilyResponsibilityBalance = leave.FamilyResponsibilityBalance;
                };

                db.Entry(consultant).State = EntityState.Modified;

                db.Entry(leave).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }

                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName", leave.ConsultantNum);
            //ViewBag.LeaveTypeId = new SelectList(db.LeaveTypes, "LeaveTypeId", "LeaveTypeName", leave.LeaveTypeId);
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

        public ActionResult MyLeave()
        {

            var username = User.Identity.GetUserName();
            var cn = (from Consultant c in db.Consultants
                      where c.ConsultantUserName == username
                      select c.ConsultantNum).FirstOrDefault();
            ViewBag.Name = (from Consultant c in db.Consultants
                            where c.ConsultantUserName == username
                            select c.FirstName).FirstOrDefault();
            ViewBag.LeaveBal = (from Consultant c in db.Consultants
                                where c.ConsultantUserName == username
                                select c.LeaveBalance).FirstOrDefault();


            return View(db.Leaves.Where(p => p.ConsultantNum == cn).ToList());

        }

        [HttpPost]
        public ActionResult MyLeave(string searchTerm)
        {
            var username = User.Identity.GetUserName();
            var cn = (from Consultant c in db.Consultants
                      where c.Email == username
                      select c.FirstName).FirstOrDefault();
            return View(db.Leaves.Where(x => x.FirstName.StartsWith(searchTerm)).ToList());
        }
    }
}
