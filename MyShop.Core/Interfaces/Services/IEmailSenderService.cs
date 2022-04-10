using MimeKit;

namespace MyShop.Core.Interfaces.Services;

public interface IEmailSender
{
    void SendEmail(MimeMessage mimeMessage);

    MimeMessage CreateMessage(string from, string to, string subject, string body);
}