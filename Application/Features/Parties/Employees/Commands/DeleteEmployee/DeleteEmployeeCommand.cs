using MechanicShop.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Parties.Employees.Commands.DeleteEmployee
{
    public record DeleteEmployeeCommand(Guid Id) : IRequest<Result<Deleted>>
    {
    }
}

