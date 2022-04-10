using System.Net;
using MyShop.Core.Interfaces.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MyShop.Core.Options;

namespace MyShop.Core.Services;

public class MailKitSenderService : IEmailSender
{
    private readonly SmtpConfig _config;
    
    public MailKitSenderService(IOptions<SmtpConfig> options)
    {
        _config = options.Value;
    }
    
    public void SendEmail(MimeMessage message)
    {
        using var client = new SmtpClient();
        client.Timeout = 60000;
        client.Connect(_config.Host, _config.Port, SecureSocketOptions.StartTls);
        client.Authenticate(_config.Username, _config.Password);
        client.Send(message);
        client.Disconnect(true);
    }

    public MimeMessage CreateMessage(string from, string to, string subject, string body)
    {
        var message = new MimeMessage();

        message.From.Add(new MailboxAddress(from, from));
        message.To.Add(new MailboxAddress(to,to));
        message.Subject = subject;
        message.Body = new TextPart(TextFormat.Plain)
        {
            Text = body
        };

        return message;
    }
}