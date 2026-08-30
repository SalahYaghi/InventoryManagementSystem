using Domain.Common.Results.Interfaces;
using Inventory.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Interceptors
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {

        public override async ValueTask<InterceptionResult<int>>
            SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
        {
            if (eventData.Context == null)
                return result;

            foreach (var entry in eventData.Context.ChangeTracker.Entries<ISoftDeletable>())
            {
                if (entry.State != EntityState.Deleted)
                     continue;

                entry.State = EntityState.Modified;
                entry.Entity.Delete();
            }

            return result;
        }
    }
}
