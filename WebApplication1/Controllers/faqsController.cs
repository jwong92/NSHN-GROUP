using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class faqsController : Controller
    {
        private NSHNContext db = new NSHNContext();

        // GET: faqs
        public ActionResult Index(string searchFAQ)
        {
            try
            {
                var faqs = from f in db.faqs select f;
                //var faqs = db.faqs.Include(f => f.category).Include(f => f.user);
                if (!String.IsNullOrEmpty(searchFAQ))
                {

                    faqs = faqs.Where(f => f.question.ToUpper().Contains(searchFAQ.ToUpper()) ||
                    f.category.name.ToUpper().Contains(searchFAQ.ToUpper()));
                }
                return View(faqs.ToList());
            }
            catch(Exception exception)
            {
                ViewBag.ExceptionMessage = exception.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }

        // GET: faqs/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (Session["role"] != null)
                {
                    if (Session["role"].ToString() == "ADM")
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }


                        faq faq = db.faqs.Find(id);
                        if (faq == null)
                        {
                            return HttpNotFound();
                        }
                        return View(faq);
                    }
                }
                else
                {
                    return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
                }
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // GET: faqs/Create
        public ActionResult Create()
        {

            try
            {
                if (Session["role"] != null)
                {
                    if (Session["role"].ToString() == "ADM" || Session["role"].ToString() == "PAT" || Session["role"].ToString() == "USR")
                    {
                        ViewBag.category_id = new SelectList(db.categories, "id", "name");
                        ViewBag.publisher_id = new SelectList(db.users, "id", "username");
                        return View();
                    }
                }
                else
                {
                    return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
                }
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }

        // POST: faqs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,question,answer,category_id")] faq faq)
        {
            try
            {
                if (Session["role"] != null)
                {
                    if (Session["role"].ToString() == "ADM" || Session["role"].ToString() == "PAT" || Session["role"].ToString() == "USR")
                    {
                        if (ModelState.IsValid)
                        {
                            if (Session["userId"] != null)
                            {
                                faq.publisher_id = Convert.ToInt32(Session["userId"]);
                            }
                            else
                            {
                                faq.publisher_id = 0;
                            }

                            faq.date_created = System.DateTime.Now;
                            db.faqs.Add(faq);
                            db.SaveChanges();
                            ViewBag.questionAdd = "Thank you for your question! Our staff will look into providing an asnwer!";

                            if (Session["role"].ToString() == "ADM")
                            {
                                return RedirectToAction("Index");
                            }

                        }

                        ViewBag.category_id = new SelectList(db.categories, "id", "name", faq.category_id);
                        //ViewBag.publisher_id = new SelectList(db.users, "id", "username", faq.publisher_id);
                        return View(faq);
                    }
                }
                else
                {
                    return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
                }
            }
            catch(DbUpdateException e) {
                ViewBag.DbExceptionMessage = e.Message;
            }
            catch(SqlException e)
            {
                ViewBag.SqlExceptionMessage = e.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // GET: faqs/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (Session["role"] != null)
                {
                    if (Session["role"].ToString() == "ADM")
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }


                        faq faq = db.faqs.Find(id);
                        if (faq == null)
                        {
                            return HttpNotFound();
                        }
                        ViewBag.category_id = new SelectList(db.categories, "id", "name", faq.category_id);
                        return View(faq);
                    }
                }
                else
                {
                    return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
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
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // POST: faqs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,question,answer,category_id")] faq faq)
        {
            try
            {
                if (Session["role"] != null)
                {
                    if (Session["role"].ToString() == "ADM")
                    {
                        if (ModelState.IsValid)
                        {
                            int pub_id = Convert.ToInt16(Session["userId"]);

                            faq.publisher_id = pub_id;
                            faq.date_created = DateTime.Now;
                            db.Entry(faq).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        ViewBag.category_id = new SelectList(db.categories, "id", "name", faq.category_id);

                        return View(faq);
                    }
                }
                else
                {
                    return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
                }
            }
            catch (DbUpdateException e)
            {
                ViewBag.DbExceptionMessage = e.Message;
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlExceptionMessage = sqlException.Message;
            }
            catch (Exception exception)
            {
                ViewBag.genericException = exception.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // GET: faqs/Delete/5
        public ActionResult Delete(int? id)
        {

            try
            {
                if (Session["role"] != null)
                {
                    if (Session["role"].ToString() == "ADM")
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        faq faq = db.faqs.Find(id);
                        if (faq == null)
                        {
                            return HttpNotFound();
                        }
                        return View(faq);
                    }
                }
                else
                {
                    return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
                }
            }
            catch (DbUpdateException e)
            {
                ViewBag.DbExceptionMessage = e.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return RedirectToAction("Details", "Errors");
        }

        // POST: faqs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                if (Session["role"] != null)
                {
                    if (Session["role"].ToString() == "ADM")
                    {
                        faq faq = db.faqs.Find(id);
                        db.faqs.Remove(faq);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
                }
            }
            catch (DbUpdateException e)
            {
                ViewBag.DbExceptionMessage = e.Message;
            }
            catch (SqlException sqlException)
            {
                TempData["SqlException"] = sqlException.Message;
            }
            catch (Exception genericException)
            {
                TempData["SqlException"] = genericException.Message;
            }
            return RedirectToAction("Delete", new { id = id });

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
