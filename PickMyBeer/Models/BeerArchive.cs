﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PickMyBeer.Models
{
    public class BeerArchive
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BarClientId { get; set; }
        public int BeerId { get; set; }
        public bool OnTap { get; set; }
        public DateTime Created { get; set; }
        [ForeignKey("BarClientId")]
        public BarClient BC{ get; set; }
        [ForeignKey("BeerId")]
        public Beer Beer { get; set; }

    }
}