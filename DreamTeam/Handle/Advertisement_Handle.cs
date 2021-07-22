using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DreamTeam.Models;
using DreamTeam.Models.Store;

namespace DreamTeam.Handle
{
    public class Advertisement_Handle
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public static List<Advertisement> getAll()
        {
            return db.Advertisements.OrderBy(x => x.Ordering).Where(x => x.Active).ToList();
        }
    }
}