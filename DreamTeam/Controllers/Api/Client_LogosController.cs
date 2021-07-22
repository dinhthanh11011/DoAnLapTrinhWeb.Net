using DreamTeam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DreamTeam.Controllers.Api
{
    public class Client_LogosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public dynamic Get()
        {
            return db.Logos.Select(x=>new { Name = Support.support.UPLOAD_FOLDER_NAME + "/" + x.Name}).FirstOrDefault();
        }
    }
}
