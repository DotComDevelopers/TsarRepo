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
    public class MobileCommentLikesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MobileCommentLikes
        public IQueryable<CommentLike> GetCommentLikes()
        {
            return db.CommentLikes;
        }

        // GET: api/MobileCommentLikes/5
        [ResponseType(typeof(CommentLike))]
        public async Task<IHttpActionResult> GetCommentLike(string id)
        {
            CommentLike commentLike = await db.CommentLikes.FindAsync(id);
            if (commentLike == null)
            {
                return NotFound();
            }

            return Ok(commentLike);
        }

        // PUT: api/MobileCommentLikes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCommentLike(string id, CommentLike commentLike)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != commentLike.CommentId)
            {
                return BadRequest();
            }

            db.Entry(commentLike).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentLikeExists(id))
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

        // POST: api/MobileCommentLikes
        [ResponseType(typeof(CommentLike))]
        public async Task<IHttpActionResult> PostCommentLike(CommentLike commentLike)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CommentLikes.Add(commentLike);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CommentLikeExists(commentLike.CommentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = commentLike.CommentId }, commentLike);
        }

        // DELETE: api/MobileCommentLikes/5
        [ResponseType(typeof(CommentLike))]
        public async Task<IHttpActionResult> DeleteCommentLike(string id)
        {
            CommentLike commentLike = await db.CommentLikes.FindAsync(id);
            if (commentLike == null)
            {
                return NotFound();
            }

            db.CommentLikes.Remove(commentLike);
            await db.SaveChangesAsync();

            return Ok(commentLike);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentLikeExists(string id)
        {
            return db.CommentLikes.Count(e => e.CommentId == id) > 0;
        }
    }
}