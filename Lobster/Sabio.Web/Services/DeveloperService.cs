using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Data;

namespace Sabio.Web.Services
{
    public class DeveloperService : BaseService
    {
        
        
        //Function WITHOUT GOOGLE MAPS API 

        public static Guid Insert(DeveloperProfileCreateRequest model, string currentUserId)
        {
            Guid uid = Guid.Empty;//000-0000-0000-0000
            int AppUserId = UserService.ConvertGuid(currentUserId);
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppDeveloperProfiles_Insert"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@AppUserId", AppUserId);
                   paramCollection.AddWithValue("@Phone", model.Phone);
                   paramCollection.AddWithValue("@Email", model.Email);
                   paramCollection.AddWithValue("@Language1", model.Language1);
                   paramCollection.AddWithValue("@Language2", model.Language2);
                   paramCollection.AddWithValue("@Language3", model.Language3);
                   paramCollection.AddWithValue("@Language4", model.Language4);
                   paramCollection.AddWithValue("@Language5", model.Language5);
                   paramCollection.AddWithValue("@Experience1", model.Experience1);
                   paramCollection.AddWithValue("@YearsExperience1", model.YearsExperience1);
                   paramCollection.AddWithValue("@Experience2", model.Experience2);
                   paramCollection.AddWithValue("@YearsExperience2", model.YearsExperience2);
                   paramCollection.AddWithValue("@Experience3", model.Experience3);
                   paramCollection.AddWithValue("@YearsExperience3", model.YearsExperience3);
                   paramCollection.AddWithValue("@Experience4", model.Experience4);
                   paramCollection.AddWithValue("@YearsExperience4", model.YearsExperience4);
                   paramCollection.AddWithValue("@Summary", model.Summary);
                   paramCollection.AddWithValue("@Goals", model.Goals);

                   SqlParameter p = new SqlParameter("@Uid", System.Data.SqlDbType.UniqueIdentifier);
                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);

               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   Guid.TryParse(param["@Uid"].Value.ToString(), out uid);
               }
               );

            AddressCreateRequest developerAddress = new AddressCreateRequest();
            developerAddress.AddressLine1 = model.AddressLine1;
            developerAddress.AddressLine2 = model.AddressLine2;
            developerAddress.City = model.City;
            developerAddress.State = model.State;
            developerAddress.Zip = model.Zip;
            developerAddress.Lat = model.Lat;
            developerAddress.Lng = model.Lng;
            Guid DeveloperAddressGuidResponse = AddressService.InsertAddress(developerAddress, currentUserId);

            DeveloperService.InsertDeveloperAddress(uid, DeveloperAddressGuidResponse);

            return uid;
        }

        public static void InsertDeveloperAddress(Guid uid, Guid DeveloperAddressGuidResponse)
        {
            DataProvider.ExecuteNonQuery(GetConnection,"dbo.AppDeveloperAddresses_Insert"
                , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                    {
                        paramCollection.AddWithValue("@developerGuid", uid);
                        paramCollection.AddWithValue("@addressGuid", DeveloperAddressGuidResponse);
                    
                    },returnParameters: delegate(SqlParameterCollection param)
                {

                }

            );
        }





        public static Guid Update(DeveloperUpdateRequest model, string currentUserId)
        {
            Guid uid = Guid.Parse(currentUserId);
            int AppUserId = UserService.ConvertGuid(currentUserId);

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppDeveloperProfiles_Update"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   
                   paramCollection.AddWithValue("@Phone", model.Phone);
                   paramCollection.AddWithValue("@Email", model.Email);
                   paramCollection.AddWithValue("@Language1", model.Language1);
                   paramCollection.AddWithValue("@Language2", model.Language2);
                   paramCollection.AddWithValue("@Language3", model.Language3);
                   paramCollection.AddWithValue("@Language4", model.Language4);
                   paramCollection.AddWithValue("@Language5", model.Language5);
                   paramCollection.AddWithValue("@Experience1", model.Experience1);
                   paramCollection.AddWithValue("@YearsExperience1", model.YearsExperience1);
                   paramCollection.AddWithValue("@Experience2", model.Experience2);
                   paramCollection.AddWithValue("@YearsExperience2", model.YearsExperience2);
                   paramCollection.AddWithValue("@Experience3", model.Experience3);
                   paramCollection.AddWithValue("@YearsExperience3", model.YearsExperience3);
                   paramCollection.AddWithValue("@Experience4", model.Experience4);
                   paramCollection.AddWithValue("@YearsExperience4", model.YearsExperience4);
                   paramCollection.AddWithValue("@Summary", model.Summary);
                   paramCollection.AddWithValue("@Goals", model.Goals);
                   paramCollection.AddWithValue("@AppUserId", AppUserId);
               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   //Guid.TryParse(param["@Uid"].Value.ToString(), out uid);
               }
               );

            
            AddressCreateRequest developerAddress = new AddressCreateRequest();
            developerAddress.AddressLine1 = model.AddressLine1;
            developerAddress.AddressLine2 = model.AddressLine2;
            developerAddress.City = model.City;
            developerAddress.State = model.State;
            developerAddress.Zip = model.Zip;
            developerAddress.Lat = model.Lat;
            developerAddress.Lng = model.Lng;
            Guid addressGuid = model.AddressGuid;
            

            AddressService.UpdateAddress(developerAddress, addressGuid, currentUserId);

            return uid;
        }


      


        public static Developer Select(string currentUserId)
        {
            Developer d = null;
            int CurrentUserId = UserService.ConvertGuid(currentUserId);
            DataProvider.ExecuteCmd(GetConnection, "dbo.AppDeveloperProfiles_Select"
                , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@AppUserId", CurrentUserId);
                }
                , map: delegate(IDataReader reader, short set)
                {
                    d = new Developer();
                    int startingIndex = 0;
                    d.Phone = reader.GetSafeString(startingIndex++);
                    d.Email = reader.GetSafeString(startingIndex++);
                    d.Language1 = reader.GetSafeString(startingIndex++);
                    d.Language2 = reader.GetSafeString(startingIndex++);
                    d.Language3 = reader.GetSafeString(startingIndex++);
                    d.Language4 = reader.GetSafeString(startingIndex++);
                    d.Language5 = reader.GetSafeString(startingIndex++);
                    d.Experience1 = reader.GetSafeString(startingIndex++);
                    d.YearsExperience1 = reader.GetSafeInt32(startingIndex++);
                    d.Experience2 = reader.GetSafeString(startingIndex++);
                    d.YearsExperience2 = reader.GetSafeInt32(startingIndex++);
                    d.Experience3 = reader.GetSafeString(startingIndex++);
                    d.YearsExperience3 = reader.GetSafeInt32(startingIndex++); 
                    d.Experience4 = reader.GetSafeString(startingIndex++);
                    d.YearsExperience4 = reader.GetSafeInt32(startingIndex++);
                    d.Summary = reader.GetSafeString(startingIndex++);
                    d.Goals = reader.GetSafeString(startingIndex++);
                    d.Uid = reader.GetGuid(startingIndex++);
                    d.Id = reader.GetInt32(startingIndex++);
                    d.MediaId = reader.GetSafeInt32(startingIndex++);
                    d.AverageRating = reader.GetSafeDouble(startingIndex++);
                    d.RatingCount = reader.GetSafeInt32(startingIndex++);
                    d.AddressId = reader.GetSafeInt32(startingIndex++);
                    d.DeveloperAddresses = new Address();
                    d.DeveloperAddresses.AddressLine1 = reader.GetSafeString(startingIndex++);
                    d.DeveloperAddresses.AddressLine2 = reader.GetSafeString(startingIndex++);
                    d.DeveloperAddresses.City = reader.GetSafeString(startingIndex++);
                    d.DeveloperAddresses.State = reader.GetSafeInt32(startingIndex++);
                    d.DeveloperAddresses.Zip = reader.GetSafeInt32(startingIndex++);
                    d.DeveloperAddresses.Uid = reader.GetGuid(startingIndex++);
                    d.DeveloperPhoto = new Media();
                    d.DeveloperPhoto.FileName = reader.GetSafeString(startingIndex++);
                    d.DeveloperPhoto.ContentType = reader.GetSafeString(startingIndex++);
                    d.DeveloperPhoto.EntityType = reader.GetSafeInt32(startingIndex++);
                    d.DeveloperPhoto.EntityId = reader.GetSafeInt32(startingIndex++);
                    d.DeveloperPhoto.CreateDate = reader.GetSafeDateTime(startingIndex++);
                    if (d.DeveloperPhoto.FileName != null && d.DeveloperPhoto.EntityType != 0)
                    {
                        d.DeveloperPhoto.FullUrl = FileIoService.FileHelperGet(1, d.DeveloperPhoto.FileName);
                    }
                    else
                    {
                        d.DeveloperPhoto = null;
                    }
                    d.DeveloperPersonalInfo = UserService.GetFullUserById(currentUserId);
                    });
                    return d;
                }

        public static Guid DeveloperAddressGuidResponse { get; set; }

        public static object AddressGuid { get; set; }
    }
    }

   