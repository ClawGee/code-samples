using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("jobsadmin")] 
    public class JobsAdminController : BaseController
    {
        // GET: ManageJobs
        [Route("jobs"), HttpGet]
        public ActionResult Index()
        {
            return View("Details");
        }
    }
}