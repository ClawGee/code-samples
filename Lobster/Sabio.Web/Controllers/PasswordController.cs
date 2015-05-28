using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sabio.Web.Models.ViewModels;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("password")]
    public class PasswordController : BaseController
    {
        //Enter password to check database
        [Route("Email")]
        public ActionResult Index()
        {
            return View("ResetPasswordEmail");
        }
        [Route("ChangePassword/{passwordGuid:guid}")]
        public ActionResult ResetPassword(Guid passwordGuid)
        {
            ItemViewModel<Guid> model = new ItemViewModel<Guid>();
            model.Item = passwordGuid;
            return View("ResetPassword", model);
        }
    }
}