using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using TSAR.Models;
namespace TSAR.Controllers
{
    [Authorize(Roles = "Admin,Consultant,Client")]
    public class TimesheetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Timesheets
        public ActionResult Index()
        {
            var timesheet = db.Timesheets;


            string username = User.Identity.GetUserName();
            int consultantnum = (from Consultant c in db.Consultants
                                 where c.ConsultantUserName == username
                                 select c.ConsultantNum).FirstOrDefault();
            var timesheets = db.Timesheets.Include(t => t.Client).Include(t => t.Consultant);


            List<Timesheet> index = null;

            if (User.IsInRole("Admin"))
            {


                index = timesheets.ToList();

            }
            else if (User.IsInRole("Consultant"))
            {

                index = db.Timesheets.Where(p => p.ConsultantNum == consultantnum).Where(p => p.ProjectName == null).ToList();
            }

            return View(index);
        }
        public ActionResult ProjectTimeSheetIndex(int id)
        {
            var timesheet = db.Timesheets;
            double bal = (from b in db.Budgets
                          where b.ProjectId == id
                          select b.Balance).FirstOrDefault();
         
            //ViewBag.Budget = "Balance R " + db.Budgets.Where(p => p.ProjectId == id);
            ViewBag.Budget ="Estimated amount used: "+ bal.ToString("R0.00");
            string username = User.Identity.GetUserName();
            int consultantnum = (from Consultant c in db.Consultants
                                 where c.ConsultantUserName == username
                                 select c.ConsultantNum).FirstOrDefault();

      string name = (from Projects d in db.Projects
                     where d.ProjectId == id
                     select d.ProjectName).FirstOrDefault();

      var timesheets = db.Timesheets.Include(t => t.Client).Include(t => t.Consultant).Where(p => p.ProjectName == name);
         //   var time = db.Timesheets.Include(t => t.Client).Include(t => t.Consultant);
   ViewBag.Total = "Actual Budget: " + timesheets.Sum(x => x.Total).ToString("R0.00");

            List<Timesheet> index = null;


            if (User.IsInRole("Admin"))
            {

              
                index = timesheets.ToList();
                

            }
            else if (User.IsInRole("Consultant"))
            {

                index = db.Timesheets.Where(p => p.ConsultantNum == consultantnum).ToList();
            }

            return View(index);
        }


        public ActionResult ViewTimeheets()
        {
            var timesheets = db.Timesheets.Include(t => t.Client).Include(t => t.Consultant);
            string username = User.Identity.GetUserName();
            int clientid = (from Client c in db.Clients
                            where c.Email == username
                            select c.Id).FirstOrDefault();
            return View(db.Timesheets.Where(p => p.Id == clientid).ToList());
        }
        // GET: Timesheets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.Timesheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }




        // GET: Timesheets/Create
        public ActionResult Create()
        {

            string username = User.Identity.GetUserName();
            string conname = (from Consultant c in db.Consultants
                              where c.ConsultantUserName == username
                              select c.FullName).FirstOrDefault();
            string clientname = (from Ticket t in db.Tickets
                                 where t.ConsultantName == conname
                                 select t.ClientName).FirstOrDefault();
            //string add1 = (from Client c  in db.Clients
            //               where c.ClientName == clientname
            //               select c.ClientAddress).FirstOrDefault();
            //string add2 = (from Client c in db.Clients
            //               where c.ClientName == clientname
            //               select c.ClientAddress2).FirstOrDefault();
            //ViewBag.Addresses = new List<string> { add1, add2};
            //   ViewBag.Addresses = new SelectList;
            //ViewBag.Addresses = new SelectList(db.Travels);

            var ca = db.Timesheets.Include(t => t.Client).Include(t => t.Consultant);
            var o = ca.ToList();
            ViewBag.MCA = new SelectList(o);
            ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName");
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FullName");
            return View();
        }
        public ActionResult ProjectTimeSheet()
        {
            string use = User.Identity.GetUserName();
            int con = (from Consultant c in db.Consultants
                       where c.ConsultantUserName == use
                       select c.ConsultantNum).FirstOrDefault();
            int cli = (from Projects t in db.Projects
                       where t.ConsultantNum == con
                       select t.Id).FirstOrDefault();
            int cno = (from Travel c in db.Travels
                       where c.Id == cli
                       select c.Id).FirstOrDefault();


            ViewBag.MClientAddress = new SelectList(db.Projects.Where(p => p.Id == cno), "TravelId", "MClientAddress");
            string user = User.Identity.GetUserName();
            int cn = (from Consultant c in db.Consultants
                      where c.ConsultantUserName == user
                      select c.ConsultantNum).FirstOrDefault();

            ViewBag.ProjectName = new SelectList(db.Projects.Where(p => p.ConsultantNum == cn), "ProjectName", "ProjectName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProjectTimeSheet([Bind(Include = "TimesheetId,CaptureDate,StartTime,EndTime,ActivityDescription,Total,Hours,Id,ConsultantNum,TicketReference,SignOff, ProjectName")] Timesheet timesheet)
        {
            string use = User.Identity.GetUserName();
            int con = (from Consultant c in db.Consultants
                       where c.ConsultantUserName == use
                       select c.ConsultantNum).FirstOrDefault();
            int cli = (from Projects t in db.Projects
                       where t.ConsultantNum == con
                       select t.Id).FirstOrDefault();
            int cno = (from Travel c in db.Travels
                       where c.Id == cli
                       select c.Id).FirstOrDefault();

       
                        ViewBag.MClientAddress = new SelectList(db.Projects.Where(p => p.Id == cno), "TravelId", "MClientAddress", timesheet.MClientAddress);

            string user = User.Identity.GetUserName();
            int cn = (from Consultant c in db.Consultants
                      where c.ConsultantUserName == user
                      select c.ConsultantNum).FirstOrDefault();

            ViewBag.ProjectName = new SelectList(db.Projects.Where(p => p.ConsultantNum == cn), "ProjectName", "ProjectName", timesheet.ProjectName);

            if (ModelState.IsValid)
            {
                //DateT p = (from Projects k in db.Projects
                //            where k.ProjectName == timesheet.ProjectName
                //            select k.EndDate);

                //if (
                //    timesheet.CaptureDate = System.DateTime.Now; }
                //else
                //{
                    timesheet.CaptureDate = System.DateTime.Now;
                
                //timesheet.Id = 1;
                //timesheet.ConsultantNum = 1;
                string username = User.Identity.GetUserName();
                int conname = (from Consultant c in db.Consultants
                               where c.ConsultantUserName == username
                               select c.ConsultantNum).FirstOrDefault();
                int clientname = (from Projects t in db.Projects
                                  where t.ConsultantNum == conname
                                  select t.Id).FirstOrDefault();
                timesheet.Id = clientname;
                timesheet.ConsultantNum = conname;
                
              
                timesheet.Hours = (timesheet.EndTime - timesheet.StartTime).TotalHours;
                timesheet.Total = (700 * (timesheet.EndTime - timesheet.StartTime).TotalHours);
               
                db.Timesheets.Add(timesheet);
                db.SaveChanges();

                return RedirectToAction("Success");

            }
            


         //   ViewBag.ProjectName = new SelectList(db.Projects.Where(p => p.ConsultantNum == cn), "ProjectName", "ProjectName", timesheet.ProjectName);
            return View(timesheet);
        }



        // POST: Timesheets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimesheetId,CaptureDate,StartTime,EndTime,ActivityDescription,Total,Hours,Id,ConsultantNum,TicketReference,SignOff")] Timesheet timesheet, string tickRef)
        {

            if (ModelState.IsValid)
            {
                timesheet.TicketReference = tickRef;
                string clientname = (from Ticket t in db.Tickets
                                     where t.TicketReference == tickRef
                                     select t.ClientName).FirstOrDefault();

                timesheet.Id = (from Client c in db.Clients
                                where c.ClientName == clientname
                                select c.Id).FirstOrDefault();

                timesheet.ConsultantNum = (from Ticket t in db.Tickets
                                           where t.TicketReference == tickRef
                                           select t.ConsultantId).FirstOrDefault();
                timesheet.CaptureDate = System.DateTime.Now;
                timesheet.Hours = (timesheet.EndTime - timesheet.StartTime).TotalHours;
                timesheet.Total = (700 * (timesheet.EndTime - timesheet.StartTime).TotalHours);
                //timesheet.MClientAddress = (from Travel c in db.Travels
                //               where c.Id == timesheet.Id
                //  select c.MClientAddress).FirstOrDefault();
                db.Timesheets.Add(timesheet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //var add = (from Travel c in db.Travels
            //                   where c.Id == timesheet.Id
            //                 select c.MClientAddress).ToList();


            string username = User.Identity.GetUserName();
            string conname = (from Consultant c in db.Consultants
                              where c.ConsultantUserName == username
                              select c.FullName).FirstOrDefault();
            string cliname = (from Ticket t in db.Tickets
                              where t.ConsultantName == conname
                              select t.ClientName).FirstOrDefault();
            //string add1 = (from Client c in db.Clients
            //               where c.ClientName == cliname
            //               select c.ClientAddress).FirstOrDefault();
            //string add2 = (from Client c in db.Clients
            //               where c.ClientName == cliname
            //               select c.ClientAddress2).FirstOrDefault();
            //ViewBag.Addresses = new List<string> { add1, add2 };
            //ViewBag.MClientAddress = new SelectList(db.Travels, "Id", "MClientAddress", timesheet.Id);
            // ViewBag.Addresses = new SelectList(db.Travels, "MClientAddress", "MClientAddress", timesheet.MClientAddress);

            var ca = db.Timesheets.Include(t => t.Client).Include(t => t.Consultant).Where(a => a.Id == timesheet.Id);
            var o = ca.ToList();
            ViewBag.MCA = new SelectList(o);
            // ViewBag.Addresses = new SelectList(timesheet.MClientAddress);
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FullName", timesheet.ConsultantNum);
            return View(timesheet);
        }

        // GET: Timesheets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.Timesheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName", timesheet.Id);
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FullName", timesheet.ConsultantNum);
            return View(timesheet);
        }

        // POST: Timesheets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TimesheetId,CaptureDate,StartTime,EndTime,ActivityDescription,Total,Hours,Id,ConsultantNum,TicketReference,SignOff")] Timesheet timesheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timesheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName", timesheet.Id);
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FullName", timesheet.ConsultantNum);
            return View(timesheet);
        }

        // GET: Timesheets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.Timesheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }

        // POST: Timesheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Timesheet timesheet = db.Timesheets.Find(id);
            db.Timesheets.Remove(timesheet);
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

        public ViewResult Success()
        {

            return View();
        }
        public ActionResult SignOff(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.Timesheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName", timesheet.Id);
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FullName", timesheet.ConsultantNum);
            return View(timesheet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignOff([Bind(Include = "TimesheetId,CaptureDate,StartTime,EndTime,ActivityDescription,Total,Hours,Id,ConsultantNum,TicketReference,SignOff")] Timesheet timesheet)
        {
            if (ModelState.IsValid)
            {
                if (timesheet.SignOff == true)
                {
                    var tick = db.Tickets.SingleOrDefault(p => p.TicketReference == timesheet.TicketReference);
                    if (tick != null)
                    {
                        tick.Status = "Closed-Ticket";
                        db.Entry(tick).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                }
                db.Entry(timesheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Create", "Rating");

            }
            ViewBag.Id = new SelectList(db.Clients, "Id", "ClientName", timesheet.Id);
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FullName", timesheet.ConsultantNum);
            return View(timesheet);
        }



    }



}
   