using DreamTeam.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DreamTeam.Areas.Admins.Controllers
{
    [Authorize(Roles = support.ACCOUNT_MANAGE_PERMISSION)]
    public class PermissionsController : Controller
    {
        // GET: Admins/Permission
        public ActionResult Index()
        {
            return View();
        }
    }
}
