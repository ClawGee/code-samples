using System;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class SearchRequest
    {
        public SearchRequest()
        {
            this.CurrentPage = 0;/*this is a 0 based index*/
            this.ItemsPerPage = 5;
        }

        public string Keyword { get; set; }

        public int CurrentPage { get; set; }

        public int ItemsPerPage {get; set;}

    }
        
}
