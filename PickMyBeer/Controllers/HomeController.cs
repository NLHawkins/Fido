using Microsoft.AspNet.Identity;
using PickMyBeer.Extensions;
using PickMyBeer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var userRole = User.Identity.GetUserRole();
            if (userRole == "BarClient")
            {
                return RedirectToAction("Index","BarUser");
            }
            else
            {
                return RedirectToAction("Index","AppUser");
            }

            
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult MatchPage()
        {
            var winners = TempData["wins"] as List<Match>;
            return View(winners);
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
            ViewBag.Locs = db.BarClients.ToList();
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
        List<Match> MList = new List<Match>();

        public ActionResult PickMyBeer(int beerId, int barId)
        {
            MList = new List<Match>();
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
            var sameBeer = availBeers.Where(b => b.Id == prefBeer.Id).FirstOrDefault();
            if (sameBeer != null)
            {
                availBeers.Remove(sameBeer);
            }


            var now = DateTime.Now;
            DateTime t = now.AddSeconds(-now.Second);

            //add style matches
            if (prefBeer.Style.Name != null)
            {
                var sMs = availBeers.Where(b => b.StyleId == prefBeer.StyleId).ToList();
                foreach (var item in sMs)
                {
                    var match = new Match();
                    match.MatchBeerId = item.Id;
                    match.PrefBeerId = prefBeer.Id;
                    match.TimeStamp = t;
                    match.Score += 30;
                    db.Matches.Add(match);
                    MList.Add(match);
                }
            }
            //add Brewery Matches and adjust scorring
            if (prefBeer.BreweryId != 0)
            {
                var bMs = availBeers.Where(b => b.BreweryId == prefBeer.BreweryId).ToList();
                foreach (var item in bMs)
                {
                    if (CkRepeatsFromMList(item.Id) == true)
                    {
                        List<Match> r = GetRepeatsFromMList(item.Id);
                        foreach (var reap in r)
                        {
                            if (reap.MatchBeer.BreweryId != 0)
                            {
                                reap.Score += 10;
                                //db.Entry(reap).State = EntityState.Modified;
                                //bMs.Remove(reap.MatchBeer);
                            }
                        }
                    }
                    else
                    {
                        var match = new Match();
                        match.MatchBeerId = item.Id;
                        match.PrefBeerId = prefBeer.Id;
                        match.TimeStamp = t;
                        match.Score += 10;
                        MList.Add(match);
                    }
                }
            }
            //add IBU matches and adjust scores
            if (prefBeer.IBU != 0)
            {
                var ibuMin = prefBeer.IBU - 15;
                var ibuMax = prefBeer.IBU + 15;
                var iBUMs = availBeers.Where(b => b.IBU >= ibuMin && b.IBU <= ibuMax).ToList();
                foreach (var item in iBUMs)
                {
                    if (CkRepeatsFromMList(item.Id) == true)
                    {
                        List<Match> r = GetRepeatsFromMList(item.Id);
                        foreach (var reap in r)
                        {

                            reap.Score += (15 -(Convert.ToInt32(Math.Abs(Math.Round(prefBeer.IBU, 0, MidpointRounding.AwayFromZero) - Math.Round(reap.MatchBeer.IBU, 0, MidpointRounding.AwayFromZero)))));
                            //db.Entry(reap).State = EntityState.Modified;
                            //iBUMs.Remove(reap.MatchBeer);
                        }
                    }
                    else
                    {
                        var im = new Match();
                        im.MatchBeerId = item.Id;
                        im.PrefBeerId = prefBeer.Id;
                        im.Score = 15 - (Convert.ToInt32(Math.Abs(Math.Round(prefBeer.IBU, 0, MidpointRounding.AwayFromZero) - Math.Round(item.IBU, 0, MidpointRounding.AwayFromZero))));
                        im.TimeStamp = t;
                        db.Matches.Add(im);
                        MList.Add(im);
                    }
                }
            }
            // Add Ing Matches and Adjust Match Scores
            var iMs = new List<Beer>();
            var pbILogs = db.IngredientLogs.Where(i => i.BeerId == prefBeer.Id).ToList();
            if (pbILogs.Count > 0)
            {
                foreach (var b in availBeers)
                {
                    var bILogs = db.IngredientLogs.Where(i => i.BeerId == b.Id).ToList();
                    if (bILogs.Count > 0)
                    {
                        foreach(var log in bILogs)
                        {
                            var logMatches = pbILogs.Where(i => i.IngredientId == log.IngredientId).ToList();
                            foreach (var match in logMatches)
                            {
                                var bMatch = match.Beer;
                                var _id = bMatch.Id;
                                var reapeat = MList.Where(m => m.MatchBeerId == _id).FirstOrDefault();
                                if (reapeat != null)
                                {
                                    reapeat.Score += 5; 
                                    //db.Entry(reapeat).State = EntityState.Modified;
                                    //logMatches.Remove(match);
                                }
                                else
                                {
                                    var ingM = new Match();
                                    ingM.PrefBeerId = prefBeer.Id;
                                    ingM.MatchBeerId = _id;
                                    ingM.Score = 5;
                                    ingM.TimeStamp = t;
                                    db.Matches.Add(ingM);
                                    MList.Add(ingM);
                                }  
                            }
                        }
                    }
                }
            }
            db.SaveChanges();
            
            var List = MList;
            var winner = MList.OrderByDescending(s => s.Score).ToList();
            ViewBag.MatchList = winner;
            ViewBag.Winner = winner[0];
            ViewBag.Score = winner[0].Score;
            ViewBag.TopScore = true;
            ViewBag.T = t;
            ViewBag.Next = true;
            ViewBag.Pg = 1;
            ViewBag.BarId = barId;
            return RedirectToAction("ShowMatch", new {match = 1, t = t, barId = barId});
        }

        public ActionResult ShowMatch(int match, DateTime t, int barId)
        {
            var time = t.AddMilliseconds(t.Millisecond * -1);
            var _Matches = db.Matches.Where(m => m.TimeStamp > time).ToList();
            var list = _Matches.OrderByDescending(s => s.Score).ToList();
            if (match >= list.Count)
            {
                ViewBag.Next = false;
            }
            else
            {
                ViewBag.Next = true;
            }
            if (match == 1)
            {
                ViewBag.TopScore = true;
            }
            else
            {
                ViewBag.TopScore = false;
            }
            if (match - 1 <= 0)
            {
                ViewBag.Prev = false;
            }
            else
            {
                ViewBag.Prev = true;
            }

            var _m = list[match - 1];
            var pageScore = list[match - 1].Score;
            var topScore = list[0].Score;
            double fracScore = (double)pageScore / topScore;
            var score = fracScore * 100;
            //var score = (list[match - 1].Score) / (list[0].Score) * 100;
            //ViewBag.Score = list[match - 1].Score;
            ViewBag.Score = score;
            if (score == 100)
            {
                ViewBag.TopScore = true;
            }
            if (_m.MatchBeer.StyleId == _m.PrefBeer.StyleId)
            {
                ViewBag.StyleM = true;
            }
            var ibuDiff = _m.MatchBeer.IBU - _m.PrefBeer.IBU;
            if (ibuDiff >= -15 && ibuDiff <= 15)
            {
                ViewBag.IBU = true;
            }

            ViewBag.T = t;
            ViewBag.BarId = barId;
            ViewBag.Match = list[match - 1];
            ViewBag.NPg = ++match;
            ViewBag.PPg = match - 2;
            return View();
        }

        public ActionResult SaveMatch(int match, DateTime t, int barId)
        {
            var time = t.AddMilliseconds(t.Millisecond * -1);
            var _Matches = db.Matches.Where(m => m.TimeStamp > time).ToList();
            var list = _Matches.OrderByDescending(s => s.Score).ToList();
            var matchSv = list[match - 1];
            var userId = User.Identity.GetUserId();
            var pcid = db.PatronClients.SingleOrDefault(s => s.UserId == userId);
            var savedMatch = new SavedMatch()
            {
                BCId = barId,
                PCId = pcid.Id,
                Match = matchSv
            };
            db.SavedMatches.Add(savedMatch);
            db.SaveChanges();
            return RedirectToAction("MyPrevs", "AppUser", new {barId = barId});

        }

        public List<Match> GetRepeats(int mbId, DateTime timestamp)
        {
            var ms = db.Matches.Where(m => m.MatchBeerId == mbId && m.TimeStamp == timestamp).ToList();
            return ms;
        }

        public List<Match> GetRepeatsFromMList(int mbId)
        {
            var ms = MList.Where(m => m.MatchBeerId == mbId).ToList();
            return ms;
        }

        public bool CkRepeats(int mbId, DateTime timestamp)
        {
            var ms = db.Matches.Where(m => m.MatchBeerId == mbId && m.TimeStamp == timestamp).ToList();
            if (ms.Count > 0)
            { return true; }
            else { return false; }
        }

        public bool CkRepeatsFromMList(int mbId)
        {
            var ms = MList.Where(m => m.MatchBeerId == mbId).ToList();
            if (ms.Count > 0)
            { return true; }
            else { return false; }
        }

        public DateTime Trim(DateTime date, long ticks)
        {
            return new DateTime(date.Ticks - (date.Ticks % ticks), date.Kind);
        }

    }
}