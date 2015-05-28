using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class FullResume
    {
        public Resume ResumeData { get; set; }

        public FullUser UserData { get; set; }

        public Address AddressData { get; set; }
    }
}