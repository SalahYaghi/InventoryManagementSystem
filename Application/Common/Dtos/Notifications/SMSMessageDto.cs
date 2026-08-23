using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Dtos.Notifications
{
    public sealed record SmsMessageDto(
     string PhoneNumber,
     string Message);

}
