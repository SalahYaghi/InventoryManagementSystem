using Application.Common.Dtos.Loggs;
using Contract.Common.Interfaces;
using Domain.AuditLoggs;
using Domain.Identity.Users;
using Domain.People;
using Inventory.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace Infrastructure.Services
{

    
    public class AuditServices(IAppDbContext context) : IAuditLogService
    {
        public async Task SaveUserLoggingAudits(CreateUserLoggingCommands request, CancellationToken ct = default)
        {

            var audit = UserLoginAuditLog.Create(request.UserId,
                request.Action, request.IpAddress, request.UserAgent
                , request.Success, request.ErrorMessages);

            if (audit.IsError) return;

            await context.UserLoginAuditLoggs.AddAsync(audit.Value, ct);
            await context.SaveChangesAsync(ct);
        }

        public async Task SaveUserOperationsAudits(CreateUserOperationsCommands request, CancellationToken ct = default)
        {

            var audit = UserOperationsAuditLog.Create(request.UserId,
                request.Request, request.IpAddress, request.UserAgent
                , request.Success, request.ErrorMessages);

            if (audit.IsError) return;

            await context.UserOperationsAuditLog.AddAsync(audit.Value
                 , ct);
            await context.SaveChangesAsync(ct);
        }




    }
}
