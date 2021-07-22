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

        // GET: Carts/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Carts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carts/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Carts/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Carts/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Carts/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Carts/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
