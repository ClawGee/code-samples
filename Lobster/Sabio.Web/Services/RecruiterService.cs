using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Data;
using Sabio.Web.Models.Requests;
using Sabio.Web.Domain;

namespace Sabio.Web.Services
{
    public class RecruiterService : BaseService
    {

        public static Guid Insert(UpdateRecruiterRequest model, string LoggedInUser)
        {
            Guid uid = Guid.Empty;//000-0000-0000-0000

            int CurrentUserId = UserService.ConvertGuid(LoggedInUser);

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppRecruiterProfiles_Insert"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Specialty", model.specialty);
                   paramCollection.AddWithValue("@Location", model.location);
                   paramCollection.AddWithValue("@URL", model.url);
                   paramCollection.AddWithValue("@PhoneNumber1", model.phoneNumber1);
                   paramCollection.AddWithValue("@PhoneNumber2", model.phoneNumber2);
                   paramCollection.AddWithValue("@CompanyLogo", model.companyLogo);
                   paramCollection.AddWithValue("@Summary", model.summary);
                   paramCollection.AddWithValue("@TwitterAccount", model.twitterAccount);
                   paramCollection.AddWithValue("@FacebookAccount", model.facebookAccount);
                   paramCollection.AddWithValue("@LinkedInAccount", model.linkedInAccount);
                   paramCollection.AddWithValue("@GooglePlusAccount", model.googlePlusAccount);
                   paramCollection.AddWithValue("@AppUserId", CurrentUserId);

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

        public static void Update(UpdateRecruiterRequest model, string LoggedInUser)
        {
            int CurrentUserId = UserService.ConvertGuid(LoggedInUser);

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppRecruiterProfiles_Update"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Specialty", model.specialty);
                   paramCollection.AddWithValue("@Location", model.location);
                   paramCollection.AddWithValue("@URL", model.url);
                   paramCollection.AddWithValue("@PhoneNumber1", model.phoneNumber1);
                   paramCollection.AddWithValue("@PhoneNumber2", model.phoneNumber2);
                   paramCollection.AddWithValue("@CompanyLogo", model.companyLogo);
                   paramCollection.AddWithValue("@Summary", model.summary);
                   paramCollection.AddWithValue("@TwitterAccount", model.twitterAccount);
                   paramCollection.AddWithValue("@FacebookAccount", model.facebookAccount);
                   paramCollection.AddWithValue("@LinkedInAccount", model.linkedInAccount);
                   paramCollection.AddWithValue("@GooglePlusAccount", model.googlePlusAccount);
                   paramCollection.AddWithValue("@AppUserId", CurrentUserId);

               }, returnParameters: delegate(SqlParameterCollection param)
               {
                  
               }
               );
        }

        public static Recruiter Select(string LoggedInUser)
        {
            Recruiter row = null;
            int CurrentUserId = UserService.ConvertGuid(LoggedInUser);
            DataProvider.ExecuteCmd(GetConnection, "dbo.AppRecruiterProfiles_Select"
                , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@AppUserId", CurrentUserId);
                }
                , map: delegate(IDataReader reader, short set)
                {
                    row = new Recruiter();
                    int startingIndex = 0;
                    row.Specialty = reader.GetSafeString(startingIndex++);
                    row.Location = reader.GetSafeString(startingIndex++);
                    row.URL = reader.GetSafeString(startingIndex++);
                    row.PhoneNumber1 = reader.GetSafeString(startingIndex++);
                    row.PhoneNumber2 = reader.GetSafeString(startingIndex++);
                    row.CompanyLogo = reader.GetSafeString(startingIndex++);
                    row.Summary = reader.GetSafeString(startingIndex++);
                    row.TwitterAccount = reader.GetSafeString(startingIndex++);
                    row.FacebookAccount = reader.GetSafeString(startingIndex++);
                    row.LinkedInAccount = reader.GetSafeString(startingIndex++);
                    row.GooglePlusAccount = reader.GetSafeString(startingIndex++);
                    row.Uid = reader.GetGuid(startingIndex++);
                    row.Id = reader.GetInt32(startingIndex++);
                    row.AverageRating = reader.GetSafeDouble(startingIndex++);
                    row.RatingCount = reader.GetSafeInt32(startingIndex++);
                    row.RecruiterPhoto = new Media();
                    row.RecruiterPhoto.FileName = reader.GetSafeString(startingIndex++);
                    row.RecruiterPhoto.ContentType = reader.GetSafeString(startingIndex++);
                    row.RecruiterPhoto.EntityType = reader.GetSafeInt32(startingIndex++);
                    row.RecruiterPhoto.EntityId = reader.GetSafeInt32(startingIndex++);
                    row.RecruiterPhoto.CreateDate = reader.GetSafeDateTime(startingIndex++);
                    if (row.RecruiterPhoto.FileName != null && row.RecruiterPhoto.EntityType != 0)
                    {
                        row.RecruiterPhoto.FullUrl = FileIoService.FileHelperGet(2, row.RecruiterPhoto.FileName);
                    }
                    else
                    {
                        row.RecruiterPhoto = null; 
                    }
                    row.RecruiterUser = new FullUser();
                    row.RecruiterUser = UserService.GetFullUserById(LoggedInUser);
                }
                );
            return row;
        }
    }
}

