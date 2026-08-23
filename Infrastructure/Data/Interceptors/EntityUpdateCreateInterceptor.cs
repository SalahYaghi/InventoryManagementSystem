using Infrastructure.Common.Options;
using MechanicShop.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Interseptors
{
    public class EntityUpdateCreateInterceptor(IUser currentUser) : SaveChangesInterceptor
    {
        public override async ValueTask<InterceptionResult<int>>
            SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
        {
            if (eventData.Context == null)
                return  result;

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry is not { State: EntityState.Modified or EntityState.Added
                   , Entity: AuditableEntity entity })
                    continue;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAtUtc = DateTimeOffset.UtcNow;
                    entity.CreatedBy = currentUser.UserName; 
                } 

                entity.LastModifiedUtc = DateTimeOffset.UtcNow;
                entity.LastModifiedBy = currentUser.UserName;
            }

            return result;
        }
    }
}

