using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class ExperienceDomainModel
    {
        public Guid Uid { get; set; }

        public string Title { get; set; }

        public string CompanyName { get; set; }

        public string CompanyCity { get; set; }

        public StateDomainModel State { get; set; }

        public Int16 StartDateMonth { get; set; }

        public Int16 StartDateYear { get; set; }

        public Int16 EndDateMonth { get; set; }

        public Int16 EndDateYear { get; set; }

        public string Description { get; set; }
    }
}