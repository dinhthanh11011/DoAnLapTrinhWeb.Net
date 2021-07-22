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
        public dynamic GetProducts(string searchKey = null)
        {
            if(searchKey == null)
                return Handle.Product_Handle.getAll();
            else
                return Handle.Product_Handle.getBySearch(searchKey);
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