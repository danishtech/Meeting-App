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
    public class ActionController : ApiController
    {
        private Meeting_AppEntities db = new Meeting_AppEntities();
        [HttpGet]
        [Route("api/Action/Search")]
        public IQueryable<Action_Item> Search(string searchString)
        {
            var Actions = from m in db.Action_Items
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                Actions = Actions.Where(s => s.ActionItem_Title.Contains(searchString));
            }

            return Actions;
        }

        // GET: api/Action
        public IQueryable<Action_Item> GetAction_Items()
        {
            return db.Action_Items;
        }

        // GET: api/Action/5
        [ResponseType(typeof(Action_Item))]
        public IHttpActionResult GetAction_Item(int id)
        {
            Action_Item action_Item = db.Action_Items.Find(id);
            if (action_Item == null)
            {
                return NotFound();
            }

            return Ok(action_Item);
        }

        // PUT: api/Action/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAction_Item(int id, Action_Item action_Item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != action_Item.ActionItemID)
            {
                return BadRequest();
            }

            db.Entry(action_Item).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Action_ItemExists(id))
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

        // POST: api/Action
        [ResponseType(typeof(Action_Item))]
        public IHttpActionResult PostAction_Item(Action_Item action_Item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Action_Items.Add(action_Item);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = action_Item.ActionItemID }, action_Item);
        }

        // DELETE: api/Action/5
        [ResponseType(typeof(Action_Item))]
        public IHttpActionResult DeleteAction_Item(int id)
        {
            Action_Item action_Item = db.Action_Items.Find(id);
            if (action_Item == null)
            {
                return NotFound();
            }

            db.Action_Items.Remove(action_Item);
            db.SaveChanges();

            return Ok(action_Item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Action_ItemExists(int id)
        {
            return db.Action_Items.Count(e => e.ActionItemID == id) > 0;
        }
    }
}