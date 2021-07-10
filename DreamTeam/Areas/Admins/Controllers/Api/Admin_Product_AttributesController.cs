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
    public class Admin_Product_AttributesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Admin_Product_Attribute/5
        [ResponseType(typeof(Product_Attribute))]
        public dynamic GetProduct_Attribute(int id)
        {
            return db.Product_Attributes.Where(x => x.ProductId == id);
        }

        // PUT: api/Admin_Product_Attribute/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct_Attribute(int id,dynamic req)
        {
            try
            {
                var ob = db.Products.Find(id);
                var pro_attrs = db.Attributes.Where(x => x.CategoryId == ob.CategoryId).ToList();
                foreach (var item in pro_attrs)
                {
                    string value = (string)req[(string)item.Code];
                    if (value == null)
                        continue;

                    if (value.Trim().Length <= 0)
                        return BadRequest("Thông tin " + item.Name + " không được bỏ trống!");

                    var pro_attr = db.Product_Attributes.Where(x => x.AttributeId == item.Id && x.ProductId == ob.Id).FirstOrDefault();
                    if (pro_attr == null)
                    {
                        db.Product_Attributes.Add(new Product_Attribute
                        {
                            AttributeId = item.Id,
                            ProductId = ob.Id,
                            Value = value
                        });
                    }
                    else
                        pro_attr.Value = value;
                }
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