using DreamTeam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DreamTeam.Controllers.Api
{
    public class Client_Product_BrandController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/Client_Product_Brand
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Client_Product_Brand/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Client_Product_Brand
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Client_Product_Brand/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Client_Product_Brand/5
        public void Delete(int id)
        {
        }
    }
}
