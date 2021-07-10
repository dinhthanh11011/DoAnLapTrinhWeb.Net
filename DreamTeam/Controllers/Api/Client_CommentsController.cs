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
using DreamTeam.Models.Account;
using Microsoft.AspNet.Identity;

namespace DreamTeam.Controllers.Api
{
    
    public class Client_CommentsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Client_Comments/5
        [ResponseType(typeof(Comment))]
        public dynamic GetComment(int id)
        {
            return db.Comments.Where(x => x.ProductId == id).Select(x=>new { x.Id,x.Ratting,x.Value,Customer = x.Customer.FullName}).ToList();
        }

        [Authorize]
        // POST: api/Client_Comments
        [ResponseType(typeof(Comment))]
        public IHttpActionResult PostComment()
        {
            try
            {
                var FormData = HttpContext.Current.Request.Form;
                db.Comments.Add(new Comment
                {
                    ProductId = int.Parse(FormData.Get("ProductId")),
                    CustomerId = User.Identity.GetUserId(),
                    Ratting = int.Parse(FormData.Get("Ratting")),
                    Value = FormData.Get("Value")
                });
                db.SaveChanges();
                return Ok();
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