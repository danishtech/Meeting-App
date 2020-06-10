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
    public class AttendanceMVCController : Controller
    {
        private Virtual_StudyEntities db = new Virtual_StudyEntities();

        // GET: AttendanceMVC
        public ActionResult Index()
        {
            var attendances = db.Attendances.Include(a => a.AppUser).Include(a => a.Meeting);
            return View(attendances.ToList());
        }

        // GET: AttendanceMVC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // GET: AttendanceMVC/Create
        public ActionResult Create()
        {
            ViewBag.AppUserID = new SelectList(db.AppUsers, "AppUserID", "FirstName");
            ViewBag.MeetingID = new SelectList(db.Meetings, "MeetingID", "project_Name");
            return View();
        }

        // POST: AttendanceMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AttendanceID,AppUserID,MeetingID,AttendanceDate,IsActive")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AppUserID = new SelectList(db.AppUsers, "AppUserID", "FirstName", attendance.AppUserID);
            ViewBag.MeetingID = new SelectList(db.Meetings, "MeetingID", "project_Name", attendance.MeetingID);
            return View(attendance);
        }

        // GET: AttendanceMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.AppUserID = new SelectList(db.AppUsers, "AppUserID", "FirstName", attendance.AppUserID);
            ViewBag.MeetingID = new SelectList(db.Meetings, "MeetingID", "project_Name", attendance.MeetingID);
            return View(attendance);
        }

        // POST: AttendanceMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AttendanceID,AppUserID,MeetingID,AttendanceDate,IsActive")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AppUserID = new SelectList(db.AppUsers, "AppUserID", "FirstName", attendance.AppUserID);
            ViewBag.MeetingID = new SelectList(db.Meetings, "MeetingID", "project_Name", attendance.MeetingID);
            return View(attendance);
        }

        // GET: AttendanceMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: AttendanceMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendance attendance = db.Attendances.Find(id);
            db.Attendances.Remove(attendance);
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
