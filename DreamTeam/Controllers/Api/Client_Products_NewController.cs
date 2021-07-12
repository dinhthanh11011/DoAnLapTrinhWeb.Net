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
    public class Client_Products_NewController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/Client_Products_New
        public dynamic Get()
        {
            return db.Products.OrderBy(x => x.Ordering).Where(x => x.Active == true && x.New == true).Select(x => new {
                x.Id,
                x.Name,
                x.Quantity,
                x.OldPrice,
                x.CurrentPrice,
                Avatar = x.Product_Imgs.OrderBy(z => z.Ordering).Select(z => support.UPLOAD_FOLDER_NAME + "/" + z.Name).FirstOrDefault()
            }).ToList();
        }
    }
}
