using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DreamTeam.Models;
using DreamTeam.Models.Store;
using DreamTeam.Support;

namespace DreamTeam.Areas.Admins.Controllers
{
    [Authorize(Roles = support.STORE_MANAGE_PERMISSION)]
    public class AdvertisementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admins/Advertisements
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var ob = db.Advertisements.Find(id);
            if (ob == null)
                return HttpNotFound();
            ob.Image = support.UPLOAD_FOLDER_NAME + "/" + ob.Image;
            return View(ob);
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
