using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PickMyBeer.Models
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float ABV { get; set; }
        public float IBU { get; set; }
        public int StyleId { get; set; }
        public int BreweryId { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public string LabelURL { get; set; }

        public virtual Brewery Brewery { get; set; }
        public virtual Style Style { get; set; }

        public Beer()
        {
            Ingredients = new IngredientCollection(); 
        }
    }

    public class BeerDetailsViewModel
    {
        [Display(Name = "Beer Name")]
        public string Name { get; set; }
        public string Desc { get; set; }
        [Display(Name = "ABV")]
        public float ABV { get; set; }
        [Display(Name = "IBU")]
        public float IBU { get; set; }
        public string Style { get; set; }
        public string BreweryName { get; set; }
        public List<String> Hops { get; set; }
        public string LabelURL { get; set; }

    }

    public class ChooseBeerModel
    {

    }
}