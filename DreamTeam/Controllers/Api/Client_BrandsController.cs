using DreamTeam.Models;
using DreamTeam.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

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
        public dynamic Get(int id)
        {
            return db.Products.Include(x => x.Product_Imgs).Where(x => x.Category.BrandId == id).OrderBy(x => x.Ordering).ToList();
        }

    }
}
