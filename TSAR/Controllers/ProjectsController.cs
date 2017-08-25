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
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Projects
        public ActionResult Index()
        {
            var projects = db.Projects.Include(p => p.Client).Include(p => p.Consultant);
            return View(projects.ToList());
        }


        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projects projects = db.Projects.Find(id);


            if (projects == null)
            {
                return HttpNotFound();
            }
            return View(projects);
        }


        //[HttpPost]
        //public ActionResult ProjectTimeSheetIndex(Timesheet timesheet, int? id)
        //{
        //    var projects = db.Projects.Find(id);

        //    var timesheets = db.Timesheets.Include(t => t.Client).Include(t => t.Consultant).Where(a => a.ProjectId == timesheet.ProjectId);
        //    var r = timesheets.ToList();
        //    return View(r);


        //}

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName");
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectId,ProjectName,Id,ConsultantNum,StartDate,EndDate")] Projects projects)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(projects);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName", projects.Id);
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName", projects.ConsultantNum);
            return View(projects);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projects projects = db.Projects.Find(id);
            if (projects == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName", projects.Id);
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName", projects.ConsultantNum);
            return View(projects);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,ProjectName,Id,ConsultantNum,StartDate,EndDate")] Projects projects)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projects).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName", projects.Id);
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FirstName", projects.ConsultantNum);
            return View(projects);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projects projects = db.Projects.Find(id);
            if (projects == null)
            {
                return HttpNotFound();
            }
            return View(projects);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Projects projects = db.Projects.Find(id);
            db.Projects.Remove(projects);
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
