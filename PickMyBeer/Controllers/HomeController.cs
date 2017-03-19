using Microsoft.AspNet.Identity;
using PickMyBeer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PickMyBeer.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult MatchPage(List<Beer> wList)
        {
            ViewBag.One = wList[0];
            ViewBag.Two = wList[1];
            ViewBag.Three = wList[2];
            var winner = wList[0];
            return View(winner);
        }

        [HttpPost]
        public ActionResult PrefPickedFave()
        {
            var beerId = int.Parse(Request.Form["SelectedBeerId"]);
            Beer beer = db.Beers.Where(b => b.Id == beerId).FirstOrDefault();
            ViewBag.BeerId = beer.Id;
            return RedirectToAction("PrefPicked", new {beerId = beer.Id});
        }

        [HttpPost]
        public ActionResult PrefPickedPops()
        {
            var beerId = int.Parse(Request.Form["SelectedBeerId"]);
            Beer beer = db.Beers.Where(b => b.Id == beerId).FirstOrDefault();
            ViewBag.BeerId = beer.Id;
            return RedirectToAction("PrefPicked", new { beerId = beer.Id });
        }

        public ActionResult PrefPickedNew(int beerId)
        {
            Beer beer = db.Beers.Where(b => b.Id == beerId).FirstOrDefault();
            ViewBag.BeerId = beer.Id;
            return RedirectToAction("PrefPicked", new { beerId = beer.Id });
        }

        public ActionResult PrefPicked(int beerId)
        {
            Beer beer = db.Beers.Where(b => b.Id == beerId).FirstOrDefault();
            ViewBag.BeerId = beer.Id;
            return View(beer);
        }

        public ActionResult ChooseLoc(int beerId)
        {
            Beer beer = db.Beers.Where(b => b.Id == beerId).FirstOrDefault();
            ViewBag.BeerId = beerId;
            var model = new ChooseLocViewModel
            {
                Bars = GetLocs(),
                
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ChooseLoc()
        {

            var barId = Request.Form["SelectedBarId"];
            var beerId = Request.Form["BeerId"];

            return RedirectToAction("PickMyBeer", new { barId = barId, beerId = beerId });
        }

        private IEnumerable<SelectListItem> GetLocs()
        {

            var bars = db.BarClients
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.UserName 
                                });

            return new SelectList(bars, "Value", "Text");
        }

        public ActionResult PickMyBeer(int beerId, int barId)
        {
            Beer prefBeer = db.Beers.Where(b => b.Id == beerId).FirstOrDefault();
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
            DateTime t = DateTime.Now;
            //add style matches
            if (prefBeer.Style.Name != null)
            {
                var sMs = availBeers.Where(b => b.StyleId == prefBeer.StyleId);
                foreach (var item in sMs)
                {
                    var match = new Match();
                    match.MatchBeerId = item.Id;
                    match.PrefBeerId = prefBeer.Id;
                    match.TimeStamp = t;
                    match.Score += 30;
                    db.Matches.Add(match);
                }
            }
            //add Brewery Matches and adjust scorring
            if (prefBeer.BreweryId != 0)
            {

                var bMs = availBeers.Where(b => b.BreweryId == prefBeer.BreweryId);
                foreach (var item in bMs)
                {
                    List<Match> r = GetRepeats(item.Id, t);
                    foreach (var reap in r)
                    {
                        if (reap.MatchBeer.BreweryId != 0)
                        {
                            reap.Score += 10;
                            db.Matches.Attach(reap);
                        }
                    }
                    var match = new Match();
                    match.MatchBeerId = item.Id;
                    match.PrefBeerId = prefBeer.Id;
                    match.TimeStamp = t;
                    match.Score += 10;
                }
            }
            //add IBU matches and adjust scores
            if (prefBeer.IBU != 0)
            {
                var ibuMin = prefBeer.IBU - 15;
                var ibuMax = prefBeer.IBU + 15;
                var iMs = availBeers.Where(b => b.IBU >= ibuMin && b.IBU <= ibuMax);
                foreach (var item in iMs)
                {
                    List<Match> r = GetRepeats(item.Id, t);
                    foreach (var reap in r)
                    {
                        if (reap.MatchBeer.IBU != 0)
                        {
                            reap.Score += Convert.ToInt32(Math.Abs(Math.Round(prefBeer.IBU, 0, MidpointRounding.AwayFromZero) - Math.Round(reap.MatchBeer.IBU, 0, MidpointRounding.AwayFromZero)));
                            db.Matches.Attach(reap);
                        }

                    }

                    var im = new Match();
                    im.MatchBeerId = item.Id;
                    im.PrefBeerId = prefBeer.Id;
                    im.Score = Convert.ToInt32(Math.Abs(Math.Round(prefBeer.IBU, 0, MidpointRounding.AwayFromZero) - Math.Round(item.IBU, 0, MidpointRounding.AwayFromZero)));
                    im.TimeStamp = t;
                    db.Matches.Add(im);
                }
            }
            db.SaveChanges();
            var allMList = db.Matches.Where(m => m.MatchBeerId == prefBeer.Id && m.TimeStamp == t).OrderByDescending(s => s.Score);
            var winners = allMList.Take(3).OrderByDescending(s => s.Score).ToList();
            return RedirectToAction("MatchPage", "Home", new { wList = winners });
        }
        public List<Match> GetRepeats(int mbId, DateTime timestamp)
        {
            var ms = db.Matches.Where(m => m.MatchBeerId == mbId && m.TimeStamp == timestamp).ToList();
            return ms;
        }
    }
}