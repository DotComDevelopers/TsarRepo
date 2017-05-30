using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TSAR.Models;
using TSAR.ViewModels;

namespace TSAR.Controllers
{
    public class TravelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Travel
        public ActionResult Index()
        {
            var travels = db.Travels.Include(t => t.Client);
            return View(travels.ToList());
        }

        // GET: Travel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Travel travel = db.Travels.Find(id);
            if (travel == null)
            {
                return HttpNotFound();
            }
            return View(travel);
        }

        // GET: Travel/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName");
            return View();
        }

        // POST: Travel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TravelViewModel model)
        {
            Travel travel = new Travel();
            if (model.Distance != null)
            {
                if (model.ClientAddress != null)
                {

                    travel.Id = model.Id;

                    travel.ClientAddress = model.ClientAddress;



                    travel.TravelRate = 3.55;

                    travel.Distance = model.Distance;
                    double p;
                    int count = 0;
                    foreach (char item in model.Distance)
                    {
                        if (item != '_')
                        {
                            count++;

                            if (count == 3)
                            {
                                //p = Convert.ToDouble(model.Distance.Substring(0, 1));
                                p = double.Parse(model.Distance.Substring(0, 1), CultureInfo.InvariantCulture);
                                travel.TravelFee = ((travel.TravelRate * p) * 2);
                            }
                            else if (count == 4)
                            {
                                //p = Convert.ToDouble(model.Distance.Substring(0, 2));
                                p = double.Parse(model.Distance.Substring(0, 2), CultureInfo.InvariantCulture);
                                travel.TravelFee = ((travel.TravelRate * p) * 2);
                            }
                            else if (count == 5)
                            {
                                //p = Convert.ToDouble(model.Distance.Substring(0, 3));
                                p = double.Parse(model.Distance.Substring(0, 3), CultureInfo.InvariantCulture);
                                travel.TravelFee = ((travel.TravelRate * p) * 2);
                            }
                            else if (count == 6)

                            {
                                //p = Convert.ToDouble(model.Distance.Substring(0, 4));
                                p = double.Parse(model.Distance.Substring(0, 4), CultureInfo.InvariantCulture);
                                travel.TravelFee = ((travel.TravelRate * p) * 2);
                            }
                            else
                            {
                                //p = Convert.ToDouble(model.Distance.Substring(0, 2));
                                p = double.Parse(model.Distance.Substring(0, 2), CultureInfo.InvariantCulture);
                                travel.TravelFee = ((travel.TravelRate * p) * 2);

                            }

                        }

                    }
                }


                db.Travels.Add(travel);
                    db.SaveChanges();
                    ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName", travel.Id);
                    string Address1 = (from Client c in db.Clients
                        where c.Id == model.Id
                        select c.ClientAddress).FirstOrDefault();
                    string Address2 = (from Client c in db.Clients
                        where c.Id == model.Id
                        select c.ClientAddress2).FirstOrDefault();

                ViewBag.Addresses = new SelectList(Address1, Address2);

                    return RedirectToAction("Index");

                }



                return View(model);
            }
        

        // GET: Travel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Travel travel = db.Travels.Find(id);
            if (travel == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName", travel.Id);
            return View(travel);
        }

        // POST: Travel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TravelId,Id,ClientAddress,TravelRate,Distance,TravelFee")] Travel travel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(travel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName", travel.Id);
            return View(travel);
        }

        // GET: Travel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Travel travel = db.Travels.Find(id);
            if (travel == null)
            {
                return HttpNotFound();
            }
            return View(travel);
        }

        // POST: Travel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Travel travel = db.Travels.Find(id);
            db.Travels.Remove(travel);
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
