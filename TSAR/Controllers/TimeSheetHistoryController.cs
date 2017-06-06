using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSAR.Models;


namespace TSAR.Controllers
{
    public class TimeSheetHistoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: TimeSheetHistory
        public ActionResult History()
        {
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FullName");
            var timesheets = db.Timesheets.Include(t => t.Client).Include(t => t.Consultant);
            return View(timesheets.ToList());
           
        }

        [HttpPost]
        public ActionResult History(Timesheet timesheet)
        {
            
            ViewBag.ConsultantNum = new SelectList(db.Consultants, "ConsultantNum", "FullName", timesheet.ConsultantNum);
            var timesheets = db.Timesheets.Include(t => t.Client).Include(t => t.Consultant).Where(a => a.ConsultantNum == timesheet.ConsultantNum);
            var r = timesheets.ToList();
            return View(r);


        }

       
    }
}