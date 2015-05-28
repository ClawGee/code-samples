using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("search")]
    public class SearchController : BaseController
    {
        // GET: DeveloperSearch
        [Route("developer")]
        public ActionResult Developer()
        {
            return View("DeveloperSearch");
        }

        // GET: EmployerSearch
        [Route("employer")]
        public ActionResult Employer()
        {
         //   string QueryString = Request.QueryString["jon"];
            return View("EmployerSearch");
        }
        
        [Route("recruiter")]
        public ActionResult Recruiter()
        {
            return View("RecruiterSearch");
        }

        [Route("jobs")]
        public ActionResult Job()
        {
            return View("JobSearch");
        }


    }
}