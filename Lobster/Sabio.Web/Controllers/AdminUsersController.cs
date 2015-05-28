using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("useradmin")] 
    public class AdminUsersController : BaseController
    {
        // GET: ManageUsers
        [Route("users"), HttpGet]
        public ActionResult Index()
        {
            return View("Details");
        }
    }
}