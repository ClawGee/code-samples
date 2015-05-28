using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Models.Requests
{
    public class DeleteMediaRequest
    {
        [Required]
        public String FileName { get; set; }

        [Required]
        public int EntityType { get; set; }


        public int EntityId { get; set; }
    }
}