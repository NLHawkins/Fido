using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PickMyBeer.Models
{
    public class SavedMatch
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        [ForeignKey("MatchId")]
        public virtual Match Match { get; set; }
        public int PCId { get; set; }
        [ForeignKey("PCId")]
        public virtual PatronClient PC { get; set; }
        public int BCId { get; set; }
        [ForeignKey("BCId")]
        public virtual BarClient BC { get; set; }
    }
}