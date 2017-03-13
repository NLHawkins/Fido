using System;
using System.Collections.Generic;
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
        public IngredientCollection Ingredients { get; set; }
        public string LabelURL { get; set; }

        public virtual Brewery Brewery { get; set; }
        public virtual Style Style { get; set; }
    }

    public class ChooseBeerModel
    {

    }
}