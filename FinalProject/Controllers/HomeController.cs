using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            ViewBag.news = from n in db.News.Take(20)
                           where n.EventOrNews == false
                           orderby n.DatePosted descending
                           select n;

            ViewBag.events = from e in db.News.Take(20)
                             where e.EventOrNews == true
                             orderby e.DatePosted descending
                             select e;

            ViewBag.players = from p in db.Users.Take(25)
                              where p.Position != null
                              orderby p.Fullname
                              select p;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}