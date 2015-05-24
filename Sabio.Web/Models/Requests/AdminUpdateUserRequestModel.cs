using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class AdminUpdateUserRequestModel
    {

        [Required]
        public int AppUserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int UserType { get; set; }
    }
}