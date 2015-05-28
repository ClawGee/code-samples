using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    
    [RoutePrefix ("accounts")]
    public class AccountsController : Controller
    {

        [Route("~/login")]
        public ActionResult LogIn()
        {
            return View();
        }

        //sign up controller (spicer)
        [Route("signup")]
        public ActionResult SignUp()
        {
            return View();
        }
        //account confirmation
        [Route("confirmation/{token:guid}")]
        public ActionResult EmailConfirmation(Guid token)
        {
            ItemViewModel<Guid> vm = new ItemViewModel<Guid>();
            vm.Item = token;

            return View("EmailConfirmation", vm);
        }

        // GET: ForgotPassword
        [Route("forgotpassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [Route("updatepassword/{token:guid}")]
        public ActionResult UpdatePassword(Guid token)
        {
            //C401859F-F480-4BD4-99C7-A90BF08C62DE

            //use viewbag to pass dynamic info from controller to cshtml

            ItemViewModel<Guid> vm = new ItemViewModel<Guid>();
            vm.Item = token;

            return View("UpdatePassword", vm);
        }

        //Manage Account 
        [Route("manage"), HttpGet]
        public ActionResult Manage()
        {
            ItemViewModel<Guid?> ma = new ItemViewModel<Guid?>();
            
            return View("Manage", ma);
        }
    }
}