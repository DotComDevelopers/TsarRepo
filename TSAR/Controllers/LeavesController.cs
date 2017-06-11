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

      //count++;
      return View();
    }
    public int count;
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

        //}
        if (User.IsInRole("Consultant"))
        {
          leave.IsConfirmed = false;
        }

        count++;
        leave.FirstName = (from Consultant c in db.Consultants where c.ConsultantUserName == conUserName select c.FirstName).FirstOrDefault();
        leave.LeaveCount = count;
        leave.ConsultantNum = (from Consultant c in db.Consultants where c.ConsultantUserName == conUserName select c.ConsultantNum).FirstOrDefault();
        leave.AllocatedLeave = 24;
        //var leavetype = (from LeaveType c 
        //                     in db.LeaveTypes where 
        //                     c.LeaveTypeId == leave.LeaveTypeId
        //                     select c.LeaveTypeName).FirstOrDefault();
        //leave.LeaveTypeName
        //leave.LeaveTypeName = leavetype;
        leave.AccumulatedLeave = leave.AllocatedLeave - leave.LeaveCount;
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
        //    return View(leave);
        //}

        return RedirectToAction("Index"); //>>>>>>>>>>Was created automatically with controller >>>> Edit also has this
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

      ViewBag.AccumulatedLeaveString = (from Leave l
        in db.Leaves
        where l.LeaveId == leave.LeaveId
        select l.AccumulatedLeave).FirstOrDefault().ToString();

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

        leave.AccumulatedLeave = (from Leave l
          in db.Leaves
          where l.LeaveId == leave.LeaveId
          select l.AccumulatedLeave).FirstOrDefault();

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

        if (leave.IsConfirmed == true)
        {
          leave.ApprovedBy = User.Identity.Name;
        }
        else
        {
          leave.ApprovedBy = "Not Approved";
        }
       
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
  }
}
