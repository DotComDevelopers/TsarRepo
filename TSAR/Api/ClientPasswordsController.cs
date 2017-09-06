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
    public class ClientPasswordsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ClientPasswords
        public IQueryable<ClientPassword> GetClientPasswords()
        {
            return db.ClientPasswords;
        }

        // GET: api/ClientPasswords/5
        [ResponseType(typeof(ClientPassword))]
        public async Task<IHttpActionResult> GetClientPassword(int id)
        {
            ClientPassword clientPassword = await db.ClientPasswords.FindAsync(id);
            if (clientPassword == null)
            {
                return NotFound();
            }

            return Ok(clientPassword);
        }

        // PUT: api/ClientPasswords/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClientPassword(int id, ClientPassword clientPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clientPassword.ClientPasswordId)
            {
                return BadRequest();
            }

            db.Entry(clientPassword).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientPasswordExists(id))
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

        // POST: api/ClientPasswords
        [ResponseType(typeof(ClientPassword))]
        public async Task<IHttpActionResult> PostClientPassword(ClientPassword clientPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ClientPasswords.Add(clientPassword);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = clientPassword.ClientPasswordId }, clientPassword);
        }

        // DELETE: api/ClientPasswords/5
        [ResponseType(typeof(ClientPassword))]
        public async Task<IHttpActionResult> DeleteClientPassword(int id)
        {
            ClientPassword clientPassword = await db.ClientPasswords.FindAsync(id);
            if (clientPassword == null)
            {
                return NotFound();
            }

            db.ClientPasswords.Remove(clientPassword);
            await db.SaveChangesAsync();

            return Ok(clientPassword);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientPasswordExists(int id)
        {
            return db.ClientPasswords.Count(e => e.ClientPasswordId == id) > 0;
        }
    }
}