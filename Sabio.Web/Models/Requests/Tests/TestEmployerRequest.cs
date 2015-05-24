using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Tests
{
    public class TestEmployerRequest
    {
        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string CompanyName { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string Title { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string CompanyLocation { get; set; }

        [Required]
        public string StartDateMonth { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(4)]
        public string StartDateYear { get; set; }

        [Required]
        public string EndDateMonth { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(4)]
        public string EndDateYear { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Description { get; set; }

        }
}

