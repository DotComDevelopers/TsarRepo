﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.UI;
using TSAR.Models;
using TSAR.ViewModels;

namespace TSAR.Controllers

{
    [Authorize(Roles = "Admin")]
    public class ClientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clients
        public ActionResult Index(string Search)
        {
            if (Search == null)
            {

                
                return View(db.Clients.ToList());
            }
            else
                return View(db.Clients.Where(p => p.ClientName == Search).ToList());

        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            ViewBag.TravelCode = new SelectList(db.ManageTravels, "TravelCode", "TravelCode");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TravelViewModel model)
        {
            Client client = new Client();

            ManageTravel mt = new ManageTravel();


            if (model.ClientAddress != null)
            {

                client.ClientName = model.ClientName;
                client.Branch = model.Branch;
                client.ClientAddress = model.ClientAddress;
                client.ContactNumber = model.ContactNumber;
                client.Email = model.Email;
                client.ProjectLeader = model.ProjectLeader;

                mt.TravelCode = model.ClientName.Substring(0, 4);

                mt.Rate = model.Rate;

                mt.Distance = model.Distance;
                double p;
                int count = 0;
                foreach (char item in model.Distance)
                {
                    if (item != null)
                    {
                        count++;

                        if (count == 3)
                        {
                            p = Convert.ToDouble(model.Distance.Substring(0, 1));
                            mt.TravelFee = ((mt.Rate * p) * 2);
                        }
                        else if (count == 4)
                        {
                            p = Convert.ToDouble(model.Distance.Substring(0, 2));
                            mt.TravelFee = ((mt.Rate * p) * 2);
                        }
                        else if (count == 5)
                        {
                            p = Convert.ToDouble(model.Distance.Substring(0, 3));
                            mt.TravelFee = ((mt.Rate * p) * 2);
                        }
                        else if (count == 6)

                        {
                            p = Convert.ToDouble(model.Distance.Substring(0, 4));
                            mt.TravelFee = ((mt.Rate * p) * 2);
                        }
                        else
                        {
                            p = Convert.ToDouble(model.Distance.Substring(0, 2));
                            mt.TravelFee = ((mt.Rate * p) * 2);

                        }

                    }
                }





                db.ManageTravels.Add(mt);

                client.TravelCode = model.ClientName.Substring(0, 4);



                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }




            return View(model);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.TravelCode = new SelectList(db.ManageTravels, "TravelCode", "TravelCode", client.TravelCode);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClientName,Branch,ClientAddress,ContactNumber,Email,ClientType,ProjectLeader,TravelCode,RoleType")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TravelCode = new SelectList(db.ManageTravels, "TravelCode", "TravelCode", client.TravelCode);
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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
