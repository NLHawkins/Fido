using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PickMyBeer.Models
{
    public class FaveBeer
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int PatronClientId { get; set; }
        [ForeignKey("PatronClientId")]
        public virtual PatronClient PC { get; set; }

        public int BeerId { get; set; }
        [ForeignKey("BeerId")]
        public virtual Beer Beer { get; set; }

        public DateTime Created { get; set; }

    }
}