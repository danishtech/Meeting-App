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
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;


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
        [HttpPost]
        [Route("api/User/SendEmail")]
        public async Task SendEmail([FromBody]JObject objData)
        {
            var message = new MailMessage();

            string[] Multi = objData["toemail"].ToString().Split(';'); //spiliting input Email id string with comma(,)
            string[] Multiarray = objData["toname"].ToString().Split(';');
            message.From = new MailAddress("checkboxnoida@gmail.com");
            message.Bcc.Add(new MailAddress("checkboxnoida@gmail.com"));
            message.Subject = objData["subject"].ToString();
            
                //adding multi reciver's Email Id
                                                            
                //spiliting input Email id string with comma(,)
                foreach (string Multiname in Multiarray)
                {
                foreach (string Multiemailid in Multi)
                {
                    message.To.Add(new MailAddress(Multiemailid)); //  message.To.Add(new MailAddress(Multiname)); //adding multi reciver's Email Id
                    message.Body = createEmailBody(Multiname, objData["message"].ToString());
                    message.IsBodyHtml = true;
                    using (var smtp = new SmtpClient())
                    {
                        await smtp.SendMailAsync(message);
                        await Task.FromResult(0);
                    }
                
                }

            }
            
        }
        private string createEmailBody(string userName, string message)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/htmlTemplate.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            body = body.Replace("{message}", message);
            return body;
        }

        [HttpPost]
        [Route("api/User/ForgetPassword")]
        public async Task ForgetPassword([FromBody]JObject objData)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(objData["toname"].ToString() + " <" + objData["toemail"].ToString() + ">"));
            message.From = new MailAddress("danish.tech@gmail.com");
            message.Bcc.Add(new MailAddress("checkboxnoida@gmail.com"));
            message.Subject = objData["subject"].ToString();
            message.Body = createEmailBodyforget(objData["toname"].ToString(), objData["message"].ToString());
            message.IsBodyHtml = true;
            using (var smtp = new SmtpClient())
            {
                await smtp.SendMailAsync(message);
                await Task.FromResult(0);
            }
        }
        private string createEmailBodyforget(string userName, string message)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/forgotPassword.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            //body = body.Replace("{message}", message);
            return body;
        }
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
        [HttpPatch]
        [Route("api/User/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult ChangeUserPassword(int id, AppUser appUser)
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