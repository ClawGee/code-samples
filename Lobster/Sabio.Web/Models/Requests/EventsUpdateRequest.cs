using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel;

namespace Sabio.Web.Models.Requests
{
    public class EventsUpdateRequest : EventsInsertRequest
    {
        [Required]
        public Guid AddressGuid { get; set; }

    }
}