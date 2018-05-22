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
    public class LabTestsConductedsController : Controller
    {
        private NSHNContext db = new NSHNContext();

        // GET: LabTestsConducteds
        public async Task<ActionResult> Index()
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                var labTestsConducteds = db.LabTestsConducteds.Include(l => l.AvailableTest).Include(l => l.user);
                return View(await labTestsConducteds.ToListAsync());
            }
            else
            {
                return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
            }
        }

        // GET: LabTestsConducteds/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LabTestsConducted labTestsConducted = await db.LabTestsConducteds.FindAsync(id);
                if (labTestsConducted == null)
                {
                    return HttpNotFound();
                }
                return View(labTestsConducted);
            }
            else
            {
                return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
            }
        }

        // GET: LabTestsConducteds/Create
        public ActionResult Create()
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                ViewBag.TestId = new SelectList(db.AvailableTests, "Id", "TestName");
                ViewBag.DoctorId = new SelectList(db.users.Where(u => u.role.role_code == "DOC"), "id", "username");
                return View();
            }
            else
            {
                return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
            }
        }

        // POST: LabTestsConducteds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,PatientName,DoctorId,TestId,TestDate")] LabTestsConducted labTestsConducted)
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                if (ModelState.IsValid)
                {
                    db.LabTestsConducteds.Add(labTestsConducted);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ViewBag.TestId = new SelectList(db.AvailableTests, "Id", "TestName", labTestsConducted.TestId);
                ViewBag.DoctorId = new SelectList(db.users, "id", "username", labTestsConducted.DoctorId);
                return View(labTestsConducted);
            }
            else
            {
                return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
            }
        }

        // GET: LabTestsConducteds/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LabTestsConducted labTestsConducted = await db.LabTestsConducteds.FindAsync(id);
                if (labTestsConducted == null)
                {
                    return HttpNotFound();
                }
                ViewBag.TestId = new SelectList(db.AvailableTests, "Id", "TestName", labTestsConducted.TestId);
                ViewBag.DoctorId = new SelectList(db.users, "id", "username", labTestsConducted.DoctorId);
                return View(labTestsConducted);
            }
            else
            {
                return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
            }
        }

        // POST: LabTestsConducteds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,PatientName,DoctorId,TestId,TestDate")] LabTestsConducted labTestsConducted)
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(labTestsConducted).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewBag.TestId = new SelectList(db.AvailableTests, "Id", "TestName", labTestsConducted.TestId);
                ViewBag.DoctorId = new SelectList(db.users, "id", "username", labTestsConducted.DoctorId);
                return View(labTestsConducted);
            }
            else
            {
                return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
            }
        }

        // GET: LabTestsConducteds/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LabTestsConducted labTestsConducted = await db.LabTestsConducteds.FindAsync(id);
                if (labTestsConducted == null)
                {
                    return HttpNotFound();
                }
                return View(labTestsConducted);
            }
            else
            {
                return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
            }
        }

        // POST: LabTestsConducteds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                LabTestsConducted labTestsConducted = await db.LabTestsConducteds.FindAsync(id);
                db.LabTestsConducteds.Remove(labTestsConducted);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
            }
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
