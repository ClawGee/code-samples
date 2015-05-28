using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Sabio.Web.Models.Requests;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using Sabio.Web.Domain;

namespace Sabio.Web.Services
{
    public class PasswordService : BaseService
    {
        public static bool checkEmail(PasswordEmailRequest model)
        {
            bool exists = UserService.IsUser(model.Email);
            if (exists == true)
            {
                string user = UserService.GetUser(model.Email).Id;
                int appuserid = UserService.ConvertGuid(user);
                Guid token = UpdateUserToken(appuserid);
                AppEmailService.resetEmail(token,model.Email);
            }
            return exists;
        }

        public static Guid UpdateUserToken(int userId)
        {
            Guid uid = Guid.Empty;
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AspNetUsers_PasswordToken"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@AppUserId", userId);
                   SqlParameter p = new SqlParameter("@UId", System.Data.SqlDbType.UniqueIdentifier);
                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);

               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   Guid.TryParse(param["@UId"].Value.ToString(), out uid);
               }
               );
            return uid;
        }

        public static string GetUserToken(int userId)
        {
            string hashCode = "";
            DataProvider.ExecuteCmd(GetConnection, "dbo.AspNetUser_PasswordToken"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@AppUserId", userId);

               }, map: delegate(IDataReader reader, short set)
               {
                   int startingIndex = 0;
                    hashCode = reader.GetString(startingIndex++);
               }
               );
            return hashCode;
        }

        public static UserTokenCheck checkToken(Guid token)
        {
            UserTokenCheck user = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.AspNetUsers_CheckPasswordToken"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@PasswordToken", token);
         
               }, map: delegate(IDataReader reader, short set)
               {
                   user = new UserTokenCheck();
                   int startingIndex = 0;
                   user.Id = reader.GetString(startingIndex++);
                   user.Email = reader.GetString(startingIndex++);
               }
               );
            return user;
        }

        public static void removeToken(string userId)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AspNetUsers_RemoveToken"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Id", userId);

               }, returnParameters: delegate(SqlParameterCollection param)
               {
               }
               );
        }
    }
}
