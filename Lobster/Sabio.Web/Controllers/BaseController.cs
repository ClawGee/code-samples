using Sabio.Web.Domain;
using Sabio.Web.Models.ViewModels;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    public class BaseController : Controller
    {
        protected new ViewResult View()
        {
            //vmtest.myNewField = "Do What Now?!";
            //return base.View(vmtest);
            BaseViewModel vm = _GetBaseViewModel();
            return base.View(vm);
        }


        protected new ViewResult View(string viewname)
        {
            BaseViewModel vm = _GetBaseViewModel();
            return base.View(viewname, vm);
        }

        protected ViewResult View(string viewname, BaseViewModel vm)
        {
            vm = _DecorateBaseViewModel(vm);
            return base.View(viewname, vm);
        }

        protected ViewResult View(BaseViewModel vm)
        {
            vm = _DecorateBaseViewModel(vm);
            return base.View(vm);
        }

        private BaseViewModel _GetBaseViewModel()
        {
            return _DecorateBaseViewModel(new BaseViewModel());
        }

        private BaseViewModel _DecorateBaseViewModel(BaseViewModel vm)
        {
            
            vm.NavigationView = "Navigation/_DefaultPartial";

            if (UserService.IsLoggedIn())
            {
                string currentUser = UserService.GetCurrentUserId();
                FullUser user = UserService.GetFullUserById(currentUser);  
                vm.CurrentUser = user;
                vm.IsLoggedIn = true;

                if(null != user)
                {
                    switch (user.UserType)
                    {
                        case 1:
                            vm.NavigationView = "Navigation/_DeveloperPartial";
                            break;

                        case 2:
                            vm.NavigationView = "Navigation/_RecruiterPartial";
                            break;

                        case 3:
                            vm.NavigationView = "Navigation/_EmployerPartial";
                            break;

                        default:
                            vm.NavigationView = "Navigation/_DefaultPartial";
                            break;
                    }
                }                
            }

            return vm;
        }
    }

}