using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("address")]
    public class UserAddressController  : Controller
    {
        // GET: Users Address

        [Route("create"), HttpGet] 
        public ActionResult CreateUserAddress() 
        {
            ItemViewModel<Guid?> vm = new ItemViewModel<Guid?>();

            return View("InputUserAddress", vm);
        }

        [Route("edit/{addressGuid:guid}"), HttpGet] 
        public ActionResult CreateUserAddress(Guid addressGuid)
        {
            ItemViewModel<Guid?> vm = new ItemViewModel<Guid?>();
            vm.Item = addressGuid;
            return View("InputUserAddress", vm);
        }
     

        [Route("list"), HttpGet]
        public ActionResult ListUserAddress()
        {
            return View();
        }
        [Route("{addressGuid:guid}"), HttpGet]
        public ActionResult ViewSingleUserAddress(Guid addressGuid)
        {
            ItemViewModel<Guid?> vm = new ItemViewModel<Guid?>();
            vm.Item = addressGuid;
            return View("ViewSingleUserAddress", vm);
        }

    }


};

