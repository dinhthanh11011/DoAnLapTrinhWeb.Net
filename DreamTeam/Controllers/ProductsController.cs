using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DreamTeam.Handle;
using DreamTeam.Models;
using DreamTeam.Models.Product;
using DreamTeam.ViewModels;

namespace DreamTeam.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            Product_Index_Object pro = new Product_Index_Object();
            pro.Products = db.Products.Include(x => x.Category).Include(x => x.Category.Brand).Include(x => x.Product_Imgs).ToList();
            pro.Categories = db.Categories.OrderBy(x => x.Ordering).ToList();
            pro.Brands = db.Brands.OrderBy(x => x.Ordering).ToList();
            return View(pro);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products
                .Include(x=>x.Category)
                .Include(x=>x.Category.Brand)
                .Where(x=>x.Id==id)
                .Include(x=>x.Product_Imgs)
                .Include(x=>x.Product_Attributes).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
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
