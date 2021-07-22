using DreamTeam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DreamTeam.Models.Product;

namespace DreamTeam.Handle
{
    public class Category_Handle
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public static List<Category> getAll()
        {
            return db.Categories
                .Include(x => x.Brand)
                .OrderBy(x => x.Ordering)
                .Where(x => x.Active && x.Brand.Active).ToList();
        }

        public static Category getOne(int id)
        {
            return getAll().Where(x => x.Id == id).FirstOrDefault();
        }
    }
}