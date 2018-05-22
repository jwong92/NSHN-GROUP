using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class VolunteersController : Controller
    {
        private NSHNContext db = new NSHNContext();

        // GET: Volunteers
        public ActionResult Index(string sortOrder, string searchNameString)
        {
            if (Session["role"] == null)
            {
                TempData["needadmin"] = "MyMessage";
                return RedirectToAction("login", "account");
            }
            if (Session["role"].ToString() == "USR")
            {
                Session["userId"] = null;
                Session["userName"] = null;
                Session["role"] = null;
                TempData["needadmin"] = "MyMessage";
                return RedirectToAction("login", "account");
            }
            if (Session["role"].ToString() == "ADM")
            {
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
                ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "F_Name_desc" : "F_Name_asc";
                ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
                var volunteers = from v in db.Volunteers
                                 select v;
                if (!String.IsNullOrEmpty(searchNameString))
                {
                    volunteers = volunteers.Where(v => v.LastName.ToUpper().Contains(searchNameString.ToUpper())
                                           || v.FirstName.ToUpper().Contains(searchNameString.ToUpper()));
                }
                switch (sortOrder)
                {
                    case "Name_desc":
                        volunteers = volunteers.OrderByDescending(v => v.LastName);
                        break;
                    case "F_Name_desc":
                        volunteers = volunteers.OrderByDescending(v => v.FirstName);
                        break;
                    case "F_Name_asc":
                        volunteers = volunteers.OrderBy(v => v.FirstName);
                        break;
                    case "Date":
                        volunteers = volunteers.OrderBy(v => v.Dob);
                        break;
                    case "Date_desc":
                        volunteers = volunteers.OrderByDescending(v => v.Dob);
                        break;
                    default:
                        volunteers = volunteers.OrderBy(v => v.LastName);
                        break;
                }
                return View(volunteers.ToList());
            }
            TempData["needadmin"] = "MyMessage";
            return RedirectToAction("login", "account");
        }

        // GET: Volunteers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volunteer volunteer = db.Volunteers.Find(id);
            if (volunteer == null)
            {
                return HttpNotFound();
            }
            return View(volunteer);
        }

        // GET: Volunteers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Volunteers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VolunteerID,FirstName,LastName,Dob,Phone,StAddress,Reason,skills,username,Email,dateavailable")] Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                volunteer.username = Session["userName"].ToString();
                db.Volunteers.Add(volunteer);
                db.SaveChanges();
                return RedirectToAction("ThankYou");
            }

            return View(volunteer);
        }

        public ActionResult ThankYou()
        {
            return View();
        }

        // GET: Volunteers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volunteer volunteer = db.Volunteers.Find(id);
            if (volunteer == null)
            {
                return HttpNotFound();
            }
            return View(volunteer);
        }

        // POST: Volunteers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VolunteerID,FirstName,LastName,Dob,Phone,StAddress,Reason,skills,username,Email,dateavailable")] Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volunteer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(volunteer);
        }

        // GET: Volunteers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volunteer volunteer = db.Volunteers.Find(id);
            if (volunteer == null)
            {
                return HttpNotFound();
            }
            return View(volunteer);
        }

        // POST: Volunteers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Volunteer volunteer = db.Volunteers.Find(id);
            db.Volunteers.Remove(volunteer);
            db.SaveChanges();
            return RedirectToAction("Index");
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

