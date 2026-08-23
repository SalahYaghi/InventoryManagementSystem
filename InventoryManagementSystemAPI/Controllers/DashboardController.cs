using Contract.Features.Dashboard.Queries.GetDashboardData;
using Contract.Features.References.Countries.Queries.GetCountryPaged;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Contract.Common.Constants;
using Microsoft.AspNetCore.Http;
using Infrastructure.Policies.OutputCachePolicies;

namespace InventoryManagementSystemAPI.Controllers
{
    [Route("api/v{version:apiVersion}/dashboard")]
    [ApiVersion("1.0")]
    [Authorize]
    public class DashboardController(IMediator sender) : ApiController
    {
    [HttpGet]
    [OutputCache(Tags = [CacheEntities.Product, CacheEntities.Category, CacheEntities.Customer, CacheEntities.Supplier, CacheEntities.Order, CacheEntities.Invoice, CacheEntities.Warehouse, CacheEntities.WarehouseStock, CacheEntities.Adjustment, CacheEntities.Document, CacheEntities.Person, CacheEntities.Address, CacheEntities.City, CacheEntities.Country, CacheEntities.ContactInfo, CacheEntities.User], PolicyName = nameof(AuthenticatedUserCachePolicy))]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves paged dashboard.")]
    [EndpointDescription("Returns a paginated list of dashboard.")]
    [EndpointName("GetDashboard")]
    [MapToApiVersion("1.0")]
     
    public async Task<IActionResult> Get(CancellationToken ct = default)
        {
            var result = await sender.Send(new GetDashboardDataQuery(), ct);
            return result.Match(response => Ok(response), Problem);
        }


    }
}
