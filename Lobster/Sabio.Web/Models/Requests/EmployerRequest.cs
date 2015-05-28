using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Models.Requests
{
    public class EmployerRequest
    {
        public int AppUserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ContactName { get; set; }
        public string Size { get; set; }
        public string Industry { get; set; }
        public string City { get; set; }

        [Required]
        [EmailAddress]
        public string ContactEmail { get; set; }

        [Required]
        [Url]
        public string URL { get; set; }
        public string Country { get; set; }
        public int StateOptions { get; set; }
        public string OtherLocation { get; set; }
        public string Tech { get; set; }

        [Required]
        [MinLength(15)]
        public string Description { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string LinkedIn { get; set; }
        public string[] Tags { get; set; }
      
    }
}



