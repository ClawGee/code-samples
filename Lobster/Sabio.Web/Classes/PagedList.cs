using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web
{
    public class PagedList<T>
    {

        public int PageIndex { get; set; }


        public int PageSize { get; set; }


        public int TotalCount { get; set; }


        public int TotalPages { get; set; }


        public List<T> PagedItems { get; set; }

        public PagedList(List<T> data, int page, int pagesize)
        {
            PageIndex = page;
            PageSize = pagesize;
            if (data != null)
            {
                TotalCount = data.Count;
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            }


            if (data != null)
            {
                this.PagedItems = data.Skip(PageIndex * PageSize).Take(PageSize).ToList();
            }

        }

        public bool HasPreviousPage
        {
            get { return (PageIndex > 0); }
        }


        public bool HasNextPage
        {
            get { return (PageIndex + 1 < TotalPages); }
        }
    }

}