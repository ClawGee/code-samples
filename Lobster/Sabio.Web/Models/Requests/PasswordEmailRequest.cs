using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Models.Requests
{
    public class PasswordEmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}