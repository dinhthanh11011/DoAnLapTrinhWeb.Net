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
using DreamTeam.Models.Store;
using DreamTeam.Support;
using Microsoft.AspNet.Identity;

namespace DreamTeam.Controllers.Api
{
    [Authorize]
    public class Client_InvoicesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Client_Invoices
        public dynamic GetInvoices()
        {
            return db.Invoices.OrderBy(x=>x.CreateAt).Where(x=>x.CustomerId == User.Identity.GetUserId()).Select(x=>new { 
                x.CreateAt,Status = x.InvoiceStatus.Name,
                Details = x.InvoiceDetails.Where(y=>y.Product.Active == true).Select(y=>new { 
                    Name = y.Product.Name,y.Price,y.Quantity,
                    Avatar = y.Product.Product_Imgs.OrderBy(z => z.Ordering).Select(z => support.UPLOAD_FOLDER_NAME + "/" + z.Name).FirstOrDefault()
                }).ToList()
            });
        }

        // GET: api/Client_Invoices/5
        // get by Invoice status
        [ResponseType(typeof(Invoice))]
        public dynamic GetInvoice(int id)
        {
            return db.Invoices.OrderBy(x => x.CreateAt).Where(x => x.CustomerId == User.Identity.GetUserId() && x.InvoiceStatusId == id).Select(x => new {
                x.CreateAt,
                Status = x.InvoiceStatus.Name,
                Details = x.InvoiceDetails.Where(y => y.Product.Active == true).Select(y => new {
                    Name = y.Product.Name,
                    y.Price,
                    y.Quantity,
                    Avatar = y.Product.Product_Imgs.OrderBy(z => z.Ordering).Select(z => support.UPLOAD_FOLDER_NAME + "/" + z.Name).FirstOrDefault()
                }).ToList()
            });
        }

        // POST: api/Client_Invoices
        [ResponseType(typeof(Invoice))]
        public IHttpActionResult PostInvoice(Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Invoices.Add(invoice);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = invoice.Id }, invoice);
        }

        // DELETE: api/Client_Invoices/5
        [ResponseType(typeof(Invoice))]
        public IHttpActionResult DeleteInvoice(int id)
        {
            try
            {
                var ob = db.Invoices.Find(id);
                if (ob.InvoiceStatusId == db.InvoiceStatuses.Where(x => x.isDefault).FirstOrDefault().Id)
                    return BadRequest("Đơn hàng này hiện không thể hủy!");
                db.Invoices.Remove(ob);
                db.SaveChanges();
                return Ok("Đã hủy đơn hàng!");
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