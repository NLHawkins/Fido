using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PickMyBeer.Models
{
    public class BeerOnTap
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public int BarClientId { get; set; }
        [ForeignKey("BarClientId")]
        public virtual BarClient BarClient { get; set; }

        public int BeerId { get; set; }
        [ForeignKey("BeerId")]
        public virtual Beer Beer { get; set; }

        public DateTime Created { get; set; }

    }
}