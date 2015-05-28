using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/rating")]
    public class RatingApiController : ApiController
    {
        [Route(""), HttpPost]
        [Authorize]
        public HttpResponseMessage PostRating(RatingRequest model)
        {
            string currentUserId = UserService.GetCurrentUserId();
            Guid NewRatingId = RatingService.PostRating(model, currentUserId);

            ItemResponse<Guid> response = new ItemResponse<Guid>();
            response.Item = NewRatingId;
            return Request.CreateResponse(response);
        }

        [Route("sorted"), HttpGet]
        public HttpResponseMessage GetListByLowest([FromUri] int entityId, int entityType, int currentPage, int pageSize, string sortOrder)
        {
            List<Rating> ratingsList = RatingService.ListRatingsBySorted(entityId, entityType, sortOrder);


            ItemResponse<PagedList<Rating>> response = new ItemResponse<PagedList<Rating>>();

            PagedList<Rating> pagedRatingsList = new PagedList<Rating>(ratingsList, currentPage, pageSize);
            response.Item = pagedRatingsList;
            return Request.CreateResponse(response);
        }

    }
}
