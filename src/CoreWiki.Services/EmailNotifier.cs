namespace CoreWiki.Services
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Configuration;
    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class EmailNotifier : IEmailSender
    {
        private readonly IConfiguration configuration;

        public EmailNotifier(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            if (string.IsNullOrEmpty(this.configuration["SendGridApiKey"]))
            {
                return;
            }

            var message = new SendGridMessage();
            message.SetFrom(new EmailAddress("noreply@corewiki.com", "No Reply Team"));
            var recipient = new EmailAddress(email);

            message.AddTo(recipient);
            message.SetSubject(subject);

            message.AddContent(MimeType.Html, htmlMessage);

            var apiKey = this.configuration["SendGridApiKey"];
            var client = new SendGridClient(apiKey);

            var response = await client.SendEmailAsync(message);
        }
    }
}