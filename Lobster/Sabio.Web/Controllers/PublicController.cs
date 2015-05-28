using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sabio.Web.Models.ViewModels;
using Sabio.Web.Enums;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("profile")]
    public class PublicController : BaseController
    {
        [Route(""), HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Route("Employer/{employerGuid:guid}")]
        public ActionResult Employer(Guid employerGuid)
        {
            ItemViewModel<Guid> model = new ItemViewModel<Guid>();
            model.Item = employerGuid;
            return View("PublicEmployer", model);
        }
                
        [Route("Job/{jobGuid:guid}")]
        public ActionResult Job(Guid jobGuid)
        {
            ItemViewModel<Guid> model = new ItemViewModel<Guid>();
            model.Item = jobGuid;
            return View("PublicJob", model);
        }

        [Route("Recruiter/{recruiterGuid:guid}")]
        public ActionResult Recruiter(Guid recruiterGuid)
        {
            ItemViewModel<Guid> model = new ItemViewModel<Guid>();
            model.Item = recruiterGuid;
            return View("PublicRecruiter", model);
        }

        [Route("Developer/{developerGuid:guid}"), HttpGet]
        public ActionResult Developer(Guid developerGuid)
        {
            ItemViewModel<Guid> model = new ItemViewModel<Guid>();
            model.Item = developerGuid;
            return View("PublicDeveloper", model);
        }

        [Route("developer/projects/{developerGuid:guid}"), HttpGet]
        public ActionResult PublicProjects(Guid developerGuid)
        {

            ItemViewModel<Guid> model = new ItemViewModel<Guid>();
            model.Item = developerGuid;
            return View("PublicProjects", model);
        }

        [Route("projects/{projectGuid:guid}"), HttpGet]
        public ActionResult PublicProjectDetail(Guid projectGuid)
        {
            ProjectsViewModel<Guid> model = new ProjectsViewModel<Guid>();
            model.TechnologyOptions = Technologies.getDictionary();
            model.Item = projectGuid;
            return View("PublicProjectDetail", model);
        }

    }
}