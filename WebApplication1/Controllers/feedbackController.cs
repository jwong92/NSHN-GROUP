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
    public class feedbackController : Controller
    {
        private NSHNContext db = new NSHNContext();

        // GET: feedback
        public async Task<ActionResult> Index()
        {
            return View(await db.feedbacks.ToListAsync());
        }

        // GET: feedback/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            feedback feedback = await db.feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // GET: feedback/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: feedback/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "feedback_id,user_id,date_location,suggestion,detail,problem")] feedback feedback)
        {
            if (ModelState.IsValid)
            {
                if (Session["userId"] != null)
                {
                    feedback.user_id = Convert.ToInt32(Session["userId"]);
                }
                feedback.date_submitted = System.DateTime.Now;
                db.feedbacks.Add(feedback);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(feedback);
        }

        // GET: feedback/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            feedback feedback = await db.feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: feedback/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "feedback_id,user_id,date_location,suggestion,detail,problem")] feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedback).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(feedback);
        }

        // GET: feedback/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            feedback feedback = await db.feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: feedback/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            feedback feedback = await db.feedbacks.FindAsync(id);
            db.feedbacks.Remove(feedback);
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
