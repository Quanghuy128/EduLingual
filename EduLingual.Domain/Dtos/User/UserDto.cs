using EduLingual.Domain.Enum;

namespace EduLingual.Domain.Dtos.User
{
    public record UserDto 
    (
        string UserName,
        string Password,
        string FullName,
        string Description,
        UserStatus UserStatus,
        BaseDto BaseDto
    );
}
