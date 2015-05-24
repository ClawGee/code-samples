using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("profile-old")]
    public class ProfilesJobSeekerController : Controller
    {
        [Route, HttpGet]
        public ActionResult Index()
        {
            return View();
        }

    }
}