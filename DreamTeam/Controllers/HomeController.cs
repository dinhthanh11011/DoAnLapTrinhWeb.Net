using DreamTeam.Models;
using DreamTeam.Support;
using DreamTeam.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace DreamTeam.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            Product_Index_Object pro = new Product_Index_Object();
            pro.Products = db.Products.Include(x => x.Category).Include(x => x.Category.Brand).Include(x => x.Product_Imgs).ToList();
            pro.Categories = db.Categories.OrderBy(x => x.Ordering).ToList();
            pro.Brands = db.Brands.OrderBy(x => x.Ordering).ToList();
            pro.Advertisements = db.Advertisements.OrderBy(x => x.Ordering).ToList();
            return View(pro);
        }
    }
}