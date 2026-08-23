using Application.Common.Dtos.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task SendEmailAsync( EmailMessageDto messageDto ,CancellationToken ct = default);
        Task SendSMSAsync(SmsMessageDto messageDto,CancellationToken ct = default); 
    }
}
