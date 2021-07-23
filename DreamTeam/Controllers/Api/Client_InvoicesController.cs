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
using DreamTeam.Models.Product;
using DreamTeam.Models.Store;
using DreamTeam.Support;
using DreamTeam.ViewModels;
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
            string userId = User.Identity.GetUserId();
            return db.Invoices.OrderBy(x => x.CreateAt).Where(x => x.CustomerId == userId).Select(x => new
            {
                x.CreateAt,
                Status = x.InvoiceStatus.Name,
                Details = x.InvoiceDetails.Where(y => y.Product.Active == true).Select(y => new
                {
                    Name = y.Product.Name,
                    y.Price,
                    y.Quantity,
                    Avatar = y.Product.Product_Imgs.OrderBy(z => z.Ordering).Select(z => support.UPLOAD_FOLDER_NAME + "/" + z.Name).FirstOrDefault()
                }).ToList()
            });
        }

        // GET: api/Client_Invoices/5
        // get by Invoice status
        [ResponseType(typeof(Invoice))]
        public dynamic GetInvoice(int id)
        {
            string userId = User.Identity.GetUserId();
            return db.Invoices.Where(x => x.InvoiceStatusId == id && x.CustomerId == userId).Select(x => new
            {
                x.Id,
                x.IsPayed,
                x.CreateAt,
                Customer = x.Customer.FullName,
                InvoiceStatus = x.InvoiceStatus.Name
            }).ToList();
        }

        // POST: api/Client_Invoices
        [HttpPost]
        public IHttpActionResult PostInvoice(Invoices_Index_Object req)
        {
            try
            {
                var add = db.Addresses.Find(req.AddressId);
                if (add == null)
                {
                    return BadRequest("Địa Chỉ Không Hợp Lệ!");
                }

                var userId = User.Identity.GetUserId();
                req.Invoice = new Invoice();
                req.Invoice.CreateAt = DateTime.Now;
                req.Invoice.CustomerId = User.Identity.GetUserId();
                req.Invoice.InvoiceStatusId = db.InvoiceStatuses.Where(x => x.isDefault).First().Id;

                req.Invoice.Person = add.Person;
                req.Invoice.Phone = add.Phone;
                req.Invoice.Location = add.Location;
                foreach (var item in req.InvoiceDetails)
                {
                    var pro = db.Products.Find(item.ProductId);
                    pro.Quantity -= item.Quantity;
                    item.Price = pro.CurrentPrice;
                    db.Carts.Remove(db.Carts.Where(x => x.CustomerId == userId && x.ProductId == item.ProductId).First());
                }
                req.Invoice.InvoiceDetails = req.InvoiceDetails;
                db.Invoices.Add(req.Invoice);
                db.SaveChanges();
                return Ok("Đã đặt hàng thành công!");
            }
            catch (Exception)
            {
                return Ok("Lỗi!");
            }
        }

        // DELETE: api/Client_Invoices/5
        [ResponseType(typeof(Invoice))]
        public IHttpActionResult DeleteInvoice(int id)
        {
            try
            {
                var ob = db.Invoices.Include(x => x.InvoiceDetails).Where(x => x.Id == id).FirstOrDefault();
                if (ob.InvoiceStatusId != db.InvoiceStatuses.Where(x => x.isDefault).FirstOrDefault().Id)
                    return BadRequest("Đơn hàng này hiện không thể hủy!");
                foreach (var item in ob.InvoiceDetails)
                {
                    db.Products.Find(item.ProductId).Quantity += item.Quantity;
                }
                db.InvoiceDetails.RemoveRange(db.InvoiceDetails.Where(x => x.InvoiceId == ob.Id).ToList());
                db.Invoices.Remove(ob);
                db.SaveChanges();
                return Ok("Đã hủy đơn hàng!");
            }
            catch (Exception)
            {
                return BadRequest("lỗi!");
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