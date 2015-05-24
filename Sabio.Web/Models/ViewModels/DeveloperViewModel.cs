using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.ViewModels
{
    public class DeveloperViewModel : BaseViewModel
    {
        public Dictionary<string, string> EducationLevels { get; set; }

        public Guid? Uid { get; set; }

    }
}