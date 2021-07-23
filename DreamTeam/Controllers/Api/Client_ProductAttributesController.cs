using DreamTeam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DreamTeam.Controllers.Api
{
    public class Client_ProductAttributesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Client_ProductAttributes/5
        public dynamic Get(int id)
        {
            return db.Product_Attributes.Where(x => x.ProductId == id && x.Product.Active && x.Product.Category.Active && x.Product.Category.Brand.Active)
                .Select(x => new { Name = x.Attribute.Name, x.Value }).ToList();
        }

      
    }
}
