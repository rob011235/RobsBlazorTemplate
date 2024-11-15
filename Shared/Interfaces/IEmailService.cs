using SharedClasses.Models;

namespace SharedClasses.Interfaces
{
    public interface IEmailService

    {
        public Task<bool> SendEmailAsync(MailData mailData, CancellationToken ct = default);
    }
}
