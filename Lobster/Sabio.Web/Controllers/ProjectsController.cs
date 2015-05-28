using Sabio.Web.Enums;
using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [Authorize]
    [RoutePrefix("projects")]            
    public class ProjectsController : BaseController
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
            ProjectsViewModel <Guid?> model = new ProjectsViewModel<Guid?>();  // Guid is the type that the suitcase is going to take
            model.Item = uid;
            model.TechnologyOptions = Technologies.getDictionary();

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