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
    public class LabTestResultsController : Controller
    {
        private NSHNContext db = new NSHNContext();

        // GET: LabTestResults
        public async Task<ActionResult> Index()
        {
            if (Session["role"] != null)
            {
                if (Session["role"].ToString() == "ADM")
                {
                    var labTestResults = db.LabTestResults.Include(l => l.LabTestsConducted);
                    return View(await labTestResults.ToListAsync());
                }
                else
                {
                    string userName = Session["userName"].ToString();
                    var labTestResults = db.LabTestResults.Where(l => l.LabTestsConducted.PatientName.Equals(userName));
                    return View(await labTestResults.ToListAsync());
                }

            }
            else
            {
                return View("~/Views/LabTestResults/LoginPrompt.cshtml");
            }
        }

        // GET: LabTestResults/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["role"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LabTestResult labTestResult = await db.LabTestResults.FindAsync(id);
                if (labTestResult == null)
                {
                    return HttpNotFound();
                }
                return View(labTestResult);
            }
            else
            {
                return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
            }
        }

        // GET: LabTestResults/Create
        public ActionResult Create()
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                ViewBag.LabTestsConductedId = new SelectList(db.LabTestsConducteds, "Id", "PatientName");
                return View();
            }
            else
            {
                return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
            }
        }

        // POST: LabTestResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,LabTestsConductedId,Result,CreatedDate")] LabTestResult labTestResult)
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                if (ModelState.IsValid)
                {
                    db.LabTestResults.Add(labTestResult);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ViewBag.LabTestsConductedId = new SelectList(db.LabTestsConducteds, "Id", "PatientName", labTestResult.LabTestsConductedId);
                return View(labTestResult);
            }
            else
            {
                return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
            }
        }

        // GET: LabTestResults/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LabTestResult labTestResult = await db.LabTestResults.FindAsync(id);
                if (labTestResult == null)
                {
                    return HttpNotFound();
                }
                ViewBag.LabTestsConductedId = new SelectList(db.LabTestsConducteds, "Id", "PatientName", labTestResult.LabTestsConductedId);
                return View(labTestResult);
            }
            else
            {
                return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
            }
        }

        // POST: LabTestResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,LabTestsConductedId,Result,CreatedDate")] LabTestResult labTestResult)
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(labTestResult).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewBag.LabTestsConductedId = new SelectList(db.LabTestsConducteds, "Id", "PatientName", labTestResult.LabTestsConductedId);
                return View(labTestResult);
            }
            else
            {
                return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
            }
        }

        // GET: LabTestResults/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LabTestResult labTestResult = await db.LabTestResults.FindAsync(id);
                if (labTestResult == null)
                {
                    return HttpNotFound();
                }
                return View(labTestResult);
            }
            else
            {
                return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
            }
        }

        // POST: LabTestResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                LabTestResult labTestResult = await db.LabTestResults.FindAsync(id);
                db.LabTestResults.Remove(labTestResult);
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
