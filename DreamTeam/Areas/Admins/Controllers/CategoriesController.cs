using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DreamTeam.Models;
using DreamTeam.Models.Product;
using DreamTeam.Support;

namespace DreamTeam.Areas.Admins.Controllers
{
    [Authorize(Roles = support.PRODUCT_MANAGE_PERMISSION)]
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admins/Categories
        public ActionResult Index()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name");
            return View();
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
