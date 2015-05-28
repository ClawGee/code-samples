using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain.Tests
{
    public class TestEmployee
    {
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime Dob { get; set; }

        public string Title { get; set; }

        public int Status { get; set; }
    }
}