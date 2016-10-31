using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SocialNetwork.Web.WebApi.Models;

namespace SocialNetwork.Web.WebApi.Controllers
{
    public class TestController : ApiController
    {
        // GET: api/Test
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Test/5
        public string Get(int id)
        {
            return "valueMega";
        }

        [HttpPut]
        [Route("api/Test/email")]
        public void Send([FromBody] MessageWithEmailModel message)
        {
            var t = message;
        }

        // POST: api/Test
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Test/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Test/5
        public void Delete(int id)
        {
        }
    }
}
