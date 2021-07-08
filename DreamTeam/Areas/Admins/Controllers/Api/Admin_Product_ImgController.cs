using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DreamTeam.Models;
using DreamTeam.Models.Product;

namespace DreamTeam.Areas.Admins.Controllers.Api
{
    public class Admin_Product_ImgController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Admin_Product_Img/5
        [ResponseType(typeof(Product_Img))]
        public IHttpActionResult GetProduct_Img(int id)
        {
            Product_Img product_Img = db.Product_Imgs.Find(id);
            if (product_Img == null)
            {
                return NotFound();
            }

            return Ok(product_Img);
        }

        // PUT: api/Admin_Product_Img/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct_Img(int id, Product_Img product_Img)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product_Img.Id)
            {
                return BadRequest();
            }

            db.Entry(product_Img).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Product_ImgExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Admin_Product_Img
        [ResponseType(typeof(Product_Img))]
        public IHttpActionResult PostProduct_Img(Product_Img product_Img)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Product_Imgs.Add(product_Img);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product_Img.Id }, product_Img);
        }

        // DELETE: api/Admin_Product_Img/5
        [ResponseType(typeof(Product_Img))]
        public IHttpActionResult DeleteProduct_Img(int id)
        {
            Product_Img product_Img = db.Product_Imgs.Find(id);
            if (product_Img == null)
            {
                return NotFound();
            }

            db.Product_Imgs.Remove(product_Img);
            db.SaveChanges();

            return Ok(product_Img);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Product_ImgExists(int id)
        {
            return db.Product_Imgs.Count(e => e.Id == id) > 0;
        }
    }
}