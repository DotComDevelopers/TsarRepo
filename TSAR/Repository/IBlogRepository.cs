using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSAR.Models;

namespace TSAR.Repository
{
    public interface IBlogRepository:IDisposable
    {
        IList<Post> GetPosts();
        IList<Category> GetPostCategories(Post post);
        IList<Tag> GetPostTags(Post post);
        int LikeDislikeCount(string typeAndLike, string id);

        IList<Category> GetCategories();
        IList<Tag> GetTags();
    }
}