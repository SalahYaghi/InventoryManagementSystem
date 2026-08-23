using Contract.Features.Identity.Commands.JwtGenerate;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens.Experimental;

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
        [EnableRateLimiting("AuthLimiter")]

        


    public async Task<ActionResult> GenerateRefreshToken([FromBody] JwtGenerateByRefreshTokenCommand request, CancellationToken ct)
        {
            var result = await sender.Send(request);

            if (result.IsError)
            {

                result.Errors.Clear();
                result.Errors.Add(Error.Validation("Failed", "Login request failed please try another method."));
            }

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
        [EnableRateLimiting("AuthLimiter")]
        
    public async Task<ActionResult> Generate([FromBody] JwtGeneratCommand request, CancellationToken ct)
        {


            var result = await sender.Send(request);

            //if (result.IsError)
            //{
            //    result.Errors.Clear();
            //    result.Errors.Add(Error.Validation("Invalid credentials", "The provided credentials are invalid."));
            //}


            return result.Match(
                response => Ok(response), Problem);
        }



    }
}