using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sabio.Web.Models.Requests
{
    public class AccountUpdateRequest
    {
        //[Required]
        //public string Uid { get; set; }
        [Required]
        [MaxLength(32)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(32)]
        public string LastName { get; set; }
    }
}