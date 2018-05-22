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
    public class TourRequestsController : Controller
    {
        private NSHNContext db = new NSHNContext();

        // GET: TourRequests
        public async Task<ActionResult> Index()
        {
            if (Session["role"].ToString() != "ADM")
            {
                var tourRequests = db.TourRequests.Include(t => t.Tour).Include(t => t.user);
                return View(await tourRequests.ToListAsync());
            }

            TempData["needadmin"] = "MyMessage";
            return RedirectToAction("login", "account");
        }

        // GET: TourRequests/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourRequest tourRequest = await db.TourRequests.FindAsync(id);
            if (tourRequest == null)
            {
                return HttpNotFound();
            }
            return View(tourRequest);
        }

        // GET: TourRequests/Create
        public ActionResult Create()
        {
            ViewBag.TourID = new SelectList(db.Tours, "TourID", "TourTime");
            ViewBag.UserID = new SelectList(db.users, "id", "username");
            return View();
        }

        // POST: TourRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RequestID,TourID,UserID")] TourRequest tourRequest)
        {
            if (ModelState.IsValid)
            {
                db.TourRequests.Add(tourRequest);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TourID = new SelectList(db.Tours, "TourID", "TourTime", tourRequest.TourID);
            ViewBag.UserID = new SelectList(db.users, "id", "username", tourRequest.UserID);
            return View(tourRequest);
        }

        // GET: TourRequests/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourRequest tourRequest = await db.TourRequests.FindAsync(id);
            if (tourRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.TourID = new SelectList(db.Tours, "TourID", "TourTime", tourRequest.TourID);
            ViewBag.UserID = new SelectList(db.users, "id", "username", tourRequest.UserID);
            return View(tourRequest);
        }

        // POST: TourRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RequestID,TourID,UserID")] TourRequest tourRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tourRequest).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TourID = new SelectList(db.Tours, "TourID", "TourTime", tourRequest.TourID);
            ViewBag.UserID = new SelectList(db.users, "id", "username", tourRequest.UserID);
            return View(tourRequest);
        }

        // GET: TourRequests/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourRequest tourRequest = await db.TourRequests.FindAsync(id);
            if (tourRequest == null)
            {
                return HttpNotFound();
            }
            return View(tourRequest);
        }

        // POST: TourRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TourRequest tourRequest = await db.TourRequests.FindAsync(id);
            db.TourRequests.Remove(tourRequest);
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
