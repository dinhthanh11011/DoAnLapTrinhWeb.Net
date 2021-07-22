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

namespace DreamTeam.Areas.Admins.Controllers.Api
{
    [Authorize(Roles = Support.support.STORE_MANAGE_PERMISSION)]
    public class Admin_InvoicesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Admin_Invoices
        public IQueryable<Invoice> GetInvoices()
        {
            return db.Invoices;
        }

        // GET: api/Admin_Invoices/5
        [ResponseType(typeof(Invoice))]
        public dynamic GetInvoice(int id)
        {
            return db.Invoices.Where(x => x.InvoiceStatusId == id).Select(x=>new { 
                x.Id,x.IsPayed,x.CreateAt, Customer =x.Customer.FullName,
                InvoiceStatus = x.InvoiceStatus.Name
            }).ToList();
        }

        // PUT: api/Admin_Invoices/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInvoice(int id,dynamic req)
        {
            try
            {
                var pro = db.Invoices.Find(id);
                if (req.IsPayed != null)
                {
                    pro.IsPayed = bool.Parse((string)req.IsPayed);
                }
                if (req.InvoiceStatusId != null)
                {
                    pro.InvoiceStatusId = int.Parse((string)req.InvoiceStatusId);
                }
                db.SaveChanges();
                return Ok("Đã lưu thay đổi");
            }
            catch (Exception)
            {
                return BadRequest("Đã xãy ra lỗi!");
            }
        }

        // POST: api/Admin_Invoices
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

        // DELETE: api/Admin_Invoices/5
        [ResponseType(typeof(Invoice))]
        public IHttpActionResult DeleteInvoice(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return NotFound();
            }

            db.Invoices.Remove(invoice);
            db.SaveChanges();

            return Ok(invoice);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvoiceExists(int id)
        {
            return db.Invoices.Count(e => e.Id == id) > 0;
        }
    }
}