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
    public class Admin_BrandsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Admin_Brands
        public IQueryable<Brand> GetBrands()
        {
            var data = db.Brands;
            foreach (var item in data)
            {
                item.Logo = support.UPLOAD_FOLDER_NAME + "/" + item.Logo;
            }
            return data;
        }


        // PUT: api/Admin_Brands/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBrand(int id, dynamic req)
        {
            try
            {
                var ob = db.Brands.Find(id);
                if (req.Name != null && ob.Name != (string)req.NAME)
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
                return Ok("Đã lưu thay đổi!");
            }
            catch (Exception)
            {
                return BadRequest("Thất bại!");
            }
        }

        //put img
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBrand()
        {
            try
            {
                var files = support.checkFileUpLoad(HttpContext.Current.Request.Files);
                if (files!=null)
                {
                    var file = files[0];
                    var fileUp = support.uploadFile(file);
                    int id = int.Parse(HttpContext.Current.Request.Form.Get("Id"));
                    var ob = db.Brands.Find(id);
                    string oldfi = ob.Logo;
                    ob.Logo = fileUp.fileName;
                    db.SaveChanges();
                    file.SaveAs(fileUp.path);
                    support.deleteImg(oldfi);
                    return Ok("Đã lưu thay đổi!");
                }
                return BadRequest("Thất bại!");
            }
            catch (Exception)
            {
                return BadRequest("Thất bại!");
            }
        }

        // POST: api/Admin_Brands
        [ResponseType(typeof(Brand))]
        public IHttpActionResult PostBrand()
        {
            var name = HttpContext.Current.Request.Form.Get("Name");
            if (HttpContext.Current.Request.Files.Count > 0 && !name.Trim().Equals(""))
            {
                var file = HttpContext.Current.Request.Files[0];
                var fileUp = support.uploadFile(file);

                db.Brands.Add(new Brand()
                {
                    Name = name,
                    Logo = fileUp.fileName,
                    Active = true,
                    Ordering = db.Brands.Select(x => x.Ordering).DefaultIfEmpty(0).Max() + 1
                }); 
                db.SaveChanges();
                file.SaveAs(fileUp.path);
                return Ok("Đã thêm mới!");
            }
            return BadRequest("Vui lòng điền đầy đủ thông tin");
        }

        // DELETE: api/Admin_Brands/5
        [ResponseType(typeof(Brand))]
        public IHttpActionResult DeleteBrand(int id)
        {
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return NotFound();
            }

            db.Brands.Remove(brand);
            db.SaveChanges();
            support.deleteImg(brand.Logo);
            return Ok(brand);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BrandExists(int id)
        {
            return db.Brands.Count(e => e.Id == id) > 0;
        }
    }
}