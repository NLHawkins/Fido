using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickMyBeer.Models
{
    public class Patron
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public BeerCollection PrefBeers { get; set; }
    }
}