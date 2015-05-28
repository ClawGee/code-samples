using Sabio.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("gallery")]
    public class GalleryController : BaseController
    {
        // GET: Gallery
        public ActionResult Index()
        {
            return View("create");
        }

        //public ActionResult Manage()
        //{
        //    return View("manage");
        //}

        [Route("manage/{galleryId:int}")]
        public ActionResult Manage(int galleryId)
        {
            ItemViewModel<int> vm = new ItemViewModel<int>();
            vm.Item = galleryId;
            return View("manage", vm);

        }
    }
}