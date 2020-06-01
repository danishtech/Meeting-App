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
    public class PollOptionMVCController : Controller
    {
        private Virtual_StudyEntities db = new Virtual_StudyEntities();

        // GET: PollOptionMVC
        public ActionResult Index()
        {
            var pollOptions = db.PollOptions.Include(p => p.Poll);
            return View(pollOptions.ToList());
        }

        // GET: PollOptionMVC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PollOption pollOption = db.PollOptions.Find(id);
            if (pollOption == null)
            {
                return HttpNotFound();
            }
            return View(pollOption);
        }

        // GET: PollOptionMVC/Create
        public ActionResult Create()
        {
            ViewBag.PollID = new SelectList(db.Polls, "PollID", "Question");
            return View();
        }

        // POST: PollOptionMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PollOptionID,PollID,Answers,Vote")] PollOption pollOption)
        {
            if (ModelState.IsValid)
            {
                db.PollOptions.Add(pollOption);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PollID = new SelectList(db.Polls, "PollID", "Question", pollOption.PollID);
            return View(pollOption);
        }

        // GET: PollOptionMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PollOption pollOption = db.PollOptions.Find(id);
            if (pollOption == null)
            {
                return HttpNotFound();
            }
            ViewBag.PollID = new SelectList(db.Polls, "PollID", "Question", pollOption.PollID);
            return View(pollOption);
        }

        // POST: PollOptionMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PollOptionID,PollID,Answers,Vote")] PollOption pollOption)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pollOption).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PollID = new SelectList(db.Polls, "PollID", "Question", pollOption.PollID);
            return View(pollOption);
        }

        // GET: PollOptionMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PollOption pollOption = db.PollOptions.Find(id);
            if (pollOption == null)
            {
                return HttpNotFound();
            }
            return View(pollOption);
        }

        // POST: PollOptionMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PollOption pollOption = db.PollOptions.Find(id);
            db.PollOptions.Remove(pollOption);
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
