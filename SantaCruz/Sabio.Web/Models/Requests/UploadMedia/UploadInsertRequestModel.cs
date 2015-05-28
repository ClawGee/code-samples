using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class UploadInsertRequestModel
    {
        
        [Required]
        public string Filename { get; set; }

        [Required]
        public int EntityType { get; set; }

        [Required]
        public int EntityId { get; set; }

        [Required]
        public int MediaType { get; set; }

        [Required]
        public string ContentType { get; set; }

    }
}