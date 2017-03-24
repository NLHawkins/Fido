using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PickMyBeer.Models
{
    public class BarClient
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string BarName { get; set; }
        public virtual BarImageLog BImgLog { get; set; }
        public virtual ICollection<SavedMatch> SavedMatches { get; set; }
        //public virtual ICollection<BeerOnTap> BeerOnTaps { get; set; }
        //public virtual ICollection<BeerArchive> BeerArchives { get; set; }
    }

}

