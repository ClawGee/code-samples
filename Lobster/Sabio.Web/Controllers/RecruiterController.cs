using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [Authorize]
    public class RecruiterController : BaseController
    {
        // GET: RecruiterProfiles
        public ActionResult Index()
        {
            return View("Details");
        }

        public ActionResult Manage()
        {
            return View("Edit");
        }
    }
}