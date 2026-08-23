using InventoryManagementSystemAPI.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystemAPI.Shared.Middewares
{
    public class UserTimeZoneMiddleware
    {
        private readonly RequestDelegate _next;
        public UserTimeZoneMiddleware(RequestDelegate next) { 
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context,
            UserTimeZone userTimeZone) {

            var time = context.Request.Headers[Constants.ProgramConstants.UserTimeZoneHeader].FirstOrDefault();

            if (TimeSpan.TryParse(time, out TimeSpan
                 timeSpan)) {

                userTimeZone.Offset = timeSpan;
                await _next(context);
                return;
            }

            await _next(context);
           // throw new ValidationException("Client must send time zone on request."); 
        }

    }
}
