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
    public class appointmentsController : Controller
    {
        private NSHNContext db = new NSHNContext();

        // GET: appointments
        public async Task<ActionResult> Index()
        {
            return View(await db.appointments.ToListAsync());
        }

        // GET: appointments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            appointment appointment = await db.appointments.FindAsync(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: appointments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "appointment_id,full_name,phone_number,appointment_date,detail")] appointment appointment)
        {
            if (ModelState.IsValid)
            {
                if (Session["userId"] != null)
                {
                    appointment.user_id = Convert.ToInt32(Session["userId"]);
                }
                db.appointments.Add(appointment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(appointment);
        }

        // GET: appointments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            appointment appointment = await db.appointments.FindAsync(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "appointment_id,full_name,phone_number,appointment_date,detail")] appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(appointment);
        }

        // GET: appointments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            appointment appointment = await db.appointments.FindAsync(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            appointment appointment = await db.appointments.FindAsync(id);
            db.appointments.Remove(appointment);
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
