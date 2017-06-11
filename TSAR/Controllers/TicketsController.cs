using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using TSAR.Models;
using TSAR.SmsHelper;

namespace TSAR.Controllers
{
    [Authorize(Roles = "Admin,Consultant,Client")]
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tickets
        public ActionResult Index(string status)
        {

            ViewBag.Status = (from r in db.Tickets
                select r.Status).Distinct();

            var model = from r in db.Tickets
                orderby  r.Date ascending
                        where r.Status == status || status == null || status == ""
                select r;

                return View(model);

            
              
          
        }

        public ActionResult MyTickets()
        {
            string username= User.Identity.GetUserName();
           string  cn = (from Consultant c in db.Consultants
                                     where c.ConsultantUserName == username
                                     select c.FullName).FirstOrDefault();

            return View(db.Tickets.Where(p => p.ConsultantName == cn));

        }

        [HttpPost]
        public ActionResult MyTickets(string searchTerm)
        {
            string username = User.Identity.GetUserName();
            string cn = (from Consultant c in db.Consultants
                         where c.Email == username
                         select c.FullName).FirstOrDefault();
            if (string.IsNullOrEmpty(searchTerm))
            {
                return View(db.Tickets.Where(p => p.ConsultantName == cn));
            }

            else
            {
                return View(db.Tickets.Where(x=>x.TicketReference.StartsWith(searchTerm)).ToList());
            }
          

        }

        public JsonResult GetTickets(string term)
        {
           
            List<string> tickets;
          
                tickets = db.Tickets.Where(x => x.TicketReference.StartsWith(term)).Select(y => y.TicketReference).ToList();

            return Json(tickets,JsonRequestBehavior.AllowGet);



        }
        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ClientName,Email,FaultDescription,Priority,Date,Category,ConsultantName,Status,ConsultantId,TicketReference")] Ticket ticket)
        {
            string tickRef =
                $"{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Second}{User.Identity.GetUserName().Substring(0, 4)}";
            if (ModelState.IsValid)
            {
                //created ticket ID should be returned as a reference
                
                string email = User.Identity.GetUserName();
                //For this to work a client must be created with the same email as the email registered to login as a client
                ticket.ClientName = (from Client c in db.Clients
                                     where c.Email == email
                                     select c.ClientName).FirstOrDefault();
                ticket.Email = email;
                ticket.Date = DateTime.Now;
                ticket.TicketReference = tickRef;
                if (ticket.Category == "Email" )
                {
                    ticket.Priority = "Low";
                }
                else if (ticket.Category == "Printer" || ticket.Category == "Other")
                {
                    ticket.Priority = "Medium";
                }
                else if (ticket.Category == "Hardware"|| ticket.Category == "Maintenance" || ticket.Category == "Network" || ticket.Category == "Software"  )
                {
                    ticket.Priority = "High";
                }
                if (ticket.Status == null)
                {
                    ticket.Status = "Open Ticket";
                }
                db.Tickets.Add(ticket);

                var textforevent = "Reference Number:    " + ticket.TicketReference + "      " + "Fault Description:    " + ticket.FaultDescription;
                var calendarevent = new Event()
                {
                    start_date = ticket.Date,
                    end_date = ticket.Date,
                    name = ticket.Category,
                    text = textforevent

                };
                db.Events.Add(calendarevent);
                db.SaveChanges();
                var twilioSmsClient = new TwilioSmsRestClient();
                var smsStatusResult = twilioSmsClient.SendMessage($"Ticket Created Successfully. Client Ticket Reference {ticket.ID}");

                if (smsStatusResult.IsCompleted)
                {
                   return RedirectToAction("Done");
                }
                else
                {//an appropriate message stating sms failed error, either try again it
                   return View(ticket);
                }

            }


            return RedirectToAction("Done");
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClientName,Email,FaultDescription,Priority,Date,Category,Consultant,Status,ConsultantId,TicketReference")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                if (ticket.Category == "Email")
                {
                    ticket.Priority = "Low";
                }
                else if (ticket.Category == "Printer"|| ticket.Category == "Other")
                {
                    ticket.Priority = "Medium";
                }
                else if (ticket.Category == "Hardware" || ticket.Category == "Maintenance" || ticket.Category == "Network" || ticket.Category == "Software")
                {
                    ticket.Priority = "High";
                }
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticket);
        }


        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
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

        public ActionResult Review(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Review([Bind(Include = "ID,ClientName,Email,FaultDescription,Priority,Date,Category,ConsultantName,Status,ConsultantId,TicketReference")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                string username = User.Identity.GetUserName();


                //For this to work a consultant must be created with the same email as the email registered to login as a consultant
                ticket.ConsultantName = (from Consultant c in db.Consultants
                                         where c.ConsultantUserName == username
                                         select c.FullName).FirstOrDefault();
                ticket.ConsultantId = (from Consultant c in db.Consultants
                                       where c.ConsultantUserName == username
                                       select c.ConsultantNum).FirstOrDefault();
                if (ticket.ConsultantName != null)
                {
                    ticket.Status = "In-Progress";
                }
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyTickets");
            }
            return View(ticket);
        }

        public ViewResult Done()
        {

            return View();
        }

    }
}
