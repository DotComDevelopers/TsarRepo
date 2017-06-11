using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Globalization;
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
            Travel mt = new Travel();
            Travel mt2 = new Travel();

            if (model.ClientName != null)
            {
                if (model.ClientAddress != null)
                {

                    client.ClientName = model.ClientName;

                    client.ClientAddress = model.ClientAddress;
                    client.ContactNumber = model.ContactNumber;
                    client.Email = model.Email;
                    client.ClientAddress2 = model.ClientAddress2;
                    client.ContactNumber2 = model.ContactNumber2;
                    client.Email2 = model.Email2;
                    //client.ProjectLeader = model.ProjectLeader;
                    //Adding a substring of the clients contactnumber to make the travel code more unique

                    //+ Convert.ToString(model.ContactNumber).Substring(0,2);
                    mt.MClientAddress = model.ClientAddress;
                    mt.TravelRate = 3.55;
                    mt.Id = model.Id;
                    mt.Distance = model.Distance;
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
                                mt.TravelFee = ((mt.TravelRate * p) * 2);
                            }
                            else if (count == 4)
                            {
                                //p = Convert.ToDouble(model.Distance.Substring(0, 2));
                                p = double.Parse(model.Distance.Substring(0, 2), CultureInfo.InvariantCulture);
                                mt.TravelFee = ((mt.TravelRate * p) * 2);
                            }
                            else if (count == 5)
                            {
                                //p = Convert.ToDouble(model.Distance.Substring(0, 3));
                                p = double.Parse(model.Distance.Substring(0, 3), CultureInfo.InvariantCulture);
                                mt.TravelFee = ((mt.TravelRate * p) * 2);
                            }
                            else if (count == 6)

                            {
                                //p = Convert.ToDouble(model.Distance.Substring(0, 4));
                                p = double.Parse(model.Distance.Substring(0, 4), CultureInfo.InvariantCulture);
                                mt.TravelFee = ((mt.TravelRate * p) * 2);
                            }
                            else
                            {
                                //p = Convert.ToDouble(model.Distance.Substring(0, 2));
                                p = double.Parse(model.Distance.Substring(0, 2), CultureInfo.InvariantCulture);
                                mt.TravelFee = ((mt.TravelRate * p) * 2);

                            }

                        }
                    }
                    db.Travels.Add(mt);
                }
                if (model.ClientAddress2 != null)
                {


                    mt2.MClientAddress = model.ClientAddress2;

                    mt2.TravelRate = 3.55;

                    mt2.Distance = model.Distance2;
                    double l;
                    int count2 = 0;
                    foreach (char item in model.Distance2)
                    {
                        if (item != '_')
                        {
                            count2++;

                            if (count2 == 3)
                            {
                                //l = Convert.ToDouble(model.Distance2.Substring(0, 1));
                                l = double.Parse(model.Distance2.Substring(0, 1), CultureInfo.InvariantCulture);
                                mt2.TravelFee = ((mt2.TravelRate * l) * 2);
                            }
                            else if (count2 == 4)
                            {
                                //l = Convert.ToDouble(model.Distance2.Substring(0, 2));
                                l = double.Parse(model.Distance2.Substring(0, 2), CultureInfo.InvariantCulture);
                                mt2.TravelFee = ((mt2.TravelRate * l) * 2);
                            }
                            else if (count2 == 5)
                            {
                                // l = Convert.ToDouble(model.Distance2.Substring(0, 3));
                                l = double.Parse(model.Distance2.Substring(0, 3), CultureInfo.InvariantCulture);
                                mt2.TravelFee = ((mt2.TravelRate * l) * 2);
                            }
                            else if (count2 == 6)

                            {
                                // l = Convert.ToDouble(model.Distance2.Substring(0, 4));
                                l = double.Parse(model.Distance2.Substring(0, 4), CultureInfo.InvariantCulture);
                                mt2.TravelFee = ((mt2.TravelRate * l) * 2);
                            }
                            else
                            {
                                // l = Convert.ToDouble(model.Distance2.Substring(0, 2));
                                l = double.Parse(model.Distance2.Substring(0, 2), CultureInfo.InvariantCulture);
                                mt2.TravelFee = ((mt2.TravelRate * l) * 2);

                            }

                        }

                    }
              db.Travels.Add(mt2);    
                
            }  
                

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
          
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClientName,Branch,ClientAddress,ContactNumber,Email,ClientAddress2,ContactNumber2,Email2,TravelCode")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
 
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
