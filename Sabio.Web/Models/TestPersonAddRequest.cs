using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class TestPersonAddRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Last { get; set; }
        
        [Required]
        public int Age { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}