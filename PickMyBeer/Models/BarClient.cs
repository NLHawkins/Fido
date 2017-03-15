using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickMyBeer.Models
{
    public class BarClient
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public BeerCollection BeersOnTap { get; set; }
        public BeerCollection BeerArchive { get; set; }
    }
}