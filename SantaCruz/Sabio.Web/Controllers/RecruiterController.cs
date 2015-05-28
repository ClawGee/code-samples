using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("recruiter")]
    public class RecruiterController : Controller
    {
        [Authorize]
        [Route("create")]
        [Route("edit/{uid:guid}")]
        public ActionResult Manage(Guid? uid = null)
        {
            ItemViewModel<Guid?> model = new ItemViewModel<Guid?>();
            model.Item = uid;
            return View("RecruiterInput", model);
        }
        [Route("")]
        public ActionResult Details()
        {
            return View("details");
        }

    }

}