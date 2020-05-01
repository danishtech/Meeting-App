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
    public class CommentMVCController : Controller
    {
        private Meeting_AppEntities db = new Meeting_AppEntities();

        // GET: CommentMVC
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Action_Item).Include(c => c.Decision_Item).Include(c => c.Meeting);
            return View(comments.ToList());
        }

        // GET: CommentMVC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: CommentMVC/Create
        public ActionResult Create()
        {
            ViewBag.ActionID = new SelectList(db.Action_Items, "ActionItemID", "ActionItem_Title");
            ViewBag.DecisionID = new SelectList(db.Decision_Items, "DecisionItemID", "DecisionItem_Title");
            ViewBag.MeetingID = new SelectList(db.Meetings, "MeetingID", "project_Name");
            return View();
        }

        // POST: CommentMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentID,project_Name,Comment1,CommentDate,CommentTime,Status,HostUser,MeetingID,ActionID,DecisionID")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActionID = new SelectList(db.Action_Items, "ActionItemID", "ActionItem_Title", comment.ActionID);
            ViewBag.DecisionID = new SelectList(db.Decision_Items, "DecisionItemID", "DecisionItem_Title", comment.DecisionID);
            ViewBag.MeetingID = new SelectList(db.Meetings, "MeetingID", "project_Name", comment.MeetingID);
            return View(comment);
        }

        // GET: CommentMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActionID = new SelectList(db.Action_Items, "ActionItemID", "ActionItem_Title", comment.ActionID);
            ViewBag.DecisionID = new SelectList(db.Decision_Items, "DecisionItemID", "DecisionItem_Title", comment.DecisionID);
            ViewBag.MeetingID = new SelectList(db.Meetings, "MeetingID", "project_Name", comment.MeetingID);
            return View(comment);
        }

        // POST: CommentMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentID,project_Name,Comment1,CommentDate,CommentTime,Status,HostUser,MeetingID,ActionID,DecisionID")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActionID = new SelectList(db.Action_Items, "ActionItemID", "ActionItem_Title", comment.ActionID);
            ViewBag.DecisionID = new SelectList(db.Decision_Items, "DecisionItemID", "DecisionItem_Title", comment.DecisionID);
            ViewBag.MeetingID = new SelectList(db.Meetings, "MeetingID", "project_Name", comment.MeetingID);
            return View(comment);
        }

        // GET: CommentMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: CommentMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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
