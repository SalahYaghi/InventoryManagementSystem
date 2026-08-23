using Contract.Common.Extensions;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using Contract.Features.Inventory.Product.DTOs;
using Contract.Features.Parties.People.DTOs;
using Contract.Features.Parties.People.Mappers;
using Contract.Features.Parties.Person.DTOs;
using MechanicShop.Domain.Common;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Parties.People.Queries.GetPersonPaged
{
    public sealed class GetPersonPagedQueryHandler : IRequestHandler<GetPersonPagedQuery, Result<PaginatedList<PersonForListDto>>>
    {
        private readonly ILogger<GetPersonPagedQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetPersonPagedQueryHandler(IAppDbContext context,
            ILogger<GetPersonPagedQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<PaginatedList<PersonForListDto>>> Handle(GetPersonPagedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetPersonPagedQueryHandler));

            var query = _context.People
                .OrderBy(x => x.FirstName)
                .AsNoTracking()
                .Select(entity => new PersonForListDto() {
                    Id = entity.Id,
                    NationalNo = entity.NationalNo,
                    FullName = $"{entity.FirstName} {entity.SecondName} {entity.ThirdName} {entity.LastName}",
                    Gender = entity.Gender ? "Male" : "Female",
                    DateOfBirth = entity.DateOfBirth,
                    DocumentId = entity.DocumentId,
                    PhoneNumber = entity.Contact != null  ? entity.Contact.PhoneNumber :  string.Empty,
                    Email = entity.Contact != null ? entity.Contact.Email : string.Empty,
                    Country =  entity.Address!.Country!.Name ,
                    City = entity.Address!.City!.Name,
      
                });

            var result = await query.ToPaginatedListAsync(
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            _logger.LogInformation("GetPersonPagedQueryHandler completed successfully.");
            return result;
        }
    }
}

