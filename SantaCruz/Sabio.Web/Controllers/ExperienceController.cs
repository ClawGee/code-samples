using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("experience")]
    public class ExperienceController : Controller
    {
        //this route used for both create and edit
        [Route("edit/{uid:guid?}"), HttpGet]
        [Route("create")]
        public ActionResult Index(Guid? uid = null)
        {
            ExperienceViewModel vm = new ExperienceViewModel();
            vm.Months = Months.NotSet.ToJsonDictionary();
            vm.Uid = uid;

            return View(vm);
        }

        //this route used for viewing list of experiences
        [Route, HttpGet]
        public ActionResult List()
        {
            Dictionary<string, string> month = Months.NotSet.ToDictionary();
            ItemViewModel<Dictionary<string, string>> model = new ItemViewModel<Dictionary<string, string>>();
            model.Item = month;

            return View(model);
        }




        //[Route("info"), HttpGet]
        //public ActionResult Index2()
        //{

        //    return View();
        //}


        
    }
}
