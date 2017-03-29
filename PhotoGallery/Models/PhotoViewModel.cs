using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoGallery.Models
{
    public class PhotoViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Summary { get; set; }
        [Required]
        public string Uri { get; set; }
        public int AlbumId { get; set; }
        public string AlbumTitle { get; set; }
        public DateTime DateUploaded { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}