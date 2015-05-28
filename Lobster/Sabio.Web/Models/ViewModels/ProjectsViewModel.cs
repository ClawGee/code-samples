using Sabio.Web.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.ViewModels
{
    public class ProjectsViewModel<T> : ItemViewModel<T>
    {
        public Dictionary<string, string> TechnologyOptions { get; set; }
    }
}