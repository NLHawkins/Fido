using PickMyBeer.Extensions;
using PickMyBeer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using System.IO;

namespace PickMyBeer.Controllers
{
    public class BarUserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Details(int barId)
        {
            var bots = db.BeerOnTaps.Where(b => b.BarClientId == barId).ToList();
            var availBeers = new List<Beer>();
            foreach (var item in bots)
            {
                Beer b = db.Beers.Where(c => c.Id == item.BeerId).FirstOrDefault();
                availBeers.Add(b);
            }
            var bips = db.BeerInPkgs.Where(b => b.BarClientId == barId).ToList();
            foreach (var item in bips)
            {
                Beer b = db.Beers.Where(c => c.Id == item.BeerId).FirstOrDefault();
                availBeers.Add(b);
            }
            ViewBag.ABs = availBeers;
            var bc = db.BarClients.SingleOrDefault(b => b.Id == barId);
            return View(bc);
        }

        public ActionResult BarList()
        {
            ViewBag.Bars = db.BarClients.ToList();
            return View();
        }

        public ActionResult ReviewMatches()
        {
            var userId = User.Identity.GetUserId();
            var user = db.BarClients.Where(p => p.UserId == userId).FirstOrDefault();
            ViewBag.PMs = user.SavedMatches;
            return View();
        }
        public ActionResult Index()
        {

            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login");
            }
            var bc = db.BarClients.SingleOrDefault(s => s.UserId == userId);
            return View(bc);

        }

        public ActionResult ManageBeers()
        {
            var userId = User.Identity.GetUserId();
            var bc = db.BarClients.SingleOrDefault(s => s.UserId == userId);
            var onTaps = db.BeerOnTaps.Where(b => b.BarClientId == bc.Id);
            ViewBag.OnTap = onTaps.ToList();
            var inPkgs = db.BeerInPkgs.Where(a => a.BarClientId == bc.Id);
            ViewBag.InPkgs = inPkgs.ToList();
            return View();
        }

        public ActionResult AddBarLogo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddLogo()
        {
            var uploadedFile = Request.Files[0];
            string filename = $"{DateTime.Now.Ticks}{uploadedFile.FileName}";
            var serverPath = Server.MapPath(@"~\Uploads");
            var fullPath = Path.Combine(serverPath, filename);
            uploadedFile.SaveAs(fullPath);

            var image = new ImageUpload
            {
                File = filename,
                Type = "logo"
                
            };
            db.Images.Add(image);

            var userId = User.Identity.GetUserId();
            var bc = db.BarClients.SingleOrDefault(b => b.UserId == userId);
            var imagelog = new BarImageLog
            {
                BarImgUpId = image.Id,
                BarClientId = bc.Id,
                Timestamp = DateTime.Now
            };
            db.BarImageLogs.Add(imagelog);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult AddBeerOnTap()
        {
            ViewBag.UserRole = User.Identity.GetUserRole();
            var userId = User.Identity.GetUserId();
            var bc = db.BarClients.Where(b => b.UserId == userId).FirstOrDefault();
            ViewBag.BCId = bc.Id;
            return View();
        }

        public ActionResult AddBeerToPops()
        {

            return View();
        }

        public ActionResult AddBeerInPkg()
        {
            ViewBag.UserRole = User.Identity.GetUserRole();
            var userId = User.Identity.GetUserId();
            var bc = db.BarClients.Where(b => b.UserId == userId).FirstOrDefault();
            ViewBag.BCId = bc.Id;
            return View();
        }

        public List<Beer> GetAvailableBeers(int barClientId)
        {
            List<Beer> aBeers = new List<Beer>();
            var beerOnTaps = db.BeerOnTaps.Where(b => b.BarClientId == barClientId).ToList();
            foreach (var b in beerOnTaps)
            {
                aBeers.Add(b.Beer);
            }
            var beerInPkgs = db.BeerInPkgs.Where(b => b.BarClientId == barClientId).ToList();
            foreach (var b in beerInPkgs)
            {
                aBeers.Add(b.Beer);
            }
            return aBeers;
        }

        public ActionResult GetBeersOnTap(int barClientId)
        {
            var beerOnTaps =  db.BeerOnTaps.Where(b => b.BarClientId == barClientId).ToList().OrderByDescending(c => c.Created);
            return Json(beerOnTaps, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBeersInPops()
        {
            var beerInPops = db.PopBeers.ToList();
            return Json(beerInPops, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBeerInPkgs(int barClientId)
        {
            var beerInPkgs = db.BeerInPkgs.Where(b => b.BarClientId == barClientId).ToList();
            return Json(beerInPkgs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveBeerFromTap(int beerId)
        {
            var beer = db.BeerOnTaps.Where(b => b.BeerId == beerId).FirstOrDefault();
            db.BeerOnTaps.Remove(beer);
            return Json("{\"result\":\"ok\"}", JsonRequestBehavior.AllowGet);
        }
    }
}