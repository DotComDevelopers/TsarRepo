using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using PagedList;
using TSAR.Models;
using TSAR.Repository;
//using PagedList;


namespace TSAR.Controllers
{
    public class BlogController : Controller
    {
        private IBlogRepository _blogRepository;
        public static List<BlogViewModel> PostList = new List<BlogViewModel>();
        public static List<AllpostsViewModel> allPostsList= new List<AllpostsViewModel>();
        public static List<AllpostsViewModel> checkcatlist = new List<AllpostsViewModel>();
        public static List<AllpostsViewModel> checkTagList = new List<AllpostsViewModel>();



        public BlogController()
        {
            _blogRepository= new BlogRepository(new ApplicationDbContext());
        }

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        // GET: Blog
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(int? page, string sortOrder, string searchString, string[] searchCategory, string[] searchTags)
        {
            checkTagList.Clear();
            checkcatlist.Clear();
            CreateCatandTagList();
            Posts(page, sortOrder, searchString, searchCategory, searchTags);
            return View();
        }

        [ChildActionOnly]
        public ActionResult Posts(int? page, string sortOrder,string searchString,string[] searchCategory,string[] searchTags )
        {
            PostList.Clear();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentSearchString = searchString;
            ViewBag.CurretnSearchCategory = searchCategory;
            ViewBag.CurrentSearchTag = searchTags;
            ViewBag.DateSortParm = string.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";

            var posts = _blogRepository.GetPosts();
            foreach (var post in posts)
            {
                var postCategories = GetPostCategories(post);
                var postTags = GetPostTags(post);
                var likes = _blogRepository.LikeDislikeCount("postlike", post.Id);
                var dislikes = _blogRepository.LikeDislikeCount("postdislike", post.Id);

               PostList.Add(new BlogViewModel()
                {
                    Post = post,
                    Modified = post.Modified,
                    Title = post.Title,
                    ShortDescription = post.ShortDescription,
                    PostedOn = post.PostedOn,
                    ID = post.Id,
                    PostLike = likes,
                    PostDislike = dislikes,
                    PostCategory = postCategories,
                    PostTag = postTags,
                    UrlSlug = post.UrlSeo
                });
                }

            if (searchString != null)
            {
                PostList = PostList.Where(x => x.Title.ToLower().Contains(searchString.ToLower())).ToList();
            }
            if (searchCategory != null)
            {
                List<BlogViewModel> newList = new List<BlogViewModel>();

                foreach (var catName in searchCategory)
                {
                    foreach (var item in PostList)
                    {
                        if (item.PostCategory.Where(x => x.Name == catName).Any())
                        {
                            newList.Add(item);
                        }
                    }
                    foreach (var item in checkcatlist)
                    {
                        if (item.Category.Name == catName)
                        {
                            item.Checked = true;
                        }
                    }
                }
                PostList = PostList.Intersect(newList).ToList();
            }

            if (searchTags != null)
                {
                    List<BlogViewModel> newlist = new List<BlogViewModel>();

                    foreach (var tagName in searchTags)
                    {
                        foreach (var item in PostList)
                        {
                            if (item.PostTag.Where(x => x.Name == tagName).Any())
                            {
                                newlist.Add(item);
                            }
                        }
                        foreach (var item in checkTagList)
                        {
                            if (item.Category.Name == tagName)
                            {
                                item.Checked = true;
                            }
                        }
                    }
                    PostList = PostList.Intersect(newlist).ToList();
                }

            switch (sortOrder)
            {
                case "date_desc":
                    PostList = PostList.OrderByDescending(x => x.PostedOn).ToList();
                    break;
                case "Title":
                    PostList = PostList.OrderBy(x => x.Title).ToList();
                    break;
                case"title_desc":
                    PostList = PostList.OrderByDescending(x => x.Title).ToList();
                    break;
                default:
                    PostList = PostList.OrderBy(x => x.PostedOn).ToList();
                    break;  
            }

            int pageSize = 2;
            int pageNumber = (page ?? 1);

                return PartialView("Posts",PostList.ToPagedList(pageSize,pageNumber));
            
        }

        public ActionResult AllPosts(int? page, string sortOrder, string searchString, string[] searchCategory,
            string[] searchTags)
        {
            allPostsList.Clear();
            checkcatlist.Clear();
            checkTagList.Clear();


            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentSearchString = searchString;
            ViewBag.CurretnSearchCategory = searchCategory;
            ViewBag.CurrentSearchTag = searchTags;
            ViewBag.DateSortParm = string.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";

            var posts = _blogRepository.GetPosts();
            foreach (var post in posts)
            {
                var postCategories = GetPostCategories(post);
                var postTags = GetPostTags(post);
                allPostsList.Add(new AllpostsViewModel()
                {
                    PostId= post.Id,
                    Date = post.PostedOn,
                    Description = post.ShortDescription,
                    Title = post.Title,
                    PostCategories = postCategories,
                    PostTags = postTags,
                    UrlSlug = post.UrlSeo
                }); 
            }

            if (searchString != null)
            {
                allPostsList = allPostsList.Where(x => x.Title.ToLower().Contains(searchString.ToLower())).ToList();
            }
            CreateCatandTagList();

            if (searchCategory != null)
            {
                List<AllpostsViewModel> newList = new List<AllpostsViewModel>();
                foreach (var catName in searchCategory)
                {
                    foreach (var item in allPostsList)
                    {
                        if (item.PostCategories.Where(x => x.Name == catName).Any())
                        {
                            newList.Add(item);
                        }   
                    }
                    foreach (var item in checkcatlist)
                    {
                        if (item.Category.Name == catName)
                        {
                            item.Checked = true;
                        }
                    }
                }
                allPostsList = allPostsList.Intersect(newList).ToList();
            }

            if (searchTags != null)
            {
                List<AllpostsViewModel> newlist = new List<AllpostsViewModel>();
                foreach (var tagName in searchTags)
                {
                    foreach (var item in allPostsList)
                    {
                        if (item.PostTags.Where(x => x.Name == tagName).Any())
                        {
                            newlist.Add(item);
                        }
                    }
                    foreach (var item in checkTagList)
                    {
                        if (item.Tag.Name == tagName)
                        {
                            item.Checked = true;
                        }
                    }
                }
                allPostsList = allPostsList.Intersect(newlist).ToList();
            }
            switch (sortOrder)
            {
                case "date_desc":
                    allPostsList = allPostsList.OrderByDescending(x => x.Date).ToList();
                    break;
                case "Title":
                    allPostsList = allPostsList.OrderBy(x => x.Title).ToList();
                    break;
                case "title_desc":
                    allPostsList = allPostsList.OrderByDescending(x => x.Title).ToList();
                    break;
                default:
                    allPostsList = allPostsList.OrderBy(x => x.Date).ToList();
                    break;
            }
            int pageSize = 2;
            int pageNumber = (page ?? 1);

            return PartialView("AllPosts", allPostsList.ToPagedList(pageSize, pageNumber));


        }

        //Posts/AllPosts

        #region Rss

        public ActionResult Feed()
        {
            var BlogTitle = "Your BLog Title";
            var blogDescription = "You Blog Description";
            var blogUrl = "";

            var posts = _blogRepository.GetPosts().Select(
                p => new SyndicationItem(p.Title, p.ShortDescription, new Uri(blogUrl)));

            //create an instance of of syndication class
            var feed = new SyndicationFeed(BlogTitle, blogDescription, new Uri(blogUrl), posts)
            {
                Copyright = new TextSyndicationContent(string.Format("Copyright {0}",BlogTitle)),
                Language = "en-US"
            };
            var feedFormatter= new Rss20FeedFormatter(feed);
            return new FeedResult(feedFormatter);
        }

        #endregion Rss

        #region Helpers

        public IList<Post> GetPosts()
        {
            return _blogRepository.GetPosts();
        }

        public IList<Category> GetPostCategories(Post post)
        {
            return _blogRepository.GetPostCategories(post);
        }

        public IList<Tag> GetPostTags(Post post)
        {
            return _blogRepository.GetPostTags(post);
        }

        public void CreateCatandTagList()
        {
            foreach (var ct in _blogRepository.GetCategories())
            {
                checkcatlist.Add(new AllpostsViewModel {Category = ct,Checked=false});
            }
            foreach (var tg in _blogRepository.GetTags())
            {
                checkTagList.Add(new AllpostsViewModel {Tag = tg,Checked = false});
            }
        }
        #endregion
    }
}