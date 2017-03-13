using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PickMyBeer.Models
{
    public class Bar
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public BeerCollection BeersOnTap { get; set; }
        public BeerCollection BeerCollection { get; set; }

    }
}