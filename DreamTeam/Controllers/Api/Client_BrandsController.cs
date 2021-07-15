using DreamTeam.Models;
using DreamTeam.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DreamTeam.Controllers.Api
{
    public class Client_BrandsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/Client_Brands
        public dynamic Get()
        {
            return db.Brands.OrderBy(x => x.Ordering).Select(x => new { x.Id, x.Name, Logo = support.UPLOAD_FOLDER_NAME + "/" + x.Logo }).ToList();
        }

        // GET: api/Client_Brands/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Client_Brands
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Client_Brands/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Client_Brands/5
        public void Delete(int id)
        {
        }
    }
}
