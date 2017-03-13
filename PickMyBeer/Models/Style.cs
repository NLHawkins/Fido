using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickMyBeer.Models
{
    public class Style
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual BeerCollection Beers { get; set; }
    }
}