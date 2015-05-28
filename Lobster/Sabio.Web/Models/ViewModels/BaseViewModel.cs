using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseViewModel
    {//Sabio: make note that this base class does not have to be, or should not be abstract. 
        // it is a perfectly good class to be used as a ViewModel 
        
        public BaseViewModel()
        {
            this.NavigationView = "Navigation/_DefaultPartial";
        }

        public bool IsLoggedIn { get; set; }
        public FullUser CurrentUser { get; set; }
        public string NavigationView { get; set; }
    }
}