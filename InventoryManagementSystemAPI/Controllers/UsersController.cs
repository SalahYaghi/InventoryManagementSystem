using Contract.Common.Constants;
using Contract.Features.Identity.Commands.JwtGenerate;
using Contract.Features.Inventory.Product.Commands.DeleteProduct;
using Contract.Features.User.Commands.CreateUser;
using Contract.Features.Users.Commands.DeleteUser;
using Contract.Features.Users.Queries.GetUserById;
using Contract.Requests.Users;
using Domain.Common.Constants;
using Domain.Identity.Users;
using Infrastructure.Policies.OutputCachePolicies;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Routing;

using Microsoft.AspNetCore.RateLimiting;

namespace InventoryManagementSystemAPI.Controllers
{
    [Route("api/v{version:apiVersion}/users")]
    [ApiController]
    [ApiVersion("1.0")]
      [Authorize]
public class UsersController(IMediator sender) : ApiController
    {
    [HttpPut("{id:guid}/password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates user password.")]
    [EndpointDescription("Updates the password for the specified user.")]
    [EndpointName("UpdateUserPassword")]
    [MapToApiVersion("1.0")]
    [EnableRateLimiting("AuthLimiter")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<ActionResult> UpdateUserPassword(Guid id,
            [FromBody] UpdateUserPasswordCommand request,
            CancellationToken ct)
        {

            var command = new UpdateUserPasswordCommand(id,request.oldpassword , request.newpassword);

            var result = await sender.Send(command);

            return result.Match(
                response => NoContent(), Problem);
        }
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates an user.")]
    [EndpointDescription("Updates the specified user.")]
    [EndpointName("UpdateUser")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<ActionResult> UpdateUser(Guid id , 
            [FromBody] UpdateUserRequest request,
            CancellationToken ct)
        {

            var command = new UpdateUserCommand(id , request.username,
          request.email , request.isActive, (Role)((int)request.role));

            var result = await sender.Send(command);

            return result.Match(
                response => Ok(response), Problem);
        }
    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates a new user.")]
    [EndpointDescription("Adds a new user to the system.")]
    [EndpointName("CreateUser")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserRequest request,
            CancellationToken ct)
        {

           var command =  new CreateUserCommand(request.Username,
            request.Password, (Role)((int)request.Role), request.Email, request.EmployeeId);

            var result = await sender.Send(command);

            return result.Match(
                response => Ok(response), Problem);
        }
    [HttpGet]
//    [OutputCache(Tags = [CacheEntities.User], PolicyName = nameof(AuthenticatedUserCachePolicy))]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves paged users.")]
    [EndpointDescription("Returns a paginated list of users.")]
    [EndpointName("GetUsers")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<ActionResult> GetUsers(
            CancellationToken ct)
        {

            var command = new GetUsersQuery();

            var result = await sender.Send(command , ct);

            return result.Match(
                response => Ok(response), Problem);
        }
    [HttpGet("{id:guid}")]
    //[OutputCache(Tags = [CacheEntities.User], PolicyName = nameof(AuthenticatedUserCachePolicy), VaryByRouteValueNames = ["id"])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves a user by ID.")]
    [EndpointDescription("Returns detailed information about the specified user.")]
    [EndpointName("GetUserById")]
    [MapToApiVersion("1.0")]
     [Authorize(Roles = RoleConstants.Admin)]
    public async Task<ActionResult> GetUserById(Guid id , 
            CancellationToken ct)
        {

            var command = new GetUserByIdQuery(id);

            var result = await sender.Send(command, ct);

            return result.Match(
                response => Ok(response), Problem);
        }
    [HttpGet("{email}")]
   // [OutputCache(Tags = [CacheEntities.User], PolicyName = nameof(AuthenticatedUserCachePolicy), VaryByRouteValueNames = ["email"])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves a user by ID.")]
    [EndpointDescription("Returns detailed information about the specified user.")]
    [EndpointName("GetUserByEmail")]
    [MapToApiVersion("1.0")]
     
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<ActionResult> GetUserByEmail(string email,
            CancellationToken ct)
        {

            var command = new GetUserByEmailQuery(email);

            var result = await sender.Send(command , ct);

            return result.Match(
                response => Ok(response), Problem);
        }



        [HttpDelete("{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Removes a user.")]
        [EndpointDescription("Deletes the specified user.")]
        [EndpointName("DeleteUser")]
        [MapToApiVersion("1.0")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> Delete(Guid userId, CancellationToken ct)
        {
            var result = await sender.Send(new DeleteUserCommand(userId), ct);
            return result.Match(_ => NoContent(), Problem);
        }


    }
}
