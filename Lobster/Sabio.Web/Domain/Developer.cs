using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Developer  
    {

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Language1 { get; set; }

        public string Language2 { get; set; }  
         
        public string Language3 { get; set; }

        public string Language4 { get; set; }

        public string Language5 { get; set; }

        public string Experience1 { get; set; }

        public int YearsExperience1 { get; set; }
        
        public string Experience2 { get; set; }
        
        public int YearsExperience2 { get; set; }
        
        public string Experience3 { get; set; }
        
        public int YearsExperience3 { get; set; }
        
        public string Experience4 { get; set; }
        
        public int YearsExperience4 { get; set; }
        
        public string Summary { get; set; }
        
        public string Goals { get; set; }

        private int appUserId { get; set; }

        public Guid Uid { get; set; }

        public int Id { get; set; }

        public int MediaId { get; set; }

        public double? AverageRating { get; set; }
        public int RatingCount { get; set; }
        public Media DeveloperPhoto { get; set; }

        public FullUser DeveloperPersonalInfo { get; set; }

        public int AddressId;

        public Guid AddressGuid { get; set; }

        public Address DeveloperAddresses { get; set; }

        public State DeveloperState { get; set; }
    }
}