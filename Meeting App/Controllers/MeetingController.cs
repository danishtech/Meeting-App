using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Meeting_App.Models;

namespace Meeting_App.Controllers
{
    public class MeetingController : ApiController
    {
        private Meeting_AppEntities db = new Meeting_AppEntities();

        [HttpGet]
        [Route("api/Meeting/Search")]
        public IQueryable<Meeting> Search(string searchString)
        {
            var meetings = from m in db.Meetings
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                meetings = meetings.Where(s => s.Meeting_Subject.Contains(searchString));
            }

            return meetings;
        }


        [HttpGet]
        [Route("api/Meeting/SearchFilter")]
        public IQueryable<Meeting> SearchFilter(string project, string createdby, int meetingStatus)
        {
            var meetings = from m in db.Meetings
                           select m;

            if (!String.IsNullOrEmpty(project))
            {
                meetings = meetings.Where(s => s.project_Name.Contains(project));
            }
            if (!String.IsNullOrEmpty(createdby))
            {
                meetings = meetings.Where(s => s.HostUser.Equals(createdby.Trim().ToLower()));
            }
            if (meetingStatus > -1 && meetingStatus < 3)
            {
                meetings = meetings.Where(s => s.Status == meetingStatus);
            }

            return meetings;
        }

        // GET: api/Meeting
        public IQueryable<Meeting> GetMeetings()
        {
            return db.Meetings;
        }

        // GET: api/Meeting/5
        [ResponseType(typeof(Meeting))]
        public IHttpActionResult GetMeeting(int id)
        {
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }

            return Ok(meeting);
        }

        // PUT: api/Meeting/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMeeting(int id, Meeting meeting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meeting.MeetingID)
            {
                return BadRequest();
            }

            db.Entry(meeting).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetingExists(id))
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

        // POST: api/Meeting
        [ResponseType(typeof(Meeting))]
        public IHttpActionResult PostMeeting(Meeting meeting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Meetings.Add(meeting);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = meeting.MeetingID }, meeting);
        }

        // DELETE: api/Meeting/5
        [ResponseType(typeof(Meeting))]
        public IHttpActionResult DeleteMeeting(int id)
        {
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }

            db.Meetings.Remove(meeting);
            db.SaveChanges();

            return Ok(meeting);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MeetingExists(int id)
        {
            return db.Meetings.Count(e => e.MeetingID == id) > 0;
        }
    }
}