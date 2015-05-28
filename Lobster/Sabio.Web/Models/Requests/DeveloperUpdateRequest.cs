using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class DeveloperUpdateRequest : DeveloperProfileCreateRequest
    {
        [Required]
        public Guid AddressGuid { get; set; }
    }
}