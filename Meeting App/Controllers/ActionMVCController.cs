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
    public class ActionMVCController : Controller
    {
        private Meeting_AppEntities db = new Meeting_AppEntities();

        // GET: ActionMVC
        public ActionResult Index()
        {
            return View(db.Action_Items.ToList());
        }

        // GET: ActionMVC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Action_Item action_Item = db.Action_Items.Find(id);
            if (action_Item == null)
            {
                return HttpNotFound();
            }
            return View(action_Item);
        }

        // GET: ActionMVC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActionMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActionItemID,ActionItem_Title,project_Name,ActionDate,ActionTime,ActionAssignedTo")] Action_Item action_Item)
        {
            if (ModelState.IsValid)
            {
                db.Action_Items.Add(action_Item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(action_Item);
        }

        // GET: ActionMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Action_Item action_Item = db.Action_Items.Find(id);
            if (action_Item == null)
            {
                return HttpNotFound();
            }
            return View(action_Item);
        }

        // POST: ActionMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActionItemID,ActionItem_Title,project_Name,ActionDate,ActionTime,ActionAssignedTo")] Action_Item action_Item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(action_Item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(action_Item);
        }

        // GET: ActionMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Action_Item action_Item = db.Action_Items.Find(id);
            if (action_Item == null)
            {
                return HttpNotFound();
            }
            return View(action_Item);
        }

        // POST: ActionMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Action_Item action_Item = db.Action_Items.Find(id);
            db.Action_Items.Remove(action_Item);
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
