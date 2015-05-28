using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Web.Services;
using System.Data;
using Sabio.Data;

namespace Sabio.Web.Services
{
    public class AdminService : BaseService
    {
        //edit button
        public static List<AdminUsersDomainModel> GetAspNetUsers()
        {

            List<AdminUsersDomainModel> list = null;

            DataProvider.ExecuteCmd(GetConnection,
            "dbo.AspNetUsersAdmin_Select",
            inputParamMapper: null
            ,
            map: delegate(IDataReader reader, short set)
            {
                AdminUsersDomainModel p = new AdminUsersDomainModel();

                int startingIndex = 0;

                p.Id = reader.GetSafeString(startingIndex++);
                p.Email = reader.GetSafeString(startingIndex++);
                p.EmailConfirmed = reader.GetSafeBool(startingIndex++);
                p.PhoneNumber = reader.GetSafeString(startingIndex++);
                p.UserName = reader.GetSafeString(startingIndex++);
                p.AppUserId = reader.GetSafeInt32(startingIndex++);
                p.FirstName = reader.GetSafeString(startingIndex++);
                p.LastName = reader.GetSafeString(startingIndex++);
                p.UserType = reader.GetSafeInt32(startingIndex++);

                if (list == null)
                {
                    list = new List<AdminUsersDomainModel>();
                }

                list.Add(p);

            }
            );

            return list;

        }
        //tab1
        public static AdminUsersDomainModel GetAspNetUser(string AspNetUsersId)
        {
            AdminUsersDomainModel p = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AspNetUsersAdminUser_Select",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", AspNetUsersId);
                },

                map: delegate(IDataReader reader, short set)
                {

                    p = new AdminUsersDomainModel();

                    int startingIndex = 0;

                    p.Id = reader.GetSafeString(startingIndex++);
                    p.Email = reader.GetSafeString(startingIndex++);
                    p.EmailConfirmed = reader.GetSafeBool(startingIndex++);
                    p.PasswordHash = reader.GetSafeString(startingIndex++);
                    p.PhoneNumber = reader.GetSafeString(startingIndex++);
                    p.UserName = reader.GetSafeString(startingIndex++);
                    p.AppUserId = reader.GetSafeInt32(startingIndex++);
                    p.FirstName = reader.GetSafeString(startingIndex++);
                    p.LastName = reader.GetSafeString(startingIndex++);
                    p.UserType = reader.GetSafeInt32(startingIndex++);
                }


                );

            return p;
        }

        public static void PutAspNetUser(AdminUpdateUserRequestModel model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AspNetUsersAdminUser_Update",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@AppUserId", model.AppUserId);
                    paramCollection.AddWithValue("@Email", model.Email);
                    paramCollection.AddWithValue("@EmailConfirmed", model.EmailConfirmed);
                    paramCollection.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    paramCollection.AddWithValue("@UserName", model.UserName);
                    paramCollection.AddWithValue("@FirstName", model.FirstName);
                    paramCollection.AddWithValue("@LastName", model.LastName);
                    paramCollection.AddWithValue("@UserType", model.UserType);
                }
                );
        }
        //tab2
        public static List<UserAddress> GetAddresses(string AppUserId)
        {
            List<UserAddress> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AppUserAddressesAdmin_Select",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@AppUserId", AppUserId);
                },
                map: delegate(IDataReader reader, short set)
                {
                    UserAddress p = new UserAddress();
                    int startingIndex = 0;

                    p.UId = reader.GetGuid(startingIndex++);
                    p.Address1 = reader.GetSafeString(startingIndex++);
                    p.Address2 = reader.GetSafeString(startingIndex++);
                    p.Zip = reader.GetSafeString(startingIndex++);
                    p.City = reader.GetSafeString(startingIndex++);
                    p.State = new StateDomainModel();
                    p.State.StateProvinceId = reader.GetInt32(startingIndex++);
                    p.State.StateProvinceCode = reader.GetString(startingIndex++);
                    p.State.CountryRegionCode = reader.GetString(startingIndex++);
                    p.State.Name = reader.GetString(startingIndex++);

                    if (list == null)
                    {
                        list = new List<UserAddress>();
                    }
                    list.Add(p);
                }
                );
            return list;
        }

        public static void DeleteAddress(string CurrentAddressGuid)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppUserAddressesAdmin_Delete",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@UId", CurrentAddressGuid);
                }
                );
        }
    }
}