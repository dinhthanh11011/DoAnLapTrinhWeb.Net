using DreamTeam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DreamTeam.Models.Product;

namespace DreamTeam.Handle
{
    public class Brand_Handle
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public static List<Brand> getAll()
        {
            return db.Brands
                .OrderBy(x => x.Ordering)
                .Where(x => x.Active).ToList();
        }
    }
}