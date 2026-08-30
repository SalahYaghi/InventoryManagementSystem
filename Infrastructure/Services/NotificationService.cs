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
    
        }

        public async Task SendSMSAsync(SmsMessageDto messageDto, CancellationToken ct = default)
        {
        }
    }
}
