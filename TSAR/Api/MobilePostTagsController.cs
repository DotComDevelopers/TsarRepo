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
    public class MobilePostTagsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MobilePostTags
        public IQueryable<PostTag> GetPostsTags()
        {
            return db.PostsTags;
        }

        // GET: api/MobilePostTags/5
        [ResponseType(typeof(PostTag))]
        public async Task<IHttpActionResult> GetPostTag(string id)
        {
            PostTag postTag = await db.PostsTags.FindAsync(id);
            if (postTag == null)
            {
                return NotFound();
            }

            return Ok(postTag);
        }

        // PUT: api/MobilePostTags/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPostTag(string id, PostTag postTag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != postTag.PostId)
            {
                return BadRequest();
            }

            db.Entry(postTag).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostTagExists(id))
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

        // POST: api/MobilePostTags
        [ResponseType(typeof(PostTag))]
        public async Task<IHttpActionResult> PostPostTag(PostTag postTag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PostsTags.Add(postTag);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PostTagExists(postTag.PostId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = postTag.PostId }, postTag);
        }

        // DELETE: api/MobilePostTags/5
        [ResponseType(typeof(PostTag))]
        public async Task<IHttpActionResult> DeletePostTag(string id)
        {
            PostTag postTag = await db.PostsTags.FindAsync(id);
            if (postTag == null)
            {
                return NotFound();
            }

            db.PostsTags.Remove(postTag);
            await db.SaveChangesAsync();

            return Ok(postTag);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostTagExists(string id)
        {
            return db.PostsTags.Count(e => e.PostId == id) > 0;
        }
    }
}