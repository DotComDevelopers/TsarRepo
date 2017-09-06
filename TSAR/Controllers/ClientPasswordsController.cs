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
    public class ClientPasswordsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ClientPasswords
        public ActionResult Index()
        {
            var clientPasswords = db.ClientPasswords.Include(c => c.Client);
            return View(clientPasswords.ToList());
        }

        // GET: ClientPasswords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientPassword clientPassword = db.ClientPasswords.Find(id);
            if (clientPassword == null)
            {
                return HttpNotFound();
            }
            return View(clientPassword);
        }

        // GET: ClientPasswords/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.Clients, "Id", "ClientName");
            return View();
        }

        // POST: ClientPasswords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientPasswordId,id,Operator,OperatorPassword,CompanyPassword")] ClientPassword clientPassword)
        {
            if (ModelState.IsValid)
            {
                db.ClientPasswords.Add(clientPassword);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.Clients, "Id", "ClientName", clientPassword.id);
            return View(clientPassword);
        }

        // GET: ClientPasswords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientPassword clientPassword = db.ClientPasswords.Find(id);
            if (clientPassword == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.Clients, "Id", "ClientName", clientPassword.id);
            return View(clientPassword);
        }

        // POST: ClientPasswords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientPasswordId,id,Operator,OperatorPassword,CompanyPassword")] ClientPassword clientPassword)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientPassword).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id = new SelectList(db.Clients, "Id", "ClientName", clientPassword.id);
            return View(clientPassword);
        }

        // GET: ClientPasswords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientPassword clientPassword = db.ClientPasswords.Find(id);
            if (clientPassword == null)
            {
                return HttpNotFound();
            }
            return View(clientPassword);
        }

        // POST: ClientPasswords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientPassword clientPassword = db.ClientPasswords.Find(id);
            db.ClientPasswords.Remove(clientPassword);
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
