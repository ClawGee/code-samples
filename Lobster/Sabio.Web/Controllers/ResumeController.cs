using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [Authorize]
    [RoutePrefix("resume")]
    public class ResumeController : BaseController
    {
        // Route to "~/resume" and GET the resumeindex page.
        [Route(""), HttpGet]
        public ActionResult Index()
        {
            return View("ResumeIndex");
        }

        // Route to "~/resume/preview" and GET the resumepreview page.
        [Route("preview"), HttpGet]
        public ActionResult Preview()
        {
            return View("ResumePreview");
        }
    }

}