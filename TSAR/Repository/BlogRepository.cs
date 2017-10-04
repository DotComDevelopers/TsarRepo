using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSAR.Models;
using System.Threading.Tasks;

namespace TSAR.Repository
{
    public class BlogRepository : IBlogRepository, IDisposable
    {
        private ApplicationDbContext db;

        public BlogRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public IList<Post> GetPosts()
        {
            return  db.Posts.ToList();
        }

        public IList<Category> GetPostCategories(Post post)
        {
            var categoryIds = db.PostCategories.Where(p => p.PostId == post.Id).Select(p => p.CategoryId).ToList();
            List<Category> categories = new List<Category>();
            foreach (var catId in categoryIds)
            {
                categories.Add(db.Categories.Where(p => p.Id == catId).FirstOrDefault());
            }
            return categories;
        }

        public IList<Tag> GetPostTags(Post post)
        {
            
            var tagIds = db.PostsTags.Where(p => p.PostId == post.Id).Select(p => p.TagId).ToList();
            List<Tag> tags = new List<Tag>();
            foreach (var tagId in tagIds)
            {
                tags.Add(db.Tags.Where(p => p.Id == tagId).FirstOrDefault());
            }
            return tags;
        }

        public int LikeDislikeCount(string typeAndLike, string id)
        {
            switch (typeAndLike)
            {
                case "postlike":
                    return db.PostLikes.Where(p => p.PostId == id && p.Like == true).Count();
                case "postdislike":
                    return db.PostLikes.Where(p => p.PostId == id && p.Dislike == true).Count();
                case "commentlike":
                    return db.CommentLikes.Where(p => p.CommentId == id && p.Like == true).Count();
                case "commentdislike":
                    return db.CommentLikes.Where(p => p.CommentId == id && p.Dislike == true).Count();
                case "replylike":
                    return db.ReplyLikes.Where(p => p.ReplyId == id && p.Like == true).Count();
                case "replydislike":
                    return db.ReplyLikes.Where(p => p.ReplyId == id && p.Dislike == true).Count();
                default:
                    return 0;

            }
        }

        public IList<Tag> GetTags()
        {
            return db.Tags.ToList();
    }

        public IList<Category> GetCategories()
        {
            return db.Categories.ToList();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}