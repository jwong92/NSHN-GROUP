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
    public class AvailableTestsController : Controller
    {
        private NSHNContext db = new NSHNContext();

        // GET: AvailableTests
        public async Task<ActionResult> Index()
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                return View(await db.AvailableTests.ToListAsync());
            }
            else
            {
                return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
            }
        }

        // GET: AvailableTests/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AvailableTest availableTest = await db.AvailableTests.FindAsync(id);
                if (availableTest == null)
                {
                    return HttpNotFound();
                }
                return View(availableTest);
            }
            else
            {
                return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
            }
        }

        // GET: AvailableTests/Create
        public ActionResult Create()
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                return View();
            }
            else
            {
                return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
            }
        }

        // POST: AvailableTests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,TestName,CreatedDate")] AvailableTest availableTest)
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                if (ModelState.IsValid)
                {
                    db.AvailableTests.Add(availableTest);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(availableTest);
            }
            else
            {
                return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
            }
        }

        // GET: AvailableTests/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AvailableTest availableTest = await db.AvailableTests.FindAsync(id);
                if (availableTest == null)
                {
                    return HttpNotFound();
                }
                return View(availableTest);
            }
            else
            {
                return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
            }
        }

        // POST: AvailableTests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,TestName,CreatedDate")] AvailableTest availableTest)
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(availableTest).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(availableTest);
            }
            else
            {
                return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
            }
        }

        // GET: AvailableTests/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AvailableTest availableTest = await db.AvailableTests.FindAsync(id);
                if (availableTest == null)
                {
                    return HttpNotFound();
                }
                return View(availableTest);
            }
            else
            {
                return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
            }
        }

        // POST: AvailableTests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (Session["role"] != null && Session["role"].ToString() == "ADM")
            {
                AvailableTest availableTest = await db.AvailableTests.FindAsync(id);
                db.AvailableTests.Remove(availableTest);
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
