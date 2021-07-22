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
using Microsoft.AspNet.Identity;

namespace DreamTeam.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Invoices
        public ActionResult Index()
        {
            ViewBag.InvoiceStatusId = new SelectList(db.InvoiceStatuses, "Id", "Name", "");
            return View();
        }

        // GET: Invoices/Details/5
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

        // GET: Invoices/Create
        public ActionResult Create()
        {
            ViewBag.AddressId = new SelectList(db.Addresses, "Id", "Person");
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "FullName");
            ViewBag.InvoiceStatusesId = new SelectList(db.InvoiceStatuses, "Id", "Name");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreateAt,IsPayed,CustomerId,InvoiceStatusesId,AddressId")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressId = new SelectList(db.Addresses, "Id", "Person", "");
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "FullName", invoice.CustomerId);
            ViewBag.InvoiceStatusesId = new SelectList(db.InvoiceStatuses, "Id", "Name", "");
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "Id", "Person", "");
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "FullName", invoice.CustomerId);
            ViewBag.InvoiceStatusesId = new SelectList(db.InvoiceStatuses, "Id", "Name", invoice.InvoiceStatus);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreateAt,IsPayed,CustomerId,InvoiceStatusesId,AddressId")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "Id", "Person", "");
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "FullName", invoice.CustomerId);
            ViewBag.InvoiceStatusesId = new SelectList(db.InvoiceStatuses, "Id", "Name", invoice.InvoiceStatus);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Index");
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
