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
    public class PollOptionController : ApiController
    {
        private Meeting_AppEntities db = new Meeting_AppEntities();

        // GET: api/PollOption
        public IQueryable<PollOption> GetPollOptions()
        {
            return db.PollOptions;
        }

        // GET: api/PollOption/5
        [ResponseType(typeof(PollOption))]
        public IHttpActionResult GetPollOption(int id)
        {
            PollOption pollOption = db.PollOptions.Find(id);
            if (pollOption == null)
            {
                return NotFound();
            }

            return Ok(pollOption);
        }

        // PUT: api/PollOption/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPollOption(int id, PollOption pollOption)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pollOption.PollOptionID)
            {
                return BadRequest();
            }

            db.Entry(pollOption).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollOptionExists(id))
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

        // POST: api/PollOption
        [ResponseType(typeof(PollOption))]
        public IHttpActionResult PostPollOption(PollOption pollOption)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PollOptions.Add(pollOption);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pollOption.PollOptionID }, pollOption);
        }

        // DELETE: api/PollOption/5
        [ResponseType(typeof(PollOption))]
        public IHttpActionResult DeletePollOption(int id)
        {
            PollOption pollOption = db.PollOptions.Find(id);
            if (pollOption == null)
            {
                return NotFound();
            }

            db.PollOptions.Remove(pollOption);
            db.SaveChanges();

            return Ok(pollOption);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PollOptionExists(int id)
        {
            return db.PollOptions.Count(e => e.PollOptionID == id) > 0;
        }
    }
}