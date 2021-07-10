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

namespace DreamTeam.Areas.Admins.Controllers.Api
{
    [Authorize(Roles = Support.support.PRODUCT_MANAGE_PERMISSION)]
    public class Admin_ProductsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Admin_Products
        public dynamic GetProducts()
        {
            return db.Products.Select(x=>new { x.Id,x.Name,x.OldPrice,x.CurrentPrice,x.Active,x.Ordering,Category = x.Category.Name,Brand = x.Category.Brand.Name}).OrderBy(x=>x.Ordering).ToList();
        }

        // GET: api/Admin_Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Admin_Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, dynamic req)
        {
            try
            {
                var pro = db.Products.Find(id);
                if (req.CategoryId != null)
                {
                    pro.CategoryId = int.Parse((string)req.CategoryId);
                }
                if (req.Name != null)
                {
                    pro.Name = (string)req.Name;
                }
                if (req.Quantity != null)
                {
                    pro.Quantity = int.Parse((string)req.Quantity);
                }
                if (req.OldPrice != null)
                {
                    pro.OldPrice = int.Parse((string)req.OldPrice);
                }
                if (req.CurrentPrice != null)
                {
                    pro.CurrentPrice = int.Parse((string)req.CurrentPrice);
                }
                if (req.Active != null)
                {
                    pro.Active = bool.Parse((string)req.Active);
                }
                if (req.Ordering != null)
                {
                    pro.Ordering = int.Parse((string)req.Ordering);
                }
                db.SaveChanges();
                return Ok("Đã lưu thay đổi");
            }
            catch (Exception)
            {
                return BadRequest("Đã xãy ra lỗi!");
            }
        }

        // POST: api/Admin_Products
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            try
            {
                if (product.Name.Trim().Length <= 0 || product.OldPrice < 0 || product.Quantity < 0 || product.CurrentPrice < 0 || product.OldPrice < product.CurrentPrice)
                {
                    return BadRequest("Thông tin sản phẩm không hợp lệ!");
                }
                product.Active = true;
                product.Ordering = db.Products.Select(x => x.Ordering).DefaultIfEmpty(0).Max() + 1;
                db.Products.Add(product);
                db.SaveChanges();
                return Ok("Đã thêm sản phẩm mới thành công!");
            }
            catch (Exception)
            {
                return BadRequest("Đã xãy ra lỗi!");
            }
        }

        // DELETE: api/Admin_Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }
    }
}