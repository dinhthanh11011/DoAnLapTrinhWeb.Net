using DreamTeam.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DreamTeam.Areas.Admins.Controllers
{
    [Authorize(Roles = support.STORE_MANAGE_PERMISSION)]
    public class HomeController : Controller
    {
        // GET: Admins/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}
