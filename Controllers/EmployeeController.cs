using Domain.Common.Constants;
using Application.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Application.Features.Employees.Commands.CreateEmployee;
using Application.Features.Employees.Commands.DeleteEmployee;
using Application.Features.Employees.Queries.GetEmployeeById;
using ContracOldCompatibile.Requests.Employees;
using Contracts.Requests.Employee;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Http;

namespace InventoryManagementSystemAPI.Controllers
{

    [Route("api/v{version:apiVersion}/employees")]
    [ApiVersion("1.0")]
    [Authorize]
[ApiController]
    public class EmployeeController(IMediator sender) : ApiController
    {
    [HttpGet]
    [OutputCache(Tags = [CacheEntities.Person])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves paged employees.")]
    [EndpointDescription("Returns a paginated list of employees.")]
    [EndpointName("GetEmployees")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<ActionResult> GetAll([FromQuery] Guid? warehouseId = null)
        {
            var result = await sender.Send(new GetEmployeesQuery(warehouseId));
            return result.Match(r => Ok(r), Problem);
        }
    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates a new employee.")]
    [EndpointDescription("Adds a new employee to the system.")]
    [EndpointName("CreateEmployeeWithPersonId")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<ActionResult> CreateEmployee(CreateEmployeeWithPersonIdRequest request)
        {
            var result = await sender.Send(
                new CreateEmployeeWithPersonIdCommand(
                    hiringDate: request.hiringDate,
                    jobTitle: request.jobTitle,
                    personId: request.personId,
                    warehouseId: request.warehouseId));


            return result.Match(r => Ok(r), Problem);
        }
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates an employee.")]
    [EndpointDescription("Updates the specified employee.")]
    [EndpointName("UpdateEmployee")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<ActionResult> UpdateEmployee(Guid id,UpdateEmployeeRequest request)
        {
            var result = await sender.Send(
                new UpdateEmployeeCommand(
                    employeeId: id,
                    hiringDate: request.hiringDate,
                    jobTitle: request.jobTitle,
                    warehouseId: request.warehouseId));


            return result.Match(r => Ok(r), Problem);
        }
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Removes an employee.")]
    [EndpointDescription("Deletes the specified employee.")]
    [EndpointName("DeleteEmployee")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<ActionResult> DeleteEmployee(Guid id)
        {
            var result = await sender.Send(
                new DeleteEmployeeCommand(id));
            return result.Match(r => NoContent(), Problem);
        }
    [HttpGet("{id:guid}")]
    [OutputCache(Tags = [CacheEntities.Person])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves an employee by ID.")]
    [EndpointDescription("Returns detailed information about the specified employee.")]
    [EndpointName("GetEmployeeById")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<ActionResult> GetById(Guid id)
        {
            var result = await sender.Send(new GetEmployeeByIdQuery(id));
            return result.Match(r => Ok(r), Problem);
        }

    }
}
