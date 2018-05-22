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
    public class TourGuidesController : Controller
    {
        private NSHNContext db = new NSHNContext();

        // GET: TourGuides
        public async Task<ActionResult> Index()
        {
            return View(await db.TourGuides.ToListAsync());
        }

        // GET: TourGuides/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourGuide tourGuide = await db.TourGuides.FindAsync(id);
            if (tourGuide == null)
            {
                return HttpNotFound();
            }
            return View(tourGuide);
        }

        // GET: TourGuides/Create
        public ActionResult Create()
        {
            var listItems = new SelectListItem[] {
                            new SelectListItem(){Text="Nurology",Value="1"},
                            new SelectListItem(){Text="Oncology",Value="2"},
                            new SelectListItem(){Text="ER",Value="3"},
                            new SelectListItem(){Text="Long Term Care",Value="4"},
                            new SelectListItem(){Text="General",Value="5"},
            };
            ViewBag.Specialities = listItems;

            return View();
        }

        // POST: TourGuides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "GuideID,GuideName,GuideSpeciality,GuideSuitability")] TourGuide tourGuide)
        {
            if (ModelState.IsValid)
            {
                db.TourGuides.Add(tourGuide);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tourGuide);
        }

        // GET: TourGuides/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourGuide tourGuide = await db.TourGuides.FindAsync(id);
            if (tourGuide == null)
            {
                return HttpNotFound();
            }
            return View(tourGuide);
        }

        // POST: TourGuides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "GuideID,GuideName,GuideSpeciality,GuideSuitability")] TourGuide tourGuide)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tourGuide).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tourGuide);
        }

        // GET: TourGuides/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourGuide tourGuide = await db.TourGuides.FindAsync(id);
            if (tourGuide == null)
            {
                return HttpNotFound();
            }
            return View(tourGuide);
        }

        // POST: TourGuides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TourGuide tourGuide = await db.TourGuides.FindAsync(id);
            db.TourGuides.Remove(tourGuide);
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
