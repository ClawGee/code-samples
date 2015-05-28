using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("events")]
    public class EventsController : BaseController
    {
        //**************GET/READ, an existing Event detail, to edit*************
        [Authorize]
        [Route("edit/{uid:guid?}")]
        [Route("create")]
        public ActionResult Create(Guid? uid = null)
        {
            ItemViewModel<Guid?> vm = new ItemViewModel<Guid?>();
            vm.Item = uid;
            return View("CreateEvent", vm);
        }
        //**************Get All user's events to edit*************
        [Authorize]
        [Route("index")]
        public ActionResult Index()
            {
                return View();
            }
    }
}