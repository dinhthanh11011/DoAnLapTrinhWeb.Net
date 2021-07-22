using DreamTeam.Models.Product;
using DreamTeam.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DreamTeam.ViewModels
{
    public class Product_Index_Object
    {
        public List<Product> Products { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Category> Categories { get; set; }
        public List<Advertisement> Advertisements { get; set; }
    }
}