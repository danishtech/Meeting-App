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
    public class UserController : ApiController
    {
        private Meeting_AppEntities db = new Meeting_AppEntities();

        // GET: api/User
        public IQueryable<AppUser> GetAppUsers()
        {
            return db.AppUsers;
        }
        //[HttpGet]
        //[Route("api/UserExits/{email}")]
        //public bool IsUserExits(AppUser user)
        //{
        //    bool userAlreadyExists = db.AppUsers.Any(x => x.Email == user.Email);
        //    return userAlreadyExists;
        //}
        // GET: api/User/5
       // [ResponseType(typeof(AppUser))]
        [HttpGet]
       [Route("api/User/UserExists")]
        public bool UserExists(string id)
        {
            // string email = Convert.ToString(id);
            bool userAlreadyExists = db.AppUsers.Any(x => x.LoginName == id);            

            return userAlreadyExists;
        }

        [HttpGet]
        [Route("api/User/UserAuthnticate")]
        public bool UserExists(string Loginname, string password)
        {
            // string email = Convert.ToString(id);
            bool userExists = db.AppUsers.Any(x => x.LoginName == Loginname && x.Password==password);

            return userExists; 
        }
        // GET: api/User/5
        [ResponseType(typeof(AppUser))]
        [Route("api/User/{id}")]
        public IHttpActionResult GetAppUser(int id)
        {
            AppUser appUser = db.AppUsers.Find(id);
            if (appUser == null)
            {
                return NotFound();
            }

            return Ok(appUser);
        }

        // PUT: api/User/5
        [Route("api/User/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAppUser(int id, AppUser appUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appUser.AppUserID)
            {
                return BadRequest();
            }

            db.Entry(appUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserExists(id))
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

        // POST: api/User
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult PostAppUser(AppUser appUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AppUsers.Add(appUser);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = appUser.AppUserID }, appUser);
        }

        [Route("api/User/{id}")]
        // DELETE: api/User/5
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult DeleteAppUser(int id)
        {
            AppUser appUser = db.AppUsers.Find(id);
            if (appUser == null)
            {
                return NotFound();
            }

            db.AppUsers.Remove(appUser);
            db.SaveChanges();

            return Ok(appUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppUserExists(int id)
        {
            return db.AppUsers.Count(e => e.AppUserID == id) > 0;
        }
    }
}