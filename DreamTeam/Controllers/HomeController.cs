using DreamTeam.Models;
using DreamTeam.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DreamTeam.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.Advertisements = db.Advertisements.OrderBy(x => x.Ordering).Select(x => new { x.Title,x.Link, x.Description, Image = support.UPLOAD_FOLDER_NAME + "/" + x.Image }).ToList();
            ViewBag.ProductsNew = db.Products.OrderBy(x => x.Ordering).Where(x => x.Active == true && x.New == true).Select(x => new {
                x.Id,
                x.Name,
                x.Quantity,
                x.OldPrice,
                x.CurrentPrice,
                Avatar = x.Product_Imgs.OrderBy(z => z.Ordering).Select(z => support.UPLOAD_FOLDER_NAME + "/" + z.Name).FirstOrDefault()
            }).ToList();
            ViewBag.ProductsHot = db.Products.OrderBy(x => x.Ordering).Where(x => x.Active == true && x.Hot == true).Select(x => new {
                x.Id,
                x.Name,
                x.Quantity,
                x.OldPrice,
                x.CurrentPrice,
                Avatar = x.Product_Imgs.OrderBy(z => z.Ordering).Select(z => support.UPLOAD_FOLDER_NAME + "/" + z.Name).FirstOrDefault()
            }).ToList();


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}