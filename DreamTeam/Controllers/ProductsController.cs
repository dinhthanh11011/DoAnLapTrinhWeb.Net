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
            pro.Products = Product_Handle.getAll();
            pro.Categories = Category_Handle.getAll();
            pro.Brands = Brand_Handle.getAll();
            return View(pro);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = Product_Handle.getOne((int)id);
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
