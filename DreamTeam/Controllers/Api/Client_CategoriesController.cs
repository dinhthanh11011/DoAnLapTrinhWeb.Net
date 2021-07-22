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
    public class Client_CategoriesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/Client_Categories
        public dynamic Get()
        {
            return db.Categories.OrderBy(x => x.Ordering).Select(x => new { x.Id, x.Name, Avatar = support.UPLOAD_FOLDER_NAME +"/"+ x.Avatar, Brand = x.Brand.Name }).ToList();
        }

        // GET: api/Client_Categories/5
        public dynamic Get(int id)
        {
            return db.Products.Include(x=>x.Product_Imgs).Where(x => x.CategoryId == id).OrderBy(x => x.Ordering).ToList();
        }
    }
}
