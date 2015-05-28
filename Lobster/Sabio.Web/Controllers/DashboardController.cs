using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    
    
    public class DashboardController : BaseController
    {
        // GET: UserDashBoard
        
        public ActionResult Index()
        {
            return View("index");
        }
    }
}

