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
using Microsoft.AspNet.Identity;

namespace DreamTeam.Controllers.Api
{
    [Authorize]
    public class Client_CartsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Client_Carts/5
        [ResponseType(typeof(Product))]
        public dynamic GetCarts()
        {
            return db.Users.Find(User.Identity.GetUserId()).Carts.OrderBy(x=>x.Ordering).Where(x => x.Active == true).Select(x=>new {
                x.Id,x.Name,x.Quantity,x.OldPrice,x.CurrentPrice,
                Avatar = x.Product_Imgs.OrderBy(z => z.Ordering).Select(z => support.UPLOAD_FOLDER_NAME + "/" + z.Name).FirstOrDefault()
            });
        }

        // POST: api/Client_Carts
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostCarts(int id)
        {
            try
            {
                db.Users.Find(User.Identity.GetUserId()).Carts.Add(db.Products.Find(id));
                db.SaveChanges();
                return Ok("Đã lưu thay đổi!");
            }
            catch (Exception)
            {
                return BadRequest("Đã xãy ra lỗi!");
            }
        }

        // DELETE: api/Client_Carts/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteCarts(int id)
        {
            try
            {
                db.Users.Find(User.Identity.GetUserId()).Carts.Remove(db.Products.Find(id));
                db.SaveChanges();
                return Ok("Đã lưu thay đổi!");
            }
            catch (Exception)
            {
                return BadRequest("Đã xãy ra lỗi!");
            }
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