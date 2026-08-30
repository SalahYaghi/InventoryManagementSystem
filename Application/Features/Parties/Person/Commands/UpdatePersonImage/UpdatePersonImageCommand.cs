using Inventory.Domain.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Parties.Person.Commands.UpdatePersonImage
{
    public class UpdatePersonImageCommand : IRequest<Result<Updated>>
    {
        public IFormFile? Image { get; set; }
        public Guid PersonId { get; set; }
    }
}

