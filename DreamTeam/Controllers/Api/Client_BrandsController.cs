using DreamTeam.Models;
using DreamTeam.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using DreamTeam.Handle;

namespace DreamTeam.Controllers.Api
{
    public class Client_BrandsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/Client_Brands
        public dynamic Get()
        {
            return Brand_Handle.getAll();
        }

        // GET: api/Client_Brands/5
        public dynamic Get(int id)
        {
            return Product_Handle.getByBrand(id);
        }

    }
}
