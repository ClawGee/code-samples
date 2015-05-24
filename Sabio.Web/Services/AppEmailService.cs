using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using SendGrid;
using Sabio.Web.Services;

namespace Sabio.Web.Services
{
    public class AppEmailService : BaseService
    {
        public static void SendContactUs(string email, string name, string subject, string message) 
        {


            SendGridMessage myMessage = new SendGridMessage();
            myMessage.AddTo("c06@sabio.la");
            myMessage.From = new MailAddress(email, name);
            myMessage.Subject = subject;
            myMessage.Text = message;
 
           // Create credentials, specifying your user name and password.
           // var credentials = new NetworkCredential("username", "password");

            // Create an Web transport for sending email.
            SendGrid.Web transportWeb = new SendGrid.Web(credentials);

            // Send the email.
            transportWeb.Deliver(myMessage);

        }

        public static void SendForgotPasswordToken(string email, string newToken)
        {


            //wuh oh where do i stick my messages about the forgot password
            SendGridMessage myMessage = new SendGridMessage();
            myMessage.AddTo(email);
            myMessage.From = new MailAddress("c06@sabio.la", "Santa Cruz Administrator");
            myMessage.Subject = "Password Reset";
            myMessage.Text = "Use this link to reset your password: http://santacruz.dev/accounts/updatepassword/" + newToken + ".";

            SendGrid.Web transportWeb = new SendGrid.Web(credentials);

            transportWeb.Deliver(myMessage);

        }

        public static void SendAccountConfirmationEmail(string email, string newToken)
        {

            SendGridMessage myMessage = new SendGridMessage();
            myMessage.AddTo(email);
            myMessage.From = new MailAddress("c06@sabio.la", "Santa Cruz Administrator");
            myMessage.Subject = "Santa Cruz Account Update - verify your identity!";
            myMessage.Text = "Use this link to confirm your account: http://santacruz.dev/accounts/confirmation/" + newToken + ". Welcome to Santa Cruz, there's hackers everywhere! You're on your way to WINNING. Add us to your address book so you don't miss any emails! - Team Santa Cruz";

            SendGrid.Web transportWeb = new SendGrid.Web(credentials);

            transportWeb.Deliver(myMessage);
        }

        internal static void SendContactUs(string p1, string p2, string p3, string p4, string p5, string p6, string p7, string p8)
        {
            throw new NotImplementedException();
        }
    }
}