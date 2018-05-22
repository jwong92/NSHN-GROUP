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
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;

namespace WebApplication1.Controllers
{
    public class departmentsController : Controller
    {
        private NSHNContext db = new NSHNContext();

        // GET: departments
        public async Task<ActionResult> Index(string searchDepartment)
        {
            //var departments = db.departments.Include(d => d.site);
            var departments = from d in db.departments select d;
            if (!String.IsNullOrEmpty(searchDepartment))
            {
               
                departments = departments.Where(d => d.dept_name.ToUpper().Contains(searchDepartment.ToUpper())||
                d.site.name.ToUpper().Contains(searchDepartment.ToUpper()));
            }
            
            return View(await departments.ToListAsync());
        }

        // GET: departments/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (Session["role"] != null)
                {
                    if (Session["role"].ToString() == "ADM")
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }

                        department department = db.departments.Find(id);
                        if (department == null)
                        {
                            return HttpNotFound();
                        }
                        return View(department);
                    }
                }
                else
                {
                    return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
                }
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // GET: departments/Create
        public ActionResult Create()
        {
            try
            {
                if (Session["role"] != null)
                {
                    if (Session["role"].ToString() == "ADM")
                    {

                        ViewBag.site_id = new SelectList(db.sites, "id", "name");
                        return View();
                    }
                }
                else
                {
                    return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
                }
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
        // POST: departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "dept_id,dept_name,dept_description,dept_extension,site_id")] department department)
        {
            try
            {
                if (Session["role"] != null)
                {
                    if (Session["role"].ToString() == "ADM" || Session["role"].ToString() == "PAT")
                    {
                        if (ModelState.IsValid)
                        {
                            department.date_added = System.DateTime.Now;
                            db.departments.Add(department);
                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }

                        ViewBag.site_id = new SelectList(db.sites, "id", "name", department.site_id);
                        return View(department);
                    }
                }
                else
                {
                    return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
                }
            }
            catch (DbUpdateException e)
            {
                ViewBag.DbExceptionMessage = e.Message;
            }
            catch (SqlException e)
            {
                ViewBag.SqlExceptionMessage = e.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }

        // GET: departments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (Session["role"] != null)
                {
                    if (Session["role"].ToString() == "ADM")
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        department department = await db.departments.FindAsync(id);
                        if (department == null)
                        {
                            return HttpNotFound();
                        }
                        ViewBag.site_id = new SelectList(db.sites, "id", "name", department.site_id);
                        return View(department);
                    }
                }
                else
                {
                    return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
                }
            }
            catch (DbUpdateException e)
            {
                ViewBag.DbExceptionMessage = e.Message;
            }
            catch (SqlException e)
            {
                ViewBag.SqlExceptionMessage = e.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // POST: departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "dept_id,dept_name,dept_description,dept_extension,site_id")] department department)
        {
            try
            {
                if (Session["role"] != null)
                {
                    if (Session["role"].ToString() == "ADM")
                    {
                        if (ModelState.IsValid)
                        {
                            db.Entry(department).State = EntityState.Modified;
                            department.date_added = DateTime.Now;
                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        ViewBag.site_id = new SelectList(db.sites, "id", "name", department.site_id);
                        return View(department);
                    }
                }
                else
                {

                    return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
                }
            }
            catch (DbUpdateException e)
            {
                ViewBag.DbExceptionMessage = e.Message;
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlExceptionMessage = sqlException.Message;
            }
            catch (Exception exception)
            {
                ViewBag.genericException = exception.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // GET: departments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (Session["role"] != null)
                {
                    if (Session["role"].ToString() == "ADM")
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        department department = await db.departments.FindAsync(id);
                        if (department == null)
                        {
                            return HttpNotFound();
                        }
                        return View(department);
                    }
                }
                else
                {
                    return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
                }
            }
            catch (DbUpdateException e)
            {
                ViewBag.DbExceptionMessage = e.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return RedirectToAction("Details", "Errors");
        }

        // POST: departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (Session["role"] != null)
                {
                    if (Session["role"].ToString() == "ADM")
                    {
                        department department = await db.departments.FindAsync(id);
                        db.departments.Remove(department);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
                }
            }
            catch (DbUpdateException e)
            {
                ViewBag.DbExceptionMessage = e.Message;
            }
            catch (SqlException sqlException)
            {
                TempData["SqlException"] = sqlException.Message;
            }
            catch (Exception genericException)
            {
                TempData["SqlException"] = genericException.Message;
            }
            return RedirectToAction("Delete", new { id = id });
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
