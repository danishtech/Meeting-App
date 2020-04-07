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
    public class ActionController : ApiController
    {
        private Meeting_AppEntities db = new Meeting_AppEntities();

        // GET: api/Action
        [HttpGet]
        public IQueryable<Action_Item> GetAction_Items()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Action_Items;
        }

        // GET: api/Action/5
        [HttpGet]
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
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PUT(int id, Action_Item action_Item)
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
        [HttpPost]
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
       // [DELETE("MM/Action"), HttpPut],HttpDelete]
        public IHttpActionResult Delete(int id)
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