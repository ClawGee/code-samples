using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using SendGrid;
using Sabio.Web.Models.Requests;
using Sabio.Web.Domain;

namespace Sabio.Web.Services
{
    public class AppEmailService
    {
        public static void SendShareProfile(ShareProfileRequest model, string userRequestedId) 
        {
            FullUser ProfileObject = UserService.GetFullUserById(userRequestedId); 
            var shareProfileMessage = new SendGridMessage(); //sets a variable that calls a SendGridMessage function.
            string senderString = model.SenderName + " <" + model.SenderEmail + ">"; //sets a variable equal to the sender's name and email address.
            string recipientString = model.RecipientName + " <" + model.RecipientEmail + ">"; //sets a variable equal to the recipient's name and address.
            shareProfileMessage.From = new MailAddress("aaron@sabio.la"); //defines which email address the email will be sent from.
            List<String> recipients = new List<String>// defines a list of email addresses to receive the outgoing email.
                    {
                        senderString,
                        recipientString
                    };
            shareProfileMessage.AddTo(recipients);
            shareProfileMessage.Subject = "Recruit.Us Profile Share: " + model.Subject; //this defines the email's subject line.
            string body = "<p>Dear " //defines a variable (body) that will contain the html content of the email's message.
                + model.RecipientName
                + ", </p>"
                + "<br />" //dear recipient, sender sends you the following profile and message.
                + "<p>"
                + model.SenderName
                + " thought you might be interested in the following profile found on Recruit.Us. "
                + model.SenderName
                + " writes: </p>"
                + "<br />"
                + "<p>'"
                + model.Message
                + "'</p>"
                + "<br />"
                + "<p>-------------</p>"
                + "<h3>RECRUIT.US PROFILE SHARE</h3>"  //beginning of profile share info: name, photo, user-type, link to full profile.
                + "<p><strong>Name: "  
                + ProfileObject.FirstName
                + " "
                + ProfileObject.LastName
                + "</strong></p>";

                switch (ProfileObject.UserType) { //switch statement based on user type (i.e. developer, recruiter, employer).
                    case 1:
                        Developer DeveloperObject = DeveloperService.Select(userRequestedId); //calls the devel service to look for a photo
                        if (DeveloperObject.DeveloperPhoto != null)
                        {
                            body += "<p><strong>Profile Photo: </strong></p>"
                                + "<a href='http://recruitus.azurewebsites.net/profile/developer/" //body contains devel profile photo (if not null), user-type, and link to public profile.
                                + ProfileObject.Id
                                + "'>"
                                + "<img style='height: 80px; width: 80px; margin: 2px; border: 2px' src='" 
                                + DeveloperObject.DeveloperPhoto.FullUrl 
                                + "' />"
                                + "</a>";
                        }
                            body += "<p><strong>Developer</strong></p>"
                                + "<br />"
                                + "<a href='http://recruitus.azurewebsites.net/profile/developer/"
                                + ProfileObject.Id
                                + "'>Click here to see the full profile on Recruit.Us.</a>";
                        break;
                    case 2:
                        Recruiter RecruiterObject = RecruiterService.Select(userRequestedId);  //calls the recru service to look for a photo
                        if (RecruiterObject.RecruiterPhoto != null)
                        {
                            body += "<p><strong>Profile Photo: </strong></p>"
                                + "<a href='http://recruitus.azurewebsites.net/profile/recruiter/" //body contains recruiter profile photo (if not null), user-type, and link to public profile.
                                + ProfileObject.Id
                                + "'>"
                                + "<img style='height: 80px; width: 80px; margin: 2px; border: 2px' src='"
                                + RecruiterObject.RecruiterPhoto.FullUrl
                                + "' />"
                                + "</a>";
                        }
                            body += "<p><strong>Recruiter</strong></p>"
                                + "<br />"
                                + "<a href='http://recruitus.azurewebsites.net/profile/recruiter/"
                                + ProfileObject.Id
                                + "'>Click here to see the full profile on Recruit.Us.</a>";
                        break;
                    case 3:
                        Employer EmployerObject = EmployerService.SelectEmployerByUserId(userRequestedId);  //calls the empl service to look for a photo
                        if (EmployerObject.Media != null)
                        {
                            body += "<p><strong>Profile Photo: </strong></p>"
                                + "<a href='http://recruitus.azurewebsites.net/profile/employer/"  //body contains empl profile photo (if not null), user-type, and link to public profile.
                                + ProfileObject.Id
                                + "'>"
                                + "<img style='height: 80px; width: 80px; margin: 2px; border: 2px' src='" 
                                + EmployerObject.Media.FullUrl 
                                + "' />"
                                + "<a/>";
                        }
                            body += "<p><strong>Employer</strong></p>"
                                + "<br />"
                                + "<a href='http://recruitus.azurewebsites.net/profile/employer/"
                                + ProfileObject.Id
                                + "'>Click here to see the full profile on Recruit.Us.</a>";
                        break;
                    default:
                        body += "<p><strong>Oops! The person being shared has not defined a user-type (i.e., developer, recruiter, or employer).</strong></p>" //id user-type isn't 1,2, or 3 a generic msg and link are given.
                            + "<br />"
                            + "<a href='http://recruitus.azurewebsites.net/'>Click here to see additional profiles on Recruit.Us.</a>";
                        break;
                };

            shareProfileMessage.Html = body;
            // Creates network credentials to access the SendGrid account.
            var username = "Gregorio";
            var pswd = "LosAngeles8!";
            var credentials = new NetworkCredential(username, pswd);
            SendGrid.Web transportWeb = new SendGrid.Web(credentials); // Creates a Web transport for sending the email.
            transportWeb.Deliver(shareProfileMessage);// Sends the email.
        }
        
        public static void SendContactUs(ContactUsRequest model)//email copy back to user
        {
            var myMessage = new SendGridMessage();
            string RecipientString = model.Name + " <" + model.Email + ">";
            myMessage.From = new MailAddress("aaron@sabio.la"); // Add the message properties.
            List<String> recipients = new List<String>// Add multiple addresses to the To field.
                    {
                        @RecipientString
                    };
            myMessage.AddTo(recipients);
            myMessage.Subject = "Recruit.Us Inquiry: " + model.Subject;
            myMessage.Html = "<h3>Thank you, " + model.Name +", for your email!</h3><p>We will respond, to your message below, within 24 hours.</p><p>Message: <em>" + model.Message + "</em></p><p>Phone Number: " + model.PhoneNumber + "</p>";
            // Create network credentials to access your SendGrid account.

            var credentials = new NetworkCredential(username, pswd);
            SendGrid.Web transportWeb = new SendGrid.Web(credentials); // Create an Web transport for sending email.
            transportWeb.Deliver(myMessage);// Send the email.
        }

        public static void SendContactUsToAdmin(ContactUsRequest model)//contact us email to us/admin
        {
            var myMessage = new SendGridMessage();
            myMessage.From = new MailAddress("ContactUsInquiry@sabio.la"); // Add the message properties.
            List<String> recipients = new List<String>// Add multiple addresses to the To field.
                    {
                        @"Admin <genehan01@gmail.com>"
                    };
            myMessage.AddTo(recipients);
            myMessage.Subject = "Contact Us Inquiry: " + model.Subject;
            myMessage.Html = "<h3>New inquiry from Contact Us page.</h3><p>Please respond, to the message below, within 24 hours.</p><p>Name: " + model.Name + "</p><p>Message: <em>" + model.Message + "</em></p><p>Phone Number: " + model.PhoneNumber + "</p>";
            // Create network credentials to access your SendGrid account.

            var credentials = new NetworkCredential(username, pswd);
            SendGrid.Web transportWeb = new SendGrid.Web(credentials); // Create an Web transport for sending email.
            transportWeb.Deliver(myMessage);// Send the email.
        }
 //SendRegistrationConfirmationEmail: for when a user first registers and once they finish, this will send an email for the to click to confirm the address
        
        public static void SendRegistrationConfirmationEmail(RegisterRequest model, string newUserId)
        {
       // Guid confirmUid = call UserService.RegistrationConfirm
            Guid NewId = UserService.RegistrationConfirm(newUserId);
       //email code
            var myMessage = new SendGridMessage();
            string RecipientString = model.FirstName + " <" + model.Email + ">";
            myMessage.From = new MailAddress("aaron@sabio.la"); 
            List<String> recipients = new List<String>// Add multiple addresses to the To field.
                    {
                        @RecipientString
                    };
            myMessage.AddTo(recipients);
            myMessage.Subject = "Registration Confirmation";
            myMessage.Html = "<h3>Thank you for registering with Recruit.Us! Please click the link below to return to the site and log in.</h3><a href='http://recruitus.azurewebsites.net/registration/confirmed/" + NewId + "'>Log in to Recruit.Us</a>";

            var credentials = new NetworkCredential(username, pswd);
            SendGrid.Web transportWeb = new SendGrid.Web(credentials); // Create an Web transport for sending email.
            transportWeb.Deliver(myMessage);// Send the email.
        }

        public static void resetEmail(Guid token, string email)
        {
            string key = token.ToString();
            Console.Write(key);
            var myMessage = new SendGridMessage();
            string RecipientString = email;
            myMessage.From = new MailAddress("aaron@sabio.la"); // Add the message properties.
            myMessage.Subject = "Password Reset Request ";
            List<String> recipients = new List<String>// Add multiple addresses to the To field.
                    {
                        @RecipientString
                    };
            myMessage.AddTo(recipients); //this is a string
            myMessage.Html = "<h3>Click on the link below to reset your password!</h3><a href=" + "'" + "http://recruitus.azurewebsites.net/Password/ChangePassword/" + key + "'>Click Here</a>";
            // Create network credentials to access your SendGrid account.

            var credentials = new NetworkCredential(username, pswd);
            SendGrid.Web transportWeb = new SendGrid.Web(credentials); // Create an Web transport for sending email.
            transportWeb.Deliver(myMessage);// Send the email.
        }
    }
}