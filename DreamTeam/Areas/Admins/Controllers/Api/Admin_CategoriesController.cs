using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DreamTeam.Models;
using DreamTeam.Models.Product;
using DreamTeam.Support;

namespace DreamTeam.Areas.Admins.Controllers.Api
{
    [Authorize(Roles = support.PRODUCT_MANAGE_PERMISSION)]
    public class Admin_CategoriesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: api/Admin_Categories
        public dynamic GetCategories()
        {
            return db.Categories.Select(x=>new { x.Id,x.Name,x.Ordering,x.Active, Avatar = support.UPLOAD_FOLDER_NAME + "/" + x.Avatar,AttributeCount = x.Attributes.Count, Brand = x.Brand.Name}).OrderBy(x=>x.Ordering);
        }

        // GET: api/Admin_Categories/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            var category = db.Categories.Select(x => new { x.Id, x.Name, x.Ordering, x.Active, x.Avatar, AttributeCount = x.Attributes.Count, Brand = x.Brand.Name }).Where(x=>x.Id ==id).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Admin_Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory(int id, dynamic req)
        {
            try
            {
                var ob = db.Categories.Find(id);
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
                if (req.BrandId != null && ob.BrandId != (int)req.BrandId)
                {
                    ob.BrandId = (int)req.BrandId;
                }
                db.SaveChanges();
                return Ok("Đã lưu thay đổi!");
            }
            catch (Exception)
            {
                return BadRequest("Thất bại!!");
            }
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory()
        {
            try
            {
                var files = support.checkFileUpLoad(HttpContext.Current.Request.Files);
                if (files != null)
                {
                    var file = files[0];
                    var fileUp = support.uploadFile(file);
                    int id = int.Parse(HttpContext.Current.Request.Form.Get("Id"));
                    var ob = db.Categories.Find(id);
                    support.deleteImg(ob.Avatar);
                    ob.Avatar = fileUp.fileName;
                    db.SaveChanges();
                    file.SaveAs(fileUp.path);
                    return Ok("Đã lưu thay đổi!");
                }
                return BadRequest("Thất bại!");
            }
            catch (Exception)
            {
                return BadRequest("Thất bại!");
            }
        }

        // POST: api/Admin_Categories
        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory()
        {
            var name = HttpContext.Current.Request.Form.Get("Name");
            var BrandId = HttpContext.Current.Request.Form.Get("BrandId");
            var files = support.checkFileUpLoad(HttpContext.Current.Request.Files);
            if (files != null && !name.Trim().Equals(""))
            {
                var file = files[0];
                var fileUp = support.uploadFile(file);

                db.Categories.Add(new Category()
                {
                    Name = name,
                    Avatar = fileUp.fileName,
                    BrandId = int.Parse(BrandId),
                    Active = true,
                    Ordering = db.Categories.Select(x => x.Ordering).DefaultIfEmpty(0).Max() + 1
                });
                db.SaveChanges();
                file.SaveAs(fileUp.path);
                return Ok("Đã thêm mới!");
            }
            return BadRequest("Vui lòng điền đầy đủ thông tin");

        }

        // DELETE: api/Admin_Categories/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            db.SaveChanges();
            support.deleteImg(category.Avatar);
            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.Id == id) > 0;
        }
    }
}