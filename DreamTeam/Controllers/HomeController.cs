using DreamTeam.Models;
using DreamTeam.Support;
using DreamTeam.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DreamTeam.Handle;

namespace DreamTeam.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            Product_Index_Object pro = new Product_Index_Object();
            pro.Products = Product_Handle.getAll();
            pro.Categories = Category_Handle.getAll();
            pro.Brands = Brand_Handle.getAll();
            pro.Advertisements = Advertisement_Handle.getAll();
            return View(pro);
        }
    }
}