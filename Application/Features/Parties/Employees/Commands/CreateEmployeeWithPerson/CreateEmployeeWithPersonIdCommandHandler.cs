using Contract.Common.Constants;
using Contract.Common.Errors;
using Contract.Common.Interfaces;
using Contract.Features.Parties.Employees.Dtos;
using Contract.Features.Parties.Employees.Mappers;
using Contract.Features.Parties.People.Commands.CreatePerson;
using Contract.Features.Parties.People.DTOs;
using Domain.Contacts.Address;
using Domain.Contacts.ContactInfo;
using Domain.Identity.Employee;
using Domain.People;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Parties.Employees.Commands.CreateEmployeeWithPerson
{

    public class CreateEmployeeWithPersonCommandHandler(IAppDbContext context,
        ILogger<CreateEmployeeWithPersonCommandHandler> logger , ICachingService cachingService) :
        IRequestHandler<CreateEmployeeWithPersonCommand, Result<EmployeeDto>>
    {
        private readonly ILogger<CreateEmployeeWithPersonCommandHandler> _logger = logger;


        private async Task<Result<Domain.People.Person>> CreatePerson(CreatePersonCommand request)
        {
            ContactInfo? contactInfo = null;
            Address? address = null;


            if (request.Contact is not null)
            {
                Result<ContactInfo> contactInfoResult = ContactInfo.Create(Guid.NewGuid(),
                    request.Contact.Email,
                    request.Contact.PhoneNumber,
                    request.Contact.AlternitavePhoneNumber,
                    request.Contact.FaxNumber,
                    request.Contact.WebsiteUrl);

                if (contactInfoResult.IsError)

                {

                    _logger.LogError("CreateEmployeeWithPersonCommandHandler stopped because an error result was returned: {ErrorResult}.", "contactInfoResult.Errors");
                    return contactInfoResult.Errors;

                }

                contactInfo = contactInfoResult.Value;
            }


            if (request.Address is not null)
            {
                Result<Domain.Contacts.Address.Address> addressResult
                    = Domain.Contacts.Address.Address.
                    Create(Guid.NewGuid(),
                    request.Address.CountryId,
                    request.Address.CityId,
                    request.Address.PostalCode,
                    request.Address.BuildingNumber,
                    request.Address.Street,
                    request.Address.Description);

                if (addressResult.IsError)

                {

                    _logger.LogError("CreateEmployeeWithPersonCommandHandler stopped because an error result was returned: {ErrorResult}.", "addressResult.Errors");
                    return addressResult.Errors;

                }

                address = addressResult.Value;

            }


            var nationalNoDuplicated = await context.People.AnyAsync(p => p.NationalNo == request.NationalNo);

            if (nationalNoDuplicated)

            {

                _logger.LogWarning("CreateEmployeeWithPersonCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.NationalNoAlreadyExist");
                return ApplicationErrors.NationalNoAlreadyExist;

            }



            var result = Domain.People.Person.Create(
                Guid.NewGuid(),
                request.NationalNo,
                request.FirstName,
                request.SecondName,
                request.ThirdName,
                request.LastName,
                request.Gender,
                request.DateOfBirth,
                contactInfo,
                address
                );

            if (result.IsError)

            {

                _logger.LogError("CreateEmployeeWithPersonCommandHandler stopped because an error result was returned: {ErrorResult}.", "result.Errors");
                return result.Errors;

            }

            _logger.LogInformation("CreateEmployeeWithPersonCommandHandler completed successfully.");
            return result.Value;
        }

        async  Task<Result<EmployeeDto>>  IRequestHandler<CreateEmployeeWithPersonCommand, Result<EmployeeDto>>.Handle(CreateEmployeeWithPersonCommand request, CancellationToken cancellationToken)
        {
            var warehouseFound = await context.Warehouses
                          .AnyAsync(r => request.warehouseId == r.Id, cancellationToken); // [FIX 6.11] +ct
                          
            if (!warehouseFound)
                          
            {
                          
                _logger.LogWarning("CreateEmployeeWithPersonCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.WarehouseNotFound");
                return ApplicationErrors.WarehouseNotFound;
                          
            }

            var  personResult = await CreatePerson(request.person);
            if(personResult.IsError) return personResult.Errors;


            var empResult = Employee.Create(request.jobTitle,
                personResult.Value, request.hiringDate, request.warehouseId);

            if (empResult.IsError)

            {

                _logger.LogError("CreateEmployeeWithPersonCommandHandler stopped because an error result was returned: {ErrorResult}.", "empResult.Errors");
                return empResult.Errors;

            }

            await context.People.AddAsync(personResult.Value, cancellationToken); // [FIX 6.11] +ct
            await context.Employees.AddAsync(empResult.Value, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            await cachingService.RemoveByTagAsync(
                CacheFanout.Expand(CacheEntities.Employee, CacheEntities.Person), cancellationToken);


            _logger.LogInformation("CreateEmployeeWithPersonCommandHandler completed successfully.");
            return empResult.Value.ToDto();
        }
    }
}

