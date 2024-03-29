﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Meeting_App.Models;

namespace Meeting_App.Controllers
{
    public class DecisionController : ApiController
    {
        private Virtual_StudyEntities db = new Virtual_StudyEntities();
        [HttpGet]
        [Route("api/Decision/Search")]
        public IQueryable<Decision_Item> Search(string searchString)
        {
            var Decision = from m in db.Decision_Items
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                Decision = Decision.Where(s => s.DecisionItem_Title.Contains(searchString));
            }

            return Decision;
        }

        [HttpGet]
        [Route("api/Decision/SearchFilter")]
        public IQueryable<Decision_Item> SearchFilter(string project, string createdby, int Status, string Priority)
        {
            var Decisions = from m in db.Decision_Items
                          select m;
            //var Actions = (from p in db.Meetings
            //               join e in db.Action_Items
            //               on p.MeetingID equals e.MeetingID

            //               select new
            //               {

            //                   ProjectName = e.project_Name,
            //                   HostUser = p.HostUser,
            //                   Priority = e.Priority,
            //                   Status = e.Status,
            //                   due = e.ActionDate
            //               });

            if (!String.IsNullOrEmpty(project))
            {
                Decisions = Decisions.Where(s => s.project_Name.Contains(project));
            }
            if (!String.IsNullOrEmpty(createdby))
            {
                Decisions = Decisions.Where(s => s.Meeting.HostUser.Equals(createdby.Trim().ToLower()));
            }
            if (Status > -1 && Status < 2)
            {
                Decisions = Decisions.Where(s => s.Status == Status);
            }
            if (!String.IsNullOrEmpty(Priority))
            {
                Decisions = Decisions.Where(s => s.Priority.Equals(Priority.Trim().ToLower()));
            }
            //if (due != null)
            //{
            //    Decisions = Decisions.Where(s => s.DecisionDate.Value.Equals(due));
            //    //DateTime.Compare(x.price_date.Value.Date, dt.Date) == 0)
            //}
            return Decisions;
        }
        // GET: api/Decision
        public IQueryable<Decision_Item> GetDecision_Items()
        {
            return db.Decision_Items;
        }

        // GET: api/Decision/5
        [ResponseType(typeof(Decision_Item))]
        public IHttpActionResult GetDecision_Item(int id)
        {
            Decision_Item decision_Item = db.Decision_Items.Find(id);
            if (decision_Item == null)
            {
                return NotFound();
            }

            return Ok(decision_Item);
        }

        // PUT: api/Decision/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDecision_Item(int id, Decision_Item decision_Item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != decision_Item.DecisionItemID)
            {
                return BadRequest();
            }

            db.Entry(decision_Item).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Decision_ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Decision
        [ResponseType(typeof(Decision_Item))]
        public IHttpActionResult PostDecision_Item(Decision_Item decision_Item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Decision_Items.Add(decision_Item);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = decision_Item.DecisionItemID }, decision_Item);
        }

        // DELETE: api/Decision/5
        [ResponseType(typeof(Decision_Item))]
        public IHttpActionResult DeleteDecision_Item(int id)
        {
            Decision_Item decision_Item = db.Decision_Items.Find(id);
            if (decision_Item == null)
            {
                return NotFound();
            }

            db.Decision_Items.Remove(decision_Item);
            db.SaveChanges();

            return Ok(decision_Item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Decision_ItemExists(int id)
        {
            return db.Decision_Items.Count(e => e.DecisionItemID == id) > 0;
        }
    }
}