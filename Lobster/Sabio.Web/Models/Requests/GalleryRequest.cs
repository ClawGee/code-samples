using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class GalleryRequest
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string title { get; set; }

        public string description { get; set; }

        public int isPublic { get; set; }

        private int appUserId { get; set; }

    }
}