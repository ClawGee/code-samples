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
        // GET: /tests/dynamic/850411A1-FF75-4440-9EFA-5EF43D6C1417
        // GET: /tests/dynamic
        [Route("dynamic/{uid:guid?}")]
        public ActionResult Dynamic(Guid? uid = null)
        {
            ViewBag.Uid = uid;

            return View();
        }

        // GET: Tests
        [Route("ajax")]
        public ActionResult Ajax()
        {
            ViewBag.Prop = "value";

            return View();
        }
    }
}