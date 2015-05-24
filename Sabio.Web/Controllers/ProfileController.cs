using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("profile")]
    public class ProfileController : Controller
    {
        [Route("developer/{uid:guid}"), HttpGet]
        public ActionResult Developer(Guid uid)
        {
            return View();
        }
    }
}