using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChatApplication.Models;

namespace ChatApplication.Controllers
{
    public class HomeController : Controller
    {
        MessContext db = new MessContext();
        public ActionResult Index()
        {
            return View(db.Messages);
        }

    }
}