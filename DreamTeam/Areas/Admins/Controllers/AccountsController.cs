using DreamTeam.Models;
using DreamTeam.Support;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DreamTeam.Areas.Admins.Controllers
{
    [Authorize(Roles = support.ACCOUNT_MANAGE_PERMISSION)]
    public class AccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admins/Accounts
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admins/Accounts/Details/5
        public ActionResult Details(string id)
        {
            var ob = db.Users.Include(x=>x.Addresses).Where(x=>x.Id == id).FirstOrDefault();
            if (ob == null)
                return HttpNotFound();
            return View(ob);
        }
    }
}
