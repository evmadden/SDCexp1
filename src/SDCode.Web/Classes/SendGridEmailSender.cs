using System;
using Microsoft.Extensions.Hosting;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SDCode.Web.Classes
{
    public interface IEmailSender
    {
        void Send(IEmailAddress fromEmail, IEmailAddress toEmail, string subject, string body);
    }

    public class SendGridEmailSender : IEmailSender
    {
        public async void Send(IEmailAddress fromEmail, IEmailAddress toEmail, string subject, string body)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY") ?? throw new Exception("SENDGRID_API_KEY environment variable not found.");
            var client = new SendGridClient(apiKey);
            var from = new SendGrid.Helpers.Mail.EmailAddress(fromEmail.Email, fromEmail.Name);
            var to = new SendGrid.Helpers.Mail.EmailAddress(toEmail.Email, toEmail.Name);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, null);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

    }

    public interface IEmailAddress
    {
        string Email { get; }
        string Name { get; }
    }

    public class EmailAddress : IEmailAddress
    {
        public EmailAddress(string email, string name)
        {
            Email = email;
            Name = name;
        }

        public string Email { get; private set; }
        public string Name { get; private set; }
    }
}
