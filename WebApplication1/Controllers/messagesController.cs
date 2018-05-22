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
    public class messagesController : Controller
    {
        private NSHNContext db = new NSHNContext();

        // GET: messages
        public async Task<ActionResult> Index()
        {

            try
            {
                if (Session["role"] != null && Session["role"].ToString() == "ADM")
                {
                    var messages = db.messages.Where(m => m.reply_id == null);
                    return View(await messages.ToListAsync());
                }
                else
                {
                    return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
                }
            }
            catch (Exception e)
            {
                ViewBag.GenericException = e.Message;
            }
            return View("~/Views/Navigate/Errors.cshtml");
        }

        // GET: messages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (Session["role"].ToString() == "ADM")
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    message message = await db.messages.FindAsync(id);
                    if (message == null)
                    {
                        return HttpNotFound();
                    }
                    return View(message);
                }
                else
                {
                    return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
                }
            }
            catch (Exception e)
            {
                ViewBag.GenericException = e.Message;
            }
            return View("~/Views/Navigate/Errors.cshtml");
        }

        // GET: messages/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.reply_id = new SelectList(db.messages, "Id", "first_name");
                return View();
            }
            catch (Exception e)
            {
                ViewBag.GenericException = e.Message;
            }
            return View("~/Views/Navigate/Errors.cshtml");
        }

        public async Task<ActionResult> Reply(int? id)
        {
            try
            {
                if (Session["role"].ToString() == "ADM")
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    message message = await db.messages.FindAsync(id);
                    if (message == null)
                    {
                        return HttpNotFound();
                    }
                    return View(message);
                }
                else
                {
                    return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
                }

            }
            catch (Exception e)
            {
                ViewBag.GenericException = e.Message;
            }
            return View("~/Views/Navigate/Errors.cshtml");
        }

        // POST: messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "first_name,last_name,email_address,message_body")] message message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    message.sent_date = System.DateTime.Now;
                    db.messages.Add(message);
                    await db.SaveChangesAsync();
                    ViewBag.MessageSent = "We have received your message. You will be contacted as soon as possible.";
                }

                ViewBag.reply_id = new SelectList(db.messages, "Id", "first_name", message.reply_id);
                return View(message);
            }
            catch (Exception e)
            {
                ViewBag.GenericException = e.Message;
            }
            return View("~/Views/Navigate/Errors.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Reply([Bind(Include = "first_name,last_name,email_address,message_body,reply_id")] message message)
        {
            try
            {
                if (Session["role"].ToString() == "ADM")
                {
                    if (ModelState.IsValid)
                    {
                   
                        message.sent_date = System.DateTime.Now;
                        db.messages.Add(message);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    return View(message);

                }
                else
                {
                    return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
                }
            }
            catch (Exception e)
            {
                ViewBag.GenericException = e.Message;
            }
            return View("~/Views/Navigate/Errors.cshtml");
        }

        // GET: messages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                message message = await db.messages.FindAsync(id);
                if (message == null)
                {
                    return HttpNotFound();
                }
                ViewBag.reply_id = new SelectList(db.messages, "Id", "first_name", message.reply_id);
                return View(message);
            }
            catch (Exception e)
            {
                ViewBag.GenericException = e.Message;
            }
            return View("~/Views/Navigate/Errors.cshtml");
        }

        // POST: messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,first_name,last_name,email_address,message_body,sent_date,reply_id")] message message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(message).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewBag.reply_id = new SelectList(db.messages, "Id", "first_name", message.reply_id);
                return View(message);
            }
            catch (Exception e)
            {
                ViewBag.GenericException = e.Message;
            }
            return View("~/Views/Navigate/Errors.cshtml");
        }

        // GET: messages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (Session["role"].ToString() == "ADM")
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    message message = await db.messages.FindAsync(id);
                    if (message == null)
                    {
                        return HttpNotFound();
                    }
                    return View(message);
                }
                else
                {
                    return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
                }
            }
            catch (Exception e)
            {
                ViewBag.GenericException = e.Message;
            }
            return View("~/Views/Navigate/Errors.cshtml");
        }

        // POST: messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (Session["role"].ToString() == "ADM")
                {
                    message message = await db.messages.FindAsync(id);
                    db.messages.Remove(message);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("~/Views/LabTestResults/NotLoggedIn.cshtml");
                }
            }
            catch (Exception e)
            {
                ViewBag.GenericException = e.Message;
            }
            return View("~/Views/Navigate/Errors.cshtml");
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
