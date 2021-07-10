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
    public class Admin_LogoesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Admin_Logoes
        public dynamic GetLogos()
        {
            var data = db.Logos.ToList();
            data[0].Name = support.UPLOAD_FOLDER_NAME + "/" + data[0].Name;
            return data;
        }

        // POST: api/Admin_Logoes
        [ResponseType(typeof(Logo))]
        public IHttpActionResult PostLogo()
        {
            try
            {
                var files = support.checkFileUpLoad(HttpContext.Current.Request.Files);
                if(files==null)
                    return BadRequest("Vui lòng thêm ảnh");
                var file = files[0];
                var fileUp = support.uploadFile(file);
                if (db.Logos.Count() <= 0)
                {
                    db.Logos.Add(new Logo
                    {
                        Name = fileUp.fileName
                    });
                }
                else
                {
                    var ob = db.Logos.First();
                    support.deleteImg(ob.Name);
                    ob.Name = fileUp.fileName;
                }
                file.SaveAs(fileUp.path);
                db.SaveChanges();

                return Ok("Đã lưu thay đổi");
            }
            catch (Exception)
            {
                return BadRequest("Đã xãy ra lỗi");
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