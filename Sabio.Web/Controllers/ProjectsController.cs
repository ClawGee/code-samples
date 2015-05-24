using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [Authorize]
    [RoutePrefix("projects")]            //prefix for whole controller
    public class ProjectsController : Controller
    {
        [Route, HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Route("create")]
        [Route("edit/{uid:guid}"), HttpGet]
        public ActionResult Create(Guid? uid = null)
        {
            ItemViewModel<Guid?> model = new ItemViewModel<Guid?>();  // Guid is the type that the suitcase is going to take
            model.Item = uid;

            return View(model);
        }

        [Route("{uid:guid}"), HttpGet]
        public ActionResult Details(Guid uid)
        {
            ItemViewModel<Guid> model = new ItemViewModel<Guid>();
            model.Item = uid;
            return View(model);
        }
    }
}