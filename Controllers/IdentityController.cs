using Microsoft.AspNetCore.Authorization;
using Application.Features.Identity.Commands.JwtGenerate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace InventoryManagementSystemAPI.Controllers
{
    [Route("api/v{version:apiVersion}/identity")]
    [ApiVersion("1.0")]
    [ApiController]
    public class IdentityController(IMediator sender) : ApiController
    {
    [HttpPost("jwt/refresh")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Generates an identity token.")]
    [EndpointDescription("Generates a JWT access token or refresh token response.")]
    [EndpointName("GenerateRefreshTokenIdentity")]
    [MapToApiVersion("1.0")]
    [AllowAnonymous]
    public async Task<ActionResult> GenerateRefreshToken([FromBody] JwtGenerateByRefreshTokenCommand request, CancellationToken ct)
        {
            var result = await sender.Send(request);

            return result.Match(
                response => Ok(response), Problem);
        }
    [HttpPost("jwt/generate")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Generates an identity token.")]
    [EndpointDescription("Generates a JWT access token or refresh token response.")]
    [EndpointName("GenerateIdentity")]
    [MapToApiVersion("1.0")]
    [AllowAnonymous]
    public async Task<ActionResult> Generate([FromBody] JwtGeneratCommand request, CancellationToken ct)
        {


            var result = await sender.Send(request);

            return result.Match(
                response => Ok(response), Problem);
        }



    }
}