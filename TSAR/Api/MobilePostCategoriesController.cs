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
    public class MobilePostCategoriesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MobilePostCategories
        public IQueryable<PostCategory> GetPostCategories()
        {
            return db.PostCategories;
        }

        // GET: api/MobilePostCategories/5
        [ResponseType(typeof(PostCategory))]
        public async Task<IHttpActionResult> GetPostCategory(string id)
        {
            PostCategory postCategory = await db.PostCategories.FindAsync(id);
            if (postCategory == null)
            {
                return NotFound();
            }

            return Ok(postCategory);
        }

        // PUT: api/MobilePostCategories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPostCategory(string id, PostCategory postCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != postCategory.PostId)
            {
                return BadRequest();
            }

            db.Entry(postCategory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostCategoryExists(id))
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

        // POST: api/MobilePostCategories
        [ResponseType(typeof(PostCategory))]
        public async Task<IHttpActionResult> PostPostCategory(PostCategory postCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PostCategories.Add(postCategory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PostCategoryExists(postCategory.PostId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = postCategory.PostId }, postCategory);
        }

        // DELETE: api/MobilePostCategories/5
        [ResponseType(typeof(PostCategory))]
        public async Task<IHttpActionResult> DeletePostCategory(string id)
        {
            PostCategory postCategory = await db.PostCategories.FindAsync(id);
            if (postCategory == null)
            {
                return NotFound();
            }

            db.PostCategories.Remove(postCategory);
            await db.SaveChangesAsync();

            return Ok(postCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostCategoryExists(string id)
        {
            return db.PostCategories.Count(e => e.PostId == id) > 0;
        }
    }
}