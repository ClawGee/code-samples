using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("jobs")]
    public class JobsController : Controller
    {


        [Route, HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        // GET: JobPosting
        [Route("post"), HttpGet]
        public ActionResult Post()
        {
            return View();
        }



    }
}