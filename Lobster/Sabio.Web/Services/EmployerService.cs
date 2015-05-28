using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Web.Models.Requests;
using Sabio.Data;


namespace Sabio.Web.Services
{
    public class EmployerService : BaseService
    {

        public static Guid InsertEmployer(EmployerRequest model, string user)
        {
            Guid uid = Guid.Empty;//000-0000-0000-0000
            int appuser = UserService.ConvertGuid(user);
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppEmployers_Insert"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@AppUserId", appuser);
                   paramCollection.AddWithValue("@CompanyName", model.Name);
                   paramCollection.AddWithValue("@ContactName", model.ContactName);
                   paramCollection.AddWithValue("@CompanySize", model.Size);
                   paramCollection.AddWithValue("@Industry", model.Industry);
                   paramCollection.AddWithValue("@ContactEmail", model.ContactEmail);
                   paramCollection.AddWithValue("@CompanyURL", model.URL);
                   paramCollection.AddWithValue("@Country", model.Country);
                   paramCollection.AddWithValue("@StateOptions", model.StateOptions);
                   paramCollection.AddWithValue("@OtherLocation", model.OtherLocation);
                   paramCollection.AddWithValue("@Tech", model.Tech);
                   paramCollection.AddWithValue("@CompanyDescription", model.Description);
                   paramCollection.AddWithValue("@CompanyFacebook", model.Facebook);
                   paramCollection.AddWithValue("@CompanyTwitter", model.Twitter);
                   paramCollection.AddWithValue("@CompanyLinkedIn", model.LinkedIn);
                   paramCollection.AddWithValue("@City", model.City);

                   SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.UniqueIdentifier);
                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);

               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   Guid.TryParse(param["@id"].Value.ToString(), out uid);
               }
               );
            int entityId = SelectEmployerByUserId(user).EmployerId;
            TagService.Upsert(model.Tags, entityId, 3);
            return uid;
        }

        public static Guid UpdateEmployer(EmployerRequest model, string user)
        {
            Guid uid = Guid.Parse(user);
            int AppUserId = UserService.ConvertGuid(user);
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppEmployers_Update"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@AppUserId", AppUserId);
                   paramCollection.AddWithValue("@CompanyName", model.Name);
                   paramCollection.AddWithValue("@ContactName", model.ContactName);
                   paramCollection.AddWithValue("@CompanySize", model.Size);
                   paramCollection.AddWithValue("@Industry", model.Industry);
                   paramCollection.AddWithValue("@ContactEmail", model.ContactEmail);
                   paramCollection.AddWithValue("@CompanyURL", model.URL);
                   paramCollection.AddWithValue("@Country", model.Country);
                   paramCollection.AddWithValue("@StateOptions", model.StateOptions);
                   paramCollection.AddWithValue("@OtherLocation", model.OtherLocation);
                   paramCollection.AddWithValue("@Tech", model.Tech);
                   paramCollection.AddWithValue("@CompanyDescription", model.Description);
                   paramCollection.AddWithValue("@CompanyFacebook", model.Facebook);
                   paramCollection.AddWithValue("@CompanyTwitter", model.Twitter);
                   paramCollection.AddWithValue("@CompanyLinkedIn", model.LinkedIn);
                   paramCollection.AddWithValue("@City", model.City);
               }, returnParameters: delegate(SqlParameterCollection param)
               {
               }
               );
            int entityId = SelectEmployerByUserId(user).EmployerId;
            TagService.Upsert(model.Tags, entityId, 3);
            return uid;
        }

        public static Employer SelectEmployerByUserId(string user)
        {
            Employer e = null;
            int AppUserId = UserService.ConvertGuid(user);
            DataProvider.ExecuteCmd(GetConnection, "dbo.AppEmployers_Select"
           , inputParamMapper: delegate(SqlParameterCollection paramCollection)
           {
               paramCollection.AddWithValue("@AppUserId", AppUserId);
           }
           , map: delegate(IDataReader reader, short set)
           {
               if (set == 0)
               {
                   e = new Employer();

                   int startingIndex = 0;
                   e.EmployerId = reader.GetSafeInt32(startingIndex++);
                   e.Uid = reader.GetGuid(startingIndex++);
                   e.Name = reader.GetSafeString(startingIndex++);
                   e.ContactName = reader.GetSafeString(startingIndex++);
                   e.Size = reader.GetSafeString(startingIndex++);
                   e.Industry = reader.GetSafeString(startingIndex++);
                   e.ContactEmail = reader.GetSafeString(startingIndex++);
                   e.URL = reader.GetSafeString(startingIndex++);
                   e.Country = reader.GetSafeString(startingIndex++);
                   e.StateOptions = reader.GetSafeInt32(startingIndex++);
                   e.OtherLocation = reader.GetSafeString(startingIndex++);
                   e.Tech = reader.GetSafeString(startingIndex++);
                   e.Description = reader.GetSafeString(startingIndex++);
                   e.Facebook = reader.GetSafeString(startingIndex++);
                   e.Twitter = reader.GetSafeString(startingIndex++);
                   e.LinkedIn = reader.GetSafeString(startingIndex++);
                   e.City = reader.GetSafeString(startingIndex++);
                   e.AverageRating = reader.GetSafeDouble(startingIndex++);
                   e.RatingCount = reader.GetSafeInt32(startingIndex++);
                   e.Media = new Media();
                   e.Media.FileName = reader.GetSafeString(startingIndex++);
                   e.Media.CreateDate = reader.GetSafeDateTime(startingIndex++);
                   e.Media.ContentType = reader.GetSafeString(startingIndex++);
                   e.Media.EntityType = reader.GetSafeInt32(startingIndex++);

                   e.Media.EntityId = reader.GetSafeInt32(startingIndex++);
                   if (!string.IsNullOrEmpty(e.Media.FileName) && e.Media.EntityType != 0)
                   {
                       e.Media.FullUrl = FileIoService.FileHelperGet(e.Media.EntityType, e.Media.FileName);
                   }

               }
               else if (set == 1)
               {
                   if (e.Tags == null)
                   {
                       e.Tags = new List<Tag>();
                   }
                   e.Tags.Add(TagService.MapTag(reader));

               }
           }
           );
            return e;
        }

        public static Employer SelectEmployerById(string employerId)
        {
            Employer e = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.AppEmployers_SelectById"
           , inputParamMapper: delegate(SqlParameterCollection paramCollection)
           {
               paramCollection.AddWithValue("@Id", employerId);
           }
           , map: delegate(IDataReader reader, short set)
           {
               if (set == 0)
               {
                   e = new Employer();

                   int startingIndex = 0;
                   e.EmployerId = reader.GetSafeInt32(startingIndex++);
                   e.Uid = reader.GetGuid(startingIndex++);
                   e.Name = reader.GetSafeString(startingIndex++);
                   e.ContactName = reader.GetSafeString(startingIndex++);
                   e.Size = reader.GetSafeString(startingIndex++);
                   e.Industry = reader.GetSafeString(startingIndex++);
                   e.ContactEmail = reader.GetSafeString(startingIndex++);
                   e.URL = reader.GetSafeString(startingIndex++);
                   e.Country = reader.GetSafeString(startingIndex++);
                   e.StateOptions = reader.GetSafeInt32(startingIndex++);
                   e.OtherLocation = reader.GetSafeString(startingIndex++);
                   e.Tech = reader.GetSafeString(startingIndex++);
                   e.Description = reader.GetSafeString(startingIndex++);
                   e.Facebook = reader.GetSafeString(startingIndex++);
                   e.Twitter = reader.GetSafeString(startingIndex++);
                   e.LinkedIn = reader.GetSafeString(startingIndex++);
                   e.City = reader.GetSafeString(startingIndex++);
                   e.AverageRating = reader.GetSafeDouble(startingIndex++);
                   e.RatingCount = reader.GetSafeInt32(startingIndex++);
                   e.Media = new Media();
                   e.Media.FileName = reader.GetSafeString(startingIndex++);
                   e.Media.CreateDate = reader.GetSafeDateTime(startingIndex++);
                   e.Media.ContentType = reader.GetSafeString(startingIndex++);
                   e.Media.EntityType = reader.GetSafeInt32(startingIndex++);

                   e.Media.EntityId = reader.GetSafeInt32(startingIndex++);
                   if (!string.IsNullOrEmpty(e.Media.FileName) && e.Media.EntityType != 0)
                   {
                       e.Media.FullUrl = FileIoService.FileHelperGet(e.Media.EntityType, e.Media.FileName);
                   }

               }
               else if (set == 1)
               {
                   if (e.Tags == null)
                   {
                       e.Tags = new List<Tag>();
                   }
                   e.Tags.Add(TagService.MapTag(reader));
                   
               }
           }
           );
            return e;
        }


        public static List<EmployerSearchResults> GetEmployersByTag(string tagName)
        {
	        List<EmployerSearchResults> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AppEmployers_SelectByTag"
            , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Tags", tagName);
                }
                , map: delegate(IDataReader reader, short set)
                {
                    EmployerSearchResults row = new EmployerSearchResults();
                    int startingIndex = 0;

                    row.EmployerId = reader.GetSafeInt32(startingIndex++);
                    row.EmployerName = reader.GetSafeString(startingIndex++);
                    row.EmployerContactName = reader.GetSafeString(startingIndex++);
                    row.EmployerIndustry = reader.GetSafeString(startingIndex++);
                    row.EmployerContactEmail = reader.GetSafeString(startingIndex++);
                    row.EmployerURL = reader.GetSafeString(startingIndex++);
                    row.EmployerOtherLocation = reader.GetSafeString(startingIndex++);
                    row.EmployerTech = reader.GetSafeString(startingIndex++);
                    row.EmployerDescription = reader.GetSafeString(startingIndex++);
                    row.AverageRating = reader.GetSafeDouble(startingIndex++);
                    row.RatingCount = reader.GetSafeInt32(startingIndex++);

                    row.EmployerMedia = new Media();
                    row.EmployerMedia.FileName = reader.GetSafeString(startingIndex++);
                    row.EmployerMedia.EntityType = reader.GetSafeInt32(startingIndex++);
                    row.Uid = reader.GetGuid(startingIndex++).ToString();

                    if (row.EmployerMedia.FileName != null && row.EmployerMedia.EntityType > 0)
                    {
                        row.EmployerMedia.FullUrl = FileIoService.FileHelperGet(row.EmployerMedia.EntityType, row.EmployerMedia.FileName);
                    }
                    else
                    {
                        row.EmployerMedia = null;
                    }

                    row.EmployerTags = TagService.GetTagList("", row.EmployerId, 3);


                    if (list == null)
                    {
                        list = new List<EmployerSearchResults>();
                    }
                    list.Add(row);
                }
          );
            return list;
        }


    }
}
