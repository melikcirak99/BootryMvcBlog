using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bootry.Controllers.Base;
using Bootry.Models;

namespace Bootry.Controllers
{
    public class PartialViewsController : SiteBaseController
    {
        private VeriTabani db = new VeriTabani();

        /************************ partial views *************************/

        public PartialViewResult ContactNotifications()
        {
            if (this.IsAdminLogin)
            {
                var model = db.Contact.ToList();
                return PartialView(model);
            }
            return PartialView();

        }
        public PartialViewResult Trush()
        {
            if (Session["admin"] != null)
            {
                var model = db.Post.Where(x => x.isActive == false).ToList();
                return PartialView(model);
            }
            else
            {
                return PartialView();
            }


        }
        public PartialViewResult AllPosts()
        {
            if (this.IsAdminLogin)
            {
                var model = db.Post.Where(x => x.isActive == true).ToList();
                return PartialView(model);
            }
            return PartialView();
        }
        public PartialViewResult GetCategories()
        {
            if (this.IsAdminLogin)
            {
                var model = db.Category.ToList();
                return PartialView(model);
            }
            return PartialView();
        }
        public PartialViewResult Writers()
        {
            if (this.IsAdminLogin)
            {
                var model = db.Writer.ToList();
                return PartialView(model);
            }
            return PartialView();


        }

        public int getUserCount()
        {
            if (this.IsAdminLogin)
            {
                int count = db.User.ToList().Count;
                return count;
            }
            return 0;
        }
        public int getPostCount()
        {
            if (IsAdminLogin)
            {
                int count = db.Post.Where(x => x.isActive == true).ToList().Count;
                return count;
            }
            return 0;
        }

        public int getPostViewSum()
        {
            if (IsAdminLogin)
            {
                int count = db.Post.Where(x => x.isActive == true).Select(x => x.ViewCount).ToList().Sum();
                return count;
            }
            return 0;
        }

        public int getCommentsCount()
        {
            if (IsAdminLogin)
            {
                int count = db.Comment.Where(x => x.isApproved == true).ToList().Count;
                return count;
            }
            return 0;
        }

        public string getSiteLogo()
        {
            string logo = db.Settings.Find(4).Value;
            return "/Materials/images/logo/" + logo;

        }
        public string getSiteSecondLogo()
        {
            string logo = db.Settings.Find(5).Value;
            return "/Materials/images/logo/" + logo;

        }


        /************************ partial views end *************************/

        /************************ partial views *************************/


        public PartialViewResult PostComments(int postID)
        {
            var uID = Convert.ToInt32(Session["user"]);
            var model = db.Comment.Where(x => x.PostID == postID).ToList();
            return PartialView(model);
        }

        public PartialViewResult SearchForm()
        {
            return PartialView();
        }
        //owl carousel section
        public PartialViewResult OwlCarousel()
        {
            var model = db.Post.Where(x => x.isActive == true).OrderBy(x => Guid.NewGuid()).Take(5).ToList();
            return PartialView(model);
        }
        //partial view for last posts
        public PartialViewResult LastPost()
        {
            var model = db.Post.Where(x => x.isActive == true).OrderByDescending(x => x.ID).FirstOrDefault();
            return PartialView(model);
        }

        //latest news section posts
        public PartialViewResult LatestPosts()
        {
            var model = db.Post.Where(x => x.isActive == true).OrderByDescending(x => x.Date).Take(4).ToList();
            return PartialView(model);
        }
        // trending now section
        public PartialViewResult TrendingNow()
        {
            var model = db.Post.Where(x => x.isActive == true).OrderByDescending(x => x.ViewCount).Take(4).ToList();
            return PartialView(model);
        }
        //partial view for most viewved posts
        public PartialViewResult MostPopular()
        {
            var model = db.Post.Where(x => x.isActive == true).OrderByDescending(x => x.ViewCount).Take(4).ToList();
            return PartialView(model);
        }

        //by a category section
        public PartialViewResult byACategory()
        {
            var _setting = db.Settings.Find(1);
            int _id = Convert.ToInt32(_setting.Value);
            var model = db.Post.Where(x => x.CategoryID == _id && x.isActive == true).Take(4).ToList();
            return PartialView(model);
        }

        public PartialViewResult PostByWriter()
        {
            int writerID = Convert.ToInt32(Session["yazar"]);
            var model = db.Post.Where(x => x.WriterID == writerID && x.isActive == true).ToList();
            return PartialView(model);
        }
        //çöpteki gönderiler
        public PartialViewResult InTrash()
        {
            if (this.IsAdminLogin)
            {
                var model = db.Post.Where(x => x.isActive == false).ToList();
                return PartialView(model);
            }
            return PartialView();
        }

        public int PostsInTrashCount()
        {
            if (this.IsAdminLogin)
            {
                var _count = db.Post.Where(x => x.isActive == false).ToList().Count();
                return _count;
            }
            return 0;
        }

        public PartialViewResult CategoriesList()
        {
            var model = db.Category.ToList();
            return PartialView(model);
        }

        public PartialViewResult CategorLinks()
        {
            var model = db.Category.OrderByDescending(x => x.Post.Count).ToList().Take(6);
            return PartialView(model);
        }

        public PartialViewResult CommentsNeedApproved()
        {
            var model = db.Comment.Where(x => x.isApproved == false).OrderByDescending(x => x.Date).ToList();
            return PartialView(model);
        }

        public int CommentsNeedApprovedCount()
        {
            if (this.IsAdminLogin)
            {
                var model = db.Comment.Where(x => x.isApproved == false).ToList();
                return model.Count();
            }
            return 0;
        }

        /************************ partial views end *************************/
    }
}