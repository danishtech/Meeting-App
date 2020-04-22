using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Meeting_App.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //[Route("{connectionId}")]
        //[HttpGet]
        //public  string Get(string connectionId)
        //{
        //    _hub.Clients.Client(connectionId).SendAsync("clientMethodName", "get called");
        //    return "value";
        //}

        // GET api/values/5 
        public string Get(string id)
        {
            Global.LogMessage("Request param : " + id);
            //Requestlog req = new Requestlog();
            //req.Clients.Client(id).SendAsync("clientMethodName", "get called");
            return "value";
        }
        // GET api/values/5 

        [Route("api/Values/Chat")]
        [HttpGet]
        public string Chat(string name, string message )
        {
            Global.LogMessage("Request param : " + name);
            return  name + ":" + message ;
        }
        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
