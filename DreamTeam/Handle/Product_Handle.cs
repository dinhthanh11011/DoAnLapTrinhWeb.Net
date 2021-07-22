using DreamTeam.Models;
using DreamTeam.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DreamTeam.Models.Product;

namespace DreamTeam.Handle
{
    public class Product_Handle
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public static List<Product> getAll()
        {
            return db.Products.Include(x => x.Category)
                .Include(x => x.Category.Brand)
                .Include(x => x.Product_Imgs)
                .Include(x => x.Product_Attributes)
                .OrderBy(x => x.Ordering)
                .Where(x => x.Active && x.Category.Active && x.Category.Brand.Active).ToList();
        }

        public static Product getOne(int id)
        {
            return getAll().Where(x => x.Id == id).FirstOrDefault();
        }

        public static List<Product> getBySearch(string searchKey)
        {
            List<Product> pro_list = new List<Product>();
            foreach (var item in getAll())
            {
                if (support.removeVietnameseTone(item.Name.ToLower()).Contains(support.removeVietnameseTone(searchKey.ToLower())))
                {
                    pro_list.Add(item);
                }
            }
            return pro_list;
        }

        public static List<Product> getByCate(int cateId)
        {
            return getAll().Where(x => x.CategoryId == cateId).ToList();
        }

        public static List<Product> getByBrand(int brandId)
        {
            return getAll().Where(x => x.Category.BrandId == brandId).ToList();
        }
    }
}