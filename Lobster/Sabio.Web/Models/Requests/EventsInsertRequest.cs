using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel;

namespace Sabio.Web.Models.Requests
{
    public class EventsInsertRequest
    {
        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string AllDay { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Invalid addressLine1")]
        public string AddressLine1 { get; set; }

        [StringLength(50, MinimumLength = 5, ErrorMessage = "Invalid addressLine2")]
        public string AddressLine2 { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Invalid city")]
        public string City { get; set; }


        public int State { get; set; }

        [Required]
        public string Zip { get; set; }


        [Required]
        [StringLength(128, MinimumLength = 1, ErrorMessage = "Invalid title")]
        public string Title { get; set; }

        [StringLength(1500, MinimumLength = 1, ErrorMessage = "Invalid description")]
        public string Description { get; set; }


        public decimal Lat { get; set; }

        public decimal Lng { get; set; }
    }
}