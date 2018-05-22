using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.IO;
using System.Data.SqlTypes;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace WebApplication1.Controllers
{
    public class newsController : Controller
    {
        private NSHNContext db = new NSHNContext();

        // GET: news
        public ActionResult Index()
        {
            try
            {
                return View(db.news.ToList());
            }
            catch (Exception e)
            {
                ViewBag.GenericException = e.Message;
            }
            return View("~/Views/Navigate/Errors.cshtml");
        }

        [HttpGet]
        public PartialViewResult GetIndexArticle()
        {
            try
            {
                var news_articles = (from n in db.news
                                     from i in db.images
                                     where i.news_article_id == n.id && i.is_main == 1
                                     orderby n.pub_date descending
                                     select new
                                     {
                                         n,
                                         i
                                     }).Take(3);

                IndexView indexview = new IndexView();
                List<image> img = new List<image>();
                List<news> news = new List<news>();
                foreach (var article in news_articles)
                {
                    img.Add(article.i);
                    news.Add(article.n);
                }
                indexview.image = img;
                indexview.news = news;

                return PartialView("~/Views/news_partials/_IndexArticles.cshtml", indexview);
            }
            catch(Exception e)
            {
                ViewBag.ErrorMssg = e.Message;
            }
            return PartialView("~/Views/Shared/_ErrorMssg.cshtml");

        }

        public PartialViewResult MainImgs(int id)
        {
            try
            {
                image img = new image();
                //SELECT ALL IMAGES FOR THIS ARTICLE ID
                List<image> imageList = db.images.Where(i => i.news_article_id == id).ToList();

                //SELECT ALL IMAGES FOR THIS ARTICLE WHERE THE IMAGE IS MAIN
                imageList = imageList.Where(i => i.is_main == 1).ToList();

                return PartialView("~/Views/news_partials/_MainImgs.cshtml", imageList);
            }
            catch(Exception e)
            {
                ViewBag.ErrorMssg = e.Message;
            }
            return PartialView("~/Views/Shared/_ErrorMssg.cshtml");
        }

        // GET: news/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                news news = db.news.Find(id);
                if (news == null)
                {
                    return HttpNotFound();
                }
                return View(news);
            }
            catch (Exception e)
            {
                ViewBag.GenericException = e.Message;
            }
            return View("~/Views/Navigate/Errors.cshtml");
        }

        public PartialViewResult ImagesAllForArticle(int id)
        {
            try
            {
                //GET THE LIST OF ALL IMAGES FROM THE ARTICLE ID
                List<image> images = db.images.Where(i => i.news_article_id == id).ToList();
                return PartialView("~/Views/news_partials/_ImagesAllForArticle.cshtml", images);
            }
            catch (Exception e)
            {
                ViewBag.ErrorMssg = e.Message;
            }
            return PartialView("~/Views/Shared/_ErrorMssg.cshtml");
        }
        [HttpPost]
        public ActionResult SaveCaption(int id)
        {
            return RedirectToAction("Details", new { id });
        }

        /*********************************************************/
        [HttpGet]
        public PartialViewResult DisplayTestAjax(int id)
        {
            try
            {
                //GET THE LIST OF ALL IMAGES FROM THE ARTICLE ID
                List<image> images = db.images.Where(i => i.news_article_id == id).ToList();
                return PartialView("~/Views/news_partials/_DisplayPhotoCaptionCreate.cshtml", images);
            }
            catch (Exception e)
            {
                @ViewBag.ErrorMssg = e.Message;
            }
            return PartialView("~/Views/Shared/_ErrorMssg.cshtml");
        }

        [HttpPost]
        public void SaveImgCaption(int id, FormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //SAVE THE CAPTION FOR THE CORRECT IMAGE
                    string caption = form["item.caption"].ToString();
                    int imgId = Convert.ToInt16(form["item.id"]);

                    //GET THE IMAGE PROPERTIES FROM THE IMG ID 
                    image articleImage = db.images.Find(imgId);

                    //MODIFY THE IMAGE CAPTION
                    articleImage.caption = caption;

                    db.Entry(articleImage).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (DbUpdateException e)
            {
                ViewBag.DbExceptionMessage = e.Message;
            }
            catch (SqlException e)
            {
                ViewBag.SqlExceptionMessage = e.Message;
            }
            catch (Exception e)
            {
                ViewBag.GenericException = e.Message;
            }
            RedirectToAction("Errors", "Navigate"); 
        }
        /*********************************************************/

        //REDIRECTED HERE FROM CREATE TO THEN ADD A CAPTION
        [HttpGet]
        public ActionResult EditCaption(int? id)
        {
            try
            {
                news news = db.news.Find(id);
                return View(news);
            }
            catch (Exception e)
            {
                ViewBag.GenericException = e.Message;
            }
            return RedirectToAction("Errors", "Navigate");
        }

        // GET: news/Create
        public ActionResult Create()
        {
            if (Session["role"] != null)
            {
                if (Session["role"].ToString() == "ADM")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("RestrictedAccess", "Navigate");
                }
            }
            else
            {
                return RedirectToAction("RestrictedAccess", "Navigate");
            }
        }

        //ALLOW USER TO CREATE A FILE
        public PartialViewResult DisplayFileUpload()
        {
            return PartialView("~/Views/news_partials/_images.cshtml");
        }

        // POST: news/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "title,pub_date,article_content,article_summary,author")] news news, HttpPostedFileBase[] files)
        {
            if (Session["role"] != null)
            {
                if (Session["role"].ToString() == "ADM")
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            //ADD TO NEWS
                            news.pub_date = DateTime.Now;
                            news.title.Trim();
                            news.article_summary.Trim();
                            news.article_content.Trim();
                            db.news.Add(news);
                            db.SaveChanges();

                            //ADD TO IMAGES
                            image img = new image();
                            List<string> filenames = new List<string>();

                            if (files.Count() > 0)
                            {
                                foreach (HttpPostedFileBase file in files)
                                {
                                    if (file != null)
                                    {
                                        //ADD EACH FILE NAME INTO AN ARRAY
                                        string filename = Path.GetFileName(file.FileName);
                                        filenames.Add(filename);

                                        //SET THE IMG SOURCE TO THE FILE NAME
                                        img.img_src = filename;

                                        //SET THE MAIN IMAGE ID
                                        if (filenames.Count() == 1)
                                        {
                                            img.is_main = 1;
                                        }
                                        else
                                        {
                                            img.is_main = 0;
                                        }

                                        //SET THE NEWS ARTICLE ID TO THE CURRENT ONE
                                        img.news_article_id = news.id;
                                        db.images.Add(img);

                                        //ADD THE IMAGES TO YOUR SERVER
                                        //string path = Path.Combine(Server.MapPath("~/News_Images/" + img.id.ToString() + "/"));
                                        string path = Path.Combine(Server.MapPath("~/News_Images/" + news.id.ToString() + "/"));
                                        Directory.CreateDirectory(path);
                                        path = Path.Combine(Server.MapPath("~/News_Images/" + news.id.ToString() + "/"), filename);

                                        file.SaveAs(path);
                                        db.SaveChanges();
                                    }
                                }
                                return RedirectToAction("EditCaption", new { news.id });
                            }
                            else
                            {
                                ViewBag.ErrorMssg = "The file you've uploaded is corrupt";
                                return View();
                            }
                        }
                        //FOR DEVELOPER TESTING//
                        var errors = ModelState.Select(x => x.Value.Errors)
                       .Where(y => y.Count > 0)
                       .ToList();

                        return View(news);
                    }
                    catch (DbUpdateException e)
                    {
                        ViewBag.DbExceptionMessage = e.Message;
                    }
                    catch (SqlException e)
                    {
                        ViewBag.SqlExceptionMessage = e.Message;
                    }
                    catch (Exception e)
                    {
                        ViewBag.GenericException = e.Message;
                    }
                    return RedirectToAction("Errors", "Navigate");
                }
                else
                {
                    return RedirectToAction("RestrictedAccess", "Navigate");
                }
            }
            else
            {
                return RedirectToAction("RestrictedAccess", "Navigate");
            }
        }

        // GET: news/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["role"] != null)
            {
                if (Session["role"].ToString() == "ADM")
                {
                    try
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        news news = db.news.Find(id);
                        if (news == null)
                        {
                            return HttpNotFound();
                        }
                        return View(news);
                    }
                    catch (Exception e)
                    {
                        ViewBag.GenericException = e.Message;
                    }
                    return RedirectToAction("Errors", "Navigate");
                }
                else
                {
                    return RedirectToAction("RestrictedAccess", "Navigate");
                }
            }
            else
            {
                return RedirectToAction("RestrictedAccess", "Navigate");
            }
        }

        public PartialViewResult EditPhoto(int? id)
        {
            //FIND IMAGES ASSOCIATED WITH THIS ARTICLE ID
            return PartialView("~/Views/news_partials/_EditPhoto.cshtml", db.images.Where(i => i.news_article_id == id).ToList());
        }

        public ActionResult DeletePhoto(int? id)
        {
            if (Session["role"] != null)
            {
                if (Session["role"].ToString() == "ADM")
                {
                    try
                    {
                        image img = db.images.Find(id);
                        int articleId = img.news_article_id;
                        string img_src = img.img_src;

                        //IF THE IMAGE SELECTED IS EQUAL TO MAIN, AND IF THERE ARE MANY PHOTOS, ASSIGN THE MAIN TO ANOTHER PHOTO
                        List<image> images = db.images.Where(i => i.news_article_id == articleId).ToList();
                        if (img.is_main == 1 && images.Count() > 1)
                        {
                            images[1].is_main = 1;
                            db.SaveChanges();
                        }

                        //REMOVE THE IMG SOURCE
                        db.images.Remove(img);
                        db.SaveChanges();

                        //REMOVE FROM DIRECTORY
                        string pathToDirectory = Request.MapPath("~/News_Images/" + articleId.ToString());
                        DirectoryInfo DirInfo = new DirectoryInfo(pathToDirectory);

                        foreach (FileInfo file in DirInfo.GetFiles())
                        {
                            if (file.Name == img_src)
                            {
                                file.Delete();
                            }
                        }

                        //IF THERE AREN'T ANY MORE FILES IN THE DIRECTORY, DELETE THE DIRECTORY
                        if (!Directory.EnumerateFileSystemEntries(pathToDirectory).Any())
                        {
                            Directory.Delete(pathToDirectory);
                        }

                        return RedirectToAction("Edit", "News", new { id = articleId });
                    }
                    catch (DbUpdateException e)
                    {
                        ViewBag.DbExceptionMessage = e.Message;
                    }
                    catch (SqlException e)
                    {
                        ViewBag.SqlExceptionMessage = e.Message;
                    }
                    catch (Exception e)
                    {
                        ViewBag.GenericException = e.Message;
                    }
                    return RedirectToAction("Errors", "Navigate");
                }
                else
                {
                    return RedirectToAction("RestrictedAccess", "Navigate");
                }
            }
            else
            {
                return RedirectToAction("RestrictedAccess", "Navigate");
            }
        }

        public ActionResult AdminEditCaption(int id, image img)
        {
            image image = db.images.Find(img.id);
            image.caption = img.caption;
            db.Entry(img).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Edit", new { id });
        }

        // POST: news/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,author,pub_date,article_content,article_summary")] news news, HttpPostedFileBase[] files)
        {
            if (Session["role"] != null)
            {
                if (Session["role"].ToString() == "ADM")
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            //UPDATE NEWS
                            news.title.Trim();
                            news.article_content.Trim();
                            news.article_summary.Trim();
                            db.Entry(news).State = EntityState.Modified;
                            db.SaveChanges();
                            //return RedirectToAction("Index");

                            //ADDING NEW PHOTOS
                            image img = new image();

                            //LIST ALL THE IMAGES FOR THIS ARTICLE
                            List<image> imageList = db.images.Where(i => i.news_article_id == news.id).ToList();

                            //DETERMINE IF THE ANY OF THE IMAGES FOR THIS ARTICLE HAVE A MAIN ID
                            var main_images = imageList.Where(i => i.is_main == 1);
                            int count = 0;
                            foreach (var main in main_images)
                            {
                                count++;
                            }

                            if (files.Count() > 0)
                            {
                                foreach (HttpPostedFileBase file in files)
                                {
                                    if (file != null)
                                    {
                                        //GET THE FILENAME OF EACH FILE
                                        string filename = Path.GetFileName(file.FileName);

                                        //SET THE IMG SOURCE TO THE FILE NAME
                                        img.img_src = filename;

                                        //SET THE MAIN IMAGE ID
                                        if (count == 0)
                                        {
                                            img.is_main = 1;
                                        }
                                        else
                                        {
                                            img.is_main = 0;
                                        }

                                        //SET THE NEWS ARTICLE ID TO THE CURRENT ONE
                                        img.news_article_id = news.id;
                                        db.images.Add(img);

                                        //ADD THE IMAGES TO YOUR SERVER
                                        //string path = Path.Combine(Server.MapPath("~/News_Images/" + img.id.ToString() + "/"));
                                        string path = Path.Combine(Server.MapPath("~/News_Images/" + news.id.ToString() + "/"));
                                        Directory.CreateDirectory(path);
                                        path = Path.Combine(Server.MapPath("~/News_Images/" + news.id.ToString() + "/"), filename);

                                        file.SaveAs(path);
                                        db.SaveChanges();
                                    }
                                }
                                return RedirectToAction("Edit");
                            }
                            return RedirectToAction("Edit");
                        }
                        return RedirectToAction("Edit");
                    }
                    catch (DbUpdateException e)
                    {
                        ViewBag.DbExceptionMessage = e.Message;
                    }
                    catch (SqlException e)
                    {
                        ViewBag.SqlExceptionMessage = e.Message;
                    }
                    catch (Exception e)
                    {
                        ViewBag.GenericException = e.Message;
                    }
                    return RedirectToAction("Errors", "Navigate");
                }
                else
                {
                    return RedirectToAction("RestrictedAccess", "Navigate");
                }
            }
            else
            {
                return RedirectToAction("RestrictedAccess", "Navigate");
            }
        }

        // GET: news/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["role"] != null)
            {
                if (Session["role"].ToString() == "ADM")
                {
                    try
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        news news = db.news.Find(id);
                        if (news == null)
                        {
                            return HttpNotFound();
                        }
                        return View(news);
                    }
                    catch (Exception e)
                    {
                        ViewBag.GenericException = e.Message;
                    }
                    return RedirectToAction("Errors", "Navigate");
                }
                else
                {
                    return RedirectToAction("RestrictedAccess", "Navigate");
                }
            }
            else
            {
                return RedirectToAction("RestrictedAccess", "Navigate");
            }
        }

        // POST: news/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["role"] != null)
            {
                if (Session["role"].ToString() == "ADM")
                {
                    try
                    {
                        //DELETE IMAGES FROM DATABASE
                        List<image> images = db.images.Where(i => i.news_article_id == id).ToList();

                        foreach (image img in images)
                        {
                            db.images.Remove(img);
                        }

                        //DELETE FROM NEWS
                        news news = db.news.Find(id);
                        db.news.Remove(news);
                        db.SaveChanges();

                        //REMOVE FROM DIRECTORY
                        string pathToDirectory = Request.MapPath("~/News_Images/" + id.ToString());
                        DirectoryInfo DirInfo = new DirectoryInfo(pathToDirectory);

                        foreach (FileInfo file in DirInfo.GetFiles())
                        {
                            file.Delete();
                        }

                        //IF THERE AREN'T ANY MORE FILES IN THE DIRECTORY, DELETE THE DIRECTORY
                        if (!Directory.EnumerateFileSystemEntries(pathToDirectory).Any())
                        {
                            Directory.Delete(pathToDirectory);
                        }

                        //DELETE THE COMMENTS ASSOCIATED WITH THAT NEWS ID
                        comment comment = new comment();
                        var comments = db.comments.Where(c => c.article_id == id);

                        foreach (var c in comments)
                        {
                            int comment_id = c.id;
                            comment = db.comments.Find(comment_id);
                            db.comments.Remove(comment);
                        }
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    catch (DbUpdateException e)
                    {
                        ViewBag.DbExceptionMessage = e.Message;
                    }
                    catch (SqlException e)
                    {
                        ViewBag.SqlExceptionMessage = e.Message;
                    }
                    catch (Exception e)
                    {
                        ViewBag.GenericException = e.Message;
                    }
                    return RedirectToAction("Errors", "Navigate");
                }
                else
                {
                    return RedirectToAction("RestrictedAccess", "Navigate");
                }
            }
            else
            {
                return RedirectToAction("RestrictedAccess", "Navigate");
            }
        }
        
        //AJAX ADDING A COMMENT
        public PartialViewResult AddComment(int id, User_News_Comments unc)
        {
            if (unc.comment.comment1 != null)
            {
                try
                {
                    //ADD INTO THE COMMENTS TABLE
                    comment comment = new comment();

                    if (Session["userId"] != null)
                    {
                        comment.user_id = Convert.ToInt16(Session["userId"]);
                    }
                    else
                    {
                        RedirectToAction("Details", new { id });
                    }
                    comment.article_id = id;
                    comment.comm_date = DateTime.Now;
                    comment.comment1 = unc.comment.comment1.Trim();
                    db.comments.Add(comment);
                    db.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    ViewBag.ErrorMssg = e.Message;
                    return PartialView("~/Views/Shared/_ErrorMssg.cshtml");
                }
                catch (SqlException e)
                {
                    ViewBag.ErrorMssg = e.Message;
                    return PartialView("~/Views/Shared/_ErrorMssg.cshtml");
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMssg = e.Message;
                    return PartialView("~/Views/Shared/_ErrorMssg.cshtml");
                }
            }
            //PULL COMMENTS FROM ARTICLE ID, AND EACH USER.
            var comments = (from n in db.news
                            join c in db.comments on n.id equals c.article_id
                            join u in db.users on c.user_id equals u.id
                            where n.id == id
                            orderby c.comm_date descending
                            select new
                            {
                                c,
                                u
                            });

            List<User_News_Comments> uncL = new List<User_News_Comments>();
            foreach (var comm in comments)
            {
                User_News_Comments user_n_c = new User_News_Comments();
                user_n_c.user = comm.u;
                user_n_c.comment = comm.c;
                user_n_c.news = db.news.Find(id);
                uncL.Add(user_n_c);
            }
            return PartialView("~/Views/news_partials/_displayComments.cshtml", uncL);
        }

        public ActionResult DisplayComments(int id)
        {
            try
            {
                //PULL COMMENTS FROM ARTICLE ID, AND EACH USER.
                var comments = (from n in db.news
                                join c in db.comments on n.id equals c.article_id
                                join u in db.users on c.user_id equals u.id
                                where n.id == id
                                orderby c.comm_date descending
                                select new
                                {
                                    c,
                                    u
                                });

                List<User_News_Comments> uncL = new List<User_News_Comments>();
                foreach (var comment in comments)
                {
                    User_News_Comments unc = new User_News_Comments();
                    unc.user = comment.u;
                    unc.comment = comment.c;
                    unc.news = db.news.Find(id);
                    uncL.Add(unc);
                }
                return PartialView("~/Views/news_partials/_displayComments.cshtml", uncL);
            }
            catch (DbUpdateException e)
            {
                ViewBag.DbExceptionMessage = e.Message;
            }
            catch (SqlException e)
            {
                ViewBag.SqlExceptionMessage = e.Message;
            }
            catch (Exception e)
            {
                ViewBag.GenericException = e.Message;
            }
            return RedirectToAction("Errors", "Navigate");
        }

        public PartialViewResult DisplayAddComment(int id)
        {
            try
            {
                User_News_Comments unc = new User_News_Comments();
                unc.news = db.news.Find(id);
                return PartialView("~/Views/news_partials/_AddComment.cshtml", unc);
            }
            catch (Exception e)
            {
                ViewBag.GenericException = e.Message;
            }
            return PartialView("~/Views/Shared/_ErrorMssg.cshtml");

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


//USEFUL LINKS
/*
 * UNDERSTANDING EDITS AND BIND VARIABLES
 * https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/examining-the-edit-methods-and-edit-view
*/