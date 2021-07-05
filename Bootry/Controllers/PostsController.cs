using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Bootry.Models;
using PagedList;
using PagedList.Mvc;

namespace Bootry.Controllers
{
    public class PostsController : Controller
    {
        private VeriTabani db = new VeriTabani();

        // GET: Posts
        public ActionResult Index()
        {
            var post = db.Post.Where(x => x.isActive == true).Include(p => p.Category).Include(p => p.Writer).ToList();

            //most popular section
            ViewBag.MostPopular = db.Post.Where(x => x.isActive == true).OrderByDescending(x => x.ViewCount).Take(4).ToList();

            //best of the week section
            ViewBag.BestOfTheWeek = db.Post.Where(x => x.isActive == true).OrderByDescending(x => x.ViewCount).Take(8).ToList();

            //for listing categories
            ViewBag.Categories = db.Category.ToList();

            return View(post);
        }


        //list by category page
        [Route("kategori/{kategori}-{id}")]
        public ActionResult ListByCategories(int id, int paged = 1)
        {
            var model = db.Post.Where(x => x.CategoryID == id && x.isActive == true).ToList().ToPagedList(paged, 4);
            ViewBag.category = db.Category.Where(x => x.ID == id).FirstOrDefault();
            return View(model);
        }



        //search page
        [HttpGet]
        [Route("search")]
        public ActionResult Search(int paged = 1, string query = "")
        {
            var Sonuc = db.Post.Where(x => x.Title.Contains(query) && x.isActive == true).OrderBy(x => x.ID).ToPagedList(paged, 4);
            ViewBag.query = query;
            return View(Sonuc);
        }



        // GET: Posts/Details/5
        [Route("gonderi/{kategori}/{isim}/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            else
            {
                //çöte değilse getir
                if (post.isActive != true)
                {
                    //çöpte
                    return HttpNotFound();
                }
                else
                {
                    //post viewcount updating
                    post.ViewCount += 1;
                    db.SaveChanges();
                    //preview and next posts
                    ViewBag.Prev = db.Post.Where(x => x.ID < post.ID && x.isActive == true).ToList().LastOrDefault();
                    ViewBag.Next = db.Post.Where(x => x.ID > post.ID && x.isActive == true).FirstOrDefault();

                    //meta tags
                    ViewBag.metaDescription = post.Summary;
                    ViewBag.metaAuthor = post.Writer.Name + " " + post.Writer.LastName;

                    //page title
                    ViewBag.Title = post.Title;
                    return View(post);
                }
            }
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            if (Session["yazar"] != null || Session["admin"] != null)
            {
                ViewBag.CategoryID = new SelectList(db.Category, "ID", "Name");
                ViewBag.WriterID = new SelectList(db.Writer, "ID", "Name");
                return View();
            }
            else
            {
                return Redirect("/giris-yazar");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,PostContent,Image,Date,UpdateDate,ViewCount,CategoryID,WriterID,SeoTitle,Summary,isActive")] Post post)
        {
            if (Session["yazar"] != null || Session["admin"] != null)
            {

                if (ModelState.IsValid)
                {
                    var _isHaveThis = db.Post.Where(x => x.Title == post.Title || x.SeoTitle == post.SeoTitle).FirstOrDefault();
                    if (_isHaveThis != null)
                    {
                        //bu isimli post daha önce eklenmmiş
                        ViewBag.message = "0";
                        ViewBag.postContent = post.PostContent;
                        ViewBag.CategoryID = new SelectList(db.Category, "ID", "Name");
                        ViewBag.WriterID = new SelectList(db.Writer, "ID", "Name");
                        return View(post);
                    }
                    else
                    {
                        db.Post.Add(post);
                        post.isActive = true;
                        db.SaveChanges();
                        ViewBag.message = "1";
                        ViewBag.CategoryID = new SelectList(db.Category, "ID", "Name");
                        ViewBag.WriterID = new SelectList(db.Writer, "ID", "Name");
                        return View(post);
                    }

                }

                ViewBag.CategoryID = new SelectList(db.Category, "ID", "Name", post.CategoryID);
                ViewBag.WriterID = new SelectList(db.Writer, "ID", "Name", post.WriterID);
                return View(post);
            }
            else
            {
                return Redirect("/giris-yazar");
            }
        }



        // GET: Posts2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["yazar"] != null || Session["admin"] != null)
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                Post post = db.Post.Find(id);
                if (post == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CategoryID = new SelectList(db.Category, "ID", "Name", post.CategoryID);
                ViewBag.WriterID = new SelectList(db.Writer, "ID", "Name", post.WriterID);
                return View(post);
            }
            else
            {
                return Redirect("/giris-yazar");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,PostContent,Image,Date,UpdateDate,ViewCount,CategoryID,WriterID,SeoTitle,Summary,isActive")] Post post)
        {

            if (Session["yazar"] != null || Session["admin"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(post).State = EntityState.Modified;
                    post.UpdateDate = DateTime.Now;
                    db.SaveChanges();
                    ViewBag.message = "1";
                    ViewBag.CategoryID = new SelectList(db.Category, "ID", "Name", post.CategoryID);
                    ViewBag.WriterID = new SelectList(db.Writer, "ID", "Name", post.WriterID);
                    return View(post);
                }
                ViewBag.CategoryID = new SelectList(db.Category, "ID", "Name", post.CategoryID);
                ViewBag.WriterID = new SelectList(db.Writer, "ID", "Name", post.WriterID);
                ViewBag.message = "0";
                return View(post);
            }
            else
            {
                return Redirect("/giris-yazar");
            }
        }

        //çöp kutusuna atma
        [HttpGet]
        [Route("cope-at/{id}")]
        public ActionResult SendToTrush(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Post post = db.Post.Find(id);
            post.isActive = false;
            db.SaveChanges();
            return Redirect("/admins");
        }

        [HttpGet]
        [Route("copten-cikar/{id}")]
        public ActionResult OutToTrash(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Post post = db.Post.Find(id);
            post.isActive = true;
            db.SaveChanges();
            return Redirect("/admins/trash");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(Comment comment)
        {
            if (Session["user"] != null)
            {
                if (ModelState.IsValid)
                {
                    comment.isApproved = false;
                    db.Comment.Add(comment);
                    db.SaveChanges();
                    Post post = db.Post.Find(comment.PostID);
                    string postCate = post.Category.Name;
                    string postSeoTitle = post.SeoTitle;
                    int postID = post.ID;
                    ViewBag.errmsg = "0";
                    return Redirect("/gonderi/" + postCate + "/" + postSeoTitle + "/" + postID);
                }
                return HttpNotFound();
            }
            else
            {
                return Redirect("/giris-kullanici");
            }

        }
        
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
