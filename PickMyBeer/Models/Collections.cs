using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickMyBeer.Models
{
    public class BeerCollection : Dictionary<int, Beer>
    {
        public void Add(Beer beer)
        {
            this.Add(beer.Id, beer);
        }
    }

    public class IngredientCollection : Dictionary<int, Ingredient>
    {
        public void Add(Ingredient ingredient)
        {
            this.Add(ingredient.Id, ingredient);
        }
    }
}