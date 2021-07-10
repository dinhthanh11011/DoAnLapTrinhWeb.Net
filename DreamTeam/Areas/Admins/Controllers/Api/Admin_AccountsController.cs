using DreamTeam.Models;
using DreamTeam.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DreamTeam.Areas.Admins.Controllers.Api
{
    [Authorize(Roles = support.ACCOUNT_MANAGE_PERMISSION)]
    public class Admin_AccountsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/Admin_Accounts
        public dynamic Get()
        {
            return db.Users.Select(x=>new { x.Id,x.Email,x.FullName,x.PhoneNumber,x.Active}).ToList();
        }

        // PUT: api/Admin_Accounts/5
        [HttpPut]
        public IHttpActionResult Put(dynamic req)
        {
            try
            {
                string id = (string)req.Id;
                var ob = db.Users.Find(id);
                ob.Active = bool.Parse((string)req.Active);
                db.SaveChanges();
                return Ok("Đã lưu thay đổi");
            }
            catch (Exception)
            {
                return BadRequest("Đã xãy ra lỗi!");
            }

        }
    }
}
