using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DreamTeam.Models;
using DreamTeam.Models.Product;
using DreamTeam.Support;

namespace DreamTeam.Controllers.Api
{
    public class Client_ProductsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Client_Products
        public dynamic GetProducts()
        {
            return db.Products.OrderBy(x=>x.Ordering).Where(x => x.Active == true).Select(x=>new { 
                x.Id,x.Name,x.Quantity,x.OldPrice,x.CurrentPrice,
                Avatar = x.Product_Imgs.OrderBy(z=>z.Ordering).Select(z=> support.UPLOAD_FOLDER_NAME + "/" + z.Name ).FirstOrDefault()
            }).ToList();
        }

        // GET: api/Client_Products/5
        [ResponseType(typeof(Product))]
        public dynamic GetProduct(int id)
        {
            return db.Products.OrderBy(x => x.Ordering).Where(x => x.Active == true && x.Id == id).Select(x => new {
                x.Id,
                x.Name,
                x.Quantity,
                x.OldPrice,
                x.CurrentPrice,
                Attributes = x.Product_Attributes.OrderBy(y => y.Attribute.Ordering).Select(y => new { Name = y.Attribute.Name, Value = y.Value }),
                Images = x.Product_Imgs.OrderBy(z => z.Ordering).Select(z => new { Name = support.UPLOAD_FOLDER_NAME + "/" + z.Name }).ToList()
            }).FirstOrDefault();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}