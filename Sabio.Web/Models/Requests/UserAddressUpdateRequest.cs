using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class UserAddressUpdateRequest : UserAddressModelRequest
    {
        [Required]
        public Guid Uid { get; set; }
    }
}