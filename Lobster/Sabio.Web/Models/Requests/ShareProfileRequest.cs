using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sabio.Web.Models.Requests
{
    public class ShareProfileRequest
    {
        [Required]
        //[StringLength(3, MinimumLength = 3, ErrorMessage = "Invalid")]
        public string SenderName { get; set; }

        [Required]
        [EmailAddress]
        public string SenderEmail { get; set; }

        [Required]
        //[StringLength(3, MinimumLength = 3, ErrorMessage = "Invalid")]
        public string RecipientName { get; set; }

        [Required]
        [EmailAddress]
        public string RecipientEmail { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}