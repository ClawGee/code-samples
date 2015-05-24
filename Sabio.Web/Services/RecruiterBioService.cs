using Sabio.Web.Domain;
using Sabio.Web.Models.request;
using Sabio.Web.Models.Requests.Requiter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services
{
    public class RecruiterBioService :BaseService
    {
        public static void Update(RecruiterUpdateRequest model, string currentUserId)
        {
            int appUserId = UserService.GetAppUserId(currentUserId);



            DataProvider.ExecuteNonQuery(GetConnection, "AppRecruiterBackGround_Update"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {

                   paramCollection.AddWithValue("@IndustryExperiance", model.Industry);
                   paramCollection.AddWithValue("@PlacementPerYear", model.Placements);
                   paramCollection.AddWithValue("@TypeOfRoles", model.Roles);
                   paramCollection.AddWithValue("@Location", model.Locations);
                   paramCollection.AddWithValue("@website", model.Website);
                   paramCollection.AddWithValue("@CompanyName", model.CompanyName);
                   paramCollection.AddWithValue("@PhoneNumber", model.PhoneNumbers);
                   paramCollection.AddWithValue("@Email", model.Email);
                   paramCollection.AddWithValue("@Uid", model.recuiterUid);
                   paramCollection.AddWithValue("@AppUserId", appUserId);
                   

               }
               

               
               );

        }
        public static Guid Create(recruiterRequestModel model, string currentUserId)
        {
            //wiki documented - service layer - search "data provider"
           // int id = 0;
            Guid uid = Guid.Empty;
            int appUserId = UserService.GetAppUserId(currentUserId);
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppRecruiterBackGround_Insert"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {



                   paramCollection.AddWithValue("@IndustryExperiance", model.Industry);
                   paramCollection.AddWithValue("@PlacementPerYear", model.Placements);
                   paramCollection.AddWithValue("@TypeOfRoles", model.Roles);
                   paramCollection.AddWithValue("@Location", model.Locations);
                   paramCollection.AddWithValue("@website", model.Website);
                   paramCollection.AddWithValue("@CompanyName", model.CompanyName);
                   paramCollection.AddWithValue("@PhoneNumber", model.PhoneNumbers);
                   paramCollection.AddWithValue("@Email", model.Email);
                   paramCollection.AddWithValue("@AppUserId", appUserId);
                  





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


        public static RecruiterBioDomainModel GetBios(string currentUserId)
        {
            //this needs to be fix to return multipl

            //SC* new code UserService.GetAppUserId here
            int AppUserId = UserService.GetAppUserId(currentUserId);

            RecruiterBioDomainModel bio = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AppRecruiterBackGround_SelectByAppUserId",
               inputParamMapper: delegate(SqlParameterCollection paramCollection)//(to level bun)
               {
                   paramCollection.AddWithValue("@Id", AppUserId);//(middle level "meat" is stored proc)
               }
               , map: delegate(IDataReader reader, short set)
               {
                   bio = new RecruiterBioDomainModel();
                   int startingIndex = 0; //startingOrdinal   type var = new type

                   bio.Industry = reader.GetString(startingIndex++);
                   bio.Placements = reader.GetString(startingIndex++);
                   bio.Roles = reader.GetString(startingIndex++);
                   bio.Locations = reader.GetString(startingIndex++);
                   bio.Website = reader.GetString(startingIndex++);
                   bio.CompanyName = reader.GetString(startingIndex++);
                   bio.PhoneNumbers = reader.GetString(startingIndex++);
                   bio.Email = reader.GetString(startingIndex++);
                   bio.Uid = reader.GetGuid(startingIndex++);
                   bio.Id = reader.GetInt32(startingIndex++);
                  

               });


            return bio;
        }



        internal static RecruiterBioDomainModel GetBios(Guid uid)
        {
            RecruiterBioDomainModel bio = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AppRecruiterBackGround_Select",
               inputParamMapper: delegate(SqlParameterCollection paramCollection)//(to level bun)
               {
                   paramCollection.AddWithValue("@uid", uid);//(middle level "meat" is stored proc)
               }
               , map: delegate(IDataReader reader, short set)
               {
                   bio = new RecruiterBioDomainModel();
                   int startingIndex = 0; //startingOrdinal   type var = new type

                   bio.Industry = reader.GetString(startingIndex++);
                   bio.Placements = reader.GetString(startingIndex++);
                   bio.Roles = reader.GetString(startingIndex++);
                   bio.Locations = reader.GetString(startingIndex++);
                   bio.Website = reader.GetString(startingIndex++);
                   bio.CompanyName = reader.GetString(startingIndex++);
                   bio.PhoneNumbers = reader.GetString(startingIndex++);
                   bio.Email = reader.GetString(startingIndex++);
                   bio.Uid = reader.GetGuid(startingIndex++);
                   bio.Id = reader.GetInt32(startingIndex++);
                   

               });


            return bio;
        }

        public static object Uid { get; set; }

        public static object appUserId { get; set; }

        public static object AppUserId { get; set; }
    }
}