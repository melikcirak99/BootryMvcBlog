using Bootry.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Bootry.Controllers
{
    public class EditableFunctionsController : Controller
    {
        private VeriTabani db = new VeriTabani();

        public ActionResult Index()
        {
            return View();
        }

        //hepsini aktif etme
        public string AddIsActive()
        {
            var model = db.Post.ToList();
            int count = 0;
            foreach (var post in model)
            {
                if (post.isActive == false)
                {
                    post.isActive = true;
                    db.SaveChanges();
                    count++;
                }
                else if (post.isActive == null)
                {
                    post.isActive = true;
                    db.SaveChanges();
                    count++;
                }
            }
            return count + " adet gönderi aktif edildi";
        }


        // özeti olmayan postlara default özet ekleme
        public string SetSummary()
        {
            List<Post> posts = db.Post.ToList();
            int _count = 0;
            foreach (var post in posts)
            {
                if (post.Summary == null)
                {
                    string _summary = "It is a long established fact that a reader" +
                        " will be distracted by the readable content of " +
                        "a page when looking at its layout";
                    post.Summary = _summary;
                    db.SaveChanges();
                    _count++;
                }
            }
            return _count + " adet gönderiye özet eklendi";
        }

        // gönderilere sıra ile resim ekleme
        public string ChangePostImages()
        {
            var model = db.Post.ToList();
            int count = 1;
            foreach (var post in model)
            {
                if (count < 10)
                {
                    post.Image = "img0" + count + "-min.jpg";
                    db.SaveChanges();
                    count++;
                }
                else
                {
                    post.Image = "img" + count + "-min.jpg";
                    db.SaveChanges();
                    count++;
                }
            }
            return count + " adet değiştirildi";
        }

        //gönderi isimlerini değiştirme
        public string ChangePostTitles()
        {
            var model = db.Post.ToList();
            int count = 1;
            foreach (var post in model)
            {
                post.Title = post.ID + " idli gönderi";
                db.SaveChanges();
                count++;
            }
            return count + "adet gönderi değiştirildi";
        }


        //seo title olmayan postlara seo title ekleme
        public string SetSeoTitle()
        {
            List<Post> postList = db.Post.ToList();
            foreach (var post in postList)
            {
                if (post.SeoTitle == null)
                {
                    post.SeoTitle = FriendlyURLTitle(post.Title);
                    db.SaveChanges();
                }
            }
            return "tüm gönderilere eklendi";
        }
          //bu fonksiyon alıntıdır...
        public string FriendlyURLTitle(string pTitle)
        {
            pTitle = pTitle.Replace(" ", "-");
            pTitle = pTitle.Replace(".", "-");
            pTitle = pTitle.Replace("ı", "i");
            pTitle = pTitle.Replace("İ", "I");

            pTitle = String.Join("", pTitle.Normalize(NormalizationForm.FormD) // türkçe karakterleri ingilizceye çevir.
                    .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));

            pTitle = HttpUtility.UrlEncode(pTitle);
            return System.Text.RegularExpressions.Regex.Replace(pTitle, @"\%[0-9A-Fa-f]{2}", "");
        }


    }
}
