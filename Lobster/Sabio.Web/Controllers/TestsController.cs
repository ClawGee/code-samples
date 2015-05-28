using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("tests")]
    public class TestsController : BaseController
    {
        // GET: Tests
        [Route("ajax")]
        public ActionResult Ajax()
        {
            return View();
        }

        // GET: Tests
        [Route("Angular")]
        public ActionResult Angular()
        {
            return View();
        }

        [Route("bug")]
        public ActionResult ListBug()
        {
            return View();
        }
    }
}