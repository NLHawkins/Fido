using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PickMyBeer.Models
{
    public class PopBeer
    {
        public int Id { get; set; }
        public int BeerId { get; set; }
        [ForeignKey("BeerId")]
        public virtual Beer Beer { get; set; }
    }
}