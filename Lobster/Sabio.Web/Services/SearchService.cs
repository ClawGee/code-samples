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
    public class SearchService : BaseService
    {
        public static List<Developer> DeveloperSearch(SearchRequest request)
        {
            List<Developer> list = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.AppDeveloperProfiles_Search_V2"
                , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    if (request == null)
                    {
                        paramCollection.AddWithValue("@Keyword", null);
                    }
                    else
                    {
                        paramCollection.AddWithValue("@Keyword", request.Keyword);
                    }
                }
                , map: delegate(IDataReader reader, short set)
                {
                    Developer row = new Developer();
                    int startingIndex = 0;
                    row.Phone = reader.GetSafeString(startingIndex++);
                    row.Email = reader.GetSafeString(startingIndex++);
                    row.Language1 = reader.GetSafeString(startingIndex++);
                    row.Language2 = reader.GetSafeString(startingIndex++);
                    row.Language3 = reader.GetSafeString(startingIndex++);
                    row.Language4 = reader.GetSafeString(startingIndex++);
                    row.Language5 = reader.GetSafeString(startingIndex++);
                    row.Experience1 = reader.GetSafeString(startingIndex++);
                    row.YearsExperience1 = reader.GetSafeInt32(startingIndex++);
                    row.Experience2 = reader.GetSafeString(startingIndex++);
                    row.YearsExperience2 = reader.GetSafeInt32(startingIndex++);
                    row.Experience3 = reader.GetSafeString(startingIndex++);
                    row.YearsExperience3 = reader.GetSafeInt32(startingIndex++); 
                    row.Experience4 = reader.GetSafeString(startingIndex++);
                    row.YearsExperience4 = reader.GetSafeInt32(startingIndex++);
                    row.Summary = reader.GetSafeString(startingIndex++);
                    row.Goals = reader.GetSafeString(startingIndex++);
                    row.Uid = reader.GetGuid(startingIndex++);
                    row.Id = reader.GetInt32(startingIndex++);
                    row.DeveloperPhoto = new Media();
                    row.DeveloperPhoto.FileName = reader.GetSafeString(startingIndex++);
                    row.DeveloperPhoto.ContentType = reader.GetSafeString(startingIndex++);
                    row.DeveloperPhoto.EntityType = reader.GetSafeInt32(startingIndex++);
                    row.DeveloperPhoto.EntityId = reader.GetSafeInt32(startingIndex++);
                    row.DeveloperPhoto.CreateDate = reader.GetSafeDateTime(startingIndex++);
                    //row.DeveloperPhoto.FullUrl = reader.GetSafeString(startingIndex++);
                    row.DeveloperPersonalInfo = new FullUser();
                    row.DeveloperPersonalInfo.Id = reader.GetSafeString(startingIndex++);
                    row.DeveloperPersonalInfo.Email = reader.GetSafeString(startingIndex++);
                    row.DeveloperPersonalInfo.EmailConfirmed = reader.GetSafeBool(startingIndex++);
                    row.DeveloperPersonalInfo.SecurityStamp = reader.GetSafeString(startingIndex++);
                    row.DeveloperPersonalInfo.PhoneNumber = reader.GetSafeString(startingIndex++);
                    row.DeveloperPersonalInfo.PhoneNumberConfirmed = reader.GetSafeBool(startingIndex++);
                    row.DeveloperPersonalInfo.TwoFactorEnabled = reader.GetSafeBool(startingIndex++);
                    row.DeveloperPersonalInfo.LockoutEndDateUtc = reader.GetSafeDateTime(startingIndex++);
                    row.DeveloperPersonalInfo.LockoutEnabled = reader.GetSafeBool(startingIndex++);
                    row.DeveloperPersonalInfo.AccessFailedCount = reader.GetSafeInt32(startingIndex++);
                    row.DeveloperPersonalInfo.UserName = reader.GetSafeString(startingIndex++);
                    row.DeveloperPersonalInfo.FirstName = reader.GetSafeString(startingIndex++);
                    row.DeveloperPersonalInfo.LastName = reader.GetSafeString(startingIndex++);
                    row.DeveloperPersonalInfo.UserType = reader.GetSafeInt32(startingIndex++);
                    //row.DeveloperPersonalInfo.ConfirmationUid = reader.GetGuid(startingIndex++);
                    row.AddressId = reader.GetSafeInt32(startingIndex++);
                    row.DeveloperAddresses = new Address();
                    row.DeveloperAddresses.AddressLine1 = reader.GetSafeString(startingIndex++);
                    row.DeveloperAddresses.AddressLine2 = reader.GetSafeString(startingIndex++);
                    row.DeveloperAddresses.City = reader.GetSafeString(startingIndex++);
                    row.DeveloperAddresses.State = reader.GetSafeInt32(startingIndex++);
                    row.DeveloperAddresses.Zip = reader.GetSafeInt32(startingIndex++);
                    Guid? addressGuid = reader.GetSafeGuid(startingIndex++);
                    if (addressGuid != null)  
                    {
                        row.DeveloperAddresses.Uid = addressGuid.Value;
                    }
                    row.DeveloperState = new State();
                    row.DeveloperState.Name = reader.GetSafeString(startingIndex++);
                    row.DeveloperState.StateProvinceCode = reader.GetSafeString(startingIndex++);

                    if (!string.IsNullOrEmpty(row.DeveloperPhoto.FileName) && row.DeveloperPhoto.EntityType > 0)
                    {
                        row.DeveloperPhoto.FullUrl = FileIoService.FileHelperGet(row.DeveloperPhoto.EntityType, row.DeveloperPhoto.FileName);
                    }
                    else
                    {
                        row.DeveloperPhoto = null;
                    }

                    if (list == null)
                    {
                        list = new List<Developer>();
                    }
                    list.Add(row);

                });
            return list;
        }

        public static List<EmployerSearchResults> SearchEmployers(SearchRequest request)
        {
            List<EmployerSearchResults> ListEmployers = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.AppEmployers_Search"
           , inputParamMapper: delegate(SqlParameterCollection paramCollection)
           {
               if (request == null)
               {
                   paramCollection.AddWithValue("@Keyword", null);
               }
               else
               {
                   paramCollection.AddWithValue("@Keyword", request.Keyword);
               }
           }
           , map: delegate(IDataReader reader, short set)
           {
               EmployerSearchResults e = new EmployerSearchResults();
               int startingIndex = 0;
               e.EmployerId = reader.GetSafeInt32(startingIndex++);
               e.EmployerName = reader.GetSafeString(startingIndex++);
               e.EmployerContactName = reader.GetSafeString(startingIndex++);
               e.EmployerIndustry = reader.GetSafeString(startingIndex++);
               e.EmployerContactEmail = reader.GetSafeString(startingIndex++);
               e.EmployerURL = reader.GetSafeString(startingIndex++);
               e.EmployerOtherLocation = reader.GetSafeString(startingIndex++);
               e.EmployerTech = reader.GetSafeString(startingIndex++);
               e.EmployerDescription = reader.GetSafeString(startingIndex++);
               e.AverageRating = reader.GetSafeDouble(startingIndex++);
               e.RatingCount = reader.GetSafeInt32(startingIndex++);
               e.Uid = reader.GetGuid(startingIndex++).ToString();
               e.EmployerMedia = new Media();
               e.EmployerMedia.FileName = reader.GetSafeString(startingIndex++);
               e.EmployerMedia.EntityType = reader.GetSafeInt32(startingIndex++);
               if (e.EmployerMedia.FileName != null && e.EmployerMedia.EntityType > 0)
               {
                   e.EmployerMedia.FullUrl = FileIoService.FileHelperGet(e.EmployerMedia.EntityType, e.EmployerMedia.FileName);
               }
               else
               {
                   e.EmployerMedia = null;
               }

               if (ListEmployers == null)
               {
                   ListEmployers = new List<EmployerSearchResults>();
               }
               ListEmployers.Add(e);
           }
          );
            return ListEmployers;
        }
        
        public static List<Recruiter> RecruiterSearch(SearchRequest request)
        {
            List<Recruiter> list = new List<Recruiter>();

            DataProvider.ExecuteCmd(GetConnection, "dbo.AppRecruiterProfiles_Search"
           , inputParamMapper: delegate(SqlParameterCollection paramCollection)
           {
               if (request == null)
               {
                   paramCollection.AddWithValue("@Keyword", null);
                   paramCollection.AddWithValue("@CurrPage", 1);
                   paramCollection.AddWithValue("@PerPage", 24);
               }
               else
               {
                   paramCollection.AddWithValue("@Keyword", request.Keyword);
                   paramCollection.AddWithValue("@CurrPage", request.CurrentPage);
                   paramCollection.AddWithValue("@PerPage", request.ItemsPerPage);
               }
           }
           , map: delegate(IDataReader reader, short set)
           {
               Recruiter r = new Recruiter();
               int startingIndex = 0;
               r.Specialty = reader.GetSafeString(startingIndex++);
               r.Location = reader.GetSafeString(startingIndex++);
               r.CompanyLogo = reader.GetSafeString(startingIndex++);
               r.URL = reader.GetSafeString(startingIndex++);
               //r.PhoneNumber1 = reader.GetSafeString(startingIndex++);
               //r.PhoneNumber2 = reader.GetSafeString(startingIndex++);
               //r.Summary = reader.GetSafeString(startingIndex++);
               //r.TwitterAccount = reader.GetSafeString(startingIndex++);
               //r.FacebookAccount = reader.GetSafeString(startingIndex++);
               //r.LinkedInAccount = reader.GetSafeString(startingIndex++);
               //r.GooglePlusAccount = reader.GetSafeString(startingIndex++);
               r.Uid = reader.GetGuid(startingIndex++);
               //r.Id = reader.GetSafeInt32(startingIndex++);
               r.RecruiterPhoto = new Media();
               //r.RecruiterPhoto.Id = reader.GetSafeInt32(startingIndex++);
               r.RecruiterPhoto.Uid = reader.GetSafeGuid(startingIndex++);
               r.RecruiterPhoto.FileName = reader.GetSafeString(startingIndex++);
               //r.RecruiterPhoto.ContentType = reader.GetSafeString(startingIndex++);
               r.RecruiterPhoto.EntityType = reader.GetSafeInt32(startingIndex++);
               r.RecruiterPhoto.EntityId = reader.GetSafeInt32(startingIndex++);
               //r.RecruiterPhoto.CreateDate = reader.GetDateTime(startingIndex++);
               //r.RecruiterPhoto.MediaType = reader.GetSafeInt32(startingIndex++);
               if (!string.IsNullOrEmpty(r.RecruiterPhoto.FileName) && r.RecruiterPhoto.EntityType != 0)
               {
                   r.RecruiterPhoto.FullUrl = FileIoService.FileHelperGet(r.RecruiterPhoto.EntityType, r.RecruiterPhoto.FileName);
               }
               r.RecruiterUser = new FullUser();
               r.RecruiterUser.Id = reader.GetSafeString(startingIndex++);
               //r.RecruiterUser.Email = reader.GetSafeString(startingIndex++);
               //r.RecruiterUser.PhoneNumber = reader.GetSafeString(startingIndex++);
               //r.RecruiterUser.UserName = reader.GetSafeString(startingIndex++);
               r.RecruiterUser.FirstName = reader.GetSafeString(startingIndex++);
               r.RecruiterUser.LastName = reader.GetSafeString(startingIndex++);
               //r.RecruiterUser.UserType = reader.GetSafeInt32(startingIndex++);
               r.RecruiterUser.Avatar = new Media();
               //r.RecruiterUser.Avatar.Id = reader.GetSafeInt32(startingIndex++);
               //r.RecruiterUser.Avatar.Uid = reader.GetSafeGuid(startingIndex++);
               r.RecruiterUser.Avatar.FileName = reader.GetSafeString(startingIndex++);
               //r.RecruiterUser.Avatar.ContentType = reader.GetSafeString(startingIndex++);
               r.RecruiterUser.Avatar.EntityType = reader.GetSafeInt32(startingIndex++);
               r.RecruiterUser.Avatar.EntityId = reader.GetSafeInt32(startingIndex++);
               //r.RecruiterUser.Avatar.CreateDate = reader.GetSafeDateTime(startingIndex++);
               //r.RecruiterUser.Avatar.MediaType = reader.GetSafeInt32(startingIndex++);
               if (!string.IsNullOrEmpty(r.RecruiterUser.Avatar.FileName) && r.RecruiterUser.Avatar.EntityType != 0)
               {
                   r.RecruiterUser.Avatar.FullUrl = FileIoService.FileHelperGet(r.RecruiterUser.Avatar.EntityType, r.RecruiterUser.Avatar.FileName);
               }
               list.Add(r);
           }
           );
            return list;
        }

        public static List<Job> SearchJobs(SearchRequest request)
        {
            List<Job> ListJobs = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.AppJobs_Search"
           , inputParamMapper: delegate(SqlParameterCollection paramCollection)
           {
               if (request == null)
               {
                   paramCollection.AddWithValue("@Keyword", null);
               }
               else
               {
                   paramCollection.AddWithValue("@Keyword", request.Keyword);
               }
           }
           , map: delegate(IDataReader reader, short set)
           {
               Job j = new Job();
               int startingIndex = 0;
               j.Uid = reader.GetGuid(startingIndex++);
               j.Title = reader.GetSafeString(startingIndex++);
               j.Description = reader.GetSafeString(startingIndex++);
               j.Qualifications = reader.GetSafeString(startingIndex++);
               j.Contacts = reader.GetSafeString(startingIndex++);
               j.PrimaryEmail = reader.GetSafeString(startingIndex++);
               j.Id = reader.GetSafeInt32(startingIndex++);
               j.Url = reader.GetSafeString(startingIndex++);
               j.Rate = reader.GetSafeString(startingIndex++);
               j.Terms = reader.GetSafeString(startingIndex++);
               j.EntityId = reader.GetSafeInt32(startingIndex++);
               j.EntityType = reader.GetSafeInt32(startingIndex++);
               if (ListJobs == null)
               {
                   ListJobs = new List<Job>();
               }
               ListJobs.Add(j);
           }
          );
            return ListJobs;
        }
    }
}