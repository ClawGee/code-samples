using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class AddressCreateRequest   
    {
        [Required]
        [MaxLength(128)]
        public string AddressLine1 { get; set; }

        [MaxLength(128)]
        public string AddressLine2 { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        public int State { get; set; }

        [Required]
        [MaxLength(10)]
        [MinLength(5)]
        public string Zip { get; set; }

       // [Required]
        public decimal Lat { get; set; }
        
       // [Required]
        public decimal Lng { get; set; }

    }
}