using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("developer")]
    public class DeveloperController : Controller
    {
        //this route used for both create and edit
        [Route("edit/{uid:guid?}"), HttpGet]
        [Route("create")]
        public ActionResult Index(Guid? uid = null)
        {
            DeveloperViewModel vm = new DeveloperViewModel();
            vm.EducationLevels = EducationLevelType.NotSet.ToDictionary();
            vm.Uid = uid;


            return View(vm);
        }

    }
}