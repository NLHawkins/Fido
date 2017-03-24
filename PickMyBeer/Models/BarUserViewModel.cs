using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PickMyBeer.Models
{
    public class BarUserViewModel
    {
        [Display(Name = "Your Logo")]
        public string File { get; set; }
    }
}