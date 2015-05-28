using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/search")]
    public class SearchApiController : ApiController
    {
        
// PAGINATION API
 
        //Search request with pagination before angularization 

        [Route("developers"), HttpGet]
        public HttpResponseMessage DeveloperSearch([FromUri] SearchRequest request)
        {
            List<Developer> DeveloperList = SearchService.DeveloperSearch(request);
            ItemResponse<PagedList<Developer>> response = new ItemResponse<PagedList<Developer>>();

            PagedList<Developer> pagedlist = new PagedList<Developer>(DeveloperList, request.CurrentPage, request.ItemsPerPage);

            response.Item = pagedlist;
            return Request.CreateResponse(response);
        }

        
        //Developer Search request without Angular and pagination
        
        //[Route("developers"), HttpGet]
        //public HttpResponseMessage DeveloperSearch([FromUri] SearchRequest request)
        //{
        //    List<Developer> DeveloperList = SearchService.DeveloperSearch(request);
        //    ItemsResponse<Developer> response = new ItemsResponse<Developer>();
        //    response.Items = DeveloperList;
        //    return Request.CreateResponse(response);
        //}





        //parameter [FromUri] allows the api to receive queries along the uri, e.g. /api/search/employers?Keyword=java
        [Route("employers"), HttpGet]
        public HttpResponseMessage EmployerSearch([FromUri] SearchRequest request, int currentpage, int pagesize)
            //public HttpResponseMessage EmployerSearch([FromUri] SearchRequest request)
        {
            //first, create the request of the employer data and put it in a list, with a employer model, name it employerlist
            List<EmployerSearchResults> employerlist = SearchService.SearchEmployers(request);

            //next, set up the envelope of the pagedlist class, to contain the response
            ItemResponse<PagedList<EmployerSearchResults>> response = new ItemResponse<PagedList<EmployerSearchResults>>();

            //grab the data and set the parameters to be displayed
            PagedList<EmployerSearchResults> pagedlist = new PagedList<EmployerSearchResults>(employerlist, currentpage, pagesize);
            //finally, set the data into the envelope and return it
            response.Item = pagedlist;
            return Request.CreateResponse(response);


            //List<Employer> EmployerList = SearchService.SearchEmployers(request);
            //ItemsResponse<Employer> response = new ItemsResponse<Employer>();
            //response.Items = EmployerList;
            

        }

        [Route("recruiters"), HttpGet]
        public HttpResponseMessage RecruiterSearch([FromUri] SearchRequest request)
        {
            List<Recruiter> List = SearchService.RecruiterSearch(request);
            ItemResponse <PagedList<Recruiter>> response = new ItemResponse<PagedList<Recruiter>>();
            PagedList<Recruiter> pList = new PagedList<Recruiter>(List,request.CurrentPage,request.ItemsPerPage);
            response.Item = pList;
            return Request.CreateResponse(response);
        }


        [Route("jobs"), HttpGet]
        public HttpResponseMessage JobSearch([FromUri] SearchRequest request, int currentpage, int pagesize)
        //public HttpResponseMessage EmployerSearch([FromUri] SearchRequest request)
        {
            //first, create the request of the employer data and put it in a list, with a employer model, name it employerlist
            List<Job> joblist = SearchService.SearchJobs(request);

            //next, set up the envelope of the pagedlist class, to contain the response
            ItemResponse<PagedList<Job>> response = new ItemResponse<PagedList<Job>>();

            //grab the data and set the parameters to be displayed
            PagedList<Job> pagedlist = new PagedList<Job>(joblist, currentpage, pagesize);
            //finally, set the data into the envelope and return it
            response.Item = pagedlist;
            return Request.CreateResponse(response);
        }
    }
}
