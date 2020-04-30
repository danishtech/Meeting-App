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
    public class DecisionMVCController : Controller
    {
        private Meeting_AppEntities db = new Meeting_AppEntities();

        // GET: DecisionMVC
        public ActionResult Index()
        {
            var decision_Items = db.Decision_Items.Include(d => d.Meeting).Include(d => d.Comment);
            return View(decision_Items.ToList());
        }

        // GET: DecisionMVC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Decision_Item decision_Item = db.Decision_Items.Find(id);
            if (decision_Item == null)
            {
                return HttpNotFound();
            }
            return View(decision_Item);
        }

        // GET: DecisionMVC/Create
        public ActionResult Create()
        {
            ViewBag.MeetingID = new SelectList(db.Meetings, "MeetingID", "project_Name");
            ViewBag.CommentID = new SelectList(db.Comments, "CommentID", "project_Name");
            return View();
        }

        // POST: DecisionMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DecisionItemID,DecisionItem_Title,project_Name,Description,DecisionDate,DecisionTime,DecisionAssignedTo,Priority,Status,MeetingID,CommentID")] Decision_Item decision_Item)
        {
            if (ModelState.IsValid)
            {
                db.Decision_Items.Add(decision_Item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MeetingID = new SelectList(db.Meetings, "MeetingID", "project_Name", decision_Item.MeetingID);
            ViewBag.CommentID = new SelectList(db.Comments, "CommentID", "project_Name", decision_Item.CommentID);
            return View(decision_Item);
        }

        // GET: DecisionMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Decision_Item decision_Item = db.Decision_Items.Find(id);
            if (decision_Item == null)
            {
                return HttpNotFound();
            }
            ViewBag.MeetingID = new SelectList(db.Meetings, "MeetingID", "project_Name", decision_Item.MeetingID);
            ViewBag.CommentID = new SelectList(db.Comments, "CommentID", "project_Name", decision_Item.CommentID);
            return View(decision_Item);
        }

        // POST: DecisionMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DecisionItemID,DecisionItem_Title,project_Name,Description,DecisionDate,DecisionTime,DecisionAssignedTo,Priority,Status,MeetingID,CommentID")] Decision_Item decision_Item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(decision_Item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MeetingID = new SelectList(db.Meetings, "MeetingID", "project_Name", decision_Item.MeetingID);
            ViewBag.CommentID = new SelectList(db.Comments, "CommentID", "project_Name", decision_Item.CommentID);
            return View(decision_Item);
        }

        // GET: DecisionMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Decision_Item decision_Item = db.Decision_Items.Find(id);
            if (decision_Item == null)
            {
                return HttpNotFound();
            }
            return View(decision_Item);
        }

        // POST: DecisionMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Decision_Item decision_Item = db.Decision_Items.Find(id);
            db.Decision_Items.Remove(decision_Item);
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
