using Infrastructure.Identity;
using Inventory.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Contract.Features.Identity.Commands.JwtGenerate
{
    public record JwtGeneratCommand(string email, string password) : IRequest<Result<JwtDto>>;
}

