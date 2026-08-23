using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace InventoryManagementSystemAPI.Services
{
    
    public class CurrentUser : IUser
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUser(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        private ClaimsPrincipal? User => _contextAccessor.HttpContext?.User;

        public Guid? UserId
        {
            get
            {
                var value =
              User?.FindFirstValue(ClaimTypes.NameIdentifier)
              ?? User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

                return Guid.TryParse(value, out var userId)
                    ? userId
                    : null;
            }
        }

        public string? UserName =>
            User?.FindFirstValue(JwtRegisteredClaimNames.Nickname);
    }
}