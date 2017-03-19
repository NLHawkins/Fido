using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickMyBeer.Models
{
    public class PatronClient
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public virtual ICollection<FaveBeer> FaveBeers { get; set; }
        public virtual ICollection<PrevPickedBeer> PrevPicks { get; set; }
    }

    
}