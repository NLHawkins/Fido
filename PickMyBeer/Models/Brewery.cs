using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickMyBeer.Models
{
    public class Brewery
    {
        public int Id { get; set; }
        public string BreweryDbId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Website { get; set; }

    }
}