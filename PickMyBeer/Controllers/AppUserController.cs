using Microsoft.AspNet.Identity;
using PickMyBeer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PickMyBeer.Controllers
{
    public class AppUserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult MyFaves()
        {
            var userId = User.Identity.GetUserId();
            var user = db.PatronClients.Where(p => p.UserId == userId).FirstOrDefault();
            ViewBag.Faves = user.FaveBeers.ToList();
            return View();
        }

        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            if (user == null)
            {
                return RedirectToAction("Login","Account");
            }
            return View();
        }
        public ActionResult MyPrevs()
        {
            var userId = User.Identity.GetUserId();
            var user = db.PatronClients.Where(p => p.UserId == userId).FirstOrDefault();
            ViewBag.PMs = user.SavedMatches;
            return View();
        }

        public ActionResult MyBeers()
        {
            var userId = User.Identity.GetUserId();
            var user = db.PatronClients.Where(p => p.UserId == userId).FirstOrDefault();
            var bList = new BeerCollection();

            ViewBag.PrevPicks = user.PrevPicks;
            return View();
        }
        public ActionResult AddBeerToFaves()
        {
            return View();
        }
        public ActionResult ChoosePref()
        {
            return View();
        }
        [Authorize]
        public ActionResult MyProfile()
        {
            if (User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = 'a' });
            }
            var pc = GetPC();
            return View(pc);
        }

        public PatronClient GetPC()
        {
            var userId = User.Identity.GetUserId();
            var pc = db.PatronClients.Where(b => b.UserId == userId).FirstOrDefault();
            return pc;
        }

        public IEnumerable<FaveBeer> GetFaves()
        {
            var pc = GetPC();
            var fbs = pc.FaveBeers;
            return fbs; 
        }

        public IEnumerable<PrevPickedBeer> GetPrevPicks()
        {
            var pc = GetPC();
            var pps= pc.PrevPicks;
            return pps;
        }

        public ActionResult ChoosePrefFaves()
        {
            var userId = User.Identity.GetUserId();
            var pc = db.PatronClients.Where(p => p.UserId == userId).FirstOrDefault();
            ViewBag.Faves = pc.FaveBeers.ToList();

            var model = new ChoosePrefFavesViewModel
            {
                Beers = GetBeers()
            };
            return View(model);
        }

        public ActionResult ChoosePrefNew()
        {

            return View();
        }

        public ActionResult ChoosePrefPops()
        {

            ViewBag.Pops = db.PopBeers.ToList();
            
            var model = new ChoosePrefPopsViewModel
            {
                Beers = GetPopBeers()
            };
            return View(model);
        }

        private IEnumerable<SelectListItem> GetBeers()
        {

            var beers = db.FaveBeers
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Beer.Id.ToString(),
                                    Text = x.Beer.Name + "-" + x.Beer.Brewery.Name
                                });

            return new SelectList(beers, "Value", "Text");
        }

        private IEnumerable<SelectListItem> GetPopBeers()
        {

            var beers = db.PopBeers
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Beer.Id.ToString(),
                                    Text = x.Beer.Name + "-" + x.Beer.Brewery.Name
                                });

            return new SelectList(beers, "Value", "Text");
        }
    }
}