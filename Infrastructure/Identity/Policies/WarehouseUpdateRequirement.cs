using Contract.Common.Interfaces;
using Domain.Identity.Users;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Identity.Policies
{
    public class WarehouseUpdateRequirement : IAuthorizationRequirement;

    public class WarehouseUpdateHandler(
        IAppDbContext dbContext,
        IHttpContextAccessor accessor,
        ILogger<WarehouseUpdateHandler> logger)
        : AuthorizationHandler<WarehouseUpdateRequirement>
    {
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            WarehouseUpdateRequirement requirement)
        {
            logger.LogInformation("Starting warehouse update authorization check.");

            var value =
                context.User?.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? context.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            logger.LogDebug("Extracted user identifier claim value: {UserIdClaim}", value);

            if (!Guid.TryParse(value, out var userId) || userId == Guid.Empty)
            {
                logger.LogWarning(
                    "Warehouse update authorization failed. Invalid or missing user id claim. ClaimValue: {UserIdClaim}",
                    value);

                context.Fail();
                return;
            }

            logger.LogInformation("User id extracted successfully. UserId: {UserId}", userId);

            var warehouseIdValue = accessor.HttpContext?
                .Request
                .RouteValues["warehouseId"]?
                .ToString();

            logger.LogDebug(
                "Extracted warehouse id route value: {WarehouseIdRouteValue}",
                warehouseIdValue);

            if (!Guid.TryParse(warehouseIdValue, out var warehouseId) || warehouseId == Guid.Empty)
            {
                logger.LogWarning(
                    "Warehouse update authorization failed. Invalid or missing warehouse id route value. RouteValue: {WarehouseIdRouteValue}, UserId: {UserId}",
                    warehouseIdValue,
                    userId);

                context.Fail();
                return;
            }

            logger.LogInformation(
                "Warehouse id extracted successfully. WarehouseId: {WarehouseId}, UserId: {UserId}",
                warehouseId,
                userId);

            logger.LogDebug("Loading user with employee data from database. UserId: {UserId}", userId);

            var user = await dbContext.Users
                .Include(u => u.Employee)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                logger.LogWarning(
                    "Warehouse update authorization failed. User was not found in database. UserId: {UserId}, WarehouseId: {WarehouseId}",
                    userId,
                    warehouseId);

                context.Fail();
                return;
            }

            logger.LogInformation(
                "User loaded successfully. UserId: {UserId}, Role: {Role}, EmployeeId: {EmployeeId}, EmployeeWarehouseId: {EmployeeWarehouseId}",
                user.Id,
                user.Role,
                user.Employee?.Id,
                user.Employee?.WarehouseId);

            if (user.Role == Role.Admin)
            {
                logger.LogInformation(
                    "Warehouse update authorization succeeded. User is Admin. UserId: {UserId}, WarehouseId: {WarehouseId}",
                    userId,
                    warehouseId);

                context.Succeed(requirement);
                return;
            }

            if (user.Employee == null)
            {
                logger.LogWarning(
                    "Warehouse update authorization failed. Non-admin user has no employee profile. UserId: {UserId}, Role: {Role}, WarehouseId: {WarehouseId}",
                    userId,
                    user.Role,
                    warehouseId);

                context.Fail();
                return;
            }

            if (user.Employee.WarehouseId != warehouseId)
            {
                logger.LogWarning(
                    "Warehouse update authorization failed. User is not assigned to requested warehouse. UserId: {UserId}, UserWarehouseId: {UserWarehouseId}, RequestedWarehouseId: {RequestedWarehouseId}, Role: {Role}",
                    userId,
                    user.Employee.WarehouseId,
                    warehouseId,
                    user.Role);

                context.Fail();
                return;
            }

            logger.LogInformation(
                "Warehouse update authorization succeeded. User is assigned to requested warehouse. UserId: {UserId}, WarehouseId: {WarehouseId}",
                userId,
                warehouseId);

            context.Succeed(requirement);
        }
    }
}