using EduLingual.Domain.Common;
using EduLingual.Domain.Enum;

namespace EduLingual.Domain.Dtos.User
{
    public class UserDto : BaseEntity
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? Description { get; set; }
        public string? Email { get; set; }
        public string? ImageUrl { get; set; }
        public UserStatus Status { get; set; }
    }
}
