using Contract.Common.Constants;
using Contract.Common.Errors;
using Contract.Common.Interfaces;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Parties.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHanlder(IAppDbContext context , ICachingService cachingService) : IRequestHandler<DeleteEmployeeCommand, Result<Deleted>>
    {
        public async Task<Result<Deleted>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var emp = await context.Employees.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (emp == null)
                return ApplicationErrors.EmployeeNotFound;

            var hasUser = await context.Users.AnyAsync(u => u.EmployeeId == request.Id, cancellationToken);

            if (hasUser)
                return ApplicationErrors.EmployeeHasUsers;

            context.Employees.Remove(emp);

            await context.SaveChangesAsync(cancellationToken);

            await cachingService.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Employee), cancellationToken);

            return Result.Deleted;
        }
    }
}

