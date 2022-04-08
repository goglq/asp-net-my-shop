using MimeKit;

namespace MyShop.Core.Interfaces.Services;

public interface IEmailSender
{
    Task SendEmail(MimeMessage mimeMessage);
}