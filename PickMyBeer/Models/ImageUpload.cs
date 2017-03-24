using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string Type { get; set; }

        public virtual string FilePath
        {
            get
            {
                return $"/Uploads/{File}";
            }
        }
    }
}