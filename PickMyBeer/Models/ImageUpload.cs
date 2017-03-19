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
        public DateTime Timestamp { get; set; }
        public string Type { get; set; }

        public virtual string FilePath
        {
            get
            {
                return $"/Uploads/{File}";
            }
        }
    }

    public class PatronBeerImageLog
    {
        public int Id { get; set; }
        public int ImgUploadId { get; set; }
        [ForeignKey("ImgUploadId")]
        public virtual ImageUpload ImgUpload { get; set; }
        public int PatronClientId { get; set; }
        [ForeignKey("PatronClientId")]
        public virtual PatronClient PatronClient { get; set; }
        public int BeerId { get; set; }
        [ForeignKey("BeerId")]
        public virtual Beer Beer { get; set; }
        public DateTime Timestamp { get; set; }

    }

    public class PatronProfileImageLog
    {
        public int Id { get; set; }
        public int PProfImgUpId { get; set; }
        [ForeignKey("ImgUploadId")]
        public virtual ImageUpload ImgUpload { get; set; }
        public int PatronClientId { get; set; }
        [ForeignKey("PatronClientId")]
        public virtual PatronClient PatronClient { get; set; }
        public DateTime Timestamp { get; set; }

    }

    public class BarImageLog
    {
        public int Id { get; set; }
        public int BarImgUpId { get; set; }
        [ForeignKey("ImgUploadId")]
        public virtual ImageUpload ImgUpload { get; set; }
        public int BarClientId { get; set; }
        [ForeignKey("BarClientId")]
        public virtual BarClient BarClient { get; set; }
        public DateTime Timestamp { get; set; }

    }
}