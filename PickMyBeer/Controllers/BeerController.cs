using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using PickMyBeer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PickMyBeer.Controllers
{
    
    public class BeerController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public ActionResult AddBeerToArchive()
        {
            Beer beer = AddBeer();
            AddFaveBeer(beer);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AddBeerToTap()
        {
            Beer beer = AddBeer();
            AddTapBeer(beer);

            return Json("{\"result\":\"ok\"}", JsonRequestBehavior.AllowGet);
        }

        public ActionResult MVCAddBeerToTap()
        {
            Beer beer = AddBeer();
            AddTapBeer(beer);
            return RedirectToAction("Index", "Home");
        }



        public ActionResult AddFaveWinner(int beerId)
        {
            var beer = db.Beers.Where(b => b.Id == beerId).FirstOrDefault();
            AddFaveBeer(beer);
            return RedirectToAction("Index", "Home");

        }
        public ActionResult Details(int beerId)
        {
            Beer beer = db.Beers.Where(b => b.Id == beerId).FirstOrDefault();
            return View(beer);
        }

        private void AddFaveBeer(Beer beer)
        {
            var userId = User.Identity.GetUserId();
            var user = db.PatronClients.Where(p => p.UserId == userId).FirstOrDefault();
            var fb = new FaveBeer();
            fb.BeerId = beer.Id;
            fb.PatronClientId = user.Id;
            fb.UserId = userId;
            fb.Created = DateTime.Now;
            db.FaveBeers.Add(fb);
            db.SaveChanges();
        }

        private void AddTapBeer(Beer beer)
        {
            var userId = User.Identity.GetUserId();
            var user = db.BarClients.Where(p => p.UserId == userId).FirstOrDefault();
            var tb = new BeerOnTap();
            tb.BeerId = beer.Id;
            tb.BarClientId = user.Id;
            tb.UserId = userId;
            tb.Created = DateTime.Now;
            db.BeerOnTaps.Add(tb);
            db.SaveChanges();
        }

        public void AddBeerToFavesCollection()
        {
            AddBeer();
        }

        public ActionResult PrefPickAdd()
        {
            Beer prefBeer = AddBeer();
            var id = prefBeer.Id.ToString();
            return Json(id, JsonRequestBehavior.AllowGet);
        }

        public Beer AddBeer()
        {
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string json = new StreamReader(req).ReadToEnd();
            dynamic beer = JsonConvert.DeserializeObject(json);

            string breweryName = beer.breweries[0].name.ToString();
            var breweryCk = db.Breweries.Where(b => b.Name == breweryName).FirstOrDefault();
            if (breweryCk == null)
            {
                var newBrewery = new Brewery();
                newBrewery.Name = beer.breweries[0].name;
                newBrewery.Website = beer.breweries[0].website;
                newBrewery.City = beer.breweries[0].locations[0].locality;
                newBrewery.State = beer.breweries[0].locations[0].region;
                db.Breweries.Add(newBrewery);
                db.SaveChanges();
            };

            if (beer.style.name != null)
            {
                string styleName = beer.style.name;
                var styleCk = db.Styles.Where(s => s.Name == styleName).FirstOrDefault();
                if (styleCk == null)
                {
                    var newStyle = new Style();
                    newStyle.Name = beer.style.name;
                    db.Styles.Add(newStyle);
                    db.SaveChanges();
                }
            }
            

            if (beer.ibu != null)
            {
                float beerIBU = beer.ibu;
            }
            
            string beerName = beer.name;
            var beerCk = db.Beers.Where(b => b.Name == beerName && b.Brewery.Name == breweryName).FirstOrDefault();
            if (beerCk == null)
            {

                var newBeer = new Beer();

                try
                {
                    newBeer.ABV = beer.abv;
                    newBeer.Description = beer.description;
                    newBeer.IBU = beer.ibu;
                    newBeer.LabelURL = beer.labels.large;
                }
                catch (Exception ex)
                {

                }

                newBeer.Name = beer.name;


                var brewery = db.Breweries.Where(b => b.Name == breweryName).FirstOrDefault();
                newBeer.BreweryId = brewery.Id;

                if(beer.style.name != null)
                {
                    string styleName = beer.style.name;
                    var style = db.Styles.Where(s => s.Name == styleName).FirstOrDefault();
                    newBeer.StyleId = style.Id;
                }

                
                //newBeer.Ingredients = new IngredientCollection();
                if (beer. ingredients != null)
                {
                    if (beer.ingredients.hops != null)
                    {
                        foreach (var ing in beer.ingredients.hops)
                        {
                            var newIng = new Ingredient();
                            string iName = ing.name;
                            newIng.Name = iName;
                            newIng.Id = ing.id;
                            var ingCk = db.Ingredients.Where(i => i.Name == iName).FirstOrDefault();
                            if (ingCk == null)
                            {
                                db.Ingredients.Add(newIng);
                            }
                        }
                    }
                }
                
                db.Beers.Add(newBeer);
                db.SaveChanges();

                if (beer.ingredients != null)
                {
                    if (beer.ingredients.hops != null)
                    {
                        foreach (var ing in beer.ingredients.hops)
                        {
                            var iLog = new IngredientLog();
                            iLog.BeerId = newBeer.Id;
                            string iName = "";
                            try
                            {
                                iName = ing[0].Name;
                            }catch(Exception ex)
                            {

                            }
                            try
                            {
                                iName = ing.Name;
                            }
                            catch (Exception ex)
                            {

                            }
                            var i = db.Ingredients.Where(x => x.Name == iName).FirstOrDefault();
                            iLog.IngredientId = i.Id;
                            db.IngredientLogs.Add(iLog);

                        }
                    }
                }
                return newBeer;
            }
            else
            {
                return beerCk; 
            }
        }
    }
} 