using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{   

    [RoutePrefix ("jobseeker")]
    public class JobSeekerController : Controller
    {
        // GET: JobSeeker
        [Route("requirements"), HttpGet]
        public ActionResult jobseekerRequirements()
        {
            return View();
        }
    }
}