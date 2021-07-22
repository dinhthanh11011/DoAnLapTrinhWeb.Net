using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DreamTeam.Models;
using DreamTeam.Models.Store;
using DreamTeam.ViewModels;

namespace DreamTeam.Areas.Admins.Controllers
{
    [Authorize(Roles = Support.support.STORE_MANAGE_PERMISSION)]
    public class InvoicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admins/Invoices
        public ActionResult Index()
        {
            ViewBag.InvoiceStatusId = new SelectList(db.InvoiceStatuses, "Id", "Name", "");
            var invoices = db.Invoices.Include(i => i.Customer).Include(i => i.InvoiceStatus);
            return View(invoices.ToList());
        }

        // GET: Admins/Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice ob = db.Invoices.Include(x => x.InvoiceDetails)
                .Include(x => x.InvoiceStatus).Where(x => x.Id == id).FirstOrDefault();
            if (ob == null)
            {
                return HttpNotFound();
            }
            var invoice = new Invoices_Index_Object
            {
                Invoice = ob,
                InvoiceDetails = db.InvoiceDetails.Where(x => x.InvoiceId == ob.Id)
                .Include(x => x.Product)
                .Include(x => x.Product.Product_Imgs).ToList()
            };
            return View(invoice);
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
