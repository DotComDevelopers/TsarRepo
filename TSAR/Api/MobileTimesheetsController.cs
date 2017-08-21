using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TSAR.Models;
using System.Data.Entity;
using Twilio.Http;


namespace TSAR.Api
{
  //[Authorize]
  public class MobileTimesheetsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MobileTimesheets
        public IQueryable<Timesheet> GetTimesheets()
    {
      
      return db.Timesheets.Include(timesheet =>timesheet.Client)
        .Include(timesheet => timesheet.Consultant)
        .Include(timesheet => timesheet.Travel)
        .Include(t=>t.Consultant.Commission);
         
    }

        // GET: api/MobileTimesheets/5
        [ResponseType(typeof(Timesheet))]
        public async Task<IHttpActionResult> GetTimesheet(int id)
        {
            Timesheet timesheet = await db.Timesheets.FindAsync(id);
            if (timesheet == null)
            {
                return NotFound();
            }

            return Ok(timesheet);
        }

        // PUT: api/MobileTimesheets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTimesheet(int id, Timesheet timesheet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != timesheet.TimesheetId)
            {
                return BadRequest();
            }

            db.Entry(timesheet).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimesheetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MobileTimesheets
        [ResponseType(typeof(Timesheet))]
        public async Task<IHttpActionResult> PostTimesheet(Timesheet timesheet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Timesheets.Add(timesheet);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = timesheet.TimesheetId }, timesheet);
        }

        // DELETE: api/MobileTimesheets/5
        [ResponseType(typeof(Timesheet))]
        public async Task<IHttpActionResult> DeleteTimesheet(int id)
        {
            Timesheet timesheet = await db.Timesheets.FindAsync(id);
            if (timesheet == null)
            {
                return NotFound();
            }

            db.Timesheets.Remove(timesheet);
            await db.SaveChangesAsync();

            return Ok(timesheet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TimesheetExists(int id)
        {
            return db.Timesheets.Count(e => e.TimesheetId == id) > 0;
        }
    }
}