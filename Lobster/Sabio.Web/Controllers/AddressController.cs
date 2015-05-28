using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    public class AddressController : BaseController
    {
        // When a user routes (i.e. makes an http GET request) to "~/address" (i.e. the first word in "AddressController")
        // the ActionResult method named "Index" will return the view "addressindex."
        public ActionResult Index()
        {
            return View("AddressIndex");
        }

        public ActionResult Search()
        {
            return View("AddressRadiusSearch");
        }
    }
}