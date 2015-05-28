using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Exceptions
{
    public class SabioException : Exception
    {

        public SabioException(string message)
            : base(message)
        {

        }
    }
}