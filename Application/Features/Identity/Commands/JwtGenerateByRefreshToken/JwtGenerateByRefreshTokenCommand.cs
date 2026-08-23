using Infrastructure.Identity;
using MechanicShop.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Contract.Features.Identity.Commands.JwtGenerate
{
    public record JwtGenerateByRefreshTokenCommand(string refresh ,bool loginSource = false) : IRequest<Result<JwtDto>>;
}

