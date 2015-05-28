using Sabio.Web.Domain;
using Sabio.Web.Exceptions;
using Sabio.Web.Models;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;
using Amazon.S3.Transfer;
using Amazon;


namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/accounts")]
    public class AccountsApiController : ApiController
    {
        //eric, jason
        [Route("login"), HttpPost]
        public HttpResponseMessage EchoAccountValidation(LoginUserRequestModel model)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                // call UserService.createUser w/ email & password

            }

            try
            {
                bool success = UserService.Signin(model.Email, model.LoginPassword);



                if (success)
                {
                    SuccessResponse sresponce = new SuccessResponse();
                    return Request.CreateResponse(HttpStatusCode.OK, sresponce);
                }
                else
                {
                    ErrorResponse sresponce = new ErrorResponse("Invalid Login. Please check your email and password, try again");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, sresponce);

                }
            }

               // try catch example for more exceptions
            //catch (SabioException mex)
            //{
            //    ErrorResponse sresponce = new ErrorResponse(mex.Message);
            //    return Request.CreateResponse(HttpStatusCode.BadRequest, sresponce);

            //}

            catch (Exception ex)
            {
                ErrorResponse sresponce = new ErrorResponse(ex.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, sresponce);
            }
        }

        //jason
        [Route("logout"), HttpGet]
        public HttpResponseMessage Logout()
        {
            SuccessResponse response = new SuccessResponse();
            UserService.Logout();

            return Request.CreateResponse(response);
        }

        //spicer
        [Route, HttpPost]
        public HttpResponseMessage AccountCreate(AccountCreateRequestModel model)
        {
            // if the Model does not pass validation, there will be an Error response returned with errors
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ApplicationUser newUser = null;
            // call UserService.createUser w/ email & password
            try
            {
                newUser = (ApplicationUser)UserService.CreateUser(model.EmailAddress, model.Password);
                UserService.UpdateUser(model, newUser.Id);

                //ItemResponse<Guid> response = new ItemResponse<Guid>();
                Guid AccountConfirmationToken = UserService.InsertAccountConfirmationToken(model);
                //response.Item = AccountConfirmationToken;

                var AccountConfirmationTokenString = AccountConfirmationToken.ToString();

                AppEmailService.SendAccountConfirmationEmail(model.EmailAddress, AccountConfirmationTokenString);

            }
            catch (IdentityResultException ire)
            {
                // here: looking for specific IdentityResultException error, 400 error
                ErrorResponse error = new ErrorResponse(ire.Result.Errors);
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
                //return Request.CreateResponse(HttpStatusCode.BadRequest, model);
                //return Request.CreateResponse(HttpStatusCode.BadRequest, "ERROR 400: User is already taken!!");
            }
            catch (Exception ex)
            {
                //here: catching any general server exception, Internal Server Error 500
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            //Account Confirmation Email written by Jason Tsai. 
            //If new user exists, then send account confirmation email



            return Request.CreateResponse(newUser);
        }

        //Claudia Get Manage Account Info
        [Route, HttpGet]
        [Authorize]
        public HttpResponseMessage GetManageAccountInfo()
        {
            string currentUserId = UserService.GetCurrentUserId();
            ItemResponse<AccountDomainModel> response = new ItemResponse<AccountDomainModel>();
            response.Item = UserService.SelectUserInfo(currentUserId);
            return Request.CreateResponse(response);
        }

        //Claudia Manage account update
        [Authorize]
        [Route, HttpPut]
        public HttpResponseMessage Update(AccountUpdateRequest model)
        { 
            string currentUserId = UserService.GetCurrentUserId();

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            //Below is for consistency's sake/professional way. 

            ItemResponse<bool> response = new ItemResponse<bool>();
            UserService.UpdateUserAccountInfo(model, currentUserId);

            response.Item = true;

            return Request.CreateResponse(response);
        }

        //jason send account confirmation email
        [Route("validateaccount/{token:guid}"), HttpGet]
        public HttpResponseMessage ValidateAccountConfirmationToken(Guid token)
        {

            SuccessResponse response = new SuccessResponse();
            UserService.AccountConfirmationUsingtoken(token);

            return Request.CreateResponse(response);

        }

        //jason
        [Route("sendforgotpasswordtoken"), HttpPost]
        public HttpResponseMessage SendEmail(ForgotPasswordRequestModel model)
        {

            //is model state valid?
            if (!ModelState.IsValid)
            {
                //if not, then throw up an error
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            }
            else
            {
                //check using an existing service to see if this email exists.
                bool doesUserExist = UserService.IsUser(model.Email);

                if (!doesUserExist)
                {
                    //if the user does not exist
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email address does not exist in our system");
                }
                else
                {
                    //pass in email and etc in store proc dbo.forgotpasswordtoken_insert

                    //why do i write this line again? do i need this?
                    ItemResponse<Guid> response = new ItemResponse<Guid>();

                    //use newToken as the variable to get the forgot password token
                    Guid newToken = UserService.InsertTokenUsingEmail(model);
                    response.Item = newToken;

                    //should i be using that token then to send out the email or should i be writing a new class

                    var newTokenString = newToken.ToString();

                    AppEmailService.SendForgotPasswordToken(model.Email, newTokenString);

                    return Request.CreateResponse(response);
                }
            }
        }

        //jason take the hash make a link out of it, make sure it exists in the database
        [Route("validateforgotpasswordtoken/{token:guid}"), HttpGet]
        public HttpResponseMessage ValidateForgotPasswordToken(string token)
        {
            //C401859F-F480-4BD4-99C7-A90BF08C62DE

            ItemResponse<ForgotPasswordUserDomainModel> response = new ItemResponse<ForgotPasswordUserDomainModel>();

            ForgotPasswordUserDomainModel item = UserService.FindUserUsingTokenSelect(token);

            response.Item = item;

            return Request.CreateResponse(response);
        }

        //jason [Route ("reset")]
        [Route("updatepassword"), HttpPut]
        public HttpResponseMessage UpdatePassword(UpdatePasswordRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {

                ItemResponse<bool> response = new ItemResponse<bool>();

                //bool updateSuccess = UserService.ChangePassWord(model.AspNetUsersId, model.Password);

                //response.Item = updateSuccess;


                response.Item = UserService.ChangePassWord(model.AspNetUsersId, model.Password);

                //response.Item = UserService.ChangePassWord(id, pwd);

                // write the call to service to make forgotpassword token null
                UserService.ResetForgotPasswordToken(model.AspNetUsersId);

                // write the call to service to make forgotpassword token null

                return Request.CreateResponse(response);

            }

        }


    //Claudia updating password in manage accounts
    //[Route("password"), HttpPut]
        //[Authorize]
    //public HttpResponseMessage NewPassword(AccountPasswordUpdateModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }
        //    else
        //    {

        //        ItemResponse<bool> response = new ItemResponse<bool>();
        //        UserService.UpdateUserAccountInfo(model);

        //        response.Item = true;

        //        return Request.CreateResponse(response);

        //    }

        //}
        [Route("upload"), HttpPost]
        public HttpResponseMessage Post() 
        {
            HttpResponseMessage result = null;

            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count > 0)
            {             
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    string uniqueName = Guid.NewGuid().ToString() + postedFile.FileName; //uniqueName is the remoteName. Guid makes it so that file names do not repeat
                    var localPath = HttpContext.Current.Server.MapPath("~/FileTemp/" + uniqueName);

                    postedFile.SaveAs(localPath);
                    docfiles.Add(localPath);
                    Sabio.Web.Services.FileService.uploadFile(localPath, uniqueName, postedFile.ContentType);
                    
                    string currentUserId = UserService.GetCurrentUserId();
                    int appUserId = UserService.GetAppUserId(currentUserId); 

                    UploadInsertRequestModel model = new UploadInsertRequestModel();
                    model.Filename = uniqueName;
                    model.ContentType = postedFile.ContentType;
                    model.EntityType = 4;
                    model.EntityId = appUserId; 
                    model.MediaType = 1;

                    MediaService.UploadInsert(model, currentUserId);

                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }


    }
}