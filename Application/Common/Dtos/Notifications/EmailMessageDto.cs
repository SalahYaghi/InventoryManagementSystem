using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Dtos.Notifications
{
    public sealed record EmailMessageDto(string To , 
    string Subject,
    string Body);
    
}
