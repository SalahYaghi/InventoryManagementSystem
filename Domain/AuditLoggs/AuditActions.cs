using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AuditLoggs
{
    public enum  AuditActions
    {
        Login,
        RefreshToken,
        AccessDeniedOutsideWorkingHours,
        UnAuthrizedResourcesAccessDenied,

        Activate,
        Deactivate,

        ResetPassword,
     }
}
