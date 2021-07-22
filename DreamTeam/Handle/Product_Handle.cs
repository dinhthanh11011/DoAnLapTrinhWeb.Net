using DreamTeam.Models;
using DreamTeam.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DreamTeam.Handle
{
    public class Product_Handle
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public static dynamic getAll()
        {
            return db.Products.Include(x=>x.Category)
                .Include(x => x.Category.Brand)
                .Include(x => x.Product_Imgs).OrderBy(x => x.Ordering).ToList();
        }
    }
}