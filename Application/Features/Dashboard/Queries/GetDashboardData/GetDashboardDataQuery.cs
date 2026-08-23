using Contract.Features.Dashboard.Dtos;
using MechanicShop.Domain.Common.Results;
using MechanicShop.Domain.Common.Results.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Dashboard.Queries.GetDashboardData
{
    public record GetDashboardDataQuery : IRequest<Result<DashboardDto>>;
    
    
}
