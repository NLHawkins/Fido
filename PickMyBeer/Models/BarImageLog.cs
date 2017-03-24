using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PickMyBeer.Models
{
    public class BarImageLog
    {
        public int Id { get; set; }
        public int BarImgUpId { get; set; }
        [ForeignKey("BarImgUpId")]
        public virtual ImageUpload ImageUpload { get; set; }
        public int BarClientId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}