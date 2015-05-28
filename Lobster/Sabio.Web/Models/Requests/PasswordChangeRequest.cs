using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Models.Requests
{
    public class PasswordChangeRequest
    {
        [Required]
        public string User { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*(_|[^\w])).+$", ErrorMessage = "Passwords must have at least one non letter or digit character. Passwords must have at least one uppercase ('A'-'Z')")]
        [DataType(DataType.Password)]
        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*(_|[^\w])).+$", ErrorMessage = "Passwords must have at least one non letter or digit character. Passwords must have at least one uppercase ('A'-'Z')")]
        [DataType(DataType.Password)]
        [Required]
        [MinLength(8)]
        public string ConfirmPassword { get; set; }
    }
}