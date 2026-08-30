using Contract.Common.Files;
using Inventory.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Parties.Person.Queries.GetPersonImage
{
    public record GetPersonImageQuery(Guid PersonId) : IRequest<Result<FileDto>>;

    
}

