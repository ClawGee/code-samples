using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class AllUsers
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int UserType { get; set; }

        public int MediaId { get; set; }

        public int EntityId { get; set; }

        public int EntityType { get; set; }

        public string ContentType { get; set; }

        public DateTime CreateDate { get; set; }

        public string FileName { get; set; }

        public string FullUrl { get; set; }

        public bool Deleted { get; set; }

        //public string Id { get; set; }
    }
}