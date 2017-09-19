using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TSAR.Models;

namespace TSAR.Api
{
    public class MobileConsultantRegistersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MobileConsultantRegisters
        public IQueryable<ConsultantRegister> GetConsultantRegisters()
        {
            return db.ConsultantRegisters.Include(t=>t.Consultant);
        }

        // GET: api/MobileConsultantRegisters/5
        [ResponseType(typeof(ConsultantRegister))]
        public async Task<IHttpActionResult> GetConsultantRegister(int id)
        {
            ConsultantRegister consultantRegister = await db.ConsultantRegisters.FindAsync(id);
            if (consultantRegister == null)
            {
                return NotFound();
            }

            return Ok(consultantRegister);
        }

        // PUT: api/MobileConsultantRegisters/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutConsultantRegister(int id, ConsultantRegister consultantRegister)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != consultantRegister.ConsultantRegisterId)
            {
                return BadRequest();
            }

            db.Entry(consultantRegister).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultantRegisterExists(id))
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

        // POST: api/MobileConsultantRegisters
        [ResponseType(typeof(ConsultantRegister))]
        public async Task<IHttpActionResult> PostConsultantRegister(ConsultantRegister consultantRegister)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ConsultantRegisters.Add(consultantRegister);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = consultantRegister.ConsultantRegisterId }, consultantRegister);
        }

        // DELETE: api/MobileConsultantRegisters/5
        [ResponseType(typeof(ConsultantRegister))]
        public async Task<IHttpActionResult> DeleteConsultantRegister(int id)
        {
            ConsultantRegister consultantRegister = await db.ConsultantRegisters.FindAsync(id);
            if (consultantRegister == null)
            {
                return NotFound();
            }

            db.ConsultantRegisters.Remove(consultantRegister);
            await db.SaveChangesAsync();

            return Ok(consultantRegister);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConsultantRegisterExists(int id)
        {
            return db.ConsultantRegisters.Count(e => e.ConsultantRegisterId == id) > 0;
        }
    }
}