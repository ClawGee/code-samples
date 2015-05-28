using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Data;
using Sabio.Web.Models.Requests;
using Sabio.Web.Domain.Tests;

namespace Sabio.Web.Services
{
    public class AddressService : BaseService
    {

        public static Guid InsertAddress(AddressCreateRequest model, string CurrentUserId)
        {
            int LoggedInUser = UserService.ConvertGuid(CurrentUserId);
            Guid Uid = Guid.Empty;//000-0000-0000-0000

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppAddresses_Insert"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@AddressLine1", model.AddressLine1);
                   paramCollection.AddWithValue("@AddressLine2", model.AddressLine2);
                   paramCollection.AddWithValue("@City", model.City);
                   paramCollection.AddWithValue("@State", model.State);
                   paramCollection.AddWithValue("@Zip", model.Zip);
                   paramCollection.AddWithValue("@Lat", model.Lat);
                   paramCollection.AddWithValue("@Lng", model.Lng);
                   paramCollection.AddWithValue("@AppUserId", LoggedInUser);

                   SqlParameter p = new SqlParameter("@Uid", System.Data.SqlDbType.UniqueIdentifier);
                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);

               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   Guid.TryParse(param["@Uid"].Value.ToString(), out Uid);
               }
               );


            return Uid;
        }

        public static void UpdateAddress(AddressCreateRequest model, Guid AddressGuid, string CurrentUserId)
        {

            int LoggedInUser = UserService.ConvertGuid(CurrentUserId);
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppAddresses_Update"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@AddressLine1", model.AddressLine1);
                   paramCollection.AddWithValue("@AddressLine2", model.AddressLine2);
                   paramCollection.AddWithValue("@City", model.City);
                   paramCollection.AddWithValue("@State", model.State);
                   paramCollection.AddWithValue("@Zip", model.Zip);
                   paramCollection.AddWithValue("@Lat", model.Lat);
                   paramCollection.AddWithValue("@Lng", model.Lng);
                   paramCollection.AddWithValue("@Uid", AddressGuid);
                   paramCollection.AddWithValue("@AppUserId", LoggedInUser);



               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   //This does not need to return anything.
               }
               );
        }

        public static List<State> GetAmericanStates()
        {
            List<State> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.StateProvince_SelectAmericanStates",
                inputParamMapper: null, map: delegate(IDataReader reader, short set)
            {
                State StateRow = new State();
                int startingIndex = 0; //startingOrdinal

                StateRow.StateProvinceId = reader.GetSafeInt32(startingIndex++);
                StateRow.StateProvinceCode = reader.GetSafeString(startingIndex++);
                StateRow.Name = reader.GetSafeString(startingIndex++);


                if (list == null)
                {
                    list = new List<State>();
                }

                list.Add(StateRow);
            }
               );
            return list;
        }

        public static Address GetAddressBlock(string CurrentUserId)
        {
            int LoggedInUser = UserService.ConvertGuid(CurrentUserId);
            Address AddressRow = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AppAddresses_Select",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@AppUserId", LoggedInUser);
               }
               , map: delegate(IDataReader reader, short set)
            {
                AddressRow = new Address();
                int startingIndex = 0; //startingOrdinal

                AddressRow.AddressLine1 = reader.GetSafeString(startingIndex++);
                AddressRow.AddressLine2 = reader.GetSafeString(startingIndex++);
                AddressRow.City = reader.GetSafeString(startingIndex++);
                AddressRow.State = reader.GetSafeInt32(startingIndex++);
                AddressRow.Zip = reader.GetSafeInt32(startingIndex++);
                AddressRow.Lat = reader.GetDecimal(startingIndex++);
                AddressRow.Lng = reader.GetDecimal(startingIndex++);
                AddressRow.Uid = reader.GetGuid(startingIndex++);

            }
               );
            return AddressRow;
        }

        public static List<FullUser> GetAddressRadiusBlock(AddressRadiusRequest model)
        {
            List<FullUser> list = new List<FullUser>();
            FullUser user = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.AppAddresses_RadiusSelect",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@lat", model.Lat);
                    paramCollection.AddWithValue("@lng", model.Lng);
                    paramCollection.AddWithValue("@distance", model.Distance);
                }
               , map: delegate(IDataReader reader, short set)
               {
                   user = new FullUser();
                   int startingIndex = 0; //startingOrdinal
                   user.Id = reader.GetSafeString(startingIndex++);
                   user.Email = reader.GetSafeString(startingIndex++);
                   user.PhoneNumber = reader.GetSafeString(startingIndex++);
                   user.UserName = reader.GetSafeString(startingIndex++);
                   user.FirstName = reader.GetSafeString(startingIndex++);
                   user.LastName = reader.GetSafeString(startingIndex++);
                   user.UserType = reader.GetSafeInt32(startingIndex++);
                   user.address = new Address();
                   user.address.AddressLine1 = reader.GetSafeString(startingIndex++);
                   user.address.AddressLine2 = reader.GetSafeString(startingIndex++);
                   user.address.City = reader.GetSafeString(startingIndex++);
                   user.address.State = reader.GetSafeInt32(startingIndex++);
                   user.address.Zip = reader.GetSafeInt32(startingIndex++);
                   user.address.Lat = reader.GetDecimal(startingIndex++);
                   user.address.Lng = reader.GetDecimal(startingIndex++);
                   user.address.Uid = reader.GetGuid(startingIndex++);
                   user.address.Distance = reader.GetSafeDouble(startingIndex++);
                   list.Add(user);
               }
               );
            return list;
        }

    }
}