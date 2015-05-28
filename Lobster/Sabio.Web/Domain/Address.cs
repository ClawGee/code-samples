using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Address
    {
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public int State { get; set; }

        public int Zip { get; set; }

        public decimal Lat { get; set; }

        public decimal Lng { get; set; }

        public Guid Uid { get; set; }

        public double? Distance { get; set; }
    }
}