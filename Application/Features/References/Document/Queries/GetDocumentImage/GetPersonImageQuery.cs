using Contract.Common.Files;
using Inventory.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Parties.Person.Queries.GetPersonImage
{
    public record GeDocumentImageQuery(Guid Id) : IRequest<Result<FileDto>>;

    
}

