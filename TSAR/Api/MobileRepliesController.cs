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
    public class MobileRepliesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MobileReplies
        public IQueryable<Reply> GetReplies()
        {
            return db.Replies;
        }

        // GET: api/MobileReplies/5
        [ResponseType(typeof(Reply))]
        public async Task<IHttpActionResult> GetReply(string id)
        {
            Reply reply = await db.Replies.FindAsync(id);
            if (reply == null)
            {
                return NotFound();
            }

            return Ok(reply);
        }

        // PUT: api/MobileReplies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutReply(string id, Reply reply)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reply.Id)
            {
                return BadRequest();
            }

            db.Entry(reply).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReplyExists(id))
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

        // POST: api/MobileReplies
        [ResponseType(typeof(Reply))]
        public async Task<IHttpActionResult> PostReply(Reply reply)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Replies.Add(reply);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReplyExists(reply.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = reply.Id }, reply);
        }

        // DELETE: api/MobileReplies/5
        [ResponseType(typeof(Reply))]
        public async Task<IHttpActionResult> DeleteReply(string id)
        {
            Reply reply = await db.Replies.FindAsync(id);
            if (reply == null)
            {
                return NotFound();
            }

            db.Replies.Remove(reply);
            await db.SaveChangesAsync();

            return Ok(reply);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReplyExists(string id)
        {
            return db.Replies.Count(e => e.Id == id) > 0;
        }
    }
}