using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("employer")]
    public class EmployerController : BaseController
    {
        // GET: EmployerProfile
        public ActionResult Index()
        {
            return View("Details");
        }
        
        // UPDATE: EmployerProfile
        public ActionResult Manage()
        {
            return View("Edit");
        }


        [Route("tag/{tagName}"), HttpGet]
        public ActionResult Tag(string tagName)
        {
            ItemViewModel<string> tag = new ItemViewModel<string>();
            tag.Item = tagName;
            
            return View(tag);
        }

    }


}