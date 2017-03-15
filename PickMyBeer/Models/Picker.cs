using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickMyBeer.Models
{
    public class Picker
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public BeerCollection PrefBeers { get; set; }
    }
}