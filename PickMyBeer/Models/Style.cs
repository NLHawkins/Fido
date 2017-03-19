using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PickMyBeer.Models
{
    public class Style
    {
        public int Id { get; set; }
        public string BreweryDbId { get; set; }
        public string Name { get; set; }

    }
}