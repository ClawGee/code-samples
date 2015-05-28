
using Sabio.Data;
using Sabio.Web.Domain;
using Sabio.Web.Domain.Tests;
using Sabio.Web.Models.Requests;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services
{

    public class RatingService : BaseService
    {
        public static Guid PostRating(RatingRequest model, String CurrentUserId)
        {
            //CurrentUserId = "1421fa80-2b50-4834-818f-d440c9e4c707"; //TEsting HARDCODED
            int AppUserId = UserService.ConvertGuid(CurrentUserId);

            Guid uid = Guid.Empty;

            DataProvider.ExecuteNonQuery(GetConnection,
            "dbo.AppRatings_Insert",
            inputParamMapper: delegate(SqlParameterCollection paramCollection)
             {
                 //paramCollection.AddWithValue("@Uid", model.Title);
                 paramCollection.AddWithValue("@AppUserId", AppUserId);
                 paramCollection.AddWithValue("@EntityId", model.EntityId);
                 paramCollection.AddWithValue("@EntityType", model.EntityType);
                 paramCollection.AddWithValue("@Rating", model.RatingValue);
                 paramCollection.AddWithValue("@Comments", model.Comments);

                 SqlParameter p = new SqlParameter("@Uid", System.Data.SqlDbType.UniqueIdentifier);
                 p.Direction = System.Data.ParameterDirection.Output;
                 paramCollection.Add(p);

             }, returnParameters: delegate(SqlParameterCollection param)
                {
                    Guid.TryParse(param["@Uid"].Value.ToString(), out uid);
                }
               );
            return uid;
        }

        public static List<Rating> ListRatingsBySorted(int entityId, int entityType, string sortOrder)
        {
            List<Rating> list = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.AppRatings_ListReviewsByEntityId_Sorted",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@EntityId", entityId);
                    paramCollection.AddWithValue("@EntityType", entityType);
                    paramCollection.AddWithValue("@SortOrder", sortOrder);
                }
                , map: delegate(IDataReader reader, short set)
                {
                    Rating ratingRow = new Rating();
                    int startingIndex = 0;
                    ratingRow.RatingValue = reader.GetSafeInt32(startingIndex++);
                    ratingRow.Comments = reader.GetSafeString(startingIndex++);
                    ratingRow.EntityId = reader.GetSafeInt32(startingIndex++);
                    ratingRow.EntityType = reader.GetSafeInt32(startingIndex++);
                    ratingRow.Uid = reader.GetSafeGuid(startingIndex++);
                    ratingRow.CreateDate = reader.GetSafeDateTime(startingIndex++);
                    ratingRow.FullUser = new FullUser();
                    ratingRow.FullUser.FirstName = reader.GetSafeString(startingIndex++);
                    ratingRow.FullUser.LastName = reader.GetSafeString(startingIndex++);
                    ratingRow.FullUser.Email = reader.GetSafeString(startingIndex++);
                    ratingRow.FullUser.UserType = reader.GetSafeInt32(startingIndex++);

                    if (list == null)
                    {
                        list = new List<Rating>();
                    }
                    list.Add(ratingRow);

                });
            return list;
        }

    }

}