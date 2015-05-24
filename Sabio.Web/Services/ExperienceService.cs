using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Data;
using Sabio.Web.Models.Requests;

namespace Sabio.Web.Services
{
    public class ExperienceService : BaseService
    {

        public static Guid Post(ExperienceRequestModel model, string currentUserId)
        {
            //000-0000-0000-0000 this is the number of digits in the Guid
            Guid uid = Guid.Empty;

            int appUserId = 0;
            appUserId = UserService.GetAppUserId(currentUserId);

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppExperiences_Insert"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@AppUserId", appUserId);
                   paramCollection.AddWithValue("@CompanyName", model.CompanyName);
                   paramCollection.AddWithValue("@Title", model.Title);
                   paramCollection.AddWithValue("@CompanyCity", model.CompanyCity);
                   paramCollection.AddWithValue("@CompanyState", model.CompanyState);
                   paramCollection.AddWithValue("@StartDateMonth", model.StartDateMonth);
                   paramCollection.AddWithValue("@StartDateYear", model.StartDateYear);
                   paramCollection.AddWithValue("@EndDateMonth", model.EndDateMonth);
                   paramCollection.AddWithValue("@EndDateYear", model.EndDateYear);
                   paramCollection.AddWithValue("@Description", model.Description);


                   SqlParameter ExperienceUId = new SqlParameter("@Uid", System.Data.SqlDbType.UniqueIdentifier);
                   ExperienceUId.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(ExperienceUId);

               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   Guid.TryParse(param["@Uid"].Value.ToString(), out uid);
               }
               );


            return uid;
        }


        public static List<ExperienceDomainModel> GetExperiencesByUserGuid(string currentUserId)//List<...> is a composite type bcuz its made up of multiple types
        {
            int appUserId = UserService.GetAppUserId(currentUserId);



            List<ExperienceDomainModel> list = null;
            //Wrapper
            DataProvider.ExecuteCmd(GetConnection, "dbo.AppExperiences_SelectByAppUserId"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)//(top level bun)
               {
                   paramCollection.AddWithValue("@AppUserId", appUserId);//(middle level "meat" is stored proc)
               },
                 map: delegate(IDataReader reader, short set)//(bottom level bun)map the rows that the database returns(suitcase within suitcase)
               {   //loops through the rows which returns one domain model (ie one row)that comes out
                   ExperienceDomainModel experienceRowModel = new ExperienceDomainModel();
                   int startingIndex = 0; //startingOrdinal   type var = new type

                   experienceRowModel.Title = reader.GetString(startingIndex++);
                   experienceRowModel.CompanyName = reader.GetString(startingIndex++);
                   experienceRowModel.CompanyCity = reader.GetString(startingIndex++);
                   //experienceRowModel.CompanyState = reader.GetString(startingIndex++);
                   experienceRowModel.StartDateMonth = reader.GetInt16(startingIndex++);
                   experienceRowModel.StartDateYear = reader.GetInt16(startingIndex++);
                   experienceRowModel.EndDateMonth = reader.GetInt16(startingIndex++);
                   experienceRowModel.EndDateYear = reader.GetInt16(startingIndex++);
                   experienceRowModel.Description = reader.GetString(startingIndex++);
                   experienceRowModel.Uid = reader.GetGuid(startingIndex++);

                   experienceRowModel.State = new StateDomainModel();
                   experienceRowModel.State.StateProvinceId = reader.GetInt32(startingIndex++);
                   experienceRowModel.State.StateProvinceCode = reader.GetString(startingIndex++);
                   experienceRowModel.State.CountryRegionCode = reader.GetString(startingIndex++);
                   experienceRowModel.State.Name = reader.GetString(startingIndex++);

                   if (list == null)
                   {
                       list = new List<ExperienceDomainModel>();
                   }

                   list.Add(experienceRowModel);
               }
               );


            return list;

        }


        public static ExperienceDomainModel Get(Guid uid)//no List because only displaying ONE Uid;
        {

            ExperienceDomainModel experienceRowModel = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AppExperiences_SelectByUId",
               inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Uid", uid);
               }
                , map: delegate(IDataReader reader, short set)
                {
                    //loops through the rows which returns one domain model (ie one row)that comes out
                    //TestEmployee p = new TestEmployee();  //type var = new type
                    experienceRowModel = new ExperienceDomainModel();

                    int startingIndex = 0; //startingOrdinal   

                    experienceRowModel.CompanyName = reader.GetString(startingIndex++);
                    experienceRowModel.Title = reader.GetString(startingIndex++);
                    experienceRowModel.CompanyCity = reader.GetString(startingIndex++);
                    //experienceRowModel.CompanyState = reader.GetInt32(startingIndex++);
                    experienceRowModel.StartDateMonth = reader.GetInt16(startingIndex++);
                    experienceRowModel.StartDateYear = reader.GetInt16(startingIndex++);
                    experienceRowModel.EndDateMonth = reader.GetInt16(startingIndex++);
                    experienceRowModel.EndDateYear = reader.GetInt16(startingIndex++);
                    experienceRowModel.Description = reader.GetString(startingIndex++);
                    experienceRowModel.Uid = reader.GetGuid(startingIndex++);

                    experienceRowModel.State = new StateDomainModel();
                    experienceRowModel.State.StateProvinceId = reader.GetInt32(startingIndex++);
                    experienceRowModel.State.StateProvinceCode = reader.GetString(startingIndex++);
                    experienceRowModel.State.CountryRegionCode = reader.GetString(startingIndex++);
                    experienceRowModel.State.Name = reader.GetString(startingIndex++);


                }
               );


            return experienceRowModel;

        }


        public static void Put(ExperienceRequestModel model, Guid uid)
        {
           DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppExperiences_Update"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {

    //@CompanyName nvarchar(50),
    //@Title nvarchar (50),
    //@CompanyCity nvarchar(50),
    //@CompanyState int,
    //@StartDateMonth nvarchar (50),
    //@StartDateYear nvarchar (50),
    //@EndDateMonth nvarchar (50),
    //@EndDateYear nvarchar (50),
    //@Description nvarchar (50),
    //@Uid uniqueidentifier


                   paramCollection.AddWithValue("@CompanyName", model.CompanyName);
                   paramCollection.AddWithValue("@Title", model.Title);
                   paramCollection.AddWithValue("@CompanyCity", model.CompanyCity);
                   paramCollection.AddWithValue("@CompanyState", model.CompanyState);
                   paramCollection.AddWithValue("@StartDateMonth", model.StartDateMonth);
                   paramCollection.AddWithValue("@StartDateYear", model.StartDateYear);
                   paramCollection.AddWithValue("@EndDateMonth", model.EndDateMonth);
                   paramCollection.AddWithValue("@EndDateYear", model.EndDateYear);
                   paramCollection.AddWithValue("@Description", model.Description);
                   paramCollection.AddWithValue("@Uid", uid);


               }, returnParameters: delegate(SqlParameterCollection param)
               {

               }
               );

        }

    }
}
       


