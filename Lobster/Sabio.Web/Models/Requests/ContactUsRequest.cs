using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sabio.Web.Models.Requests
{
    public class ContactUsRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

    }
}