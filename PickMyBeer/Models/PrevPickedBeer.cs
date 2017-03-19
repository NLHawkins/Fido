using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PickMyBeer.Models
{
    public class PrevPickedBeer
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PrefBeerId { get; set; }
        public int MatchBeerId { get; set; }
        public DateTime Created { get; set; }
        public int PatronClientId { get; set; }
        [ForeignKey("PrefBeerId")]
        public virtual Beer PrefBeer { get; set; }
        [ForeignKey("MatchBeerId")]
        public virtual Beer MatchBeer { get; set; }

    }
}