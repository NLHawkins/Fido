using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PickMyBeer.Models
{
    public class ChoosePrefFavesViewModel
    {
        public int? SelectedBeerId { get; set; }
        public IEnumerable<SelectListItem> Beers { get; set; }
    }

    public class ChoosePrefPopsViewModel
    {
        public int? SelectedBeerId { get; set; }
        public IEnumerable<SelectListItem> Beers { get; set; }
    }

    public class ChooseLocViewModel
    {
        public int? SelectedBarId { get; set; }
        public IEnumerable<SelectListItem> Bars { get; set; }
        public int? BeerId { get; set; }
    }

}