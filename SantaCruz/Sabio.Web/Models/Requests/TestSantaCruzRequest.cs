using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class TestSantaCruzRequest
    {
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.EmailAddress]
        [MaxLength(6)]  //this should be no more than 6 characters long
        public string Email { get; set; }

        public double Weight { get; set; }
    }
}