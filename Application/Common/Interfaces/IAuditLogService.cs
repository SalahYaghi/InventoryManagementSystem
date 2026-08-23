using Application.Common.Dtos.Loggs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Common.Interfaces
{
    public interface IAuditLogService
    {

        Task SaveUserOperationsAudits(CreateUserOperationsCommands request, CancellationToken ct = default);
        Task SaveUserLoggingAudits(CreateUserLoggingCommands request, CancellationToken ct = default);

    }
}
