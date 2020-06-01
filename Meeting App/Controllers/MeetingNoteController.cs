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
    public class MeetingNoteController : ApiController
    {
        private Virtual_StudyEntities db = new Virtual_StudyEntities();

        // GET: api/MeetingNote
        public IQueryable<Meeting_Note> GetMeeting_Notes()
        {
            return db.Meeting_Notes;
        }

        // GET: api/MeetingNote/5
        [ResponseType(typeof(Meeting_Note))]
        public IHttpActionResult GetMeeting_Note(int id)
        {
            Meeting_Note meeting_Note = db.Meeting_Notes.Find(id);
            if (meeting_Note == null)
            {
                return NotFound();
            }

            return Ok(meeting_Note);
        }

        // PUT: api/MeetingNote/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMeeting_Note(int id, Meeting_Note meeting_Note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meeting_Note.MeetingNotesID)
            {
                return BadRequest();
            }

            db.Entry(meeting_Note).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Meeting_NoteExists(id))
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

        // POST: api/MeetingNote
        [ResponseType(typeof(Meeting_Note))]
        public IHttpActionResult PostMeeting_Note(Meeting_Note meeting_Note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Meeting_Notes.Add(meeting_Note);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = meeting_Note.MeetingNotesID }, meeting_Note);
        }

        // DELETE: api/MeetingNote/5
        [ResponseType(typeof(Meeting_Note))]
        public IHttpActionResult DeleteMeeting_Note(int id)
        {
            Meeting_Note meeting_Note = db.Meeting_Notes.Find(id);
            if (meeting_Note == null)
            {
                return NotFound();
            }

            db.Meeting_Notes.Remove(meeting_Note);
            db.SaveChanges();

            return Ok(meeting_Note);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Meeting_NoteExists(int id)
        {
            return db.Meeting_Notes.Count(e => e.MeetingNotesID == id) > 0;
        }
    }
}