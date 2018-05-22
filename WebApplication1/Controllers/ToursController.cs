using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ToursController : Controller
    {
        private NSHNContext db = new NSHNContext();

        // GET: Tours
        public async Task<ActionResult> Index()
        {
            return View(await db.Tours.ToListAsync());

        }

        // GET: Tours/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = await db.Tours.FindAsync(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // GET: Tours/Create
        public ActionResult Create()
        {
            if (Session["role"].ToString() != "ADM")
            {
                var guideName = db.TourGuides.ToList();
                List<SelectListItem> guideList = new List<SelectListItem>();
                foreach (TourGuide item in guideName)
                {
                    guideList.Add(new SelectListItem
                    {
                        Text = item.GuideName,
                        Value = item.GuideID.ToString()
                    });
                }
                ViewBag.TourGuides = guideList;

                return View();
            }

            TempData["needadmin"] = "MyMessage";
            return RedirectToAction("login", "account");
        }

        // POST: Tours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TourID,TourTime,TourName,GuideName")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                db.Tours.Add(tour);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tour);
        }

        // POST: Tours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Request([Bind(Include = "TourID,UserId")] TourRequest tourrequest)
        {
            if (ModelState.IsValid)
            {
                db.TourRequests.Add(tourrequest);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tourrequest);
        }

        // GET: Tours/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = await db.Tours.FindAsync(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Tours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TourID,TourTime,TourName,GuideName")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tour);
        }

        // GET: Tours/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = await db.Tours.FindAsync(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tour tour = await db.Tours.FindAsync(id);
            db.Tours.Remove(tour);
            await db.SaveChangesAsync();
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

