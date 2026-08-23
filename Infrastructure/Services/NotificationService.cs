using Application.Common.Dtos.Notifications;
using Application.Common.Interfaces;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
 

namespace Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        public async Task SendEmailAsync(EmailMessageDto messageDto, CancellationToken ct = default)
        {
        //    var message = new MimeMessage();

        //    message.From.Add(new MailboxAddress(messageDto));
        //    message.To.Add(MailboxAddress.Parse(messageDto.To));
        //    message.Subject = messageDto.Subject;

        //    message.Body = new BodyBuilder
        //    {
        //        TextBody =  messageDto.Body
        //    }.ToMessageBody();


        //    using var client = new SmtpClient();

        //    await client.ConnectAsync(
        //        _settings.Host,
        //        _settings.Port,
        //        SecureSocketOptions.StartTls,
        //        ct);

        //    await client.AuthenticateAsync(
        //        _settings.Username,
        //        _settings.Password,
        //        ct);

        //    await client.SendAsync(message, ct);
        //    await client.DisconnectAsync(true, ct);
        }

        public async Task SendSMSAsync(SmsMessageDto messageDto, CancellationToken ct = default)
        {
           // throw new NotImplementedException();
        }
    }
}
