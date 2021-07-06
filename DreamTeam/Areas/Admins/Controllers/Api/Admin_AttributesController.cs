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

namespace DreamTeam.Areas.Admins.Controllers.Api
{
    [Authorize(Roles = support.PRODUCT_MANAGE_PERMISSION)]
    public class Admin_AttributesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: api/Admin_Attributes/5
        [ResponseType(typeof(Models.Product.Attribute))]
        public dynamic GetAttribute(int id)
        {
            return db.Attributes.Where(x=>x.CategoryId==id).OrderBy(x=>x.Ordering).ToList();
        }

        // PUT: api/Admin_Attributes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAttribute(int id, dynamic req)
        {
            try
            {
                var ob = db.Attributes.Find(id);
                if (req.Name != null && ob.Name != (string)req.Name)
                {
                    ob.Name = (string)req.Name;
                }
                if (req.Ordering != null && ob.Ordering != (int)req.Ordering)
                {
                    ob.Ordering = (int)req.Ordering;
                }
                if (req.Active != null && ob.Active != (bool)req.Active)
                {
                    ob.Active = (bool)req.Active;
                }
                db.SaveChanges();
                return Ok("Đã lưu thay đổi");
            }
            catch (Exception)
            {
                return BadRequest("Đã xãy ra lỗi");
            }
        }

        // POST: api/Admin_Attributes
        [ResponseType(typeof(Models.Product.Attribute))]
        public IHttpActionResult PostAttribute(Models.Product.Attribute attribute)
        {
            if(attribute.Name.Trim().Length > 0)
            {
                attribute.Active = true;
                attribute.Ordering = db.Attributes.Where(x => x.CategoryId == attribute.CategoryId).Select(x => x.Ordering).DefaultIfEmpty(0).Max() + 1;

                db.Attributes.Add(attribute);
                db.SaveChanges();
                return Ok("Đã thêm mới!");
            }
            return BadRequest("Vui lòng điền đầy đủ thông tin!");
        }

        // DELETE: api/Admin_Attributes/5
        [ResponseType(typeof(Models.Product.Attribute))]
        public IHttpActionResult DeleteAttribute(int id)
        {
            Models.Product.Attribute attribute = db.Attributes.Find(id);
            if (attribute == null)
            {
                return NotFound();
            }

            db.Attributes.Remove(attribute);
            db.SaveChanges();

            return Ok(attribute);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AttributeExists(int id)
        {
            return db.Attributes.Count(e => e.Id == id) > 0;
        }
    }
}