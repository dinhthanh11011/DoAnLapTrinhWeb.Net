using DreamTeam.Models;
using DreamTeam.Support;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DreamTeam.Areas.Admins.Controllers.Api
{
    [Authorize(Roles = support.ACCOUNT_MANAGE_PERMISSION)]
    public class Admin_PermissionsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/Admin_Permissions
        public dynamic Get()
        {
            dynamic data = new System.Dynamic.ExpandoObject();
            data.Permissions = db.Roles.Select(x=>new { x.Id,x.Name}).ToList();
            data.AccountPermissions = db.Users.Select(x => new { x.Id, Name = x.UserName, Permissions = x.Roles.Select(y => new { Id = y.RoleId }) }).ToArray();
            return data;
        }



        // PUT: api/Admin_Permissions/5
        [HttpPut]
        public IHttpActionResult Put(dynamic req)
        {
            try
            {
                var UserId = (string)req.Id;
                var target = (string)req.Target;
                var action = (string)req.Action;
                if (UserId.Equals(User.Identity.GetUserId()))
                    return BadRequest("Không thể tự thay đổi quyền cho bản thân!");

                var user = db.Users.Find(UserId);
                var roleId = db.Roles.Where(x => x.Name.Equals(target)).First().Id;

                if (action.ToLower().Equals("active"))
                {
                    db.Database.ExecuteSqlCommand("INSERT INTO dbo.AspNetUserRoles ( UserId, RoleId ) VALUES  ( N'" + user.Id + "',  N'" + roleId + "')");
                    db.SaveChanges();
                    return Ok("Đã thêm quyền cho tài khoản!");
                }
                db.Database.ExecuteSqlCommand("DELETE FROM dbo.AspNetUserRoles WHERE UserId = N'" + user.Id + "' AND RoleId = N'" + roleId + "'");
                db.SaveChanges();
                return Ok("Đã xóa quyền cho tài khoản!");
            }
            catch (Exception)
            {
                return BadRequest("Đã xãy ra lỗi");
            }

        }

    }
}
