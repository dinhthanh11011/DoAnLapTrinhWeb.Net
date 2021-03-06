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
using DreamTeam.Models.Account;
using DreamTeam.Models.Product;
using DreamTeam.Support;
using Microsoft.AspNet.Identity;

namespace DreamTeam.Controllers.Api
{
    [Authorize]
    public class Client_CartsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [ResponseType(typeof(Product))]
        public dynamic GetCarts()
        {
            string userId = User.Identity.GetUserId();
            return db.Carts.OrderBy(x => x.Product.Ordering).Where(x => x.Product.Active == true && x.CustomerId == userId).Select(x=>new {
                x.Product.Id,x.Product.Name,x.Product.Quantity,x.Product.OldPrice,x.Product.CurrentPrice,
                CartCount = 1,
                Avatar = x.Product.Product_Imgs.OrderBy(z => z.Ordering).Select(z => support.UPLOAD_FOLDER_NAME + "/" + z.Name).FirstOrDefault()
            });
        }

        // POST: api/Client_Carts
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostCarts(int id)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if(db.Carts.Where(x=>x.CustomerId==userId && x.ProductId == id).FirstOrDefault()!=null)
                {
                    return Ok("Sản phẩm này đã có trong giỏ hàng!");
                }
                db.Carts.Add(new Models.Account.Cart { 
                    ProductId = id,
                    CustomerId = userId
                });
                db.SaveChanges();
                return Ok("Đã thêm vào giỏ hàng!");
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
                string userId = User.Identity.GetUserId();
                db.Carts.Remove(db.Carts.Where(x => x.CustomerId == userId && x.ProductId == id).FirstOrDefault());
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