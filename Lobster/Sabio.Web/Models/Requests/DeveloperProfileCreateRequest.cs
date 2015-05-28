using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class DeveloperProfileCreateRequest
    {

        [Required]
        public string Phone { get; set; }

        [Required]  
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Language1 { get; set; }
        [Required]
        public string Language2 { get; set; }

        public string Language3 { get; set; }
        public string Language4 { get; set; }
        public string Language5 { get; set; }

        [Required]
        public string Experience1 { get; set; }
        [Required]
        public int YearsExperience1 { get; set; }
        [Required]
        public string Experience2 { get; set; }
        [Required]
        public int YearsExperience2 { get; set; }
   
        public string Experience3 { get; set; }
        public int YearsExperience3 { get; set; }
        public string Experience4 { get; set; }
        public int YearsExperience4 { get; set; }
        public string Summary { get; set; }
        public string Goals { get; set; }
        //public string Uid { get; set; }
        //public int Id { get; set; }
        public int AppUserId { get; set; }

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