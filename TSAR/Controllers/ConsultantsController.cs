using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using TSAR.Models;

namespace TSAR.Controllers
{
  [Authorize(Roles = "Admin")]
  public class ConsultantsController : Controller
  {

    private ApplicationUserManager _userManager;

    public ApplicationUserManager UserManager
    {
      get
      {
        return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
      }
      private set
      {
        _userManager = value;
      }
    }
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Clients
    public ActionResult Index(string Search)

    {
      var consultants = db.Consultants.Include(t => t.CommissionId);
    
      if (Search == null)
      {
        return View(db.Consultants.ToList());
      }
      else
        return View(db.Consultants.Where(p => p.FirstName == Search).ToList());
    }

    //test 

    // GET: Consultants/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Consultant consultant = db.Consultants.Find(id);
      if (consultant == null)
      {
        return HttpNotFound();
      }
      return View(consultant);
    }

    // GET: Consultants/Create
    public ActionResult Create()
    {
      ViewBag.CommissionId = new SelectList(db.Commissions, "CommissionId", "CommissionName");
      return View();
    }

    // POST: Consultants/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create([Bind(Include = "ConsultantNum,FirstName,LastName,FullName,ContactNumber,ConsultantAddress,Email,ConsultantType,CommissionId,Password,ConsultantUserName,Gender,LeaveBalance")] Consultant consultant)
    {

      if (ModelState.IsValid)
      {
        var consuser = new ApplicationUser
        {
          Email = consultant.Email,
          EmailConfirmed = true,
          PasswordHash = consultant.Password,
          UserName = consultant.ConsultantUserName,        
        };
        Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //var result = UserManager.CreateAsync(consuser, consultant.Password);
        
        //var userStore = new UserStore<ApplicationUser>(db);
        //var userManager = new UserManager<ApplicationUser>(userStore);        
        consultant.FullName = $"{consultant.FirstName} {consultant.LastName}";
        consultant.LeaveBalance = 24;
        db.Consultants.Add(consultant);
        db.Users.Add(consuser);
        //await result;
        db.SaveChanges();       
        //userManager.AddToRole(consuser.Id, "Consultant");


        //if (await UserManager.IsInRoleAsync(consuser.Id, "Member")) return RedirectToAction("Index", "Home");
        if (consultant.ConsultantUserName == consuser.UserName)
        {
          UserManager.AddToRole(consuser.Id, "Consultant");
        }

        return RedirectToAction("Index");

      }
      ViewBag.CommissionId = new SelectList(db.Commissions, "CommissionId", "CommissionName", consultant.CommissionId);
      return View(consultant);
    }

    // GET: Consultants/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Consultant consultant = db.Consultants.Find(id);
      if (consultant == null)
      {
        return HttpNotFound();
      }
      ViewBag.CommissionId = new SelectList(db.Commissions, "CommissionId", "CommissionName", consultant.CommissionId);
      return View(consultant);
    }

    // POST: Consultants/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "ConsultantNum,FirstName,LastName,FullName,ContactNumber,ConsultantAddress,Email,ConsultantType,CommissionId,Password,Gender,LeaveBalance")] Consultant consultant)
    {
      if (ModelState.IsValid)
      {
        db.Entry(consultant).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      ViewBag.CommissionId = new SelectList(db.Commissions, "CommissionId", "CommissionName", consultant.CommissionId);
      return View(consultant);
    }

    // GET: Consultants/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Consultant consultant = db.Consultants.Find(id);
      if (consultant == null)
      {
        return HttpNotFound();
      }
      return View(consultant);
    }

    // POST: Consultants/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      Consultant consultant = db.Consultants.Find(id);
      db.Consultants.Remove(consultant);
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
