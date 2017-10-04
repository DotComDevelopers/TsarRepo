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
    public class MobileReplyLikesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MobileReplyLikes
        public IQueryable<ReplyLike> GetReplyLikes()
        {
            return db.ReplyLikes;
        }

        // GET: api/MobileReplyLikes/5
        [ResponseType(typeof(ReplyLike))]
        public async Task<IHttpActionResult> GetReplyLike(string id)
        {
            ReplyLike replyLike = await db.ReplyLikes.FindAsync(id);
            if (replyLike == null)
            {
                return NotFound();
            }

            return Ok(replyLike);
        }

        // PUT: api/MobileReplyLikes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutReplyLike(string id, ReplyLike replyLike)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != replyLike.ReplyId)
            {
                return BadRequest();
            }

            db.Entry(replyLike).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReplyLikeExists(id))
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

        // POST: api/MobileReplyLikes
        [ResponseType(typeof(ReplyLike))]
        public async Task<IHttpActionResult> PostReplyLike(ReplyLike replyLike)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ReplyLikes.Add(replyLike);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReplyLikeExists(replyLike.ReplyId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = replyLike.ReplyId }, replyLike);
        }

        // DELETE: api/MobileReplyLikes/5
        [ResponseType(typeof(ReplyLike))]
        public async Task<IHttpActionResult> DeleteReplyLike(string id)
        {
            ReplyLike replyLike = await db.ReplyLikes.FindAsync(id);
            if (replyLike == null)
            {
                return NotFound();
            }

            db.ReplyLikes.Remove(replyLike);
            await db.SaveChangesAsync();

            return Ok(replyLike);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReplyLikeExists(string id)
        {
            return db.ReplyLikes.Count(e => e.ReplyId == id) > 0;
        }
    }
}