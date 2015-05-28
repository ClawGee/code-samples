using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("registration")]
    public class RegistrationController : BaseController
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        [Route("confirmed/{emailGuid:guid}"), HttpGet]
        public ActionResult EmailConfirmed(Guid emailGuid)//parameter is using the guid in email link
        {
            ItemViewModel<Guid?> vm = new ItemViewModel<Guid?>();
            vm.Item = emailGuid;
            return View("EmailConfirmed", vm);
        }
  
    }

}

    

