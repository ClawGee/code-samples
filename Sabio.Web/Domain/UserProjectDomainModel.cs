using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class UserProjectDomainModel
    {
        public Guid Uid { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public DateTime Launchdate { get; set; }

        public string Technologies { get; set; }

        public int Id { get; set; }

    }

}