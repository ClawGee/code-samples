using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sabio.Web.Controllers;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("users")]
    public class UserController : Controller
    {
        // GET: Users

        [Route("address"), HttpGet]
        public ActionResult CreateUserAddress()
        {
            return View();
        }
    }
}





