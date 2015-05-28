using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix ("jobseeker")]
    public class JobSeekerSkillsController : Controller
    {
        // GET: JobSeekerSkills
        [Route ("skills"), HttpGet]
        public ActionResult Index()
        {
            JobSkillsViewModel vm = new JobSkillsViewModel();
            vm.EducationLevels  = EducationLevelType.NotSet.ToDictionary();

            return View(vm);
        }
    }
}