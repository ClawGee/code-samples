using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.request
{
    public class recruiterRequestModel
    {

        [Required]
        [MaxLength(300)]
        public string Industry { get; set; }

        [Required]
        [MaxLength(32)]
        public string Placements { get; set; }

        [Required]
        public string Roles { get; set; }

        [Required]
        public string Locations { get; set; }

        [Required]
        public string Website { get; set; }

        [Required]
        [MaxLength(32)]
        public string CompanyName { get; set; }

        [Required]
        public string PhoneNumbers { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        
        
    }
}