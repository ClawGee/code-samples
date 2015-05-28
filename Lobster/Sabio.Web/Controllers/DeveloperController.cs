using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    public class DeveloperController : BaseController

    {
        // GET: DeveloperProfile
        [Authorize]
        public ActionResult Index()  
        {
            return View();
        }

        // GET: Developer Details        
 
    }  
}