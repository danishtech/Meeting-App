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
    public class Decision_ItemController : Controller
    {
        private Virtual_StudyEntities db = new Virtual_StudyEntities();

        // GET: Decision_Item
        public ActionResult Index()
        {
            var decision_Items = db.Decision_Items.Include(d => d.Meeting);
            return View(decision_Items.ToList());
        }

        // GET: Decision_Item/Details/5
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

        // GET: Decision_Item/Create
        public ActionResult Create()
        {
            ViewBag.MeetingID = new SelectList(db.Meetings, "MeetingID", "project_Name");
            return View();
        }

        // POST: Decision_Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DecisionItemID,DecisionItem_Title,project_Name,Description,DecisionDate,DecisionTime,DecisionAssignedTo,Priority,Status,MeetingID")] Decision_Item decision_Item)
        {
            if (ModelState.IsValid)
            {
                db.Decision_Items.Add(decision_Item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MeetingID = new SelectList(db.Meetings, "MeetingID", "project_Name", decision_Item.MeetingID);
            return View(decision_Item);
        }

        // GET: Decision_Item/Edit/5
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
            return View(decision_Item);
        }

        // POST: Decision_Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DecisionItemID,DecisionItem_Title,project_Name,Description,DecisionDate,DecisionTime,DecisionAssignedTo,Priority,Status,MeetingID")] Decision_Item decision_Item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(decision_Item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MeetingID = new SelectList(db.Meetings, "MeetingID", "project_Name", decision_Item.MeetingID);
            return View(decision_Item);
        }

        // GET: Decision_Item/Delete/5
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

        // POST: Decision_Item/Delete/5
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
