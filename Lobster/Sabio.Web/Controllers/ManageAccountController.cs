using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [Authorize]
    public class ManageAccountController : BaseController
    {
        // GET: ManageAccount
        public ActionResult Index()
        {
            return View("ManageAccountIndex");
        }
    }
}