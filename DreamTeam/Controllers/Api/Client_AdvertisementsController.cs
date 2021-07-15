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
    public class Client_AdvertisementsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/Client_Advertisements
        public dynamic Get()
        {
            return Handle.Advertisements_Handle.getAll();
        }

        // GET: api/Client_Advertisements/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Client_Advertisements
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Client_Advertisements/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Client_Advertisements/5
        public void Delete(int id)
        {
        }
    }
}
