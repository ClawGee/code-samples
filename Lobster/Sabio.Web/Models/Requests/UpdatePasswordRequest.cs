using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class UpdatePasswordRequest
    {
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*(_|[^\w])).+$", ErrorMessage = "Passwords must be at least 8 characters and have at least one non letter or digit character. Passwords must have at least one uppercase ('A'-'Z')")]
        [DataType(DataType.Password)]
        public string currentPassword { get; set; }
        
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*(_|[^\w])).+$", ErrorMessage = "Passwords must be at least 8 characters and have at least one non letter or digit character. Passwords must have at least one uppercase ('A'-'Z')")]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("newPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string verifyNewPassword { get; set; }

    }
}