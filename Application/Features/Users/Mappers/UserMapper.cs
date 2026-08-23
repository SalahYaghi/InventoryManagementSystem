using Contract.Features.Parties.Employees.Mappers;
using Contract.Features.User.Dtos;

namespace Contract.Features.Users.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(this Domain.Identity.Users.User user)
        {
            return new UserDto()
            {
                Id = user.Id,

                Email = user.Email,
                Employee = user.Employee?.ToDto(),
                EmployeeId = user.EmployeeId,
                IsActive = user.IsActive,
                LastLoginAt = user.LastLoginAt,
                Role = user.Role,
                Username = user.Username
            };
        }
    }
}
