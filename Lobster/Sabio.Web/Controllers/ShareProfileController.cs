using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sabio.Web.Models.ViewModels;

namespace Sabio.Web.Controllers
{
    public class ShareProfileController : BaseController
    {
        // POST: ShareProfile
        public ActionResult Index()
        {
            return View("Index");
        }

        [Route("shareprofile/{profileGuid:guid}")]
        public ActionResult ShareProfile(Guid profileGuid)
        {
            ItemViewModel<Guid?> model = new ItemViewModel<Guid?>();
            model.Item = profileGuid;
            return View("ShareProfileIndex", model);
        }
    }
}