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
    public class Admin_InvoiceStatusesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/InvoiceStatuses
        public dynamic GetInvoiceStatuses()
        {
            return db.InvoiceStatuses.OrderBy(x=>x.Ordering);
        }


        // PUT: api/InvoiceStatuses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInvoiceStatus(int id, dynamic req)
        {
            try
            {
                var ob = db.InvoiceStatuses.Find(id);

                if ((string)req.isDefault!=null)
                {
                    bool isdef = bool.Parse((string)req.isDefault);
                    if(isdef != ob.isDefault)
                    {
                        if(isdef == false)
                            return BadRequest("Không thể tắt trạng thái mặc định");
                        else
                        {
                            foreach (var item in db.InvoiceStatuses.Where(x=>x.isDefault == true).ToList())
                            {
                                item.isDefault = false;
                            }
                            ob.isDefault = isdef;
                        }
                    }
                }
                if ((string)req.Name != null)
                {
                    string name = (string)req.Name;
                    ob.Name = name;
                }
                if ((string)req.Ordering != null)
                {
                    int Ordering = int.Parse((string)req.Ordering);
                    ob.Ordering = Ordering;
                }
                db.SaveChanges();
                return Ok("Đã lưu thay đổi!");
            }
            catch (Exception)
            {
                return BadRequest("Đã xãy ra lỗi");
            }
        }

        // POST: api/InvoiceStatuses
        [ResponseType(typeof(InvoiceStatus))]
        public IHttpActionResult PostInvoiceStatus(InvoiceStatus invoiceStatus)
        {
            try
            {
                if (invoiceStatus.Name.Trim().Length <= 0)
                    return BadRequest("Vui lòng điền đầy đủ thông tin!");
                invoiceStatus.Ordering = db.InvoiceStatuses.Select(x => x.Ordering).DefaultIfEmpty(0).Max() + 1;
                if (invoiceStatus.isDefault)
                {
                    foreach (var item in db.InvoiceStatuses.Where(x => x.isDefault == true).ToList())
                    {
                        item.isDefault = false;
                    }
                }
                db.InvoiceStatuses.Add(invoiceStatus);
                db.SaveChanges();
                return Ok("Đã thêm mới!");
            }
            catch (Exception)
            {
                return BadRequest("Đã xãy ra lỗi");
            }
        }

        // DELETE: api/InvoiceStatuses/5
        [ResponseType(typeof(InvoiceStatus))]
        public IHttpActionResult DeleteInvoiceStatus(int id)
        {
            try
            {
                InvoiceStatus ob = db.InvoiceStatuses.Find(id);
                if (ob.isDefault)
                    return BadRequest("Không thể xóa trạng thái mặc định!");
                db.InvoiceStatuses.Remove(ob);
                db.SaveChanges();
                return Ok("Đã xóa");
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

        private bool InvoiceStatusExists(int id)
        {
            return db.InvoiceStatuses.Count(e => e.Id == id) > 0;
        }
    }
}