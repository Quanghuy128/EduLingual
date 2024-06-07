using EduLingual.Domain.Dtos.Role;
using EduLingual.Domain.Entities;
using EduLingual.Domain.Enum;

namespace EduLingual.Domain.Dtos.User
{
    public class UserViewModel: UserDto
    {
        public RoleDto Role { get; set; }
    }
}
