using DreamTeam.Models;
using DreamTeam.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DreamTeam.Handle
{
    public class Advertisements_Handle
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public static dynamic getAll()
        {
            return db.Advertisements.OrderBy(x => x.Ordering).Select(x => new { x.Title, x.Link, x.Description, Image = support.UPLOAD_FOLDER_NAME + "/" + x.Image }).ToList();
        }
    }
}