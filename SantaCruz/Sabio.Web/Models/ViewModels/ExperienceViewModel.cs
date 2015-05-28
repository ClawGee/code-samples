using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.ViewModels
{
    public class ExperienceViewModel: BaseViewModel
    {
        public KeyValuePair<int, string>[] Months { get; set; }

        public Guid? Uid { get; set; }

    }
}