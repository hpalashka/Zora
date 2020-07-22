using System.Threading.Tasks;

namespace Zora.EmailService
{
    public interface IEmailSender
    {
        void SendEmail(MailMessage message);
        Task SendEmailAsync(MailMessage message);
    }
}
