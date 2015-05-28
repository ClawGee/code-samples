using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sabio.Web.Models;
using System;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Sabio.Web.Exceptions;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Sabio.Web.Models.Requests;
using System.Data.SqlClient;
using Sabio.Web.Domain;
using System.Data;
using Sabio.Data;


namespace Sabio.Web.Services
{
    public class UserService : BaseService
    {


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


        //SC* Wed night 2015-02-11
        public static void UpdateUser(AccountCreateRequestModel model, string id)
        {

            //string currentUser = UserService.GetCurrentUserId();
            int AppUserId = UserService.GetAppUserId(id);

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AspNetUsers_Update",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@AppUserId", AppUserId);
                    paramCollection.AddWithValue("@FirstName", model.FirstName);
                    paramCollection.AddWithValue("@LastName", model.LastName);
                    paramCollection.AddWithValue("@UserType", model.UserType);

                });

            // SC* added this 2015-02-17 to automatically login user after account is created  :-)
            // JT - took this auto sign in away because accounts must be confirmed first. UserService.Signin(model.EmailAddress, model.Password);

        }

        //check if the acct 
        //Inserting the Account Confirmation Token - Jason
        public static Guid InsertAccountConfirmationToken(AccountCreateRequestModel model)
        {
            Guid token = Guid.Empty;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AspNetUsersConfirmationToken_Insert",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Email", model.EmailAddress);

                SqlParameter p = new SqlParameter("@AccountConfirmationToken", System.Data.SqlDbType.UniqueIdentifier);
                p.Direction = System.Data.ParameterDirection.Output;

                paramCollection.Add(p);
            },

            returnParameters: delegate(SqlParameterCollection param)
            {
                Guid.TryParse(param["@AccountConfirmationToken"].Value.ToString(), out token);
            }
            );

            return token;

        }

        //Validating using the account confirmation token, and setting emailconfrimed to 1
        public static void AccountConfirmationUsingtoken(Guid token)
        {


            //may need to use execute nonquery
            // 1. token 

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AspNetUsersEmailConfirmed_Update",
            inputParamMapper: delegate(SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@AccountConfirmationToken", token);

            }
            );


        }

        //takes email from forgot password and inserts a token in the table
        public static Guid InsertTokenUsingEmail(ForgotPasswordRequestModel model)
        {

            Guid token = Guid.Empty;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AspNetUsersForgotPasswordToken_Insert",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Email", model.Email);

                    SqlParameter p = new SqlParameter("@ForgotPasswordToken", System.Data.SqlDbType.UniqueIdentifier);
                    p.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(p);

                },

                returnParameters: delegate(SqlParameterCollection param)
                {
                    Guid.TryParse(param["@ForgotPasswordToken"].Value.ToString(), out token);
                }
            );

            return token;

        }

        //when user clicks on the link in email, this validates the token
        public static ForgotPasswordUserDomainModel FindUserUsingTokenSelect(string token)
        {

            ForgotPasswordUserDomainModel p = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.AspNetUsersForgotPasswordUser_Select",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@ForgotPasswordToken", token);
                },
                map: delegate(IDataReader reader, short set)
                {
                    p = new ForgotPasswordUserDomainModel();

                    int startingIndex = 0;
                    p.Id = reader.GetSafeString(startingIndex++);
                    p.Email = reader.GetSafeString(startingIndex++);
                    p.FirstName = reader.GetSafeString(startingIndex++);


                }

                );

            return p;
        }

        //when user types in new pwd, this sets his forgot password token back to null
        public static void ResetForgotPasswordToken(string AspNetUsersId)
        {


            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AspNetUsersForgotPasswordTokenReset_Update",
            inputParamMapper: delegate(SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", AspNetUsersId);

            }
            );


        }

        //
        public static bool CheckEmailConfirmed(string emailaddress)
        {
            bool exists = false;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AspNetUsersEmailConfirmed_Select",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Email", emailaddress);

                SqlParameter p = new SqlParameter("@EmailConfirmed", System.Data.SqlDbType.Bit);
                p.Direction = System.Data.ParameterDirection.Output;

                paramCollection.Add(p);
            },
            returnParameters: delegate(SqlParameterCollection param)
            {
                exists = Convert.ToBoolean(param["@EmailConfirmed"].Value.ToString());
            }
            );

            return exists;
        }

        public static bool Signin(string emailaddress, string password)
        {
            bool result = false;

            ApplicationUserManager userManager = GetUserManager();
            IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

            ApplicationUser user = userManager.Find(emailaddress, password);

            if (user != null)
            {
                bool userEmailConfirmed = CheckEmailConfirmed(emailaddress);

                if (!userEmailConfirmed)
                {

                    throw new SabioException("Email Not Confirmed - this is a card that needs to be picked up, send another email to confirm email.");

                }

                else
                {
                    ClaimsIdentity signin = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, signin);
                    result = true;
                }


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
            bool result = false;

            IdentityUser user = GetCurrentUser();

            if (user != null)
            {
                IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                authenticationManager.SignOut();
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

        public static int GetAppUserId(string hashId)
        {

            int appUserId = 0;

            //executeQuery would get a whole row back
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AppUserIdSelect", inputParamMapper: delegate(SqlParameterCollection paramCollection)
            {

                //parameters of the stored procedure 
                paramCollection.AddWithValue("@Id", hashId);

                SqlParameter p = new SqlParameter("@AppUserId", System.Data.SqlDbType.Int);
                p.Direction = System.Data.ParameterDirection.Output;

                paramCollection.Add(p);

            },

            returnParameters: delegate(SqlParameterCollection param)
            {
                string aUserId = param["@AppUserId"].Value.ToString();
                Int32.TryParse(aUserId, out appUserId);
            });

            return appUserId;

        }

        public static AccountDomainModel SelectUserInfo(string currentUserId)
        {
            int appuserid = UserService.GetAppUserId(currentUserId);
            AccountDomainModel accountModel = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.AspNetUsers_SelectAccount"
                , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@AppUserId", appuserid);
                }
               , map: delegate(IDataReader reader, short set)
               {
                   accountModel = new AccountDomainModel();
                   int startingIndex = 0;

                   accountModel.FirstName = reader.GetSafeString(startingIndex++);
                   accountModel.LastName = reader.GetSafeString(startingIndex++);
                   //accountModel.EmailAddress = reader.GetSafeString(startingIndex++);
                   //accountModel.EmailConfirmed = reader.GetSafeBool(startingIndex++);
                   //accountModel.Uid = reader.GetSafeString(startingIndex++);
               });
            return accountModel;
        }

        public static void UpdateUserAccountInfo(AccountUpdateRequest model, string currentUserId)
        {
            int appuserid = UserService.GetAppUserId(currentUserId);
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.AspNetUsers_UpdateAccount"
                , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@AppUserId", appuserid);
                    paramCollection.AddWithValue("@FirstName", model.FirstName);
                    paramCollection.AddWithValue("@LastName", model.LastName);

                }, returnParameters: delegate(SqlParameterCollection param)
                {
                    //should be empty
                }
                );

        }
    }


}
