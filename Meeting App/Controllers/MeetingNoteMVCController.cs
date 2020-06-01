using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Meeting_App.Models;

namespace Meeting_App.Controllers
{
    public class MeetingNoteMVCController : Controller
    {
        private Virtual_StudyEntities db = new Virtual_StudyEntities();

        // GET: MeetingNoteMVC
        public ActionResult Index()
        {
            var meeting_Notes = db.Meeting_Notes.Include(m => m.Meeting);
            return View(meeting_Notes.ToList());
        }

        // GET: MeetingNoteMVC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting_Note meeting_Note = db.Meeting_Notes.Find(id);
            if (meeting_Note == null)
            {
                return HttpNotFound();
            }
            return View(meeting_Note);
        }

        // GET: MeetingNoteMVC/Create
        public ActionResult Create()
        {
            ViewBag.MeetingID = new SelectList(db.Meetings, "MeetingID", "project_Name");
            return View();
        }

        // POST: MeetingNoteMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MeetingNotesID,MeetingNotes,MeetingNotes_Description,project_Name,CreatedDate,Status,LoginName,MeetingID")] Meeting_Note meeting_Note)
        {
            if (ModelState.IsValid)
            {
                db.Meeting_Notes.Add(meeting_Note);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MeetingID = new SelectList(db.Meetings, "MeetingID", "project_Name", meeting_Note.MeetingID);
            return View(meeting_Note);
        }

        // GET: MeetingNoteMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting_Note meeting_Note = db.Meeting_Notes.Find(id);
            if (meeting_Note == null)
            {
                return HttpNotFound();
            }
            ViewBag.MeetingID = new SelectList(db.Meetings, "MeetingID", "project_Name", meeting_Note.MeetingID);
            return View(meeting_Note);
        }

        // POST: MeetingNoteMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MeetingNotesID,MeetingNotes,MeetingNotes_Description,project_Name,CreatedDate,Status,LoginName,MeetingID")] Meeting_Note meeting_Note)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meeting_Note).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MeetingID = new SelectList(db.Meetings, "MeetingID", "project_Name", meeting_Note.MeetingID);
            return View(meeting_Note);
        }

        // GET: MeetingNoteMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting_Note meeting_Note = db.Meeting_Notes.Find(id);
            if (meeting_Note == null)
            {
                return HttpNotFound();
            }
            return View(meeting_Note);
        }

        // POST: MeetingNoteMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meeting_Note meeting_Note = db.Meeting_Notes.Find(id);
            db.Meeting_Notes.Remove(meeting_Note);
            db.SaveChanges();
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
