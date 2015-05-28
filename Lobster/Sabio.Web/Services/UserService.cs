using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sabio.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Sabio.Data;
using Microsoft.AspNet.Identity.Owin;
using Sabio.Web.Exceptions;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Data.SqlClient;
using Sabio.Web.Models.Requests;
using Sabio.Web.Domain;
using System.Diagnostics;
using System.Net;


namespace Sabio.Web.Services
{
    public class UserService : BaseService
    {

        public static void UpdateUser(RegisterRequest model, string id)
        {

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AspNetUsers_Update"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@FirstName", model.FirstName);
                   paramCollection.AddWithValue("@LastName", model.LastName);
                   paramCollection.AddWithValue("@Email", model.Email);
                   paramCollection.AddWithValue("@UserType", model.ProfileType);
                   paramCollection.AddWithValue("@Id", id);

               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   //No output parameters 
               }
               );
        }

        public static void AdminUpdate(AdminRequest model, string id)
        {

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AspNetUsers_Update"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@FirstName", model.FirstName);
                   paramCollection.AddWithValue("@LastName", model.LastName);
                   paramCollection.AddWithValue("@Email", model.Email);
                   paramCollection.AddWithValue("@UserType", model.UserType);
                   paramCollection.AddWithValue("@Id", id);

               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   //No output parameters 
               }
               );
        }




        private static ApplicationUserManager GetUserManager()
        {
            return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public static IdentityUser CreateUser(string email, string password)
        {
            ApplicationUserManager userManager = GetUserManager();

            ApplicationUser newUser = new ApplicationUser { UserName = email, Email = email, LockoutEnabled = false };
            IdentityResult result = null;
            try
            {
                result = userManager.Create(newUser, password);

            }
            catch
            {
                new IdentityResultException(result);
            }

            if (result.Succeeded)
            {
                return newUser;
            }
            else
            {
                throw new IdentityResultException(result);
            }
        }


        public static bool Signin(string emailaddress, string password)
        {
            bool result = false;

            ApplicationUserManager userManager = GetUserManager();
            IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

            ApplicationUser user = userManager.Find(emailaddress, password);
            if (user != null)
            {
                ClaimsIdentity signin = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, signin);
                result = true;

            }
            return result;
        }

        public static bool IsUser(string emailaddress)
        {
            bool result = false;

            ApplicationUserManager userManager = GetUserManager();
            IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

            ApplicationUser user = userManager.FindByEmail(emailaddress);


            if (user != null)
            {

                result = true;

            }

            return result;
        }

        public static ApplicationUser GetUser(string emailaddress)
        {


            ApplicationUserManager userManager = GetUserManager();
            IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

            ApplicationUser user = userManager.FindByEmail(emailaddress);

            return user;
        }


        public static ApplicationUser GetUserById(string userId)
        {

            ApplicationUserManager userManager = GetUserManager();
            IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

            ApplicationUser user = userManager.FindById(userId);

            return user;
        }

        public static FullUser GetFullUserById(string CurrentUserId)
        {
            int LoggedInUser = UserService.ConvertGuid(CurrentUserId);
            FullUser FullUserRow = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AspNetUsers_Select",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@AppUserId", LoggedInUser);
                }
               , map: delegate(IDataReader reader, short set)
               {
                   FullUserRow = new FullUser();
                   int startingIndex = 0; //startingOrdinal

                   FullUserRow.Id = reader.GetSafeString(startingIndex++);
                   FullUserRow.Email = reader.GetSafeString(startingIndex++);
                   FullUserRow.EmailConfirmed = reader.GetSafeBool(startingIndex++);
                   FullUserRow.SecurityStamp = reader.GetSafeString(startingIndex++);
                   FullUserRow.PhoneNumber = reader.GetSafeString(startingIndex++);
                   FullUserRow.PhoneNumberConfirmed = reader.GetSafeBool(startingIndex++);
                   FullUserRow.TwoFactorEnabled = reader.GetSafeBool(startingIndex++);
                   FullUserRow.LockoutEndDateUtc = reader.GetSafeDateTime(startingIndex++);
                   FullUserRow.LockoutEnabled = reader.GetSafeBool(startingIndex++);
                   FullUserRow.AccessFailedCount = reader.GetSafeInt32(startingIndex++);
                   FullUserRow.UserName = reader.GetSafeString(startingIndex++);
                   FullUserRow.FirstName = reader.GetSafeString(startingIndex++);
                   FullUserRow.LastName = reader.GetSafeString(startingIndex++);
                   FullUserRow.UserType = reader.GetSafeInt32(startingIndex++);
                   //FullUserRow.ConfirmationUid = reader.GetGuid(startingIndex++);
                   FullUserRow.Avatar = new Media();
                   FullUserRow.Avatar.EntityId = reader.GetSafeInt32(startingIndex++);
                   FullUserRow.Avatar.EntityType = reader.GetSafeInt32(startingIndex++);
                   FullUserRow.Avatar.ContentType = reader.GetSafeString(startingIndex++);
                   //  FullUserRow.Avatar.Uid = reader.GetGuid(startingIndex++);
                   FullUserRow.Avatar.CreateDate = reader.GetSafeDateTime(startingIndex++);
                   FullUserRow.Avatar.FileName = reader.GetSafeString(startingIndex++);

                   if ((FullUserRow.Avatar.FileName != null) && (FullUserRow.Avatar.EntityType > 0))
                   {
                       FullUserRow.Avatar.FullUrl = FileIoService.FileHelperGet(FullUserRow.Avatar.EntityType, FullUserRow.Avatar.FileName);
                   }
                   else
                   {
                       FullUserRow.Avatar = null;
                   }
               }
               );
            return FullUserRow;
        }

        public static List<AllUsers> GetAllUsers()
        {
            List<AllUsers> allUsersList = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AspNetUsers_AllUsersSelect"
               , inputParamMapper: null
               , map: delegate(IDataReader reader, short set)
               {
                   AllUsers AllUsersRow = new AllUsers();
                   int startingIndex = 0; //startingOrdinal

                   AllUsersRow.Id = reader.GetSafeString(startingIndex++);
                   AllUsersRow.Email = reader.GetSafeString(startingIndex++);
                   AllUsersRow.EmailConfirmed = reader.GetSafeBool(startingIndex++);
                   AllUsersRow.SecurityStamp = reader.GetSafeString(startingIndex++);
                   AllUsersRow.PhoneNumber = reader.GetSafeString(startingIndex++);
                   AllUsersRow.PhoneNumberConfirmed = reader.GetSafeBool(startingIndex++);
                   AllUsersRow.TwoFactorEnabled = reader.GetSafeBool(startingIndex++);
                   AllUsersRow.LockoutEndDateUtc = reader.GetSafeDateTime(startingIndex++);
                   AllUsersRow.LockoutEnabled = reader.GetSafeBool(startingIndex++);
                   AllUsersRow.AccessFailedCount = reader.GetSafeInt32(startingIndex++);
                   AllUsersRow.UserName = reader.GetSafeString(startingIndex++);
                   AllUsersRow.FirstName = reader.GetSafeString(startingIndex++);
                   AllUsersRow.LastName = reader.GetSafeString(startingIndex++);
                   AllUsersRow.UserType = reader.GetSafeInt32(startingIndex++);
                   AllUsersRow.MediaId = reader.GetSafeInt32(startingIndex++);
                   AllUsersRow.EntityId = reader.GetSafeInt32(startingIndex++);
                   AllUsersRow.EntityType = reader.GetSafeInt32(startingIndex++);
                   AllUsersRow.ContentType = reader.GetSafeString(startingIndex++);
                   AllUsersRow.CreateDate = reader.GetSafeDateTime(startingIndex++);
                   AllUsersRow.FileName = reader.GetSafeString(startingIndex++);
                   AllUsersRow.Deleted = reader.GetSafeBool(startingIndex++);
                   //AllUsersRow.Id = reader.GetSafeString(startingIndex++);

                   if ((AllUsersRow.FileName != null) && (AllUsersRow.EntityType > 0) && (AllUsersRow.Deleted != true))
                   {
                       AllUsersRow.FullUrl = FileIoService.FileHelperGet(AllUsersRow.EntityType, AllUsersRow.FileName);
                   }
                   else
                   {
                       AllUsersRow.FullUrl = null;
                   }

                   if (allUsersList == null)
                   {
                       allUsersList = new List<AllUsers>();
                   }

                   allUsersList.Add(AllUsersRow);
               }
               );
               return allUsersList;
        }
        
        public static bool ChangePassWord(string userId, string newPassword)
        {
            bool result = false;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(newPassword))
            {
                throw new Exception("You must provide a userId and a password");
            }

            ApplicationUser user = GetUserById(userId);

            if (user != null)
            {

                ApplicationUserManager userManager = GetUserManager();

                userManager.RemovePassword(userId);
                IdentityResult res = userManager.AddPassword(userId, newPassword);

                result = res.Succeeded;

            }

            return result;
        }


        public static bool Logout()
        {
           // AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            bool result = false;

            IdentityUser user = GetCurrentUser();

            

            if (user != null)
            {
                IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                result = true;
            }

            return result;
        }


        public static IdentityUser GetCurrentUser()
        {
            if (!IsLoggedIn())
                return null;
            ApplicationUserManager userManager = GetUserManager();

            IdentityUser currentUserId = userManager.FindById(GetCurrentUserId());
            return currentUserId;
        }

        public static string GetCurrentUserId()
        {
            return HttpContext.Current.User.Identity.GetUserId();
        }


        public static bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(GetCurrentUserId());

        }

        public static int ConvertGuid(string guid)
        {
            int userId = 0;
            DataProvider.ExecuteCmd(GetConnection, "dbo.AspNetUserId_Select"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Uid", guid);
               }
               , map: delegate(IDataReader reader, short set)
               {
                   int startingIndex = 0;
                   userId = reader.GetSafeInt32(startingIndex++);
               }
               );
            return userId;

        }
        public static void UpdateAccount(UpdateAccountRequest model, string loggedInUser)
        {
            int CurrentUserId = UserService.ConvertGuid(loggedInUser);

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AspNetUsers_UpdateAccount"
                , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@FirstName", model.firstName);
                    paramCollection.AddWithValue("@LastName", model.lastName);
                    paramCollection.AddWithValue("@PhoneNumber", model.phoneNumber);
                    paramCollection.AddWithValue("@AppUserId", CurrentUserId);

                }, returnParameters: delegate(SqlParameterCollection param)
                {
                    //no output parameters
                }
                );
        }

        //==============gene's code to return guid for registration email.==================
        public static Guid RegistrationConfirm(string NewUserId)
        {
            Guid uid = Guid.Empty;//000-0000-0000-0000 

            DataProvider.ExecuteNonQuery(GetConnection, "AspNetUsersRegistrationConfirm_Update"

               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@NewUserId", NewUserId);

                   SqlParameter p = new SqlParameter("@ConfirmationUid", System.Data.SqlDbType.UniqueIdentifier);
                   p.Direction = System.Data.ParameterDirection.Output;
                   paramCollection.Add(p);

               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   Guid.TryParse(param["@ConfirmationUid"].Value.ToString(), out uid);
               });

            return uid;
        }

        public static UserTokenCheck GetGuidRegistrationMatchToken(Guid confirmationUid)//SELECT
        {
            UserTokenCheck user = null;

            DataProvider.ExecuteCmd(GetConnection, "AspNetUsersRegistrationConfirm_Select"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@ConfirmationUid", confirmationUid);

               }, map: delegate(IDataReader reader, short set)
               {
                   user = new UserTokenCheck();
                   int startingIndex = 0; //startingOrdinal

                   user.Id = reader.GetSafeString(startingIndex++);
                   user.Email = reader.GetSafeString(startingIndex++);
               }
               );
            return user;
        }

        public static void ChangeRegistrationEmailConfirmed(Guid confirmationUid)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AspNetUsersRegistrationEmailConfirm_Update"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {

                   paramCollection.AddWithValue("@confirmationUid", confirmationUid);

               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   //No output parameters 
               }
               );
        }

        public static EntityIdentifiers GetEntity(string loggedInUser)
        {
            int entityType = 0;
            int entityId = 0;

            EntityIdentifiers ident = null;

            int currentUserId = UserService.ConvertGuid(loggedInUser);

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppGalleries_SelectByEntityType"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@AppUserId", currentUserId);

               SqlParameter type = new SqlParameter("@EntityType", System.Data.SqlDbType.Int);
                   type.Direction = System.Data.ParameterDirection.Output;
               SqlParameter id = new SqlParameter("@EntityId", System.Data.SqlDbType.Int);
                   id.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(type);
                   paramCollection.Add(id);

               }, returnParameters: delegate(SqlParameterCollection param)
               {
                   ident = new EntityIdentifiers();
                   int.TryParse(param["@EntityType"].Value.ToString(), out entityType);
                   int.TryParse(param["@EntityId"].Value.ToString(), out entityId);
                   ident.EntityType = entityType;
                   ident.EntityId = entityId;
               }
               );
            return ident;
        }
    }
}