using Microsoft.AspNetCore.Identity;
using Server.Data;
using SharedClasses.Interfaces;
using SharedClasses.Models;

namespace Server.Services
{
    public class EmailSender : IEmailSender<ApplicationUser>
    {
        private readonly ILogger _logger;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private string webSiteName = "webSiteName";

        public EmailSender(ILogger<EmailSender> logger, IEmailService emailService, IConfiguration configuration)
        {
            _logger = logger;
            _emailService = emailService;
            _configuration = configuration;
            webSiteName = _configuration["WebSiteName"]??"Web Site";
        }

        public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
        {
            MailData mailData = new MailData(
                to: new List<string>() { email },
                subject: $"Please confirm your email for {webSiteName}",
                body: $"Please comfirm your email here: {confirmationLink}"
            );

            bool result = await _emailService.SendEmailAsync(mailData);
        }

        public async Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
        {
            MailData mailData = new MailData(
                to: new List<string>() { email },
                subject: $"Reset code for {webSiteName}",
                body: $"Here is your reset code: {resetCode}"
            );

            bool result = await _emailService.SendEmailAsync(mailData);
        }

        public async Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
        {

            MailData mailData = new MailData(
                to: new List<string>() { email },
                subject: $"Password reset for {webSiteName}",
                body: $"Please comfirm your email here: {resetLink}"
            );

            bool result = await _emailService.SendEmailAsync(mailData);
        }
    }
}
