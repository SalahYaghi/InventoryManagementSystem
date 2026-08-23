using Domain.Common.Constants;
using Application.Common.Constants;
 using Application.Features.Parties.People.Commands.CreatePerson;
using Application.Features.Parties.People.Commands.DeletePerson;
using Application.Features.Parties.People.Commands.UpdatePerson;
using Application.Features.Parties.People.DTOs;
using Application.Features.Parties.People.Queries.GetPerson;
using Application.Features.Parties.People.Queries.GetPersonPaged;
using Application.Features.Parties.Person.Commands.UpdatePersonImage;
using Application.Features.Parties.Person.Queries.GetPersonImage;
using Application.Features.References.Addresses.Commands.CreateAddress;
using Application.Features.References.Addresses.Commands.UpdateAddress;
using Application.Features.References.ContactInfos.Commands.CreateContactInfo;
using Application.Features.References.ContactInfos.Commands.UpdateContactInfo;
using Application.Features.References.Documents.Commands.CreateDocument;
using Application.Features.References.Documents.DTOs;
using Contracts.Requests.Documents;
using Contracts.Requests.People;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Http;

namespace InventoryManagementSystemAPI.Controllers;

[Route("api/v{version:apiVersion}/people")]
[ApiVersion("1.0")]
//[Authorize]
public sealed class PeopleController(ISender sender) : ApiController
{
    [HttpPost("fuck")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates a new people.")]
    [EndpointDescription("Adds a new people to the system.")]
    [EndpointName("FuckPeople")]
    [MapToApiVersion("1.0")]
    public async Task<IActionResult> Fuck( CancellationToken ct)
    {
        throw new Exception("Fuck You Salah");
    }

    [HttpPost("{personId:guid}/document")]
    [ProducesResponseType(typeof(DocumentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates people document.")]
    [EndpointDescription("Creates a document for the specified people.")]
    [EndpointName("CreatePersonDocument")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> CreateDocument(Guid personId, [FromForm] CreateDocumentRequest request, CancellationToken ct)
    {
        var cmd = new CreatePersonDocumentCommand
        {
            PersonId = personId,
            Document = new CreateDocumentCommand
            {
                DocumentType = (Domain.Document.DocumentType)request.DocumentType,
                DocumentImage = request.DocumentImage
            }
        };
        var result = await sender.Send(cmd, ct);

        return result.Match(
            response => Ok(response),
            Problem);
    }

    [HttpPut("{personId:guid}/image")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates people image.")]
    [EndpointDescription("Updates the image associated with the specified people.")]
    [EndpointName("UpdatePersonImage")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> UpdateImage(Guid personId, [FromForm] UpdatePersonImageRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new UpdatePersonImageCommand
        {
            PersonId = personId,
            Image = request.Image
        }, ct);

        return result.Match(
            _ => Ok(),
            Problem);
    }

    [HttpGet]
    [OutputCache(Tags = [CacheEntities.Person])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves paged people.")]
    [EndpointDescription("Returns a paginated list of people.")]
    [EndpointName("GetPeople")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
    {
        var result = await sender.Send(new GetPersonPagedQuery { PageNumber = pageNumber, PageSize = pageSize }, ct);
        return result.Match(response => Ok(response), Problem);
    }

    [HttpGet("{personId:guid}", Name = "GetPersonById")]
    [OutputCache(Tags = [CacheEntities.Person])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves a people by ID.")]
    [EndpointDescription("Returns detailed information about the specified people.")]
    [EndpointName("GetPeopleById")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Viewer)]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid personId, CancellationToken ct)
    {
        var result = await sender.Send(new GetPersonQuery(personId), ct);
        return result.Match(response => Ok(response), Problem);
    }

    [HttpGet("{personId:guid}/image", Name = "GetPersonImageById")]
    [OutputCache(Tags = [CacheEntities.Person])]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves people image.")]
    [EndpointDescription("Returns the image associated with the specified people.")]
    [EndpointName("GetPersonImage")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Viewer)]
    [AllowAnonymous]
    public async Task<IActionResult> GetImageById(Guid personId, CancellationToken ct)
    {
        var result = await sender.Send(new GetPersonImageQuery(personId), ct);
        return result.Match(response => PhysicalFile(response.FileUrl! , response.ContentType), Problem);
    }

    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates a new people.")]
    [EndpointDescription("Adds a new people to the system.")]
    [EndpointName("CreatePerson")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Create([FromBody] CreatePersonRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new CreatePersonCommand
        {
            NationalNo = request.NationalNo,
            FirstName = request.FirstName,
            SecondName = request.SecondName,
            ThirdName = request.ThirdName,
            LastName = request.LastName,
            Gender = request.Gender,
            DateOfBirth = request.DateOfBirth,
            Contact = new CreateContactInfoCommand
            {
                Email = request.Contact.Email,
                PhoneNumber = request.Contact.PhoneNumber,
                AlternitavePhoneNumber = request.Contact.AlternitavePhoneNumber,
                FaxNumber = request.Contact.FaxNumber,
                WebsiteUrl = request.Contact.WebsiteUrl
            },
            Address = new CreateAddressCommand
            {
                CountryId = request.Address.CountryId,
                CityId = request.Address.CityId,
                PostalCode = request.Address.PostalCode,
                BuildingNumber = request.Address.BuildingNumber,
                Street = request.Address.Street,
                Description = request.Address.Description
            }
        }, ct);

        return result.Match(
            response => CreatedAtRoute("GetPersonById", new { version = "1.0", personId = response.Id }, response),
            Problem);
    }

    [HttpPut("{personId:guid}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates a people.")]
    [EndpointDescription("Updates the specified people.")]
    [EndpointName("UpdatePerson")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Update(Guid personId, [FromBody] UpdatePersonRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new UpdatePersonCommand
        {
            Id = personId,
            NationalNo = request.NationalNo,
            FirstName = request.FirstName,
            SecondName = request.SecondName,
            ThirdName = request.ThirdName,
            LastName = request.LastName,
            Gender = request.Gender,
            DateOfBirth = request.DateOfBirth,
            Contact = request.Contact is null ? null : new UpdateContactInfoCommand
            {
                Email = request.Contact.Email,
                PhoneNumber = request.Contact.PhoneNumber,
                AlternitavePhoneNumber = request.Contact.AlternitavePhoneNumber,
                FaxNumber = request.Contact.FaxNumber,
                WebsiteUrl = request.Contact.WebsiteUrl
            },
            Address = request.Address is null ? null : new UpdateAddressCommand
            {
                CountryId = request.Address.CountryId,
                CityId = request.Address.CityId,
                PostalCode = request.Address.PostalCode,
                BuildingNumber = request.Address.BuildingNumber,
                Street = request.Address.Street,
                Description = request.Address.Description
            }
        }, ct);

        return result.Match(response => Ok(response), Problem);
    }

    [HttpDelete("{personId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Removes a people.")]
    [EndpointDescription("Deletes the specified people.")]
    [EndpointName("DeletePerson")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Delete(Guid personId, CancellationToken ct)
    {
        var result = await sender.Send(new DeletePersonCommand(personId), ct);
        return result.Match(_ => NoContent(), Problem);
    }
}
