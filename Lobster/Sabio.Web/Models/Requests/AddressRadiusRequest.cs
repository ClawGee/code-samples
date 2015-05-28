using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class AddressRadiusRequest
    {
        [Required]
        public decimal Lat { get; set; }
        
        [Required]
        public decimal Lng { get; set; }

        public decimal Distance { get; set; }
    }
}