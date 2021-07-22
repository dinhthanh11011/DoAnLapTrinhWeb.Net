using DreamTeam.Models;
using DreamTeam.Models.Account;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DreamTeam.Controllers.Api
{
    [Authorize]
    public class Client_AddressesController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/ClientAddresses
        public dynamic Get()
        {
            var userId = User.Identity.GetUserId();
            return db.Addresses.Where(x => x.UserId == userId).ToList();
        }

        // GET: api/ClientAddresses/5
        public dynamic Get(int id)
        {
            var userId = User.Identity.GetUserId();
            return db.Addresses.Where(x => x.UserId == userId && x.Id == id).FirstOrDefault();
        }

    }
}
