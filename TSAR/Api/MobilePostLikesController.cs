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
    public class MobilePostLikesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MobilePostLikes
        public IQueryable<PostLike> GetPostLikes()
        {
            return db.PostLikes;
        }

        // GET: api/MobilePostLikes/5
        [ResponseType(typeof(PostLike))]
        public async Task<IHttpActionResult> GetPostLike(string id)
        {
            PostLike postLike = await db.PostLikes.FindAsync(id);
            if (postLike == null)
            {
                return NotFound();
            }

            return Ok(postLike);
        }

        // PUT: api/MobilePostLikes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPostLike(string id, PostLike postLike)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != postLike.PostId)
            {
                return BadRequest();
            }

            db.Entry(postLike).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostLikeExists(id))
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

        // POST: api/MobilePostLikes
        [ResponseType(typeof(PostLike))]
        public async Task<IHttpActionResult> PostPostLike(PostLike postLike)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PostLikes.Add(postLike);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PostLikeExists(postLike.PostId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = postLike.PostId }, postLike);
        }

        // DELETE: api/MobilePostLikes/5
        [ResponseType(typeof(PostLike))]
        public async Task<IHttpActionResult> DeletePostLike(string id)
        {
            PostLike postLike = await db.PostLikes.FindAsync(id);
            if (postLike == null)
            {
                return NotFound();
            }

            db.PostLikes.Remove(postLike);
            await db.SaveChangesAsync();

            return Ok(postLike);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostLikeExists(string id)
        {
            return db.PostLikes.Count(e => e.PostId == id) > 0;
        }
    }
}