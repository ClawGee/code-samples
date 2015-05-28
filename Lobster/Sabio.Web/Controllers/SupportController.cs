using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("ContactUs")]
    public class SupportController : BaseController
    {
        // GET: ContactUs
        [Route("")]
        public ActionResult Index()
        {
            return View("ContactUs");
        }
    }
}