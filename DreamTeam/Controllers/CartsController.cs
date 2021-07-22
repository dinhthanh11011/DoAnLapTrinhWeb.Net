using DreamTeam.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace DreamTeam.Controllers
{
    [Authorize]
    public class CartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext(); 
        // GET: Carts
        public ActionResult Index()
        {
            ViewBag.AddressId = new SelectList(db.Addresses, "Id", "Person", "");
            return View();
        }
    }
}
