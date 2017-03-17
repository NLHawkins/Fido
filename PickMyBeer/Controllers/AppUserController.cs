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
        // GET: User
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var user = db.PatronClients.Where(p => p.UserId == userId).FirstOrDefault();
            var bList = new BeerCollection();
            foreach (var item in user.FaveBeers)
            {
                Beer beer = db.Beers.Where(b => b.Id == item.BeerId).FirstOrDefault();
                bList.Add(beer);
            }

            ViewBag.UserFaves = user.FaveBeers;
            ViewBag.FaveList = bList;
            
            return View();
        }
        public ActionResult AddBeerToFaves()
        {
            return View();
        }

        public ActionResult ChoosePrefFaves()
        {
            var userId = User.Identity.GetUserId();
            var pc = db.PatronClients.Where(p => p.UserId == userId).FirstOrDefault();
            ViewBag.Faves = pc.FaveBeers;

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

            ViewBag.Pops = db.PopBeers;

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