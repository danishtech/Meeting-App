﻿using System;
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
    public class MeetingMVCController : Controller
    {
        private Meeting_AppEntities db = new Meeting_AppEntities();

        // GET: MeetingMVC
        public ActionResult Index()
        {
            var meetings = db.Meetings.Include(m => m.Comment);
            return View(meetings.ToList());
        }

        // GET: MeetingMVC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        // GET: MeetingMVC/Create
        public ActionResult Create()
        {
            ViewBag.CommentID = new SelectList(db.Comments, "CommentID", "project_Name");
            return View();
        }

        // POST: MeetingMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MeetingID,project_Name,Meeting_Subject,Meeting_objective,Agenda,Agenda_SubItem,MeetingDate,MeetingTime,MeetingAssignedTo,reoccrence,Meeting_Location,Partipatents,Share_Link,Status,HostUser,Conclusion,CommentID")] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                db.Meetings.Add(meeting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CommentID = new SelectList(db.Comments, "CommentID", "project_Name", meeting.CommentID);
            return View(meeting);
        }

        // GET: MeetingMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            ViewBag.CommentID = new SelectList(db.Comments, "CommentID", "project_Name", meeting.CommentID);
            return View(meeting);
        }

        // POST: MeetingMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MeetingID,project_Name,Meeting_Subject,Meeting_objective,Agenda,Agenda_SubItem,MeetingDate,MeetingTime,MeetingAssignedTo,reoccrence,Meeting_Location,Partipatents,Share_Link,Status,HostUser,Conclusion,CommentID")] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meeting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CommentID = new SelectList(db.Comments, "CommentID", "project_Name", meeting.CommentID);
            return View(meeting);
        }

        // GET: MeetingMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        // POST: MeetingMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meeting meeting = db.Meetings.Find(id);
            db.Meetings.Remove(meeting);
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
