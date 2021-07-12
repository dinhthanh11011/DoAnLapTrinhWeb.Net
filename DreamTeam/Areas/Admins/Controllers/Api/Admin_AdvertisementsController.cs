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
using DreamTeam.Models.Store;
using DreamTeam.Support;

namespace DreamTeam.Areas.Admins.Controllers.Api
{
/*    [Authorize(Roles = support.STORE_MANAGE_PERMISSION)]*/
    public class Admin_AdvertisementsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Admin_Advertisements
        public dynamic GetAdvertisements()
        {
            var data = db.Advertisements.OrderBy(x=>x.Ordering).ToList();
            foreach (var item in data)
            {
                item.Image = support.UPLOAD_FOLDER_NAME + "/" + item.Image;
            }
            return data;
        }

        // PUT: api/Admin_Advertisements/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdvertisement(int id, dynamic req)
        {
            try
            {
                var ob = db.Advertisements.Find(id);
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

        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdvertisement()
        {
            try
            {
                var data = HttpContext.Current.Request.Form;
                var id = int.Parse(data.Get("Id"));
                var title = data.Get("Title");
                var link = data.Get("Link");
                var description = data.Get("Description");
                var active = data.Get("Active");
                var ordering = int.Parse(data.Get("Ordering"));
                if(title.Length <= 0 || description.Length<=0)
                    return BadRequest("Vui lòng điền đầy đủ thông tin");

                var ob = db.Advertisements.Find(id);
                var files = support.checkFileUpLoad(HttpContext.Current.Request.Files);
                if (files!=null)
                {
                    var file = files[0];
                    var fileUp = support.uploadFile(file);
                    support.deleteImg(ob.Image);
                    ob.Image = fileUp.fileName;
                    file.SaveAs(fileUp.path);
                }
                ob.Active = active.Contains("true");
                ob.Title = title;
                ob.Link = link;
                ob.Description = description;
                ob.Ordering = ordering;
                db.SaveChanges();
                return Ok("Đã lưu thay đổi!");
            }
            catch (Exception)
            {
                return BadRequest("Đã xãy ra lỗi!");
            }
        }

        // POST: api/Admin_Advertisements
        [ResponseType(typeof(Advertisement))]
        public IHttpActionResult PostAdvertisement()
        {
            try
            {
                var files = support.checkFileUpLoad(HttpContext.Current.Request.Files);
                if (files != null)
                {
                    var title = HttpContext.Current.Request.Form.Get("Title");
                    var link = HttpContext.Current.Request.Form.Get("Link");
                    var des = HttpContext.Current.Request.Form.Get("Description");
                    for (int i = 0; i < files.Count; i++)
                    {
                        var item = files[i];
                        var fileUp = support.uploadFile(item);
                        db.Advertisements.Add(new Advertisement
                        {
                            Image = fileUp.fileName,
                            Active =true,
                            Title = title,
                            Link = link,
                            Description = des,
                            Ordering = db.Advertisements.Select(x => x.Ordering).DefaultIfEmpty(0).Max() + 1
                        });
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

        // DELETE: api/Admin_Advertisements/5
        [ResponseType(typeof(Advertisement))]
        public IHttpActionResult DeleteAdvertisement(int id)
        {
            try
            {
                var ob = db.Advertisements.Find(id);
                support.deleteImg(ob.Image);
                db.Advertisements.Remove(ob);
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