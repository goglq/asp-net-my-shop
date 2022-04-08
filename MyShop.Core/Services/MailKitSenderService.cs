using MyShop.Core.Interfaces.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace MyShop.Core.Services;

public class MailKitSenderService : IEmailSender
{
    public async Task SendEmail(MimeMessage message)
    {
        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
        await client.AuthenticateAsync("username", "password");
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}