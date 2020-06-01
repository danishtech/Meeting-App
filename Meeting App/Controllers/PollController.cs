using System;
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
    public class PollController : ApiController
    {
        private Virtual_StudyEntities db = new Virtual_StudyEntities();

        // GET: api/Poll
        public IQueryable<Poll> GetPolls()
        {
            return db.Polls;
        }

        // GET: api/Poll/5
        [ResponseType(typeof(Poll))]
        public IHttpActionResult GetPoll(int id)
        {
            Poll poll = db.Polls.Find(id);
            if (poll == null)
            {
                return NotFound();
            }

            return Ok(poll);
        }

        // PUT: api/Poll/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPoll(int id, Poll poll)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != poll.PollID)
            {
                return BadRequest();
            }

            db.Entry(poll).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollExists(id))
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

        // POST: api/Poll
        [ResponseType(typeof(Poll))]
        public IHttpActionResult PostPoll(Poll poll)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Polls.Add(poll);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = poll.PollID }, poll);
        }

        // DELETE: api/Poll/5
        [ResponseType(typeof(Poll))]
        public IHttpActionResult DeletePoll(int id)
        {
            Poll poll = db.Polls.Find(id);
            if (poll == null)
            {
                return NotFound();
            }

            db.Polls.Remove(poll);
            db.SaveChanges();

            return Ok(poll);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PollExists(int id)
        {
            return db.Polls.Count(e => e.PollID == id) > 0;
        }
    }
}