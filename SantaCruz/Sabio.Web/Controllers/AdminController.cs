using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix ("admin")]
    public class AdminController : Controller
    {
        [Route ("users")]
        public ActionResult Users()
        {
            return View();
        }
    }
}