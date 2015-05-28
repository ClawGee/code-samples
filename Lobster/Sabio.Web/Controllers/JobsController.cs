using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers.Jobs
{
    [RoutePrefix("jobs")]
    public class JobsController : BaseController
    {
        // GET: Jobs
       
        [Authorize]
        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }
        
        // MANAGE: Jobs
        
        [Authorize]
        [Route("manage/{jobGuid:guid?}")]
        public ActionResult Manage(Guid? jobGuid = null )
        {
            ItemViewModel<Guid?> vm = new ItemViewModel<Guid?>();
            vm.Item = jobGuid;
            return View("Manage", vm);

        }

        // Get: Job Details
        [Authorize]
        [Route("details/{jobGuid:guid}"), HttpGet]
        public ActionResult Details(Guid jobGuid)
        {  
            ItemViewModel<Guid?> vm = new ItemViewModel<Guid?>();
            vm.Item = jobGuid;
            return View("Details", vm);
        }

        // Get Jobs List
        [Authorize]
        [Route("details")]
        public ActionResult Details()
        {
            return View();
        }
    }
}