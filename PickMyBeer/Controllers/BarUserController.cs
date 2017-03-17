using PickMyBeer.Extensions;
using PickMyBeer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;

namespace PickMyBeer.Controllers
{
    public class BarUserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            ViewBag.UserRole = User.Identity.GetUserRole();
            var userId = User.Identity.GetUserId();
            var bc = db.BarClients.Where(b => b.UserId == userId).FirstOrDefault();
            ViewBag.BCId = bc.Id;
            return View();
        }

        public ActionResult GetBeersOnTap(int barClientId)
        {
            var beerOnTaps =  db.BeerOnTaps.Where(b => b.BarClientId == barClientId).ToList();
            return Json(beerOnTaps, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveBeerFromTap(int beerId)
        {
            var beer = db.BeerOnTaps.Where(b => b.BeerId == beerId).FirstOrDefault();
            db.BeerOnTaps.Remove(beer);
            return Json("{\"result\":\"ok\"}", JsonRequestBehavior.AllowGet);
        }
    }
}