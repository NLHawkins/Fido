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
            return RedirectToAction("Index");
        }
        public void AddBeerToFavesCollection()
        {
            Beer beer = AddBeer();
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

            string styleName = beer.style.name;
            var styleCk = db.Styles.Where(s => s.Name == styleName).FirstOrDefault();
            if (styleCk == null)
            {
                var newStyle = new Style();
                newStyle.Name = beer.style.name;
                db.Styles.Add(newStyle);
                db.SaveChanges();
            }
            int beerIBU = beer.ibu;
            string beerName = beer.name;
            var beerCk = db.Beers.Where(b => b.Name == beerName && b.IBU == beerIBU).FirstOrDefault();
            if (beerCk == null)
            {

                var newBeer = new Beer();
                newBeer.ABV = beer.abv;
                newBeer.Description = beer.description;
                newBeer.IBU = beer.ibu;
                newBeer.LabelURL = beer.labels.large;
                newBeer.Name = beer.name;


                var brewery = db.Breweries.Where(b => b.Name == breweryName).FirstOrDefault();
                newBeer.BreweryId = brewery.Id;

                var style = db.Styles.Where(s => s.Name == styleName).FirstOrDefault();
                newBeer.StyleId = style.Id;
                newBeer.Ingredients = new IngredientCollection();
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
                    newBeer.Ingredients.Add(newIng.Id, newIng);
                }
                db.Beers.Add(newBeer);
                db.SaveChanges();
                return newBeer;
            }
            else
            {
                return beerCk; 
            }
        }
    }
} 