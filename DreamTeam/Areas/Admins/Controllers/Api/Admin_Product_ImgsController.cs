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
    public class Admin_Product_ImgsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Admin_Product_Img/5
        [ResponseType(typeof(Product_Img))]
        public dynamic GetProduct_Img(int id)
        {
            var imgs = db.Product_Imgs.Where(x => x.ProductId == id).OrderBy(x=>x.Ordering).ToList();
            foreach (var item in imgs)
            {
                item.Name = Support.support.UPLOAD_FOLDER_NAME + "/" + item.Name;
            }
            return imgs;
        }

        // POST: api/Admin_Product_Img
        [ResponseType(typeof(Product_Img))]
        public IHttpActionResult PostProduct_Img()
        {
            try
            {
                var files = support.checkFileUpLoad(HttpContext.Current.Request.Files);
                if (files != null)
                {
                    int proID = int.Parse(HttpContext.Current.Request.Form.Get("ProductId"));
                    for (int i = 0; i < files.Count; i++)
                    {
                        var item = files[i];
                        var fileUp = support.uploadFile(item);
                        db.Product_Imgs.Add(new Product_Img
                        {
                            Name = fileUp.fileName,
                            Active = true,
                            ProductId = proID,
                            Ordering = db.Product_Imgs.Where(x => x.ProductId == proID).Select(x => x.Ordering).DefaultIfEmpty(0).Max() + 1
                        }) ;
                        db.SaveChanges();
                        item.SaveAs(fileUp.path);
                    }
                    return Ok("Đã thêm mới thành công!");
                }
                return BadRequest("Không tin thấy File Upload!");
            }
            catch (Exception)
            {
                return BadRequest("Đã xãy ra lỗi!");
            }
        }

        //put
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct_Img(int id, dynamic req)
        {
            try
            {
                var ob = db.Product_Imgs.Find(id);
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
                return BadRequest("Đã xãy ra lỗi!");
            }
        }

        // DELETE: api/Admin_Product_Img/5
        [ResponseType(typeof(Product_Img))]
        public IHttpActionResult DeleteProduct_Img(int id)
        {
            try
            {
                var ob = db.Product_Imgs.Find(id);
                support.deleteImg(ob.Name);
                db.Product_Imgs.Remove(ob);
                db.SaveChanges();
                return Ok("Đã xóa!");
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