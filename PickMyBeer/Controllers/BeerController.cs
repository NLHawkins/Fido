using Newtonsoft.Json;
using PickMyBeer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PickMyBeer.Controllers
{
    
    public class BeerController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public ActionResult AddBeer()
        {
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string json = new StreamReader(req).ReadToEnd();
            dynamic beer = JsonConvert.DeserializeObject(json);

            if (!db.Breweries.Find(beer.breweries.name))
            {
                var newBrewery = new Brewery();
                newBrewery.Name = beer.breweries.name;
                newBrewery.Website = beer.breweries.website;
                newBrewery.City = beer.breweries.locations.locality;
                newBrewery.State = beer.breweries.locations.region;
                db.Breweries.Add(newBrewery);
            };

            if (!db.Styles.Find(beer.style.name))
            {
                var newStyle = new Style();
                newStyle.Name = beer.style.name;
                db.Styles.Add(newStyle);
            }

            if (!db.Beers.Find(beer.name))
            {
                
                var newBeer = new Beer();
                newBeer.ABV = beer.abv;
                newBeer.Description = beer.description;
                newBeer.IBU = beer.ibu;
                newBeer.LabelURL = beer.labels.large;
                newBeer.Name = beer.name;

                string brewName = beer.breweries.name;
                var brewery = db.Breweries.Where(b => b.Name == brewName).FirstOrDefault();
                newBeer.BreweryId = brewery.Id;

                string styleName = beer.style.name;
                var style = db.Styles.Where(s => s.Name == styleName).FirstOrDefault();
                newBeer.StyleId = style.Id;

                foreach (var ing in beer.ingredients.hops)
                {
                    var newIng = new Ingredient();
                    newIng.Name = ing.name;
                    if (!db.Ingredients.Find(ing.name))
                    {
                        db.Ingredients.Add(newIng);
                    }
                    newBeer.Ingredients.Add(newIng);
                }
                db.Beers.Add(newBeer);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
} 