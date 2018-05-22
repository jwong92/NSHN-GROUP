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
    public class sitesController : Controller
    {
        private NSHNContext db = new NSHNContext();

        // GET: sites
        public async Task<ActionResult> Index()
        {
            return View(await db.sites.ToListAsync());
        }

        // GET: sites/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            site site = await db.sites.FindAsync(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        // GET: sites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: sites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,name,phone,address,city,province,postal_code")] site site)
        {
            if (ModelState.IsValid)
            {
                db.sites.Add(site);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(site);
        }

        // GET: sites/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            site site = await db.sites.FindAsync(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        // POST: sites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,name,phone,address,city,province,postal_code")] site site)
        {
            if (ModelState.IsValid)
            {
                db.Entry(site).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(site);
        }

        // GET: sites/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            site site = await db.sites.FindAsync(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        // POST: sites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            site site = await db.sites.FindAsync(id);
            db.sites.Remove(site);
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
