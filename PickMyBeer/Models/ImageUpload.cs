using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PickMyBeer.Models
{
    public class ImageUploadViewModel
    {
        [Required]
        public HttpPostedFile File { get; set; }
    }

    public class ImageUpload
    {
        public int Id { get; set; }
        public string File { get; set; }
        public int BeerId { get; set; }
        public int PatronId { get; set; }
        public int BarId { get; set; }

        public virtual string FilePath
        {
            get
            {
                return $"/Uploads/{File}";
            }
        }
    }
}