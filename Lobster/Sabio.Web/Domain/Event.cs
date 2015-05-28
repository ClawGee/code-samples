using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Event
    {
        public Guid Uid { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string AllDay { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        //using the 'Address' request model here :
        public Address EventAddress { get; set; }

       
    }
}